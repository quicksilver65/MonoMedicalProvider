using System;
using System.Collections.Generic;

namespace CommonLogic
{
	public class ViewModel{
		
		public event EventHandler SearchCompleted;
		
		private FileStorage fs;
		private NetworkStorage ns;
		
		
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
			};
			ns.MedicalSpecialtiesRetrievedCompleted+=(collection)=>
			{
				Specialties = collection;
				this.fs.SaveSpecialties(Specialties);
			};
			ns.MedicalProviderRetrievedCompleted+=(collection)=>
			{
				ProviderResults = collection;
				if(SearchCompleted!=null)
					SearchCompleted(null,null);
			};
			
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
			
			
		}
		
	}
}

