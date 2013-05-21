using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Hashbot.Logic;

namespace Hashbot.IPhone
{
	public partial class TweetController : UIViewController
	{
		private TwitterMessage _tweet;
		private UILabel _dateLabel;
		private UILabel _tweetLabel;
		private UILabel _userLabel;
		private UILabel _sourceLabel;
		private UILabel _urlLabel;
		private UIImageView _line;
		private UIImageView _avatar;
		private UIImageView _imageView;
	

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
			NavigationItem.Title = "Твит";
			InitLayout();
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		void InitLayout()
		{
			_imageView = new UIImageView(UIImage.FromFile("ios/Tweets/bg.png"));
			View.AddSubview(_imageView);
			View.SendSubviewToBack(_imageView);
			var maskedAvatar = Helpers.GetMaskedAvatar(UIImage.FromFile(Helpers.FileById(_tweet.MessageId)),UIImage.FromFile("ios/Main/mask_avatar.png"));
			_avatar = new UIImageView(maskedAvatar);
			_avatar.Frame = new RectangleF(View.Bounds.X + 10, View.Bounds.X + 20, 64, 64);

			UIFont tweetFont = UIFont.FromName("HelveticaNeue", 18);
			NSString tweetStringSize = (NSString)_tweet.Text;
			var twitLabelSize = tweetStringSize.StringSize(tweetFont,View.Bounds.Width-60,UILineBreakMode.WordWrap);
			_tweetLabel = new UILabel(new RectangleF(20,90, View.Bounds.Width - 60, twitLabelSize.Height));
			_tweetLabel.BackgroundColor = UIColor.Clear;
			_tweetLabel.Text = _tweet.Text;
			_tweetLabel.Font = UIFont.FromName("HelveticaNeue", 18);
			_tweetLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_tweetLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_tweetLabel.Lines = 0;
			_tweetLabel.SizeToFit();
		

			_userLabel = new UILabel(new RectangleF(80,20, View.Bounds.Width, 25));
			_userLabel.BackgroundColor = UIColor.Clear;
			_userLabel.Text = _tweet.TwitterUser.Name;
			_userLabel.Font = UIFont.FromName("HelveticaNeue-Bold", 25);
			_userLabel.TextColor = UIColor.FromRGB(68, 100, 43);

			_line = new UIImageView(UIImage.FromFile("ios/Tweets/line.png"));
			_line.Frame = new RectangleF(20, _tweetLabel.Frame.Bottom + 2, View.Bounds.Width - 60, 1);

			_dateLabel = new UILabel(new RectangleF(20, _line.Frame.Bottom + 2,_line.Bounds.Width/3, 25));
			_dateLabel.Text = _tweet.CreatedAt.ToString("dd:mm:yyyy");
			_dateLabel.Font = UIFont.FromName("HelveticaNeue", 15);
			_dateLabel.BackgroundColor = UIColor.Clear;
			_dateLabel.TextColor = UIColor.FromRGB(119, 119, 119);

			_sourceLabel = new UILabel(new RectangleF(80,60, View.Bounds.Width, 25));
			_sourceLabel.BackgroundColor = UIColor.Clear;
			_sourceLabel.Font = UIFont.FromName("HelveticaNeue-Bold", 18);
			_sourceLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_sourceLabel.Text = _tweet.Source;

			_urlLabel = new UILabel(new RectangleF(_dateLabel.Bounds.Right+15, _line.Frame.Bottom + 2,_line.Bounds.Width-_dateLabel.Bounds.Width, 25));
			_urlLabel.Text = _tweet.Url;
			_urlLabel.Font = UIFont.FromName("HelveticaNeue", 15);
			_urlLabel.BackgroundColor = UIColor.Clear;
			_urlLabel.TextColor = UIColor.FromRGB(119, 119, 119);

			Add(_tweetLabel);
			Add(_userLabel);
			Add(_sourceLabel);
			Add(_line);
			Add(_dateLabel);
			Add(_avatar);
			Add(_urlLabel);
		}
	}
}

