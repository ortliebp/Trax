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
using Parse;
using System.IO;

namespace Trax
{
	[Activity(Label = "ViewInputActivity")]
	public class ViewInputActivity : Activity
	{
		List<Button> ButtonViewList = new List<Button>();

		ParseQuery<ParseObject> query = ParseObject.GetQuery("Note");

		protected override void OnCreate(Bundle savedInstanceState)
		{
			//string text = Intent.GetStringExtra("MyData") ?? "Data not available"; //The contents of the "MyData" string is retrieved from the main activity
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ViewInputLayout);
			Button goBack = FindViewById<Button>(Resource.Id.GoBackButton);

			for (int i = 0; i < 5; i++)
			{
				ButtonViewList.Add(new Button(this));
				//TextViewList[i].Location = new System.Drawing.Point(600, (i * 20));
				//this.Controls.Add(TextViewList[i]);
			}

			goBack.Click += (sender, e) =>
			{
				var MainActivityIntent = new Intent(this, typeof(Trax.MainActivity));
				StartActivity(MainActivityIntent);
			};

		}

		public void InsertButton(Button x)
		{
			ButtonViewList.Add(x);
		}

		public bool CheckIfButtonExists(Button x)
		{
			if (ButtonViewList.Contains(x))
				return true;
			else
				return false;
		}
	}
}

