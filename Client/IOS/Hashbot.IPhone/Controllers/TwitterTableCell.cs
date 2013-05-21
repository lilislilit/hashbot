using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Hashbot.Logic;

namespace Hashbot.IPhone
{
	public class TwitterTableCell : UITableViewCell
	{

		public const string CellId = "TwitterTableCell";
		private UILabel _headingLabel, _subheadingLabel, _dateLabel;
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
			_headingLabel = new UILabel() {
				Font = UIFont.FromName("Helvetica",24f),
				TextColor = UIColor.FromRGB (0, 0, 0),
				BackgroundColor = UIColor.Clear
			};
			_subheadingLabel = new UILabel() {
				Font = UIFont.FromName("Helvetica", 16f),
				TextColor = UIColor.FromRGB (137, 137, 137),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};
			_dateLabel = new UILabel() {
				Font = UIFont.FromName("Helvetica", 16f),
				TextColor = UIColor.FromRGB (137, 137, 137),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};

			_clippingImage = UIImage.FromFile("ios/Main/mask_avatar_mini.png");
		
			ContentView.Add(_headingLabel);
			ContentView.Add(_subheadingLabel);
			ContentView.Add(_imageView);
			ContentView.Add(_dateLabel);
		}

		public void UpdateCell(string caption, string subtitle, UIImage image, string Date)
		{
			_imageView.Image = image;
			_headingLabel.Text = caption;
			_subheadingLabel.Text = subtitle;
			_dateLabel.Text = Date;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			_imageView.Frame = new RectangleF(5, 5, _clippingImage.Size.Width + 2, _clippingImage.Size.Height + 2);

			_headingLabel.Frame = new RectangleF(60, 10, ContentView.Frame.Width - 63, 25);
			_subheadingLabel.Frame = new RectangleF(60, 40, ContentView.Frame.Width - 63, 20);
			_dateLabel.Frame = new RectangleF(ContentView.Frame.Width-100, 10, 100, 15);
		}

		public void InitWith(TwitterMessage twitt)
		{
			var preclippedAvatar = UIImage.FromFile(twitt.TwitterUser.ImageUri);
			var rowDate = twitt.CreatedAt;
			var clippedImage = preclippedAvatar.GetMaskedAvatar(_clippingImage);
			
			var dateLabel = (rowDate - DateTime.Now).Hours > 24 ? rowDate.ToString() : String.Format("{0:G} часов", Math.Abs((rowDate - DateTime.Now).Hours));
			UpdateCell(twitt.TwitterUser.Name, twitt.Text, clippedImage, dateLabel);
		}
	}
}

