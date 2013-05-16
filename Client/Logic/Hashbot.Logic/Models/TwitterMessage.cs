using System;

namespace Hashbot.Logic
{
	public class TwitterMessage
	{
		public string Text { get; set; }
		public DateTime CreatedAt {get;set;}
		public string Source { get; set; }
		public string Url {get; set;}
		public User TwitterUser {get;set;}
		public string MessageId {get;set;}
	}

	public class User 
	{
		public string Name { get; set; }
		public string ImageUri { get; set; }
	}
}

