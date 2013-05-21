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

namespace Hashbot.IPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get; set; }

		private TwitterClient _twitter;
		private UITableView _table;
		private TwitterSource _source;
		private int _page;
		private UIButton _moreButton;
		private TweetController _tweetController;
		private InfoController _info;

		public HashTagController() : base ("HashTagController", null)
		{
			_twitter = new TwitterClient();
			var infoButton = new UIBarButtonItem("Инфо", UIBarButtonItemStyle.Plain, HandleRightBarButton);
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

			_twitter.MessagesByTag(HashTag);
			_twitter.MessagesLoaded += HandleMessagesLoaded;

			_page = 1;


			// Perform any additional setup after loading the view, typically from a nib.
		}

		void LoadTable()
		{
			_table = new UITableView(new RectangleF(0, 0, View.Bounds.Width, View.Bounds.Height));
			_table.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			var tmpView = new UIView(new RectangleF(0, 0, _table.Bounds.Width, 50));
			tmpView.BackgroundColor = UIColor.Clear;
			_table.TableFooterView = tmpView;
			_moreButton = new UIButton(UIButtonType.RoundedRect);
			_moreButton.Frame = new RectangleF((tmpView.Bounds.Width - 130) / 2, (tmpView.Frame.Height - 40) / 2, 130, 40);
			_moreButton.SetTitleColor(UIColor.FromRGB(0, 0, 0), UIControlState.Normal);
			_moreButton.TouchUpInside += HandleTouchMoreButton;
			_moreButton.SetTitle("Loading", UIControlState.Normal);
			Add(_table);
			tmpView.Add(_moreButton);
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();

			_table.Frame = View.Bounds;
		}

		void HandleMessagesLoaded(Exception error, TwitterMessage[] tweets)
		{
			if (error != null)
			{
				new UIAlertView("Ошибка", "Ошибка соединения с твиттером", null, "Ок").Show();
			} else
			{
				if (_source == null)
				{
					_source = new TwitterSource(tweets);
					_source.RowSelectedEvent += HandleRowSelectedEvent;
				} else
				{
					_source.AddTweets(tweets);
				}

				InvokeOnMainThread(UIUpdate);


			}
		}

		private void UIUpdate()
		{
			_table.Source = _source;
			_table.ReloadData();
			_moreButton.SetTitle("Показать еще", UIControlState.Normal);

		}

		void HandleRowSelectedEvent(TwitterMessage tweet)
		{
			_tweetController = new TweetController(tweet);

			NavigationController.PushViewController(_tweetController, true);
		}

		private void HandleTouchMoreButton(object sender, EventArgs e)
		{

			_moreButton.SetTitle("Loading", UIControlState.Normal);
			_page++;
			_twitter.MessagesByTag(HashTag, _page);

		}

		private void HandleRightBarButton(object sender, EventArgs args)
		{
			if (_info == null)
				_info = new InfoController();

			this.PresentViewController (_info,true,null);
		}
	}
}

