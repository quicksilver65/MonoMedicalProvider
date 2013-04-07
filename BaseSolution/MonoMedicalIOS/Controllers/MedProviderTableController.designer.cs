// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("MedProviderTableController")]
	partial class MedProviderTableController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView MedProviderTable { get; set; }

		[Action ("CancelItem:")]
		partial void CancelItem (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (MedProviderTable != null) {
				MedProviderTable.Dispose ();
				MedProviderTable = null;
			}
		}
	}
}
