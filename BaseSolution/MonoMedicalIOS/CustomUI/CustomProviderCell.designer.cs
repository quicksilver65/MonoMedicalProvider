// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("CustomProviderCell")]
	partial class CustomProviderCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel ProviderName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ProviderAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ProviderCityState { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProviderName != null) {
				ProviderName.Dispose ();
				ProviderName = null;
			}

			if (ProviderAddress != null) {
				ProviderAddress.Dispose ();
				ProviderAddress = null;
			}

			if (ProviderCityState != null) {
				ProviderCityState.Dispose ();
				ProviderCityState = null;
			}
		}
	}
}
