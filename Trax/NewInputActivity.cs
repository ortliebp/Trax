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
		public string DSSTstring = "";
		public string POstring = "";
		public string IDCstring = "";
		protected override void OnCreate(Bundle savedInstanceState)
		{
			var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "localName.txt");

			string text = Intent.GetStringExtra("NewInputData") ?? "Error";
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NewInputLayout);

			TextView DSSTname = FindViewById<TextView>(Resource.Id.DSSTname);
			TextView POnumberTextBox = FindViewById<EditText>(Resource.Id.POtextInput);
			TextView IDCnumberTextBox = FindViewById<EditText>(Resource.Id.IDCtextInput);
			Button confirmButton = FindViewById<Button>(Resource.Id.inputConfirmButton);

			confirmButton.Click += (sender, e) =>
			{
				// Send text box info to DB
				DSSTstring = DSSTname.Text;
				POstring = POnumberTextBox.Text;
				IDCstring = IDCnumberTextBox.Text;
				DBpush();

				// Return to MainActiity after confirm
				var returnToMain = new Intent(this, typeof(Trax.MainActivity));
				StartActivity(returnToMain);
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

		public void DBpush()
		{
			string connectionParam = "server=148.61.177.102;uid=username;port=8889;pwd=password;database=test;";
			MySqlConnection connection = null;
			MySqlDataReader dataReader = null;

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				//string stm = "INSERT INTO trax (Entry, Name, PO, IDC) VALUES(@Entry, @Name, @PO, @IDC)";
				string stm = "INSERT INTO trax (Name, PO, IDC) VALUES(@Name, @PO, @IDC)";

				MySqlCommand cmd = new MySqlCommand(stm, connection);
				cmd.Parameters.AddWithValue("@Name", DSSTstring);
				cmd.Parameters.AddWithValue("@PO", POstring);
				cmd.Parameters.AddWithValue("@IDC", IDCstring);
				dataReader = cmd.ExecuteReader();
			}
			catch (MySqlException error)
			{

			}
			finally //We need to close all of our connections once everything is retrieved
			{
				if (dataReader != null)
				{
					dataReader.Close();
				}

				if (connection != null)
				{
					connection.Close();
				}
			}
		}
	}
}



