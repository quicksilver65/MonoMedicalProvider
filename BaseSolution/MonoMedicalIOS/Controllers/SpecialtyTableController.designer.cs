// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("SpecialtyTableController")]
	partial class SpecialtyTableController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView specialtiesTable { get; set; }

		[Action ("SelectItem:")]
		partial void SelectItem (MonoTouch.Foundation.NSObject sender);

		[Action ("CancelItem:")]
		partial void CancelItem (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (specialtiesTable != null) {
				specialtiesTable.Dispose ();
				specialtiesTable = null;
			}
		}
	}
}
