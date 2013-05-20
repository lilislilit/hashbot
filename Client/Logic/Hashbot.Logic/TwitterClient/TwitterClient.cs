using System;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Linq;
using System.IO;

namespace Hashbot.Logic
{
	public class TwitterClient
	{
		public TwitterClient()
		{
		}

		public TwitterMessage[] MessagesByTag(string hashtag, int page=1)
		{
			try
			{
				using (var webClient = new WebClient())
				{
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
						TwitterUser=new User() { Name=jtemp["from_user"], ImageUri = jtemp["profile_image_url"] }
					});
					foreach(var tweet in finalresults)
					{
						var bytes = webClient.DownloadData(tweet.TwitterUser.ImageUri);
						string documentsPath =Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
						string localFilename = tweet.MessageId +".png";
						string localPath = Path.Combine (documentsPath, localFilename);
						File.WriteAllBytes (localPath, bytes); // writes to local storage
						tweet.TwitterUser.ImageUri = localPath;
					}
					return finalresults.ToArray();
				}


			} catch (WebException ex)
			{
				throw ex;
			}
		}
	}
}

