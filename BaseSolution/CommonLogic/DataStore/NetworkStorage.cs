using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CommonLogic
{

	public class UrlAddresses{
		public static readonly string GetLatLong = "http://www.azdevelop.net/client/mobileprovider/api/Provider/GetLatLong?Id=";
		public static readonly string GetSpecialties="http://www.azdevelop.net/client/mobileprovider/api/Provider/GetDimensions?field=specialty";
		public static readonly string GetCities = "http://www.azdevelop.net/client/mobileprovider/api/Provider/GetDimensions?field=city";
		public static readonly string GetMedicalProviders="http://www.azdevelop.net/client/mobileprovider/api/Provider/GetProvidersByDimensionCollection";
	}

    public delegate void MedicalSpecialtiesRetrieved(List<string> collection);
    public delegate void MedicalProviderRetrieved(List<MedicalProvider> collection);
    public delegate void MedicalCitiesRetrieved(List<string> collection);
    public delegate void LatLongRetrieved(LatLong obj);

    public class NetworkStorage
    {
        public event MedicalProviderRetrieved MedicalProviderRetrievedCompleted;
        public event MedicalSpecialtiesRetrieved MedicalSpecialtiesRetrievedCompleted;
        public event MedicalCitiesRetrieved MedicalCitiesRetrievedCompleted;
        public event LatLongRetrieved LatLongRetrievedCompleted;

        private Rest rest;

        public NetworkStorage()
        {
            rest = new Rest();
        }

        public void GetLatLong(int id)
        {
			var url = UrlAddresses.GetLatLong + id;

            rest.Get(url, (content) =>
            {
                var obj = JsonConvert.DeserializeObject<LatLong>(content);
                if (LatLongRetrievedCompleted != null)
                    LatLongRetrievedCompleted(obj);
            });
        }

        public void GetSpecialties()
        {
			var url = UrlAddresses.GetSpecialties;

            rest.Get(url, (content) =>
            {
                var collection = JsonConvert.DeserializeObject<List<string>>(content);
                if (MedicalSpecialtiesRetrievedCompleted != null)
                    MedicalSpecialtiesRetrievedCompleted(collection);
            });

        }

        public void GetCities()
        {
			var url = UrlAddresses.GetCities;

            rest.Get(url, (content) =>
            {
                var collection = JsonConvert.DeserializeObject<List<string>>(content);
                collection.Sort();
                if (MedicalCitiesRetrievedCompleted != null)
                    MedicalCitiesRetrievedCompleted(collection);
            });

        }

        public void GetMedicalProviders(SearchCriteria criteria)
        {
			var bytes = criteria.ConvertToByteArray();
			string url = UrlAddresses.GetMedicalProviders;

            rest.Post(url, bytes, (content) =>
            {
                var collection = JsonConvert.DeserializeObject<List<MedicalProvider>>(content);
                if (MedicalProviderRetrievedCompleted != null)
                    MedicalProviderRetrievedCompleted(collection);
            });

        }
    }


    public class Rest
    {

        public Rest() { }

        public void Get(string url, Action<string> action)
        {
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            httpReq.BeginGetResponse((ar) =>
            {
                var request = (HttpWebRequest)ar.AsyncState;
                using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                {
                    var s = response.GetResponseStream();
                    var sr = new StreamReader(s);
                    var content = sr.ReadToEnd();
                    s.Flush();
                    sr.Close();
                    action(content);
                }
            }, httpReq);
        }
        public void Post(string url, byte[] bytes, Action<string> action)
        {
            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            httpReq.Method = "POST";
            httpReq.ContentType = "application/json";
            httpReq.ContentLength = bytes.Length;

            httpReq.BeginGetRequestStream((sr) =>
            {
                var innerRequest = (HttpWebRequest)sr.AsyncState;

                var innerStream = innerRequest.EndGetRequestStream(sr);
                innerStream.Write(bytes, 0, bytes.Length);
                innerStream.Close();
                innerRequest.BeginGetResponse((ar) =>
                {
                    var request = (HttpWebRequest)ar.AsyncState;
                    using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                    {
                        var responseStream = response.GetResponseStream();
                        var responseReader = new StreamReader(responseStream);
                        var content = responseReader.ReadToEnd();
                        responseStream.Flush();
                        responseReader.Close();
                        action(content);
                    }
                }, innerRequest);
            }, httpReq);
        }

    }

}

