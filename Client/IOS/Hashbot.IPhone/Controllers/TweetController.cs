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

			base.ViewDidLoad();
			InitLayout();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void InitLayout()
		{
			var imageView = new UIImageView(UIImage.FromFile("ios/Tweets/bg.png"));
			View.AddSubview(imageView);
			View.SendSubviewToBack(imageView);

			var avatar = new UIImageView(UIImage.FromFile("ios/Main/avatar.png"));
			avatar.Frame = new RectangleF(View.Bounds.X + 10, View.Bounds.X + 20, 64, 64);

			var tweetLabel = new UILabel(new RectangleF(View.Bounds.X + 20, View.Bounds.Y + 80, View.Bounds.Width - 60, View.Bounds.Height / 3));
			tweetLabel.BackgroundColor = UIColor.Clear;
			tweetLabel.Text = _tweet.Text;
			tweetLabel.Font = UIFont.FromName("Helvetica", 18);
			tweetLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			tweetLabel.LineBreakMode = UILineBreakMode.WordWrap;
			tweetLabel.Lines = 0;

			var userLabel = new UILabel(new RectangleF(View.Bounds.X + 80, View.Bounds.Y + 20, View.Bounds.Width, 25));
			userLabel.BackgroundColor = UIColor.Clear;
			userLabel.Text = _tweet.TwitterUser.Name;
			userLabel.Font = UIFont.FromName("Helvetica", 25);
			userLabel.TextColor =  UIColor.FromRGB(68, 100, 43);

			var line = new UIImageView(UIImage.FromFile("ios/Tweets/line.png"));
			line.Frame = new RectangleF(View.Bounds.X + 20, tweetLabel.Frame.Bottom + 2, View.Bounds.Width - 60, 1);

			var dateLabel = new UILabel(new RectangleF(View.Bounds.X + 20, line.Frame.Bottom + 2,line.Bounds.Width/3, 25));
			dateLabel.Text = _tweet.CreatedAt.ToString("dd:mm:yyyy");
			dateLabel.Font = UIFont.FromName("Helvetica", 15);
			dateLabel.BackgroundColor = UIColor.Clear;
			dateLabel.TextColor = UIColor.FromRGB(119, 119, 119);

			var sourceLabel = new UILabel(new RectangleF(View.Bounds.X + 80, View.Bounds.Y + 60, View.Bounds.Width, 25));
			sourceLabel.BackgroundColor = UIColor.Clear;
			sourceLabel.Font = UIFont.FromName("Helvetica", 18);
			sourceLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			var sourceText = _tweet.Source.Substring(_tweet.Source.IndexOf("&gt") + 4);
			sourceLabel.Text = "via " + sourceText.Substring(0, sourceText.Length - 10);

			var urlLabel = new UILabel(new RectangleF(dateLabel.Bounds.Right+15, line.Frame.Bottom + 2,line.Bounds.Width-dateLabel.Bounds.Width, 25));
			urlLabel.Text = _tweet.Url;
			urlLabel.Font = UIFont.FromName("Helvetica", 15);
			urlLabel.BackgroundColor = UIColor.Clear;
			urlLabel.TextColor = UIColor.FromRGB(119, 119, 119);

			Add(tweetLabel);
			Add(userLabel);
			Add(sourceLabel);
			Add(line);
			Add(dateLabel);
			Add(avatar);
			Add(urlLabel);
		}
	}
}

