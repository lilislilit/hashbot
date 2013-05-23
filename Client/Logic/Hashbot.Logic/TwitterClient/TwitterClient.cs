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
		private const string _baseUrl = "http://search.twitter.com";
		private string _lastId=String.Empty;
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
			request.AddParameter("rpp", "5");
			request.AddParameter("page", page);
			request.AddParameter("max_id", _lastId);

			var asyncHandle = client.ExecuteAsync<TwitterResponse>(request, HandleResponse);

		}

		void HandleResponse(IRestResponse<TwitterResponse> response)
		{
			if (response.ErrorException != null||response.ErrorMessage!=null)
			{
				MessagesLoaded(response.ErrorException??new Exception(response.ErrorMessage), null);
			} else
			{
				var finalResults = new List<TwitterMessage>();
				if (response.Data.Results.Count != 0)
				{
					foreach (var tweet in response.Data.Results)
					{
						finalResults.Add(ParseItem(tweet));

					}
					_lastId = finalResults.Last().MessageId;
					MessagesLoaded(null, finalResults.ToArray());
				} else
				{
					MessagesLoaded(new Exception("Больше не могу"),null);
				}

			}
		}

		private TwitterMessage ParseItem(TwitterResult rawTweet)
		{
			using (var webClient = new WebClient())
			{
				var bytes = webClient.DownloadData(rawTweet.ProfileImageUrl);
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var localFilename = rawTweet.Id + ".png";
				var localPath = Path.Combine(documentsPath, localFilename);
				File.WriteAllBytes(localPath, bytes);
			
				var url = rawTweet.Entities.Urls.Count != 0 ? rawTweet.Entities.Urls.First().DisplayUrl : "";
				var message = new TwitterMessage() {MessageId = rawTweet.Id.ToString(), Text = rawTweet.Text,
					Url = url, CreatedAt = DateTime.Parse(rawTweet.CreatedAt), 
					Source = rawTweet.Source, TwitterUser = new User(){Name = rawTweet.FromUser, ImageUri = localPath }
				};
				return message;
			}
		}
	}
}

