using System;
using System.Collections.Generic;

namespace CommonLogic
{
	public class ViewModel{
		
		public event EventHandler SearchCompleted;
		public event EventHandler ResourcesLoaded;
		
		private FileStorage fs;
		private NetworkStorage ns;

		private bool LoadingCompleted{
			get{return (Specialties!=null && Specialties.Count!=0 && Cities!=null && Cities.Count!=0);}
		}

		public List<MedicalProvider> ProviderResults {
			get;
			set;
		}
		public List<MedicalProvider> Favorites {
			get;
			set;
		}
		public List<string> Cities {
			get;
			set;
		}
		public List<string> Specialties {
			get;
			set;
		}
		public List<SearchCriteria> SearchParameters{
			get;
			set;
		}
		
		public ViewModel ()
		{
			this.ns = new NetworkStorage();
			this.fs = new FileStorage();
			ns.MedicalCitiesRetrievedCompleted+=(collection)=>
			{
	
				Cities = collection;
				this.fs.SaveCities(Cities);
				if(LoadingCompleted)
					if(ResourcesLoaded!=null)
						ResourcesLoaded(null,null);

			};
			ns.MedicalSpecialtiesRetrievedCompleted+=(collection)=>
			{
				Specialties = collection;
				this.fs.SaveSpecialties(Specialties);
				Notify();
			};
			ns.MedicalProviderRetrievedCompleted+=(collection)=>
			{
				ProviderResults = collection;
				Notify();
			};

		}
		public void InitResources(){
			this.Favorites = fs.LoadFavorites();
			
			this.Cities = fs.LoadCities();
			if(this.Cities.Count==0)
			{
				this.ns.GetCities();
			}
			this.Specialties=fs.LoadSpecialties();
			if(this.Specialties.Count==0)
			{
				this.ns.GetSpecialties();
			}
			Notify();
		}
		private void Notify(){
			if(LoadingCompleted)
				if(ResourcesLoaded!=null)
					ResourcesLoaded(null,null);
		}
		
	}
}

