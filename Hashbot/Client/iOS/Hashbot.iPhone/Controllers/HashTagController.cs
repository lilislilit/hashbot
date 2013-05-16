
using System;
using System.Drawing;
using Hashbot.Logic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Hashbot.IPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get;set; }
		private TwitterClient _twitter;
		public HashTagController() : base ("HashTagController", null)
		{
			_twitter = new TwitterClient();
		}
		
		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad()
		{

			var table = new UITableView(View.Bounds); // defaults to Plain style
			var tweets = _twitter.MessagesByTag(HashTag); 
			table.Source = new TwitterTable(tweets.ToArray());
			Add (table);
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

