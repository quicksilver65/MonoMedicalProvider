// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("MedProviderCell")]
	partial class MedProviderCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblProviderName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CityStateZip { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblProviderName != null) {
				lblProviderName.Dispose ();
				lblProviderName = null;
			}

			if (lblAddress != null) {
				lblAddress.Dispose ();
				lblAddress = null;
			}

			if (CityStateZip != null) {
				CityStateZip.Dispose ();
				CityStateZip = null;
			}
		}
	}
}
