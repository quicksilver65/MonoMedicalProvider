// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("CitiesTableController")]
	partial class CitiesTableController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView citiesTable { get; set; }

		[Action ("CancelCities:")]
		partial void CancelCities (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (citiesTable != null) {
				citiesTable.Dispose ();
				citiesTable = null;
			}
		}
	}
}
