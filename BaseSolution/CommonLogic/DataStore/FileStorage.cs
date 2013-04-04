using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.IO;

namespace CommonLogic
{


    public class FileStorage
    {
        private readonly string medicalFavorites = "MedicalStoreFavorites";
		private readonly string specialtiesList = "SpecialtiesList";
		private readonly string citiesList = "CitiesList";

        public List<MedicalProvider> LoadFavorites()
        {
			var content = GetIsolatedStorage(medicalFavorites);
			if(content==null)
				return new List<MedicalProvider>();

			var collection = JsonConvert.DeserializeObject<List<MedicalProvider>>(content);
			return collection;

        }
        public void SaveFavorites(List<MedicalProvider> collection)
        {
            var content = JsonConvert.SerializeObject(collection);
			SaveIsolatedStorage(medicalFavorites,content);

        }
		public List<string> LoadCities()
		{
			var content = GetIsolatedStorage(citiesList);
			if(content==null)
				return new List<string>();
			var collection = JsonConvert.DeserializeObject<List<string>>(content);
			return collection;
			
		}
		public void SaveCities(List<string> collection)
		{
			var content = JsonConvert.SerializeObject(collection);
			SaveIsolatedStorage(citiesList,content);
			
		}
		public List<string> LoadSpecialties()
		{
			var content = GetIsolatedStorage(specialtiesList);
			if(content==null)
				return new List<string>();
			var collection = JsonConvert.DeserializeObject<List<string>>(content);
			return collection;
			
		}
		public void SaveSpecialties(List<string> collection)
		{
			var content = JsonConvert.SerializeObject(collection);
			SaveIsolatedStorage(specialtiesList,content);
			
		}


		private string GetIsolatedStorage(string contentName){
			using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
			{
				if(isoStorage.FileExists(contentName))
				{
					var s = isoStorage.OpenFile(contentName, FileMode.OpenOrCreate);
					var sr = new StreamReader(s);
					var content = sr.ReadToEnd();
					sr.Close();
					return content;
				}
				else
				{
					return null;
				}
			}
		}
		private void SaveIsolatedStorage(string contentName,string contentValue){
			using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
			{
				var s = isoStorage.OpenFile(contentName, FileMode.Create);
				var sw = new StreamWriter(s);
				sw.Write(contentValue);
				sw.Flush();
				sw.Close();
			}
		}

    }
}

