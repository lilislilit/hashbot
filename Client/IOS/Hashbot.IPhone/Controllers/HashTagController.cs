using System;
using System.Collections.Generic;
using System.Drawing;
using Hashbot.Logic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Net;

namespace Hashbot.IPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get; set; }

		private TwitterMessage[] _messages;
		private TwitterClient _twitter;
		private UITableView _table;
		private TwitterTable _source;
		private int _page;
		private TweetController _tweetController;

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
			base.ViewDidLoad();
			_table = new UITableView(new RectangleF(0,0,View.Bounds.Width,View.Bounds.Height-60)); // defaults to Plain style
			var moreButton = new UIButton(UIButtonType.RoundedRect);
			moreButton.Frame = new RectangleF(60, View.Bounds.Height - 40, 130, 40);
			moreButton.SetTitle("Показать еще", UIControlState.Normal);
			moreButton.SetTitleColor(UIColor.FromRGB(0,0,0), UIControlState.Normal);
			moreButton.TouchUpInside += HandleTouchMoreButton;
			try
			{
				_messages = _twitter.MessagesByTag(HashTag);
				_source = new TwitterTable(_messages);
				_source.RowSelectedEvent += HandleRowSelectedEvent;
				_table.Source = _source;
				Add(_table);
				Add(moreButton);
				_page = 1;
			} catch (WebException ex)
			{
				new UIAlertView("Ошибка", "Ошибка соединения с твиттером", null, "Ок").Show();
			}

			// Perform any additional setup after loading the view, typically from a nib.
		}

		void HandleRowSelectedEvent(TwitterMessage tweet)
		{
			/*if (_tweetController == null)
				_tweetController = new TweetController(tweet);
			_tweetController.SetTweet(tweet); Не работает,спросить у Макса, как переиницилизировать элементы вьюхи*/
			NavigationController.PushViewController(new TweetController(tweet),true);
		}

		private void HandleTouchMoreButton(object sender, EventArgs e)
		{

			_page++;
			TwitterMessage[] newMessages;
			try
			{
				newMessages = _twitter.MessagesByTag(HashTag, _page);
				_source.AddTweets(newMessages);
				_table.Source = _source;
				_table.ReloadData();
			} catch (WebException ex)
			{
				new UIAlertView("Ошибка", "Ошибка соединения с твиттером", null, "Ок").Show();
			}


		}
	}
}

