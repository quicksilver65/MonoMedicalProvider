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

		public ViewModel AppModel {
			get;
			set;
		}
		private MedicalProvider[] ProviderList {
			get{return this.AppModel.ProviderResults.ToArray();}
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

			var actionSheet = new UIActionSheet (selectedProvider.LastName);                      
			actionSheet.AddButton ("Cancel");
			actionSheet.AddButton ("Add Favorites");
			actionSheet.AddButton ("Call");
			actionSheet.AddButton ("Directions");
			actionSheet.CancelButtonIndex = 0; 
			actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
				switch (b.ButtonIndex) {
					case 0:
						break;
					case 1:
						AppModel.AddFavorite(selectedProvider);
						break;
					case 2:
						break;
					case 3:
						break;
				}
			};
			actionSheet.ShowInView (tableView);
		}

	}
}

