using System;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Linq;
using System.IO;
using RestSharp;

namespace Hashbot.Logic
{
	public class TwitterClient
	{
		private string _lastId = String.Empty;
		private const string _baseUrl = "http://search.twitter.com";

		public event Action<Exception,TwitterMessage[]> MessagesLoaded;

		public TwitterClient()
		{
		}

		public void MessagesByTag(string hashtag, int page=1)
		{
			var request = new RestRequest("search.json");
			var client = new RestClient();
			client.BaseUrl = _baseUrl;
			 
			request.RootElement = "TwitterResponse";
			request.AddParameter("q", hashtag);
			request.AddParameter("include_entities", "true");
			request.AddParameter("rpp", 15);
			request.AddParameter("page", page);
			request.AddParameter("max_id", _lastId);

			client.ExecuteAsync<TwitterResponse>(request, HandleResponse);
		}

		void HandleResponse(IRestResponse<TwitterResponse> response)
		{
			if (response.ErrorException != null || response.ErrorMessage != null)
			{
				MessagesLoaded(response.ErrorException ?? new Exception(response.ErrorMessage), null);
			} else
			{
				if (response.Data.Results.Count == 0)
				{
					MessagesLoaded(new Exception("Больше не могу"), null);

					return;
				}

				var finalResults = new List<TwitterMessage>();

				foreach (var tweet in response.Data.Results)
				{
					finalResults.Add(new TwitterMessage(tweet));
				}

				_lastId = finalResults.Last().MessageId;

				MessagesLoaded(null, finalResults.ToArray());
			}
		}
	}
}