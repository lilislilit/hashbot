using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Hashbot.Logic;
using Hashbot.IPhone.ImageExtensions;

namespace Hashbot.IPhone
{
	public class TwitterTableCell : UITableViewCell
	{

		public const string CellId = "TwitterTableCell";
		private UILabel _userLabel, _tweetLabel, _dateLabel;
		private UIImageView _imageView;
		private UIImageView _backImageView;
		private UIImageView _backPressedImageView;
		private UIImage _clippingImage;

		public TwitterTableCell() : base (UITableViewCellStyle.Default, CellId)
		{
		

			ContentView.BackgroundColor = UIColor.Clear;
			_backImageView = new UIImageView(UIImage.FromFile("ios/Main/table.png"));
			var backgroundRect = new RectangleF(0, 0, ContentView.Frame.Width, Frame.Height - 2);
			_backImageView.Frame = backgroundRect;
			_backPressedImageView = new UIImageView(UIImage.FromFile("ios/Main/table_pressed.png"));
			_backPressedImageView.Frame = backgroundRect;


			BackgroundView = _backImageView;
			SelectedBackgroundView = _backPressedImageView;

			_imageView = new UIImageView();
			_userLabel = new UILabel() {
				Font = Fonts.HelveticaBold(16),
				TextColor = UIColor.FromRGB (0, 0, 0),
				BackgroundColor = UIColor.Clear
			};
			_tweetLabel = new UILabel() {
				Font = Fonts.Helvetica(14),
				TextColor = UIColor.FromRGB (137, 137, 137),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear
			};
			_dateLabel = new UILabel() {
				Font = Fonts.Helvetica(14),
				TextColor = UIColor.FromRGB (137, 137, 137),
				TextAlignment = UITextAlignment.Right,
				BackgroundColor = UIColor.Clear
			};

			_clippingImage = UIImage.FromFile("ios/Main/mask_avatar_mini.png");
		
			ContentView.Add(_userLabel);
			ContentView.Add(_tweetLabel);
			ContentView.Add(_imageView);
			ContentView.Add(_dateLabel);
		}

		public void UpdateCell(string caption, string subtitle, UIImage image, string Date)
		{
			_imageView.Image = image;
			_userLabel.Text = caption;
			_tweetLabel.Text = subtitle;
			_dateLabel.Text = Date;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			_imageView.Frame = new RectangleF(5, 5, _clippingImage.Size.Width + 2, _clippingImage.Size.Height + 2);

			_userLabel.Frame = new RectangleF(60, 10, ContentView.Frame.Width - _dateLabel.Frame.Width, 15);
			_tweetLabel.Frame = new RectangleF(60, _userLabel.Frame.Bottom + 10, ContentView.Frame.Width - 63, 20);
			var dateSize = ((NSString)_dateLabel.Text).StringSize(Fonts.Helvetica(14));
			_dateLabel.Frame = new RectangleF(ContentView.Frame.Width-dateSize.Width-2, 10, dateSize.Width, 15);
		}

		public void InitWith(TwitterMessage twitt)
		{
			var preclippedAvatar = UIImage.FromFile(twitt.TwitterUser.ImageUri);
			var rowDate = twitt.CreatedAt;
			var clippedImage = preclippedAvatar.GetMaskedAvatar(_clippingImage);
			var timeDifference = DateTime.Now - rowDate;
			var dateLabel = PrepareDate(timeDifference, rowDate);
			UpdateCell(twitt.TwitterUser.Name, twitt.Text, clippedImage, dateLabel);
		}

		private string PrepareDate(TimeSpan date, DateTime origDate)
		{

			if (date.Seconds <=0 && date.Minutes <= 0 && date.Hours == 0 && date.Days == 0)
				{

					return "сейчас";
						
				}
			else if (date.Seconds > 0 && date.Minutes == 0 && date.Hours == 0 && date.Days == 0)
				{
					return date.Seconds + " c.";

				} 
			else if (date.Minutes > 0 && date.Hours == 0 && date.Days == 0)
				{

					return date.Minutes + " м.";
						
				} 
			else if (date.Hours > 0 && date.Days == 0)
				{

					return date.Hours + " ч.";

				}
			else 
				{

					return origDate.ToString("dd.MM.yyyy");

				}


		}
	}
}

