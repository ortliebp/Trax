using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

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

			Button settings = FindViewById<Button>(Resource.Id.settingsButton);
			settings.Click += delegate
			{
				var whoAreYou = new Intent(this, typeof(Trax.whoAreYouPrompt));
				whoAreYou.PutExtra("MyData", "Confirm"); //"Confirm" string is sent to whoAreYou activity
				StartActivity(whoAreYou);
			};
		}
		public void setName(string confirmedName)
		{
			Name = confirmedName;
		}
	}
}
		