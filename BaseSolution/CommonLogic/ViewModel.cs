using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CommonLogic
{


	public class ViewModel:INotifyPropertyChanged
	{
		public event EventHandler ProviderSearchCompleted;
		public event EventHandler FavoritesUpdated;
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

	
		private FileStorage fs;
		private NetworkStorage ns;
		private bool hasLoadedResources;
		private ObservableCollection<MedicalProvider> providerResults;
		private ObservableCollection<MedicalProvider> favorites;
		private ObservableCollection<string> cities;
		private ObservableCollection<string> specialties;

		public ObservableCollection<string> Specialties {
			get {
				return specialties;
			}
			set {
				specialties = value;
			}
		}
		public ObservableCollection<string> Cities {
			get {
				return cities;
			}
			set {
				cities = value;
			}
		}

		public ObservableCollection<MedicalProvider> Favorites {
			get {
				return favorites;
			}
			set {
				favorites = value;
			}
		}

		public ObservableCollection<MedicalProvider> ProviderResults {
			get {
				return providerResults;
			}
			set {
				providerResults = value;
			}
		}

		public bool HasLoadedResources {
			get {
				return hasLoadedResources;
			}
			set {
				hasLoadedResources = value; Notify("HasLoadedResources");
			}
		}

	
		public SearchCriteria ProviderSearchCriteria {
			get;
			set;
		}
		
		public ViewModel ()
		{
			this.ns = new NetworkStorage ();
			this.fs = new FileStorage ();
			ProviderResults = new ObservableCollection<MedicalProvider> ();
			Favorites = new ObservableCollection<CommonLogic.MedicalProvider> ();
			Cities = new ObservableCollection<string> ();
			Specialties = new ObservableCollection<string> ();
			ProviderSearchCriteria = new SearchCriteria();

			ns.MedicalCitiesRetrievedCompleted += (collection) =>
			{
				Cities.Clear();
				collection.ForEach ((x)=>Cities.Add(x));
				this.fs.SaveCities (Cities.ToList());
				if (Cities.Count != 0 && Specialties.Count != 0 && HasLoadedResources==false) {
					HasLoadedResources=true;
				}

			};
			ns.MedicalSpecialtiesRetrievedCompleted += (collection) =>
			{
				Specialties.Clear();
				collection.ForEach ((x)=>Specialties.Add(x));
				this.fs.SaveSpecialties (Specialties.ToList());
				if (Cities.Count != 0 && Specialties.Count != 0 && HasLoadedResources==false) {
					HasLoadedResources=true;
				}
			};
			ns.MedicalProviderRetrievedCompleted += (collection) =>
			{
				ProviderResults.Clear();
				collection.ForEach ((x)=>ProviderResults.Add(x));
				if(ProviderSearchCompleted!=null)
				{
					ProviderSearchCompleted(null,null);
				}
			};

		}

		public void SearchForProviders(){
			ns.GetMedicalProviders(this.ProviderSearchCriteria);
		}

		public void AddFavorite(MedicalProvider provider){

			if(!Favorites.Any(x=>x.id==provider.id)){
				Favorites.Add(provider);
				this.fs.SaveFavorites(Favorites.ToList());
				if(FavoritesUpdated!=null)
					FavoritesUpdated(null,null);
			}
		}
		public void RemoveFavorite(MedicalProvider provider){
			if(Favorites.Any(x=>x.id==provider.id)){
				Favorites.Remove(provider);
				this.fs.SaveFavorites(Favorites.ToList());
				if(FavoritesUpdated!=null)
					FavoritesUpdated(null,null);
			}
		}

		public void InitResources ()
		{
			fs.LoadCities().ForEach((x)=>Cities.Add (x));
			fs.LoadFavorites().ForEach((x)=>Favorites.Add (x));
			fs.LoadSpecialties().ForEach((x)=>Specialties.Add (x));
			if(FavoritesUpdated!=null)
				FavoritesUpdated(null,null);

			if (Cities.Count != 0 && Specialties.Count != 0 && HasLoadedResources==false) {
				HasLoadedResources=true;
			} else {
				if (this.Cities.Count == 0) {
					this.ns.GetCities ();
				}
				if (this.Specialties.Count == 0) {
					this.ns.GetSpecialties ();
				}
			}
		}

		private void Notify(string propName){
			if(PropertyChanged!=null)
				PropertyChanged(this,new PropertyChangedEventArgs(propName));
		}
	
	}

	public static class ObservableExtension{
		public static List<T> ToList<T>(this ObservableCollection<T> collection){
			List<T> list = new List<T>();
			foreach(var obj in collection)
				list.Add(obj);
			return list;
		}
		public static T[] ToArray<T>(this ObservableCollection<T> collection){
			List<T> list = new List<T>();
			foreach(var obj in collection)
				list.Add(obj);
			return list.ToArray();
		}
	}
}

