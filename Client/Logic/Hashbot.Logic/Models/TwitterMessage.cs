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
		public double completed_in { get; set; }

		public long max_id { get; set; }

		public string max_id_str { get; set; }

		public string next_page { get; set; }

		public int page { get; set; }

		public string query { get; set; }

		public string refresh_url { get; set; }

		public List<TwitterResult> results { get; set; }
	}

	public class TwitterResult
	{
		public string created_at { get; set; }

		public TwitterEntities entities { get; set; }

		public string from_user { get; set; }

		public long from_user_id { get; set; }

		public string from_user_id_string { get; set; }

		public string geo { get; set; }

		public long id { get; set; }

		public string id_str { get; set; }

		public string iso_language_code { get; set; }

		public string profile_image_url { get; set; }

		public string source { get; set; }

		public string text { get; set; }

		public long to_user_id { get; set; }

		public string to_user_id_str { get; set; }
	}

	public class TwitterEntities
	{
		public List<TwitterUrls> urls { get; set; }
	}

	public class TwitterUrls
	{
		public string display_url { get; set; }
	}
}



