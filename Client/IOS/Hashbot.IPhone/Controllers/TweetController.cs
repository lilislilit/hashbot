using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Hashbot.Logic;

namespace Hashbot.IPhone
{
	public partial class TweetController : UIViewController
	{
		private TwitterMessage _tweet;

		public TweetController(TwitterMessage tweet) : base ("TweetController", null)
		{
			_tweet = tweet;
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}
	
		public override void ViewDidLoad()
		{
			var imageView = new UIImageView (UIImage.FromFile("ios/Tweets/bg.png"));
			var line = new UIImageView(UIImage.FromFile("ios/Tweets/line.png"));
			View.AddSubview(imageView);
			View.SendSubviewToBack(imageView);
			base.ViewDidLoad();
			var tweetLabel = new UILabel(new RectangleF(View.Bounds.X+40, View.Bounds.Y+80, View.Bounds.Width-60, View.Bounds.Height/3));
			var userLabel = new UILabel(new RectangleF(View.Bounds.X+80, View.Bounds.Y+20, View.Bounds.Width, 25));
			var sourceLabel = new UILabel(new RectangleF(View.Bounds.X+80, View.Bounds.Y+40, View.Bounds.Width, 25));
			line.Frame = new RectangleF(View.Bounds.X+40, tweetLabel.Frame.Bottom+2, View.Bounds.Width - 60, 1);
			var dateLabel = new UILabel(new RectangleF(View.Bounds.X+80,line.Frame.Bottom+2, 50, 25));
			imageView.BackgroundColor = UIColor.Clear;
			tweetLabel.BackgroundColor = UIColor.Clear;
			tweetLabel.Text = _tweet.Text;
			tweetLabel.LineBreakMode = UILineBreakMode.WordWrap;
			tweetLabel.Lines = 0;
			userLabel.BackgroundColor = UIColor.Clear;
			dateLabel.BackgroundColor = UIColor.Clear;
			userLabel.Text = _tweet.TwitterUser.Name;
			sourceLabel.BackgroundColor = UIColor.Clear;
			var sourceText = _tweet.Source.Substring(_tweet.Source.IndexOf("&gt")+4);
			sourceLabel.Text = "via " + sourceText.Substring(0, sourceText.Length - 10);;
			dateLabel.Text = _tweet.CreatedAt.ToString("dd:mm:yyyy");
			Add(tweetLabel);
			Add(userLabel);
			Add(sourceLabel);
			Add(line);
			Add(dateLabel);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

