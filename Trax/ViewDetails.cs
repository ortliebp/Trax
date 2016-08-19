using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using Android.Graphics;

namespace Trax
{
	[Activity(Label = "ViewDetails")]
	public class ViewDetails : Activity
	{
		string connectionParam = "server=35.39.32.163;uid=username;port=8889;pwd=password;database=test;";
		MySqlConnection connection = null;
		MySqlDataReader dataReader = null;
		int notDelButtonCount = 0;
		int DelButtonCount = 0;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ViewDetailsLayout);
			Button goBack = FindViewById<Button>(Resource.Id.goBack);
			LinearLayout linLayView = FindViewById<LinearLayout>(Resource.Id.linearLayoutView);
			Button notDeliveredButton = FindViewById<Button>(Resource.Id.NotDeliveredButton);
			Button deliveredButton = FindViewById<Button>(Resource.Id.DeliveredButton);
			deliveredButton.SetBackgroundColor(Color.LightGray);
			notDeliveredButton.SetBackgroundColor(Color.LightGray);

			string customQuery = Intent.GetStringExtra("MyData") ?? "Data not available";
			goBack.Text = customQuery;
			goBack.Click += delegate
			{
				StartActivity(typeof(ViewInputActivity));
			};

			notDeliveredButton.Click += delegate
			{
				notDeliveredButton.Text = "Not Delivered";
				deliveredButton.Text = "Delivered";
				DelButtonCount = 0;
				deliveredButton.SetBackgroundColor(Color.LightGray);
				notDelButtonCount++;
				notDeliveredButton.Text = "Not Delivered \r\n Tap again to confirm";
				notDeliveredButton.SetBackgroundColor(Color.LimeGreen);
				if (notDelButtonCount >= 2)
				{
					notDelButtonCount = 0;
					StartActivity(typeof(MainActivity));
				}
			};

			deliveredButton.Click += delegate
			{
				notDeliveredButton.Text = "Not Delivered";
				deliveredButton.Text = "Delivered";
				notDelButtonCount = 0;
				notDeliveredButton.SetBackgroundColor(Color.LightGray);
				DelButtonCount++;
				deliveredButton.Text = "Delivered \r\n Tap again to confirm";
				deliveredButton.SetBackgroundColor(Color.LimeGreen);
				if (DelButtonCount >= 2)
				{
					DelButtonCount = 0;
					StartActivity(typeof(MainActivity));
				}
			};

			try
			{
				connection = new MySqlConnection(connectionParam);
				connection.Open();
				string stm = customQuery; //String using query syntax
				MySqlCommand cmd = new MySqlCommand(stm, connection); //Query is placed alongside our MySQLConnection object to create a command object
				dataReader = cmd.ExecuteReader(); //Our data reader which was null is given our new command

				int count = dataReader.FieldCount; //This keeps track of how many items are in the query
				Button[] TextArray = new Button[count];
				string[] retrievedData = new string[count];

				while (dataReader.Read())
				{
					for (int i = 0; i < count; i++)
					{
						retrievedData[i] = dataReader.GetString(i);
						TextArray[i] = new Button(this);
						TextArray[i].Text = retrievedData[i];
						linLayView.AddView(TextArray[i]); //Adds the text box to our view
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

