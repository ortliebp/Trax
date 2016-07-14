using Android.App;
using Android.Widget;
using Android.OS;

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
			settings.Click += delegate {
				StartActivity(typeof(whoAreYourPrompt));
			};
		}



	}
}


