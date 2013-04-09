// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CommonLogic;

namespace MonoMedicalIOS
{
	public partial class Favorites : UIViewController,AppModelInterface
	{
		private ProviderTableSource dataSource;

		public ViewModel AppModel {
			get;
			set;
		}
		public Favorites (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			dataSource = new ProviderTableSource(){ ProviderList = AppModel.Favorites.ToArray() };
			dataSource.ProviderSelected+=(mp)=>{
				AppModel.SelectedProvider=mp;
				var actionSheet = new UIActionSheet (mp.LastName);                      
				actionSheet.AddButton ("Cancel");
				actionSheet.AddButton ("Delete Favorite");
				actionSheet.AddButton ("Call");
				actionSheet.AddButton ("Directions");
				actionSheet.CancelButtonIndex = 0; 
				actionSheet.DestructiveButtonIndex=1;
				actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
					switch (b.ButtonIndex) {
					case 0:
						break;
					case 1:
						AppModel.RemoveFavorite(mp);
						break;
					case 2:
						UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + mp.PhoneNumber));
						break;
					case 3:
						var directions = (DirectionsController)this.Storyboard.InstantiateViewController("Directions");
						directions.AppModel = this.AppModel;
						this.PresentViewController(directions,true,null);
						break;
					}
				};
				actionSheet.ShowInView (View);
			};
			this.MedProviderTable.Source= dataSource;

			AppModel.FavoritesUpdated+=(e,a)=>{
				dataSource.ProviderList = AppModel.Favorites.ToArray();
				this.MedProviderTable.ReloadData();
			};
		}

	}
}
