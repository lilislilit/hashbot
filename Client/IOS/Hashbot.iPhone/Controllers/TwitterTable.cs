using System;
using Hashbot.Logic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
namespace Hashbot.IPhone
{
	public class TwitterTable : UITableViewSource
	{
		TwitterMessage[] tableItems;
		string cellIdentifier = "TableCell";
		public event Action<TwitterMessage> RowSelectedEvent; 

		public TwitterTable(TwitterMessage[] items)
		{
			tableItems = items;
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			TwitterTableCell cell = tableView.DequeueReusableCell((NSString)cellIdentifier) as TwitterTableCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new TwitterTableCell((NSString)cellIdentifier);
			var rowDate = tableItems[indexPath.Row].CreatedAt;
			var dateLabel = (rowDate - DateTime.Now).Hours > 24 ? rowDate.ToString() : String.Format("{0:G} часов", Math.Abs((rowDate - DateTime.Now).Hours)); 
			cell.UpdateCell(tableItems[indexPath.Row].TwitterUser.Name, tableItems[indexPath.Row].Text, UIImage.FromFile("ios/Main/avatar.png"), dateLabel);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{

			var tweet =new TweetController (tableItems[indexPath.Row]);
			RowSelectedEvent(tableItems[indexPath.Row]);
			tableView.DeselectRow(indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 70; 
		}
	}
}


