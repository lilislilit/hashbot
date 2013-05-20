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

		public TwitterTableCell() : base (UITableViewCellStyle.Default, CellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			ContentView.BackgroundColor = UIColor.FromRGB(235, 235, 235);
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
			_imageView.Frame = new RectangleF(0, 5, 33, 33);
			_headingLabel.Frame = new RectangleF(40, 10, ContentView.Bounds.Width - 63, 25);
			_subheadingLabel.Frame = new RectangleF(40, 40, ContentView.Bounds.Width - 63, 20);
			_dateLabel.Frame = new RectangleF(ContentView.Bounds.Width-100, 10, 100, 15);
		}

		public void InitWith(TwitterMessage twitt)
		{
			var rowDate = twitt.CreatedAt;
			var dateLabel = (rowDate - DateTime.Now).Hours > 24 ? rowDate.ToString() : String.Format("{0:G} часов", Math.Abs((rowDate - DateTime.Now).Hours));
			UpdateCell(twitt.TwitterUser.Name, twitt.Text, UIImage.FromFile(twitt.TwitterUser.ImageUri), dateLabel);
		}
	}
}

