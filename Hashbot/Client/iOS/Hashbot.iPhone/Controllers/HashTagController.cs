
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Hashbot.iPhone
{
	public partial class HashTagController : UIViewController
	{
		public string HashTag { get;set; }

		public HashTagController() : base ("HashTagController", null)
		{
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
			lblTag.Text = HashTag+" stab text";
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

