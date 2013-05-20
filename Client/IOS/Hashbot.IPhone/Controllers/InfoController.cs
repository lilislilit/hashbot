
using System;
using System.Drawing;
using MonoTouch.CoreServices;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using MonoTouch.AddressBookUI;

namespace Hashbot.IPhone
{
	public partial class InfoController : UIViewController
	{
		private UILabel _infoTextLabel;
		private UIButton _phoneButton;
		private UIButton _mailButton;
		private UIImageView _logo;
		private string _phoneNumber;
		private string _email;
		private MFMailComposeViewController _mailController;

		public InfoController() : base ("InfoController", null)
		{
			_phoneNumber = "7-812-309-3879";
			_email = "hello24@touchin.ru";
			_mailController = new MFMailComposeViewController ();
			_mailController.SetToRecipients (new string[]{_email});
			_mailController.SetSubject ("Заказать приложение");
			_mailController.SetMessageBody ("Текст Заказа", false);
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
			_phoneButton = new UIButton(UIButtonType.Custom);

			_phoneButton.SetBackgroundImage(UIImage.FromFile("ios/Info/button_pressed.png"), UIControlState.Selected);
			_phoneButton.SetBackgroundImage(UIImage.FromFile("ios/Info/button.png"), UIControlState.Normal);
			_phoneButton.SetImage(UIImage.FromFile("ios/Info/icon_phone.png"), UIControlState.Normal);
			_phoneButton.Frame = new RectangleF(View.Bounds.X + 20, _infoTextLabel.Frame.Bottom, 130, 40);
			_phoneButton.TouchUpInside += HandleTouchPhoneButton;
			_mailButton = new UIButton(UIButtonType.Custom);
			_mailButton.SetBackgroundImage(UIImage.FromFile("ios/Info/button.png"), UIControlState.Normal);
			_mailButton.SetBackgroundImage(UIImage.FromFile("ios/Info/button_pressed.png"), UIControlState.Selected);
			_mailButton.SetImage(UIImage.FromFile("ios/Info/icon_mail.png"), UIControlState.Normal);
			_mailButton.Frame = new RectangleF(View.Bounds.Width -150, _infoTextLabel.Frame.Bottom, 130, 40);
			_mailButton.TouchUpInside += HandleTouchMailButton; 

			_mailController.Finished+= HandleFinishedMail;

			Add(_phoneButton);
			Add(_logo);
			Add(_infoTextLabel);
			Add(_mailButton);
		
		}

		void HandleFinishedMail (object sender, MFComposeResultEventArgs e)
		{
		  e.Controller.DismissViewController(true,null); 
		}

		void HandleTouchMailButton (object sender, EventArgs e)
		{
			this.PresentViewController (_mailController, true,null);
		}

		void HandleTouchPhoneButton (object sender, EventArgs e)
		{
			var pref = @"tel://";
			UIApplication.SharedApplication.OpenUrl(new NSUrl(pref+_phoneNumber));
		}
		private void InitData()
		{

			_infoTextLabel.Text = System.IO.File.ReadAllText("info.txt");
		}

	}
}

