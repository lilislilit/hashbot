using System;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Linq;

namespace Hashbot.Logic
{
	public class TwitterClient
	{
		public TwitterClient()
		{
		}

		public List<TwitterMessage> MessagesByTag(string hashtag, int page=1)
		{

			var webClient = new WebClient();
			var url = new Uri("http://search.twitter.com/search.json?q=%23"+hashtag+"&include_entities=true&rpp=5&page="+page);
			var result = webClient.DownloadString(url);
			var json = (JsonObject)JsonObject.Parse(result);

			var finalresults = (from temp in (JsonArray)json["results"]
			                    let jtemp = temp as JsonObject
			           			select new TwitterMessage {
				MessageId = jtemp["id"].ToString(),
				Text= jtemp["text"],
				CreatedAt=DateTime.Parse(jtemp["created_at"]),
				Source=jtemp["source"],
				Url = ((JsonArray)((JsonObject)jtemp["entities"])["urls"]).FirstOrDefault() == null ? "" :
				((JsonArray)((JsonObject)jtemp["entities"])["urls"]).FirstOrDefault()["url"].ToString(),
				TwitterUser=new User() { Name=jtemp["from_user"] }
			});

			return finalresults.ToList();
		}
	}
}

