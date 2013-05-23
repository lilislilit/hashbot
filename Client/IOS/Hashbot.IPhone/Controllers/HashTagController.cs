using System;
using System.Collections.Generic;
using System.Drawing;
using Hashbot.Logic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace Hashbot.IPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get; set; }

		private int _page;
		private TwitterClient _twitter;
		private UITableView _table;
		private TwitterSource _source;
		private UIButton _moreButton;
		private TweetController _tweetController;
		private UIAlertView _loadingAlert;
		private UIView _buttonSubView;

		private InfoController _info;
		private InfoController Info
		{
			get {
				return _info ?? (_info = new InfoController());
			}
		}
		public HashTagController() : base()
		{
			_twitter = new TwitterClient();
			var infoButton = new UIBarButtonItem(TextBundle.InfoTitle, UIBarButtonItemStyle.Plain, HandleRightBarButton);

			NavigationItem.SetRightBarButtonItem(infoButton, false);
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			if (_source != null)
			{
				_source.PurgeImages();
				_source = null;
				_page = 1;

				_twitter.MessagesByTag(HashTag, _page);
			}   
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			LoadTable(); 
			InitPreloader();
		
			_twitter.MessagesByTag(HashTag);
			_twitter.MessagesLoaded += HandleMessagesLoaded;

			_page = 1;

			// Perform any additional setup after loading the view, typically from a nib.
		}



		void InitPreloader()
		{
			_loadingAlert = new UIAlertView(new RectangleF(10, 20, 290, 100));

			var centerX = _loadingAlert.Bounds.GetMidX(); //Width / 2;
			var centerY = _loadingAlert.Bounds.GetMidY(); //Height / 2;
			var activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);

			activitySpinner.Frame = new RectangleF(centerX - (activitySpinner.Frame.Width / 2), centerY - activitySpinner.Frame.Height, activitySpinner.Frame.Width, activitySpinner.Frame.Height);
			activitySpinner.StartAnimating();

			_loadingAlert.AddSubview(activitySpinner);

			var loadingLabel = new UILabel(new RectangleF(centerX - 70, activitySpinner.Frame.Bottom, 200, 15)) {
				Text = TextBundle.LoadinCaption,
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Font = UIFont.FromName(Fonts.HelveticaBold, 14)
			};

			_loadingAlert.AddSubview(loadingLabel);
			_loadingAlert.SizeToFit();
			_loadingAlert.Show();
		}


		void LoadTable()
		{
			_table = new UITableView(new RectangleF(0, 0, View.Bounds.Width, View.Bounds.Height));
			_table.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			_buttonSubView = new UIView(new RectangleF(0, 0, _table.Bounds.Width, 50));
			_buttonSubView.BackgroundColor = UIColor.Clear;
			_table.TableFooterView = _buttonSubView;

		
			_moreButton = new UIButton(UIButtonType.Custom);
			_moreButton.TintColor = UIColor.FromRGB(247, 247, 247);

			_moreButton.Frame = new RectangleF(10, (_buttonSubView.Frame.Height - 40) / 2, 300, 40);
			_moreButton.BackgroundColor = UIColor.FromRGB(247, 247, 247);
			_moreButton.Layer.CornerRadius = 10;
			_moreButton.Layer.BorderWidth = 2;
			_moreButton.Layer.BorderColor = UIColor.FromRGB(186, 188, 187).CGColor;

			_moreButton.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			_moreButton.SetTitleColor(UIColor.FromRGB(0, 0, 0), UIControlState.Normal);
			_moreButton.TouchUpInside += HandleTouchMoreButton;
			_moreButton.SetTitle(TextBundle.LoadMore, UIControlState.Normal);

			Add(_table);
			_buttonSubView.Add(_moreButton);
		}


		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();

			_table.Frame = View.Bounds;
			_buttonSubView.Frame.Width = _table.Bounds.Width;
			_moreButton.Frame.Width = _buttonSubView.Frame.Width - 50;
		}


		void HandleMessagesLoaded(Exception error, TwitterMessage[] tweets)
		{
			if (error != null)
			{
				InvokeOnMainThread(()=> {
					_loadingAlert.DismissWithClickedButtonIndex(0,false);
					new UIAlertView(TextBundle.Errors.Error, String.Format("{0}: {1}",TextBundle.Errors.TwitterConnError,error.Message), null, TextBundle.AlertButton).Show();
				});
			} else {
				if (_source == null)
				{
					_source = new TwitterSource(tweets);
					_source.RowSelectedEvent += HandleRowSelectedEvent;
				} else {
					_source.AddTweets(tweets);
				}

				InvokeOnMainThread(UIUpdate);
			}
		}


		private void UIUpdate()
		{
			_table.Source = _source;
			_table.ReloadData();
			_loadingAlert.DismissWithClickedButtonIndex(0, true);
		}


		void HandleRowSelectedEvent(TwitterMessage tweet)
		{
			_tweetController = new TweetController(tweet);

			NavigationController.PushViewController(_tweetController, true);
		}


		private void HandleTouchMoreButton(object sender, EventArgs e)
		{
			_page++;

			_loadingAlert.Show();
			_twitter.MessagesByTag(HashTag, _page);
		}


		private void HandleRightBarButton(object sender, EventArgs args)
		{
			NavigationController.PushViewController(Info, true);
		}
	}
}

