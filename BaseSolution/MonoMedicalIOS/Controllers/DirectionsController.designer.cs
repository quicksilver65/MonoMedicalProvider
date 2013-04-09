// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace MonoMedicalIOS
{
	[Register ("DirectionsController")]
	partial class DirectionsController
	{
		[Outlet]
		MonoTouch.MapKit.MKMapView map { get; set; }

		[Action ("CloseMap:")]
		partial void CloseMap (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
