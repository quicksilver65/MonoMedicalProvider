using System;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace CommonLogic
{
	public class SearchCriteria:INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		private string city;
		private string specialty;
		private string name;

		public string City {
			get {
				return city;
			}
			set {
				city = value;Notify("City");
			}
		}

		public string Specialty {
			get {
				return specialty;
			}
			set {
				specialty = value; Notify("Specialty");
			}
		}

		public string Name {
			get {
				return name;
			}
			set {
				name = value;Notify("Name");
			}
		}

		public byte[] ConvertToByteArray ()
		{
			List<Criteria> criteria = new List<Criteria> ();
			if (!String.IsNullOrEmpty (City))
				criteria.Add (new Criteria (){ Selector="city", Parameter=City});
			if (!String.IsNullOrEmpty (Specialty))
				criteria.Add (new Criteria (){ Selector="specialty", Parameter=Specialty});
			if (!String.IsNullOrEmpty (City))
				criteria.Add (new Criteria (){ Selector="name", Parameter=Name});

			var criteriaData = JsonConvert.SerializeObject (criteria.ToArray ());
			var bytes = Encoding.UTF8.GetBytes (criteriaData);
			return bytes;
		}

		internal class Criteria
		{
			public string Selector {
				get;
				set;
			}

			public string Parameter {
				get;
				set;
			}
		}

		private void Notify(string propName){
			if(PropertyChanged!=null)
				PropertyChanged(this,new PropertyChangedEventArgs(propName));
		}
	}
}


