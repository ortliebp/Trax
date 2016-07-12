using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Trax
{
	[Activity(Label = "Trax", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		// Was previously a means of storing a list of phone numbers
		//static readonly List<string> phoneNumbers = new List<string>();

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our UI controls from the loaded layout
			Button helloButton = FindViewById<Button>(Resource.Id.HelloWorldButton);

			// Set spinner selector from "Main" layout resource
			Spinner dsstSpinner = FindViewById<Spinner>(Resource.Id.action_bar_spinner);
			dsstSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.DSST_list, Android.Resource.Layout.SimpleSpinnerItem); //<----------- ERROR: Resource.Array??? 
			
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			dsstSpinner.Adapter = adapter;

			helloButton.Click += (object sender, EventArgs e) =>
				{
					// helloButton functionality goes here
					

				};
			//------------- Old Premade code -------- //
			//		protected override void OnCreate(Bundle savedInstanceState)
			//		{
			//			base.OnCreate(savedInstanceState);

			//			// Set our view from the "main" layout resource
			//			//contributor test1
			//			SetContentView(Resource.Layout.Main);

			//			// Get our button from the layout resource,
			//			// and attach an event to it
			//			Button button = FindViewById<Button>(Resource.Id.myButton);

			//			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
			//		}
			//	}
			//}  
			// --------------------------------------------------------------- //
		}
		private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
			Toast.MakeText(this, toast, ToastLength.Long).Show();
		}
	}
}