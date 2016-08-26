using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using System;

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
			ScrollView btnScrollView = FindViewById<ScrollView>(Resource.Id.buttonScrollView);
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
				string stm = "SELECT Entry, Name, PO, Delivered FROM trax"; //String using query syntax
				MySqlCommand cmd = new MySqlCommand(stm, connection); //Query is placed alongside our MySQLConnection object to create a command object
				dataReader = cmd.ExecuteReader(); //Our data reader which was null is given our new command

				int count = dataReader.FieldCount; //This keeps track of how many items are in the query
				Button[] ButtonArray = new Button[count];
				int[] retrievedDataEntry = new int[count];
				string[] retrievedData = new string[count];
				string[] retrievedDataPO = new string[count];
				int[] retrievedDataDelivered = new int[count];

				while (dataReader.Read())
				{
					for (int i = 0; i < count; i += 4) //Increment by four in order to seperate all of the values from Entry
					{
						string name; //We need this declared in the loop for the Intent to receive the correct string
						int entry;
						int deliveryStatus;

						retrievedDataEntry[i] = dataReader.GetInt16(i); //Entry numbers show up every three indexes
						entry = retrievedDataEntry[i];

						retrievedData[i + 1] = dataReader.GetString(i + 1); //Names show up after the Entry
						name = retrievedData[i + 1];

						retrievedData[i + 2] = dataReader.GetString(i + 2);
						retrievedDataPO[i + 2] = retrievedData[i + 2]; //Dumps the odd indexes into the PO array

						retrievedDataDelivered[i + 3] = dataReader.GetInt16(i + 3);
						deliveryStatus = retrievedDataDelivered[i + 3];

						ButtonArray[i] = new Button(this); //Initializes our buttons. One button every two indexes 

						ButtonArray[i].Text = name + " P" + retrievedDataPO[i + 2] + deliveredOrNot(deliveryStatus); //Combine the even and odd indexes
						btnScrollView.AddView(ButtonArray[i]); //Adds the buttons to our view
						ButtonArray[i].Click += delegate
						{
							var viewDetails = new Intent(this, typeof(Trax.ViewDetails));
							viewDetails.PutExtra("MyData", Convert.ToString(entry));
							StartActivity(viewDetails);
						};
					}
				}
			}

			catch (MySqlException error) //If at any point there's a connection or query error, we want to know what exactly is going on
			{
				Button errorButton = new Button(this);
				btnScrollView.AddView(errorButton);
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

		string deliveredOrNot(int x)
		{
			if (x == 1)
			{
				return " Delivered";
			}
			else return "";
		}
	}
}
