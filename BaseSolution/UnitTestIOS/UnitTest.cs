
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
