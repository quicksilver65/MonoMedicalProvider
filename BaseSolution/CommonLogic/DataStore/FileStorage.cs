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
        private readonly string fileName = "MedicalStoreFavorites";


        public List<MedicalProvider> LoadFavorites()
        {
            using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var s = isoStorage.OpenFile(fileName, FileMode.OpenOrCreate);
                var sr = new StreamReader(s);
                var content = sr.ReadToEnd();
                sr.Close();
                var collection = JsonConvert.DeserializeObject<List<MedicalProvider>>(content);
                return collection;
            }

        }
        public void SaveFavorites(List<MedicalProvider> collection)
        {
            var content = JsonConvert.SerializeObject(collection);
            using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var s = isoStorage.OpenFile(fileName, FileMode.Create);
                var sw = new StreamWriter(s);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }

        }


    }
}

