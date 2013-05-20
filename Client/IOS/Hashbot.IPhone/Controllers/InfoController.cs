
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Hashbot.IPhone
{
	public partial class InfoController : UIViewController
	{
		private UILabel _infoTextLabel;
		private UIButton _phoneButton;
		private UIButton _mailButton;
		private UIImageView _logo;

		public InfoController() : base ("InfoController", null)
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
			InitLayout();
			InitData();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		private void InitLayout()
		{

			_logo = new UIImageView(UIImage.FromFile("ios/Info/logo.png"));
			_logo.Frame = new RectangleF(View.Bounds.Width/2-_logo.Frame.Width/2, 20, 179, 138);

			_infoTextLabel = new UILabel(new RectangleF(View.Bounds.X + 20, _logo.Bounds.Bottom, View.Bounds.Width - 20, View.Bounds.Height -_logo.Bounds.Height));
			_infoTextLabel.BackgroundColor = UIColor.Clear;
			_infoTextLabel.Font = UIFont.FromName("Helvetica", 14);
			_infoTextLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_infoTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_infoTextLabel.Lines = 0;
			Add(_logo);
			Add(_infoTextLabel);

		
		}
		private void InitData()
		{

			_infoTextLabel.Text = System.IO.File.ReadAllText("info.txt");
		}

	}
}

