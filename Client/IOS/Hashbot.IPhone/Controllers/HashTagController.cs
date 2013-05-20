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
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_table = new UITableView(new RectangleF(0,0,View.Bounds.Width,View.Bounds.Height-60)); 

			_moreButton = new UIButton(UIButtonType.RoundedRect);
			_moreButton.Frame = new RectangleF(60, View.Bounds.Height - 40, 130, 40);
			_moreButton.SetTitle("Показать еще", UIControlState.Normal);
			_moreButton.SetTitleColor(UIColor.FromRGB(0,0,0), UIControlState.Normal);
			_moreButton.TouchUpInside += HandleTouchMoreButton;
			_moreButton.SetTitle("Loading", UIControlState.Normal);

			_twitter.MessagesByTag(HashTag);
			_twitter.MessagesLoaded += HandleMessagesLoaded;

			Add(_table);
			Add(_moreButton);

			_page = 1;


			// Perform any additional setup after loading the view, typically from a nib.
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
			if (_tweetController == null)
				_tweetController = new TweetController(tweet);
			_tweetController.InitWith(tweet); 
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

			NavigationController.SetNavigationBarHidden(true, true);

			NavigationController.PushViewController(_info, true);
		}
	}
}

