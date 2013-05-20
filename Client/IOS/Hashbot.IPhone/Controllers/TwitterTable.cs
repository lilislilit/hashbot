using System;
using Hashbot.Logic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.IO;
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
		public void AddTweets(TwitterMessage[] items)
		{
			var temp = new TwitterMessage[tableItems.Length+items.Length];
			tableItems.CopyTo(temp, 0);
			items.CopyTo(temp, tableItems.Length);
			tableItems = temp;

		    
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(TwitterTableCell.CellId) as TwitterTableCell;
			// if there are no cells to reuse, create a new one
			cell = cell ?? new TwitterTableCell();
			cell.InitWith(tableItems[indexPath.Row]);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			RowSelectedEvent(tableItems[indexPath.Row]);
			tableView.DeselectRow(indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 70; 
		}
	}
}


