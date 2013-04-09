using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using CommonLogic;
using MonoTouch.ObjCRuntime;

namespace MonoMedicalIOS
{
	public interface ITableViewSelectedItem
	{
		string SelectedText{ set; get; }
	}

	public delegate void ProviderSelectedHandler(MedicalProvider mp);

	public class TableSource : UITableViewSource, ITableViewSelectedItem
	{
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
			cell.TextLabel.Text = tableItems [indexPath.Row];
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.SelectedText = tableItems [indexPath.Row];
		}
	}

	public class ProviderTableSource : UITableViewSource
	{
		public event ProviderSelectedHandler ProviderSelected;

		public MedicalProvider[] ProviderList {
			get;
			set;
		}

		//protected MedicalProvider[] tableItems;
		protected string cellIdentifier = "CustomProvider";


		public override int RowsInSection (UITableView tableview, int section)
		{
			return ProviderList.Length;
		}

		public override UITableViewCell  GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory

			var cell = tableView.DequeueReusableCell (cellIdentifier) as CustomProviderCell;

			if (cell == null) {
				cell = new CustomProviderCell (cellIdentifier);
			}
			MedicalProvider obj = ProviderList [indexPath.Row];
			cell.BindFields (obj);
			//cell.lbl.Text = tableItems[indexPath.Row].FirstName;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var selectedProvider = ProviderList [indexPath.Row];
			if(ProviderSelected!=null){
				ProviderSelected(selectedProvider);
			}

		}

	}
}

