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
			//Button newButton = FindViewById<Button>(Resource.Id.newButton);

			helloButton.Click += (object sender, EventArgs e) =>
				{
					// helloButton functionality goes here
					

				};
		}
	}
}