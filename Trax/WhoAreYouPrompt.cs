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
	[Activity(Label = "whoAreYouPrompt")]
	public class whoAreYouPrompt : Activity
	{
		MainActivity mainInstance = new MainActivity(); //Instance declaration to access the main activity's methods
		protected override void OnCreate(Bundle savedInstanceState)
		{
			string text = Intent.GetStringExtra("MyData") ?? "Data not available"; //The contents of the "MyData" string is retrieved from the main activity
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.whoAreYouLayout);

			Button confirm = FindViewById<Button>(Resource.Id.confirmButton);
			TextView namePromptTextBox = FindViewById<EditText>(Resource.Id.namePrompt);
			confirm.Text = text;
			confirm.Click += delegate {
				mainInstance.setName(namePromptTextBox.Text);
				StartActivity(typeof(MainActivity));
			};
		}
	}
}