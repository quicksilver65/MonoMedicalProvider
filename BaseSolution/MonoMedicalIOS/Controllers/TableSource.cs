using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using CommonLogic;

namespace MonoMedicalIOS
{
	public interface ITableViewSelectedItem{
		string SelectedText{set;get;}
	}
	public interface ITableViewSelectedProvider{
		MedicalProvider SelectedProvider{set;get;}
	}

	public class TableSource : UITableViewSource, ITableViewSelectedItem {
		#region ITableViewSelectedItem implementation
		public string SelectedText {
			get;
			set;
		}
		#endregion
		
		protected string[] tableItems;
		protected string cellIdentifier = "TableCell";
		public TableSource (string[] items)
		{
			tableItems = items;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Length;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			cell.TextLabel.Text = tableItems[indexPath.Row];
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.SelectedText=tableItems[indexPath.Row];
		}
	}
	public class ProviderTableSource : UITableViewSource, ITableViewSelectedProvider{
		#region ITableViewSelectedProvider implementation
		public MedicalProvider SelectedProvider {
			get; 
			set;
		}
		#endregion

		protected MedicalProvider[] tableItems;
		protected string cellIdentifier = "TableCell";

		public ProviderTableSource (MedicalProvider[] tableItems)
		{
			this.tableItems=tableItems;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Length;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			cell.TextLabel.Text = tableItems[indexPath.Row].FirstName;
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.SelectedProvider=tableItems[indexPath.Row];
		}

	}
}

