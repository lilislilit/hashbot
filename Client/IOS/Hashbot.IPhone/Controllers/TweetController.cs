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
		private AvatarLoader _avatarLoader;

		private const int leftBound = 20;

		public TweetController(TwitterMessage tweet) : base ()
		{
			_tweet = tweet;
			_tweet.Url = "ссылка на твит";

		}

		void InitSizes()
		{

			_tweetFont = UIFont.FromName(Fonts.Helvetica, 15);
			var tweetStringSize = (NSString)_tweet.Text;
			_twitLabelSize = tweetStringSize.StringSize(_tweetFont, View.Bounds.Width - 60, UILineBreakMode.WordWrap);

			_dateFont = UIFont.FromName(Fonts.Helvetica, 10);
			var dateString = (NSString)_tweet.CreatedAt.ToString("dd.MM.yyyy");
			_dateStringSize = dateString.StringSize(_dateFont);

			_urlFont = UIFont.FromName(Fonts.Helvetica, 10);
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

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			_tweetLabel.Frame = new RectangleF(leftBound, _sourceLabel.Frame.Bottom + 2, View.Bounds.Width - 30, _twitLabelSize.Height);
			_tweetLabel.SizeToFit();

			_line.Frame = new RectangleF(leftBound, _tweetLabel.Frame.Bottom + 2, View.Bounds.Width / 2, 1);

			_dateLabel.Frame = new RectangleF(leftBound, _line.Frame.Bottom + 2, _line.Bounds.Width / 3, 25);
			_dateLabel.SizeToFit();


			var urlStartPosition = _line.Frame.Right - _urlSize.Width;
			_urlLabel.Frame = new RectangleF(urlStartPosition, _line.Frame.Bottom + 2, _urlSize.Width, 25);
			_urlLabel.SizeToFit();
		}

		private void ImageLoadedHandler(string uri, string origUri)
		{
			if (uri == origUri)
			{
				var avatarImage = UIImage.FromFile(uri).GetMaskedAvatar();
			
				InvokeOnMainThread(()=> {
				_avatar.Image = avatarImage;
				});
			}

		}

		void InitLayout()
		{
			_imageView = new UIImageView(UIImage.FromFile("ios/Tweets/bg.png"));
			_imageView.Frame = View.Bounds;
			_imageView.AutoresizingMask = UIViewAutoresizing.All;
			View.AddSubview(_imageView);

			View.SendSubviewToBack(_imageView);

			_avatarLoader = _avatarLoader ?? new AvatarLoader();
			_avatarLoader.GetAvatarByUri(_tweet.AvatarUri, _tweet.UserId);
			_avatarLoader.ImageDownloaded += ImageLoadedHandler;

			var imgLocalUri =_avatarLoader.GetAvatarByUri(_tweet.AvatarUri, _tweet.UserId);
			var clippedImage = new UIImage();
			if (!String.IsNullOrEmpty(imgLocalUri))
			{
				clippedImage = UIImage.FromFile(imgLocalUri).GetMaskedAvatar();
			} 



			_avatar = new UIImageView {
				Frame = new RectangleF(leftBound, 30, 64, 64)
			};
			_avatar.Image = clippedImage;



			Add(_avatar);


			var userFont = UIFont.FromName(Fonts.HelveticaBold, 22);

			var avatarMargin = _avatar.Frame.Right + 20;

			_userLabel = new UILabel(new RectangleF(avatarMargin,50, View.Bounds.Width, 25)) {
				BackgroundColor = UIColor.Clear,
				Text = _tweet.UserName,
				Font = userFont,
				TextColor = Colors.DeepBlue,
			};
			Add(_userLabel);

			var sourceFont = UIFont.FromName(Fonts.HelveticaBold, 15);
			_sourceLabel = new UILabel(new RectangleF(avatarMargin,_userLabel.Frame.Bottom, View.Bounds.Width-_avatar.Frame.Width-40, 25)) {	BackgroundColor = UIColor.Clear,
				Font = sourceFont,
				TextColor = Colors.LightBlack,
				Text = _tweet.Source
			};
			Add(_sourceLabel);


			_tweetLabel = new UILabel(new RectangleF(leftBound,_sourceLabel.Frame.Bottom, View.Bounds.Width - 30, _twitLabelSize.Height)) {
				BackgroundColor = UIColor.Clear,
				Text = _tweet.Text,
				Font = _tweetFont,
				TextColor = Colors.LightBlack,
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines = 0
			};
			_tweetLabel.SizeToFit();
			Add(_tweetLabel);


			_line = new UIImageView(UIImage.FromFile("ios/Tweets/line.png"));
			_line.Frame = new RectangleF(leftBound, _tweetLabel.Frame.Bottom + 2, View.Bounds.Width / 2, 1);
			Add(_line);



			_dateLabel = new UILabel(new RectangleF(leftBound, _line.Frame.Bottom + 2,_dateStringSize.Width, 25)) {
				Text = _tweet.CreatedAt.ToString("dd.MM.yyyy"),
				Font = _dateFont,
				BackgroundColor = UIColor.Clear,
				TextColor = Colors.LightGray
			};
			_dateLabel.SizeToFit();
			Add(_dateLabel);

	
			var urlStartPosition = _line.Frame.Right - _urlSize.Width;
			_urlLabel = new UILabel(new RectangleF(urlStartPosition, _line.Frame.Bottom + 2,_urlSize.Width, 25)) {
				Text = _tweet.Url,
				Font = _urlFont,
				BackgroundColor = UIColor.Clear,
				TextColor = Colors.LightGray
			};
			_urlLabel.SizeToFit();
			Add(_urlLabel);
		}
	}
}

