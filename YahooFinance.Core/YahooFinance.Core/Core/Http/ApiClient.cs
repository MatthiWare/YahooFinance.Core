using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using NodaTime;
using MatthiWare.YahooFinance.Core.Converters;
using MatthiWare.YahooFinance.Core.History;
using System.Threading;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly HttpClient client;
        private readonly ILogger logger;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public ApiClient(HttpClient client, ILogger logger)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                MaxDepth = 7
            };
        }

        public async Task<IApiResponse<IEnumerable<ReturnType>>> ExecuteCsvAsync<ReturnType>(
            string urlPattern, NameValueCollection nvc, QueryStringBuilder qsb, CancellationToken cancellationToken)
            where ReturnType : class
        {
            HttpStateModel state = null;

            try
            {
                ReplaceUrlPatterns(ref urlPattern, ref nvc);

                var url = $"{urlPattern}{qsb.Build()}";

                state = await ExecuteInternalAsync(url, cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (state.Error)
                    return ApiResponse.FromError<IEnumerable<ReturnType>>(state.Meta, state.Response);

                using (var reader = new CsvReader(new StringReader(state.Response), CultureInfo.InvariantCulture))
                {
                    reader.Configuration.TypeConverterCache.AddConverter<Instant>(new NodaTimeCsvConverter());
                    reader.Configuration.TypeConverterCache.AddConverter<SplitData>(new SplitCsvConverter());

                    cancellationToken.ThrowIfCancellationRequested();

                    var records = reader.GetRecords<ReturnType>().ToList().AsEnumerable();

                    cancellationToken.ThrowIfCancellationRequested();
                    logger.LogDebug("Correctly deserialized response");

                    return ApiResponse.FromSucces(state.Meta, records);
                }
            }
            catch (OperationCanceledException tce)
            {
                logger.LogInformation("Task cancelled");
                return await Task.FromCanceled<IApiResponse<IEnumerable<ReturnType>>>(cancellationToken);
            }
            catch (Exception ex)
            {
                return ApiResponse.FromError<IEnumerable<ReturnType>>(state?.Meta ?? new ErrorHttpMetadata(), ex.ToString());
            }
        }

        public async Task<IApiResponse<ReturnType>> ExecuteJsonAsync<ReturnType>(
            string urlPattern, QueryStringBuilder qsb, CancellationToken cancellationToken)
            where ReturnType : class
        {
            var url = $"{urlPattern}{qsb.Build()}";

            HttpStateModel state = null;

            try
            {
                state = await ExecuteInternalAsync(url, cancellationToken);

                if (state.Error)
                    return ApiResponse.FromError<ReturnType>(state.Meta, state.Response);

                cancellationToken.ThrowIfCancellationRequested();

                var data = JsonConvert.DeserializeObject<ReturnType>(state.Response, jsonSerializerSettings);

                cancellationToken.ThrowIfCancellationRequested();

                logger.LogDebug("Correctly deserialized response");

                return ApiResponse.FromSucces(state.Meta, data);
            }
            catch (OperationCanceledException tce)
            {
                logger.LogInformation("Task cancelled");
                return await Task.FromCanceled<IApiResponse<ReturnType>>(cancellationToken);
            }
            catch (Exception ex)
            {
                return ApiResponse.FromError<ReturnType>(state.Meta, ex.ToString());
            }
        }


        private async Task<HttpStateModel> ExecuteInternalAsync(string url, CancellationToken cancellationToken)
        {
            logger.LogDebug("Executing GET on {url}", url);

            HttpMetadata meta = null;
            var startTime = DateTime.UtcNow;

            using (var responseContent = await client.GetAsync(url, cancellationToken))
            {
                try
                {
                    meta = new HttpMetadata(responseContent.StatusCode, responseContent.ReasonPhrase, (DateTime.UtcNow - startTime));

                    logger.LogDebug("Response status code: {StatusCode}", responseContent.StatusCode);

                    cancellationToken.ThrowIfCancellationRequested();

                    var content = await responseContent.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                    return new HttpStateModel(meta, content, false);
                }
                catch (OperationCanceledException tce)
                {
                    return await Task.FromCanceled<HttpStateModel>(cancellationToken);
                }
                catch (Exception ex)
                {
                    return new HttpStateModel(meta, ex.ToString(), true);
                }
            }
        }

        private void ReplaceUrlPatterns(ref string urlPattern, ref NameValueCollection pathNVC)
        {
            if (pathNVC.Count == 0)
                return;

            foreach (string key in pathNVC.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException("key cannot be null");
                }
                else if (string.IsNullOrWhiteSpace(pathNVC[key]))
                {
                    throw new ArgumentException($"{key} value cannot be null");
                }
                else if (urlPattern.IndexOf($"[{key}]") < 0)
                {
                    throw new ArgumentException($"urlPattern doesn't contain key [{key}]");
                }
                else
                {
                    urlPattern = urlPattern.Replace($"[{key}]", pathNVC[key]);
                }
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
