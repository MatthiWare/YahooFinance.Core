using MatthiWare.YahooFinance.Abstractions.Http;
using MatthiWare.YahooFinance.Core.Helpers;
using MatthiWare.YahooFinance.Core.Search;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatthiWare.YahooFinance.Core.Http
{
    public class ApiClient : IApiClient
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
			};
		}

		public async Task<IApiResponse<ReturnType>> ExecuteAsync<ReturnType>(
			string urlPattern, NameValueCollection pathNVC, QueryStringBuilder qsb)
			where ReturnType : class
		{
			var content = string.Empty;

			using (var responseContent = await client.GetAsync($"{urlPattern}{qsb.Build()}"))
			{
				try
				{
					content = await responseContent.Content.ReadAsStringAsync();

					if (content.Length > 7 && content.Substring(startIndex: 0, length: 8) == "{\"error\"")
					{ 
						var token = JToken.Parse(content);
						return new ApiResponse<ReturnType>() { Error = token["error"].ToString() };
					}
					else if (content == "Forbidden")
					{ 
						return new ApiResponse<ReturnType>() { Error = content };
					}
					else return new ApiResponse<ReturnType>()
					{

						Data = JsonConvert.DeserializeObject<ReturnType>(content, jsonSerializerSettings)
					};
				}
				catch (JsonException ex)
				{
					throw new JsonException(content, ex);
				}
			}
		}
	}
}
