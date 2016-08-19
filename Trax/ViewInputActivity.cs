using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace Trax
{
	[Activity(Label = "ViewInputActivity")]
	public class ViewInputActivity : Activity
	{
		//Eventually we will want what is below to be more secure, but first we need a brute-force method
		string connectionParam = "server=148.61.177.102;uid=username;port=8889;pwd=password;database=test;";
		MySqlConnection connection = null;
		MySqlDataReader dataReader = null;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ViewInputLayout);
			Button goBack = FindViewById<Button>(Resource.Id.GoBackButton);
			LinearLayout linLayView = FindViewById<LinearLayout>(Resource.Id.linearLayoutView);
			goBack.Text = "Go Back";

			goBack.Click += (sender, e) =>
			{
				var MainActivityIntent = new Intent(this, typeof(Trax.MainActivity));
				StartActivity(MainActivityIntent);
			};

			//When the activity loads and the appropriate resources are initialized, we want to immediately connect to the remote database
			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = "SELECT Name, PO FROM trax"; //String using query syntax
				MySqlCommand cmd = new MySqlCommand(stm, connection); //Query is placed alongside our MySQLConnection object to create a command object
				dataReader = cmd.ExecuteReader(); //Our data reader which was null is given our new command

				int count = dataReader.FieldCount; //This keeps track of how many items are in the query
				Button[] ButtonArray = new Button[count];
				string[] retrievedData = new string[count];
				string[] retrievedDataPO = new string[count];
				while (dataReader.Read())
				{
					for (int i = 0; i < count; i += 2) //Increment by two in order to seperate Names and POs
					{
						string name; //We need this declared in the loop for the Intent to receive the correct string
						retrievedData[i] = dataReader.GetString(i); //Names show up in every other index
						name = retrievedData[i];
						retrievedData[i + 1] = dataReader.GetString(i + 1); //POs show up in between
						retrievedDataPO[i + 1] = retrievedData[i + 1]; //Dumps the odd indexes into the PO array
						ButtonArray[i] = new Button(this); //Initializes our buttons. One button every two indexes 

						ButtonArray[i].Text = name + " PO: " + retrievedDataPO[i + 1]; //Combine the even and odd indexes
						linLayView.AddView(ButtonArray[i]); //Adds the buttons to our view
						ButtonArray[i].Click += delegate
						{
							var viewDetails = new Intent(this, typeof(Trax.ViewDetails));
							viewDetails.PutExtra("MyData", "SELECT Name, PO, IDC FROM trax WHERE Name = '" + name + "'"); //"Confirm" string is sent to whoAreYou activity
							StartActivity(viewDetails);
						};
					}
				}
			}

			catch (MySqlException error) //If at any point there's a connection or query error, we want to know what exactly is going on
			{
				Button errorButton = new Button(this);
				linLayView.AddView(errorButton);
				errorButton.Text = "Error: {0}" + error;
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
