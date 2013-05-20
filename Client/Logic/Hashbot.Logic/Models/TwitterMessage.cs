using System;
using System.Collections.Generic;

namespace Hashbot.Logic
{
	public class TwitterMessage
	{
		private string _source;

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

		public User TwitterUser { get; set; }

		public string MessageId { get; set; }
	}

	public class User
	{
		public string Name { get; set; }

		public string ImageUri { get; set; }
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



