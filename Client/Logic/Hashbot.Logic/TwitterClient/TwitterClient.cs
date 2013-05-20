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
			request.AddParameter("q", "%23" + hashtag);
			request.AddParameter("include_entities", "true");
			request.AddParameter("rpp", "5");
			request.AddParameter("page", page);

			var asyncHandle = client.ExecuteAsync<TwitterResponse>(request, response => {
				HandleResponse(response);

			});
		}

		void HandleResponse(IRestResponse<TwitterResponse> response)
		{
			if (response.ErrorException != null)
			{
				MessagesLoaded(response.ErrorException, null);
			} else
			{
				var finalResults = new List<TwitterMessage>();
				foreach (var tweet in response.Data.results)
				{
					finalResults.Add(ParseItem(tweet));

				}
				MessagesLoaded(null, finalResults.ToArray());
			}
		}

		private TwitterMessage ParseItem(TwitterResult raw_response)
		{
			using (var webClient = new WebClient())
			{
				var bytes = webClient.DownloadData(raw_response.profile_image_url);
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string localFilename = raw_response.id + ".png";
				string localPath = Path.Combine(documentsPath, localFilename);
				File.WriteAllBytes(localPath, bytes);
			
				var url = raw_response.entities.urls.Count != 0 ? raw_response.entities.urls.First().display_url : "";
				var message = new TwitterMessage() {MessageId = raw_response.id.ToString(), Text = raw_response.text,
					Url = url, CreatedAt = DateTime.Parse(raw_response.created_at), 
					Source = raw_response.source, TwitterUser = new User(){Name = raw_response.from_user, ImageUri = localPath }
				};
				return message;
			}
		}
	}
}

