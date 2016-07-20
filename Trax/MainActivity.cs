using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Trax
{
	[Activity(Label = "Trax", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			Button settings = FindViewById<Button>(Resource.Id.settingsButton);
			settings.Click += delegate{
				StartActivity(typeof(whoAreYourPrompt));
			};

			Button newInputButton = FindViewById<Button>(Resource.Id.NewInputButton);
			newInputButton.Click += delegate{
				var newInputIntent = new Intent(this, typeof(NewInputActivity));
				StartActivity(newInputIntent);
				//StartActivity(typeof(NewInputActivity));
			};
	}

		// For newInput 
		//newInputButton.Click += (sender, e) =>


	}
}


