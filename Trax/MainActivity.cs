using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;

namespace Trax
{
	[Activity(Label = "Trax", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		public string Name = "";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			TextView greeting = FindViewById<TextView>(Resource.Id.greetingTextBox);
			Button settings = FindViewById<Button>(Resource.Id.settingsButton);
			Button newInputButton = FindViewById<Button>(Resource.Id.NewInputButton);
			Button viewInputButton = FindViewById<Button>(Resource.Id.ViewButton);

			var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			if (System.IO.File.Exists(documents + "/localName.txt"))
			{
				var filename = Path.Combine(documents, "localName.txt");
				var text = File.ReadAllText(filename);
				greeting.Text = text;
			}
			else {
				var whoAreYou = new Intent(this, typeof(Trax.whoAreYouPrompt));
				whoAreYou.PutExtra("MyData", "Confirm"); //"Confirm" string is sent to whoAreYou activity
				StartActivity(whoAreYou);
			}

			settings.Click += delegate
			{
				var whoAreYou = new Intent(this, typeof(Trax.whoAreYouPrompt));
				whoAreYou.PutExtra("MyData", "Confirm"); //"Confirm" string is sent to whoAreYou activity
				StartActivity(whoAreYou);
			};

			newInputButton.Click += (sender, e) =>
			{
				var newInputIntent = new Intent(this, typeof(Trax.NewInputActivity));
				newInputIntent.PutExtra("NewInputData", Name);
				StartActivity(newInputIntent);
			};

			viewInputButton.Click += (sender, e) =>
			{
				var viewInputIntent = new Intent(this, typeof(Trax.ViewInputActivity));
				viewInputIntent.PutExtra("ViewInputData", Name);
				StartActivity(viewInputIntent);
			};
		}

		public void setName(string confirmedName)
		{
			Name = confirmedName;
		}
	}
}
		
