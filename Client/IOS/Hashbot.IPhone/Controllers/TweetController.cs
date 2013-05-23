using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Hashbot.Logic;
using Hashbot.IPhone.ImageExtensions;

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
		private UIFont _tweetFont;
		private SizeF _twitLabelSize;
		private SizeF _urlSize;
		private UIFont _urlFont;
		private SizeF _dateStringSize;
		private UIFont _dateFont;

		public TweetController(TwitterMessage tweet) : base ("TweetController", null)
		{

			_tweet = tweet;
		}

		void InitSizes()
		{

			_tweetFont = Fonts.Helvetica(15);
			var tweetStringSize = (NSString)_tweet.Text;
			_twitLabelSize = tweetStringSize.StringSize(_tweetFont, View.Bounds.Width - 60, UILineBreakMode.WordWrap);

			_dateFont = Fonts.Helvetica(10);
			var dateString = (NSString)_tweet.CreatedAt.ToString("dd.MM.yyyy");
			_dateStringSize = dateString.StringSize(_dateFont);

			_urlFont = Fonts.Helvetica(10);
			var urlString = (NSString)_tweet.Url;
			_urlSize = urlString.StringSize(_urlFont);
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
			NavigationItem.Title = "Твит";
			InitSizes();
			InitLayout();


			// Perform any additional setup after loading the view, typically from a nib.

		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			_tweetLabel.Frame = new RectangleF(20, _sourceLabel.Frame.Bottom + 2, View.Bounds.Width - 30, _twitLabelSize.Height);
			_tweetLabel.SizeToFit();

			_line.Frame = new RectangleF(20, _tweetLabel.Frame.Bottom + 2, View.Bounds.Width / 2, 1);

			_dateLabel.Frame = new RectangleF(20, _line.Frame.Bottom + 2, _line.Bounds.Width / 3, 25);
			_dateLabel.SizeToFit();


			var urlStartPosition = _line.Frame.Right - _urlSize.Width;
			_urlLabel.Frame = new RectangleF(urlStartPosition, _line.Frame.Bottom + 2, _urlSize.Width, 25);
			_urlLabel.SizeToFit();
		}

		void InitLayout()
		{
			_imageView = new UIImageView(UIImage.FromFile("ios/Tweets/bg.png"));
			_imageView.AutoresizingMask = UIViewAutoresizing.All;
			View.AddSubview(_imageView);

			View.SendSubviewToBack(_imageView);
			var tempAvatar = UIImage.FromFile(_tweet.TwitterUser.ImageUri);
			var maskedAvatar = tempAvatar.GetMaskedAvatar(UIImage.FromFile("ios/Main/mask_avatar.png"));
			_avatar = new UIImageView(maskedAvatar);
			_avatar.Frame = new RectangleF(20, 30, 64, 64);
			Add(_avatar);


			var userFont = Fonts.HelveticaBold(22);
			_userLabel = new UILabel(new RectangleF(_avatar.Frame.Right+20,50, View.Bounds.Width, 25));
			_userLabel.BackgroundColor = UIColor.Clear;
			_userLabel.Text = _tweet.TwitterUser.Name;
			_userLabel.Font = userFont;
			_userLabel.TextColor = UIColor.FromRGB(42, 66, 114);
			Add(_userLabel);

			var sourceFont = Fonts.HelveticaBold(15);
			_sourceLabel = new UILabel(new RectangleF(_avatar.Frame.Right+20,_userLabel.Frame.Bottom, View.Bounds.Width-_avatar.Frame.Width-40, 25));
			_sourceLabel.BackgroundColor = UIColor.Clear;
			_sourceLabel.Font = sourceFont;
			_sourceLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_sourceLabel.Text = _tweet.Source;
			Add(_sourceLabel);


			_tweetLabel = new UILabel(new RectangleF(20,_sourceLabel.Frame.Bottom, View.Bounds.Width - 30, _twitLabelSize.Height));
			_tweetLabel.BackgroundColor = UIColor.Clear;
			_tweetLabel.Text = _tweet.Text;
			_tweetLabel.Font = _tweetFont;
			_tweetLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_tweetLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_tweetLabel.Lines = 0;
			_tweetLabel.SizeToFit();
			Add(_tweetLabel);



			_line = new UIImageView(UIImage.FromFile("ios/Tweets/line.png"));
			_line.Frame = new RectangleF(20, _tweetLabel.Frame.Bottom + 2, View.Bounds.Width / 2, 1);
			Add(_line);



			_dateLabel = new UILabel(new RectangleF(20, _line.Frame.Bottom + 2,_dateStringSize.Width, 25));
			_dateLabel.Text = _tweet.CreatedAt.ToString("dd.MM.yyyy");
			_dateLabel.Font = _dateFont;
			_dateLabel.BackgroundColor = UIColor.Clear;
			_dateLabel.TextColor = UIColor.FromRGB(119, 119, 119);
			_dateLabel.SizeToFit();
			Add(_dateLabel);

	
			var urlStartPosition = _line.Frame.Right - _urlSize.Width;
			_urlLabel = new UILabel(new RectangleF(urlStartPosition, _line.Frame.Bottom + 2,_urlSize.Width, 25));
			_urlLabel.Text = _tweet.Url;
			_urlLabel.Font = _urlFont;
			_urlLabel.BackgroundColor = UIColor.Clear;
			_urlLabel.TextColor = UIColor.FromRGB(119, 119, 119);
			_urlLabel.SizeToFit();
			 Add(_urlLabel);
		}
	}
}

