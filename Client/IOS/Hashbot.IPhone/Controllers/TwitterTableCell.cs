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

			UILabel headingLabel, subheadingLabel,dateLabel;
			UIImageView imageView;
		public TwitterTableCell () : base (UITableViewCellStyle.Default, CellId)
			{
				SelectionStyle = UITableViewCellSelectionStyle.Gray;
				ContentView.BackgroundColor = UIColor.FromRGB (235, 235, 235);
				imageView = new UIImageView();
				headingLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica",24f),
					TextColor = UIColor.FromRGB (0, 0, 0),
					BackgroundColor = UIColor.Clear
				};
				subheadingLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica", 16f),
					TextColor = UIColor.FromRGB (137, 137, 137),
					TextAlignment = UITextAlignment.Center,
					BackgroundColor = UIColor.Clear
				};
			    dateLabel = new UILabel () {
				Font = UIFont.FromName("Helvetica", 16f),
				TextColor = UIColor.FromRGB (137, 137, 137),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			    };
				ContentView.Add (headingLabel);
				ContentView.Add (subheadingLabel);
				ContentView.Add (imageView);
			    ContentView.Add(dateLabel);
			}

			public void UpdateCell (string caption, string subtitle, UIImage image,string Date)
			{
				imageView.Image = image;
				headingLabel.Text = caption;
				subheadingLabel.Text = subtitle;
			    dateLabel.Text = Date;
			}

			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				imageView.Frame = new RectangleF(0, 5, 33, 33);
				headingLabel.Frame = new RectangleF(40, 10, ContentView.Bounds.Width - 63, 25);
				subheadingLabel.Frame = new RectangleF(40,40, ContentView.Bounds.Width-63, 20);
			    dateLabel.Frame = new RectangleF(ContentView.Bounds.Width-100,10, 100, 15);
			}

		public void InitWith(TwitterMessage twitt)
		{
			var rowDate = twitt.CreatedAt;
			var dateLabel = (rowDate - DateTime.Now).Hours > 24 ? rowDate.ToString() : String.Format("{0:G} часов", Math.Abs((rowDate - DateTime.Now).Hours));
			UpdateCell(twitt.TwitterUser.Name,twitt.Text, UIImage.FromFile(twitt.TwitterUser.ImageUri), dateLabel);
		}
	}

}

