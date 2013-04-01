using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;


namespace CommonLogic
{
	public delegate void MedicalSpecialtiesRetrieved(List<string> collection);
	public delegate void MedicalProviderRetrieved(List<MedicalProvider> collection);
	public class NetworkStorage
	{
		public event MedicalProviderRetrieved MedicalProviderRetrievedCompleted;
		public event MedicalSpecialtiesRetrieved MedicalSpecialtiesRetrievedCompleted;

		public void GetSpecialties(){
			var url="http://www.azdevelop.net/client/mobileprovider/api/Provider/GetDimensions?field=specialty";
			var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			httpReq.BeginGetResponse ((ar) => {
				var request = (HttpWebRequest)ar.AsyncState;
				using (var response = (HttpWebResponse)request.EndGetResponse (ar))     {                           
					var s = response.GetResponseStream ();
					var sr = new StreamReader(s);
					var content = sr.ReadToEnd();
					s.Flush();
					sr.Close();
					var collection = JsonConvert.DeserializeObject<List<string>>(content);
					if(MedicalSpecialtiesRetrievedCompleted!=null)
						MedicalSpecialtiesRetrievedCompleted(collection);
					
				}
			} , httpReq);
		}
		public void Test(){
			List<MedicalProvider> collection =null;
			string url = "http://search.twitter.com/search.json?q=xamarin&rpp=10&include_entities=false&result_type=mixed";
			var httpReq = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			httpReq.BeginGetResponse ((ar) => {
				var request = (HttpWebRequest)ar.AsyncState;
				using (var response = (HttpWebResponse)request.EndGetResponse (ar))     {                           
					var s = response.GetResponseStream ();
					var sr = new StreamReader(s);
					var content = sr.ReadToEnd();
					s.Flush();
					sr.Close();
					collection = JsonConvert.DeserializeObject<List<MedicalProvider>>(content);
					if(MedicalProviderRetrievedCompleted!=null)
						MedicalProviderRetrievedCompleted(collection);

				}
			} , httpReq);
		}
	}
	
}

