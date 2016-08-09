using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.IO;

namespace Trax
{
	[Activity(Label = "whoAreYouPrompt")]
	public class whoAreYouPrompt : Activity
	{
		//Instance declaration in order to access the main activity's methods
		MainActivity mainInstance = new MainActivity();
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.whoAreYouLayout);
			Button confirm = FindViewById<Button>(Resource.Id.confirmButton);
			TextView namePromptTextBox = FindViewById<EditText>(Resource.Id.namePrompt);

			var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "localName.txt");

			confirm.Click += delegate {
				File.WriteAllText(filename, namePromptTextBox.Text);
				mainInstance.setName(namePromptTextBox.Text);
				StartActivity(typeof(MainActivity));
			};
		}
	}
}