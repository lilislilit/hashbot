
using System;
using System.Drawing;
using MonoTouch.CoreServices;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using MonoTouch.AddressBookUI;
using Hashbot.IPhone.ButtonExtensions;

namespace Hashbot.IPhone
{
	public class InfoController : UIViewController
	{
		private UILabel _infoTextLabel;
		private UIButton _phoneButton;
		private UIButton _mailButton;
		private UIImageView _logo;
		private string _phoneNumber;
		private string _email;
		private MFMailComposeViewController _mailController;
		private string _buttonBackground;
		private string _buttonPressedBackground;
		private UIEdgeInsets _buttonInsets;
		private UIScrollView _infoTextScrollView;

		private RectangleF _portaitInfoRect;
		private RectangleF _portaitLabelRect;

		public InfoController() : base()
		{
			_phoneNumber = "78123093879";
			_email = "hello24@touchin.ru";
			_buttonBackground = "ios/Info/button.png";
			_buttonPressedBackground = "ios/Info/button_pressed.png";
			_buttonInsets = new UIEdgeInsets(0,0,9,0);

			Title = TextBundle.InfoTitle;
			HidesBottomBarWhenPushed = true;
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

			InitSubviews();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void InitSubviews()
		{

			View.BackgroundColor = UIColor.White;
			_logo = new UIImageView(UIImage.FromFile("ios/Info/logo.png"));
			_logo.Frame = _logo.Frame.CenterHorizontIn(View.Bounds,10);
			_logo.Frame = new RectangleF(_logo.Frame.X, _logo.Frame.Y,_logo.Frame.Width, View.Bounds.Height/4);
			_portaitLabelRect = _logo.Frame;
			Add(_logo);


			_portaitInfoRect = new RectangleF(20, (_logo.Frame.Bottom / 2) - 20, View.Frame.Width - 20, View.Frame.Height - _logo.Frame.Height);

			_infoTextScrollView = new UIScrollView(_portaitInfoRect);
		
			var labelRect = _portaitInfoRect;
			labelRect.Y = 0;
			labelRect.X = 0;

			_infoTextLabel = new UILabel(labelRect);
			_infoTextLabel.Frame.Y = _infoTextLabel.Bounds.Y;
			_infoTextLabel.Text = System.IO.File.ReadAllText("info.txt");
			_infoTextLabel.BackgroundColor = UIColor.Clear;
			_infoTextLabel.Font = UIFont.FromName(Fonts.Helvetica, 14);
			_infoTextLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_infoTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_infoTextLabel.Lines = 0;




			_infoTextScrollView.ContentSize = _portaitInfoRect.Size;
			_infoTextScrollView.Bounces = false;

			//_infoTextScrollView.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			_infoTextScrollView.AddSubview(_infoTextLabel);

			 Add(_infoTextScrollView);


			_phoneButton = new UIButton(UIButtonType.Custom);
			_phoneButton.SetBackground(_buttonBackground, 11);
			_phoneButton.SetSelectedBackground(_buttonPressedBackground, 11);
			_phoneButton.SetImage(UIImage.FromFile("ios/Info/icon_phone.png"), UIControlState.Normal);
			_phoneButton.ImageEdgeInsets = _buttonInsets;
			_phoneButton.Frame = new RectangleF((View.Bounds.Width-100)/8, _infoTextScrollView.Frame.Bottom, 100, 40);
			_phoneButton.TouchUpInside += HandleTouchPhoneButton;
			_phoneButton.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			Add(_phoneButton);
		
			_mailButton = new UIButton(UIButtonType.Custom);
			_mailButton.SetBackground(_buttonBackground,11);
			_mailButton.SetSelectedBackground(_buttonPressedBackground,11);
			_mailButton.SetImage(UIImage.FromFile("ios/Info/icon_mail.png"), UIControlState.Normal);
			_mailButton.ImageEdgeInsets = _buttonInsets;
			_mailButton.Frame = new RectangleF(View.Bounds.Width-100-_phoneButton.Frame.X, _infoTextScrollView.Frame.Bottom, 100, 40);
			_mailButton.TouchUpInside += HandleTouchMailButton; 
			_mailButton.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;;
			Add(_mailButton);	



			InitMailController();		
		}

		void InitLandscapeLayout()
		{
			_infoTextLabel.Frame = new RectangleF(0, 0, View.Bounds.Width / 2 - 20, View.Bounds.Height);
			var frame = _infoTextLabel.Frame;
			_infoTextScrollView.ContentSize = frame.Size;
			frame.X = View.Bounds.Width / 2;
			frame.Y = 0;
			frame.Height = View.Bounds.Height - 80;
			_infoTextScrollView.Frame = frame;
			var logoFrame = _logo.Frame;
			logoFrame.Height = View.Bounds.Height - _phoneButton.Bounds.Height - 80;
			logoFrame.X =40;
			_logo.Frame = logoFrame;


		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			if (fromInterfaceOrientation == UIInterfaceOrientation.Portrait || fromInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown)
			{
				InitLandscapeLayout();
			} else{
				_infoTextScrollView.Frame = _portaitInfoRect;
				var rect = _portaitInfoRect;
				rect.X = 0;
				rect.Y = 0;
				_infoTextLabel.Frame = rect;
				_logo.Frame = _portaitLabelRect;
			}

		}
		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
			if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
			{
				InitLandscapeLayout();
			}
		}
		void InitMailController()
		{
			_mailController = new MFMailComposeViewController();
		    
			_mailController.SetToRecipients(new [] {
				_email
			});

			_mailController.SetSubject(TextBundle.EmailSubject);
			_mailController.SetMessageBody(TextBundle.EmailBody, false);
			_mailController.Finished += HandleFinishedMail;
		}

		void HandleFinishedMail (object sender, MFComposeResultEventArgs e)
		{
		 	e.Controller.DismissViewController(true,null); 
			InitMailController();
		}

		void HandleTouchMailButton (object sender, EventArgs e)
		{
			PresentViewController (_mailController, true,null);
		}

		void HandleTouchPhoneButton (object sender, EventArgs e)
		{
			var phoneTo = NSUrl.FromString("tel:" + _phoneNumber);

			if (UIApplication.SharedApplication.CanOpenUrl(phoneTo)) {
				UIApplication.SharedApplication.OpenUrl(phoneTo);
			} else {
				new UIAlertView(TextBundle.Errors.Error, TextBundle.Errors.PhoneAccessError, null, TextBundle.AlertButton, null).Show();
			}

		
		}

	}

}

