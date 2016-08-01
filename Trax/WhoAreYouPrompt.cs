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
	[Activity(Label = "whoAreYouPrompt")]
	public class whoAreYouPrompt : Activity
	{
		MainActivity mainInstance = new MainActivity(); //Instance declaration to access the main activity's methods
		protected override void OnCreate(Bundle savedInstanceState)
		{
			var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "localName.txt");

			string text = Intent.GetStringExtra("MyData") ?? "Data not available"; //The contents of the "MyData" string is retrieved from the main activity
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.whoAreYouLayout);

			Button confirm = FindViewById<Button>(Resource.Id.confirmButton);
			TextView namePromptTextBox = FindViewById<EditText>(Resource.Id.namePrompt);
			confirm.Text = text;
			confirm.Click += delegate {
				File.WriteAllText(filename, namePromptTextBox.Text);
				mainInstance.setName(namePromptTextBox.Text);
				StartActivity(typeof(MainActivity));
			};

			var btn = this.FindViewById<Button>(Resource.Id.parseButton);
			btn.Click += async (sender, e) =>
			{
				var obj = new ParseObject("Note");
				obj["text"] = "It's working!";
				obj["tags"] = new List<string> { "welcome", "xamarin", "parse" };
				await obj.SaveAsync();
			};
		}
	}
}