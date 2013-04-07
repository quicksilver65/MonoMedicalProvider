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
		MonoTouch.UIKit.UITextField txtCity { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtSpecialty { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtProviderName { get; set; }

		
		void ReleaseDesignerOutlets ()
		{
			if (txtCity != null) {
				txtCity.Dispose ();
				txtCity = null;
			}

			if (txtSpecialty != null) {
				txtSpecialty.Dispose ();
				txtSpecialty = null;
			}

			if (txtProviderName != null) {
				txtProviderName.Dispose ();
				txtProviderName = null;
			}
		}
	}
}
