
using System;
using NUnit.Framework;
using CommonLogic;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestIOS
{
	[TestFixture]
	public class UnitTest
	{
		[Test]
		public void TestViewModel(){
			var vm = new ViewModel();
			vm.ResourcesLoaded+=(e,a)=>{
				foreach(var str in vm.Cities)
					Debug.WriteLine(str);
				foreach(var str in vm.Specialties)
					Debug.WriteLine(str);
				Assert.AreNotEqual(0,vm.Specialties.Count);
				Assert.AreNotEqual(0,vm.Cities.Count);
				Assert.AreNotEqual(0,vm.Favorites.Count);	
			};
			IsolatedStorage();
			vm.InitResources();

		}

		[Test]
		public void GetLatLong(){
			var net =new NetworkStorage();
			net.LatLongRetrievedCompleted+=(data)=>{
				Assert.AreNotEqual(null,data);
			
				Debug.WriteLine(data.Latitude + " - " + data.Longtitude);

			};
			net.GetLatLong(1);
		}

		[Test]
		public void GetSpecialties(){

			var net =new NetworkStorage();
			net.MedicalSpecialtiesRetrievedCompleted+=(data)=>{
				Assert.AreNotEqual(0,data.Count);
				foreach(var str in data){
					Debug.WriteLine(str);
				}
			};
			net.GetSpecialties();

		}
		[Test]
		public void GetCities(){
			
			var net =new NetworkStorage();
			net.MedicalCitiesRetrievedCompleted+=(data)=>{
				Assert.AreNotEqual(0,data.Count);
				foreach(var str in data){
					Debug.WriteLine(str);
				}
			};
			net.GetCities();
			
		}

		[Test]
		public void GetMedicalProviders(){
			
			var net =new NetworkStorage();
			net.MedicalProviderRetrievedCompleted+=(data)=>{
				Assert.AreNotEqual(0,data.Count);
				foreach(var mp in data){
					Debug.WriteLine(mp.FirstName + " " + mp.LastName);
				}
			};

			List<SearchCriteria> list = new List<SearchCriteria>();
			list.Add(new SearchCriteria(){Selector="city", Parameter="Tucson"});
			net.GetMedicalProviders(list);
			
		}


		[Test]
		public void IsolatedStorage ()
		{
			var collection = new List<MedicalProvider>();

			var m = new MedicalProvider(){
				id=1, FirstName="Jack", 
				LastName="Sparrow",
				Address="111 E Pearl Lane",
				ZipCode=85233, 
				City="Gilbert", 
				State="AZ", 
				PhoneNumber=4801112222, 
				Facility="TMC", 
				Specialty="Pediatrics"};

			collection.Add(m);
			FileStorage fs = new FileStorage();
			fs.SaveFavorites(collection);

			var data = fs.LoadFavorites();
			Assert.AreNotEqual(0,data.Count);

		}
	}
}
