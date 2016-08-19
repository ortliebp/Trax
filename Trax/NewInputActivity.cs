using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace Trax
{
	[Activity(Label = "NewInputActivity")]
	public class NewInputActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "localName.txt");

			// DB Connection
			string connectionParam = "server=35.39.32.163;uid=username;port=8889;pwd=password;database=test;";
			MySqlConnection connection = null;

            string text = Intent.GetStringExtra("NewInputData") ?? "Error";
            base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewInputLayout);

			TextView DSSTname = FindViewById<TextView>(Resource.Id.DSSTname);
            TextView POnumberTextBox = FindViewById<EditText>(Resource.Id.POtextInput);
            TextView IDCnumberTextBox = FindViewById<EditText>(Resource.Id.IDCtextInput);
            Button confirmButton = FindViewById<Button>(Resource.Id.inputConfirmButton);

            confirmButton.Click += (sender, e) =>
            {
                var returnToMain = new Intent(this, typeof(Trax.MainActivity));
                StartActivity(returnToMain);
				// Send text box info to DB

            };

			// Local text file for DSST name that is entered
			if (File.Exists(documents + "/localName.txt"))
			{
				var file = Path.Combine(documents, "localName.txt");
				var nameText = File.ReadAllText(file);
				DSSTname.Text = nameText;
			}

			else
			{
				var whoAreYou = new Intent(this, typeof(Trax.whoAreYouPrompt));
				whoAreYou.PutExtra("MyData", "Confirm"); //"Confirm" string is sent to whoAreYou activity
				StartActivity(whoAreYou);
			}
		}
        }
	}



