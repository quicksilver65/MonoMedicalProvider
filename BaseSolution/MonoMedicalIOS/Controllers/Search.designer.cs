// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("Search")]
	partial class Search
	{
		[Outlet]
		MonoTouch.UIKit.UITextField txtSpecialties { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtCities { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtSpecialties != null) {
				txtSpecialties.Dispose ();
				txtSpecialties = null;
			}

			if (txtCities != null) {
				txtCities.Dispose ();
				txtCities = null;
			}

			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}
		}
	}
}
