using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Hashbot.IPhone
{
	public class TwitterTableCell : UITableViewCell  {
			UILabel headingLabel, subheadingLabel;
			UIImageView imageView;
		public TwitterTableCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
			{
				SelectionStyle = UITableViewCellSelectionStyle.Gray;
				ContentView.BackgroundColor = UIColor.FromRGB (218, 255, 127);
				imageView = new UIImageView();
				headingLabel = new UILabel () {
					Font = UIFont.FromName("Cochin-BoldItalic", 22f),
					TextColor = UIColor.FromRGB (127, 51, 0),
					BackgroundColor = UIColor.Clear
				};
				subheadingLabel = new UILabel () {
					Font = UIFont.FromName("AmericanTypewriter", 12f),
					TextColor = UIColor.FromRGB (38, 127, 0),
					TextAlignment = UITextAlignment.Center,
					BackgroundColor = UIColor.Clear
				};
				ContentView.Add (headingLabel);
				ContentView.Add (subheadingLabel);
				ContentView.Add (imageView);
			}
			public void UpdateCell (string caption, string subtitle, UIImage image)
			{
				imageView.Image = image;
				headingLabel.Text = caption;
				subheadingLabel.Text = subtitle;
			}
			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				imageView.Frame = new RectangleF(ContentView.Bounds.Width - 63, 5, 33, 33);
				headingLabel.Frame = new RectangleF(5, 4, ContentView.Bounds.Width - 63, 25);
				subheadingLabel.Frame = new RectangleF(100, 18, 100, 20);
			}

	}

}

