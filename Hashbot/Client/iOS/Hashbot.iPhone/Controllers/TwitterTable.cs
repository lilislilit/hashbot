using System;
using Hashbot.Logic;
using MonoTouch.UIKit;
namespace Hashbot.IPhone
{
   public class TwitterTable : UITableViewSource {
			TwitterMessage[] tableItems;
			string cellIdentifier = "TableCell";
		    public TwitterTable (TwitterMessage[] items)
			{
				tableItems = items;
			}
			public override int RowsInSection (UITableView tableview, int section)
			{
				return tableItems.Length;
			}
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
				// if there are no cells to reuse, create a new one
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);

			cell.TextLabel.Text = tableItems[indexPath.Row].TwitterUser.Name;
			cell.DetailTextLabel.Text = tableItems[indexPath.Row].Text;
			cell.ImageView.Image = UIImage.FromFile("ios/Main/avatar.png");
			    return cell;
			}
		}
}


