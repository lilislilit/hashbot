using System;
using Hashbot.Logic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.IO;
namespace Hashbot.IPhone
{
	public class TwitterSource : UITableViewSource
	{
		private TwitterMessage[] _tableItems;
		private const int _rowHeight = 70;

		public event Action<TwitterMessage> RowSelectedEvent; 

		public TwitterSource(TwitterMessage[] items)
		{
			_tableItems = items;
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return _tableItems.Length;
		}
		public void AddTweets(TwitterMessage[] items)
		{
			var temp = new TwitterMessage[_tableItems.Length+items.Length];
			_tableItems.CopyTo(temp, 0);
			items.CopyTo(temp, _tableItems.Length);
			_tableItems = temp;

		    
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(TwitterTableCell.CellId) as TwitterTableCell;
			// if there are no cells to reuse, create a new one
			cell = cell ?? new TwitterTableCell();
			cell.InitWith(_tableItems[indexPath.Row]);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			RowSelectedEvent(_tableItems[indexPath.Row]);
			tableView.DeselectRow(indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}

		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return _rowHeight; 
		}
	}
}


