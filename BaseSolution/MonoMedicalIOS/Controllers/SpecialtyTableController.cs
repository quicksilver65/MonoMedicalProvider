// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CommonLogic;

namespace MonoMedicalIOS
{
	public partial class SpecialtyTableController : UIViewController,AppModelInterface
	{
		private ITableViewSelectedItem tableSource;
		
		#region AppModelInterface implementation
		public CommonLogic.ViewModel AppModel {
			get;
			set;
		}
		
		#endregion

		public SpecialtyTableController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if(AppModel!=null){
				this.specialtiesTable.Source= new TableSource(AppModel.Specialties.ToArray());
				tableSource =(ITableViewSelectedItem) this.specialtiesTable.Source;
			}
		}
		partial void SelectItem (MonoTouch.Foundation.NSObject sender){
			AppModel.ProviderSearchCriteria.Specialty=tableSource.SelectedText;
			DismissViewController(true,null);
		}
		partial void CancelItem (MonoTouch.Foundation.NSObject sender){
			DismissViewController(true,null);
		}

	}
}
