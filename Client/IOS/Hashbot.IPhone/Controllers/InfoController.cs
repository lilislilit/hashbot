
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
		private string _buttonBackground;
		private string _buttonPressedBackground;
		private UIEdgeInsets _buttonInsets;


		public InfoController() : base()
		{
			_phoneNumber = "78123093879";
			_email = "hello24@touchin.ru";
			_buttonBackground = "ios/Info/button.png";
			_buttonPressedBackground = "ios/Info/button_pressed.png";
			_buttonInsets = new UIEdgeInsets(0,0,9,0);

			Title = "Инфо";
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

			InitLayout();

			InitData();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void InitLayout()
		{

			View.BackgroundColor = UIColor.White;
			_logo = new UIImageView(UIImage.FromFile("ios/Info/logo.png"));
			_logo.Frame = new RectangleF(View.Bounds.Width/2-_logo.Frame.Width/2, 20, _logo.Image.Size.Width, _logo.Image.Size.Height);
			Add(_logo);

			var frame = new RectangleF(20, _logo.Frame.Bottom-40, View.Frame.Width - 20, View.Frame.Height - _logo.Frame.Height-80);
			_infoTextLabel = new UILabel(frame);
			_infoTextLabel.BackgroundColor = UIColor.Clear;
			_infoTextLabel.Font = UIFont.FromName("Helvetica", 11);
			_infoTextLabel.TextColor = UIColor.FromRGB(65, 65, 65);
			_infoTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_infoTextLabel.Lines = 0;
			Add(_infoTextLabel);

			_phoneButton = new UIButton(UIButtonType.Custom);
			_phoneButton.SetBackground(_buttonBackground, 11);
			_phoneButton.SetSelectedBackground(_buttonPressedBackground, 11);
			_phoneButton.SetImage(UIImage.FromFile("ios/Info/icon_phone.png"), UIControlState.Normal);
			_phoneButton.ImageEdgeInsets = _buttonInsets;
			_phoneButton.Frame = new RectangleF(View.Bounds.X + 20, _infoTextLabel.Frame.Bottom, 120, 40);
			_phoneButton.TouchUpInside += HandleTouchPhoneButton;
			Add(_phoneButton);
		
			_mailButton = new UIButton(UIButtonType.Custom);
			_mailButton.SetBackground(_buttonBackground,11);
			_mailButton.SetSelectedBackground(_buttonPressedBackground,11);
			_mailButton.SetImage(UIImage.FromFile("ios/Info/icon_mail.png"), UIControlState.Normal);
			_mailButton.ImageEdgeInsets = _buttonInsets;
			_mailButton.Frame = new RectangleF(View.Bounds.Width -150, _infoTextLabel.Frame.Bottom, 120, 40);
			_mailButton.TouchUpInside += HandleTouchMailButton; 

			Add(_mailButton);	 

			InitMailController();		
		}

		void InitMailController()
		{


			_mailController = new MFMailComposeViewController();
		    
			_mailController.SetToRecipients(new string[] {
				_email
			});
			_mailController.SetSubject("Заказать приложение");
			_mailController.SetMessageBody("Текст Заказа", false);
			_mailController.Finished += HandleFinishedMail;
		}

		void HandleFinishedMail (object sender, MFComposeResultEventArgs e)
		{
		  e.Controller.DismissViewController(true,null); 
			InitMailController();

		}

		void HandleTouchMailButton (object sender, EventArgs e)
		{
			this.PresentViewController (_mailController, true,null);
		}

		void HandleTouchPhoneButton (object sender, EventArgs e)
		{

			var phoneTo = NSUrl.FromString("tel:"+_phoneNumber);
			if (UIApplication.SharedApplication.CanOpenUrl(phoneTo)) {
				UIApplication.SharedApplication.OpenUrl(phoneTo);
			} else {
				new UIAlertView("Ошибка", "Не можем позвонить с данного устройства", null, "Ок", null).Show();
			}

		
		}
		private void InitData()
		{

			_infoTextLabel.Text = System.IO.File.ReadAllText("info.txt");
		}

	}

}

