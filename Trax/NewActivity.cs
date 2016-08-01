using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Trax
{
	[Activity(Label = "New")]
	public class NewActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.NewLayout);

			// Set spinner selector from "Main" layout resource
			Spinner dsstSpinner = FindViewById<Spinner>(Resource.Id.action_bar_spinner);
			dsstSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource(
				this, Resource.Array.DSST_list, Android.Resource.Layout.SimpleSpinnerItem); //<----------- ERROR: Resource.Array??? 

			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			dsstSpinner.Adapter = adapter;
		}
		private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
			Toast.MakeText(this, toast, ToastLength.Long).Show();
		}
	}
}

