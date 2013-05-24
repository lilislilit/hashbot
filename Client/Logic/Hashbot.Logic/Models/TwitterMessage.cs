using System;
using System.Collections.Generic;
using System.Linq;

namespace Hashbot.Logic
{
	public class TwitterMessage
	{

		public TwitterMessage(TwitterResult rawTweet)
		{
			var url = rawTweet.Entities.Urls.Count != 0 ? rawTweet.Entities.Urls.First().DisplayUrl : "";
			MessageId = rawTweet.Id.ToString();
			Text = rawTweet.Text;
			Url = url;
			CreatedAt = DateTime.Parse(rawTweet.CreatedAt); 
			Source = rawTweet.Source;
			UserName = rawTweet.FromUser;
			AvatarUri = rawTweet.ProfileImageUrl;
			UserId = rawTweet.FromUserId.ToString();
		}

		private string _source;

		public string UserId { get; set; }

		public string Text { get; set; }

		public DateTime CreatedAt { get; set; }

		public string Source
		{
			get
			{
				var sourceText = _source.Substring(_source.IndexOf("&gt") + 4);
				return  "via " + sourceText.Substring(0, sourceText.Length - 10);
			}
			set
			{
				_source = value;
			}
		}

		public string Url { get; set; }

		public string MessageId { get; set; }

		public string UserName { get; set; }

		public string AvatarUri { get; set; }
	}


	public class TwitterResponse
	{
		public double CompletedIn { get; set; }

		public long MaxId { get; set; }

		public string MaxIdStr { get; set; }

		public string NextPage { get; set; }

		public int Page { get; set; }

		public string Query { get; set; }

		public string RefreshUrl { get; set; }

		public List<TwitterResult> Results { get; set; }
	}

	public class TwitterResult
	{
		public string CreatedAt { get; set; }

		public TwitterEntities Entities { get; set; }

		public string FromUser { get; set; }

		public long FromUserId { get; set; }

		public long Id { get; set; }

		public string ProfileImageUrl { get; set; }

		public string Source { get; set; }

		public string Text { get; set; }

	}

	public class TwitterEntities
	{
		public List<TwitterUrls> Urls { get; set; }
	}

	public class TwitterUrls
	{
		public string DisplayUrl { get; set; }
	}
}



