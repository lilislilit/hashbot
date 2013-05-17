
using System;
using System.Collections.Generic;
using System.Drawing;
using Hashbot.Logic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Hashbot.IPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get;set; }
		private List<TwitterMessage> _messages;
		private TwitterClient _twitter;
		private UITableView _table;
		private int _page;
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

			_table = new UITableView(new RectangleF(0,0,View.Bounds.Width,View.Bounds.Height-60)); // defaults to Plain style
			var moreButton = new UIButton(UIButtonType.RoundedRect);
			moreButton.Frame = new RectangleF(60, View.Bounds.Height-40, 130, 40);
			moreButton.SetTitle("Показать еще",UIControlState.Normal);
			moreButton.SetTitleColor(UIColor.FromRGB(0,0,0),UIControlState.Normal);
			moreButton.TouchUpInside += HandleTouchMoreButton;
			_messages =  _twitter.MessagesByTag(HashTag);
			var source = new TwitterTable(_messages.ToArray());
			source.RowSelectedEvent+= HandleRowSelectedEvent;
			_table.Source = source;


			Add(_table);
			Add(moreButton);
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void HandleRowSelectedEvent (TwitterMessage obj)
		{
			NavigationController.PushViewController(new TweetController(obj),true);
		}
		private void HandleTouchMoreButton(object sender, EventArgs e)
		{
			_page++;
			_messages.AddRange(_twitter.MessagesByTag(HashTag,_page));
			var source = new TwitterTable(_messages.ToArray());
			source.RowSelectedEvent+= HandleRowSelectedEvent;
			_table.Source = source;

		}
	}
}

