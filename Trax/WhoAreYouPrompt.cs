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
		protected override void OnCreate(Bundle savedInstanceState)
		{
			string text = Intent.GetStringExtra("MyData") ?? "Data not available";
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.whoAreYouLayout);

			Button confirm = FindViewById<Button>(Resource.Id.confirmButton);
			confirm.Text = text;
			confirm.Click += delegate
			{
				StartActivity(typeof(MainActivity));
			};
		}
	}
}