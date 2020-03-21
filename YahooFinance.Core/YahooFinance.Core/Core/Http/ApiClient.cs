using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

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

		public async Task<IApiResponse<ReturnType>> ExecuteAsync<ReturnType>(
			string urlPattern, NameValueCollection pathNVC, QueryStringBuilder qsb)
			where ReturnType : class
		{
			var url = $"{urlPattern}{qsb.Build()}";

			logger.LogDebug("Executing GET on {url}", url);

			HttpMetadata meta = null;
			var startTime = DateTime.UtcNow;

			using (var responseContent = await client.GetAsync(url))
			{
				try
				{
					meta = new HttpMetadata(responseContent.StatusCode, responseContent.ReasonPhrase, (DateTime.UtcNow - startTime));

					logger.LogDebug("Response status code: {StatusCode}", responseContent.StatusCode);

					var content = await responseContent.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

					var data = JsonConvert.DeserializeObject<ReturnType>(content, jsonSerializerSettings);

					logger.LogDebug("Correctly deserialized response");

					return ApiResponse.FromSucces(meta, data);
				}
				catch (Exception ex)
				{
					return ApiResponse.FromError<ReturnType>(meta, ex.ToString());
				}
			}
		}

		public void Dispose()
		{
			client.Dispose();
		}
	}
}
