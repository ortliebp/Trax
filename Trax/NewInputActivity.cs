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
	[Activity(Label = "NewInputActivity")]
	public class NewInputActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
            //var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //var filename = Path.Combine(documents, "localName.txt");

            string text = Intent.GetStringExtra("NewInputData") ?? "Error";
            base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewInputLayout);

            TextView POnumberTextBox = FindViewByID<EditText>(Resource.Id.POtextInput);
            TextView IDCnumberTextBox = findViewByID<EditText>(Resource.Id.IDCtextInput);
            Button confirmButton = FindViewById<Button>(Resource.Id.inputConfirmButton);

            confirmButton.Click += (sender, e) =>
            {
                var returnToMain = new Intent(this, typeof(Trax.MainActivity));
                StartActivity(returnToMain);
            };

            // Set spinner selector from "Main" layout resource
            //Spinner dsstSpinner = FindViewById<Spinner>(Resource.Id.action_bar_spinner);
            //	dsstSpinner.ItemSelected += spinner_ItemSelected;
            //var adapter = ArrayAdapter.CreateFromResource(
            //	this, Resource.Array.DSST_list, Android.Resource.Layout.SimpleSpinnerItem);

            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //dsstSpinner.Adapter = adapter;
        }
		private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			string toast = string.Format("Yes!", spinner.GetItemAtPosition(e.Position));
			Toast.MakeText(this, toast, ToastLength.Long).Show();
		}
	}
}


