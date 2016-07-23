using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Trax {
	[Activity(Label = "Trax", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);
			//Intent whoAreYou = new Intent(this, typeof(whoAreYouPrompt));

			Button settings = FindViewById<Button>(Resource.Id.settingsButton);
			settings.Click += delegate {
				Intent whoAreYou = new Intent(this, typeof(Trax.whoAreYouPrompt));
				whoAreYou.PutExtra("MyData", "Data from Activity1");
				StartActivity(whoAreYou);
			};
		}
	}
}