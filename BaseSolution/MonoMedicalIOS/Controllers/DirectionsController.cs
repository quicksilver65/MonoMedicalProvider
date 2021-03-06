// This file has been autogenerated from parsing an Objective-C header file added in Xcode.
using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;

namespace MonoMedicalIOS
{
	public partial class DirectionsController : UIViewController,AppModelInterface
	{

		private LoadingOverlay loadingOverlay;

		#region AppModelInterface implementation

		public CommonLogic.ViewModel AppModel {
			get;
			set;
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if (AppModel != null && AppModel.SelectedProvider != null) {

				loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
				View.Add (loadingOverlay);
				AppModel.CoordinatedUpdated += (e,a) => {
					this.BeginInvokeOnMainThread(delegate {
						loadingOverlay.Hide ();
						CLLocationCoordinate2D coords = new CLLocationCoordinate2D(AppModel.ProviderLocation.Latitude, AppModel.ProviderLocation.Longtitude);
						MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(5), MilesToLongitudeDegrees(5, coords.Latitude));
						map.Region = new MKCoordinateRegion(coords, span);
						var prompt = string.Format("Dr {0} {1}", AppModel.SelectedProvider.FirstName,AppModel.SelectedProvider.LastName);
						var annotation = new ProviderAnnotation (coords, prompt);
						map.AddAnnotation(annotation);
							 
					});
				};

				AppModel.GetProviderLocation ();

			}

		}

		partial void CloseMap (NSObject sender)
		{
			DismissViewController(true,null);
		}

		public DirectionsController (IntPtr handle) : base (handle)
		{
		}

		private double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0; // in miles
			double radiansToDegrees = 180.0/Math.PI;
			return (miles/earthRadius) * radiansToDegrees;
		}
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0; // in miles
			double degreesToRadians = Math.PI/180.0;
			double radiansToDegrees = 180.0/Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return (miles / radiusAtLatitude) * radiansToDegrees;
		}

	}
	internal class ProviderAnnotation:MKAnnotation{
		#region implemented abstract members of MKAnnotation

		private string title;

		public override CLLocationCoordinate2D Coordinate {
			get;
			set;
		}

		#endregion

		public override string Title {
			get {
				return title;
			}
		}

		public ProviderAnnotation (CLLocationCoordinate2D coordinate, string title )
		{
			this.Coordinate = coordinate;
			this.title = title;
		}
	}
}
