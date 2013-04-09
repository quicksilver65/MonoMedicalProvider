// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CommonLogic;
using System.Collections.Generic;
using System.Drawing;


namespace MonoMedicalIOS
{
	public partial class MedProviderTableController : UIViewController, AppModelInterface
	{
		//private ITableViewSelectedProvider tableSource;
		private ProviderTableSource dataSource;
		private LoadingOverlay loadingOverlay;

		#region AppModelInterface implementation
		public CommonLogic.ViewModel AppModel {
			get;
			set;
		}
		
		#endregion

		public MedProviderTableController (IntPtr handle) : base (handle)
		{
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if(AppModel!=null){
				loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
				View.Add (loadingOverlay);

				dataSource = new ProviderTableSource(){ ProviderList = new MedicalProvider[0] };
				dataSource.ProviderSelected+=(mp)=>{
					AppModel.SelectedProvider=mp;
					var actionSheet = new UIActionSheet (mp.LastName);                      
					actionSheet.AddButton ("Cancel");
					actionSheet.AddButton ("Add Favorites");
					actionSheet.AddButton ("Call");
					actionSheet.AddButton ("Directions");
					actionSheet.CancelButtonIndex = 0; 
					actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
						switch (b.ButtonIndex) {
						case 0:
							break;
						case 1:
							AppModel.AddFavorite(mp);
							break;
						case 2:
							UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + mp.PhoneNumber));
							break;
						case 3:
							var directions = (DirectionsController)this.Storyboard.InstantiateViewController("Directions");
							directions.AppModel=this.AppModel;
							this.PresentViewController(directions,true,null);
							break;
						}
					};
					actionSheet.ShowInView (View);
				};
				this.MedProviderTable.Source= dataSource;
				AppModel.ProviderSearchCompleted+=(e,a)=>{
					this.BeginInvokeOnMainThread(delegate {
						dataSource.ProviderList = AppModel.ProviderResults.ToArray();
						this.MedProviderTable.ReloadData();
						loadingOverlay.Hide ();
					});
				};
				AppModel.SelectedProvider=null;
				AppModel.SearchForProviders();
			}
		}


		partial void CancelItem (MonoTouch.Foundation.NSObject sender){
			DismissViewController(true,null);
		}
	}

}
