using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Trax
{
	[Activity(Label = "ViewInputActivity")]
	public class ViewInputActivity : Activity
	{
		List<Button> ButtonViewList = new List<Button>();
		List<string> retrievedData = new List<string>();
		string testText = "";
		int pubCount = 0;

		//Eventually we will want what is below to be more secure, but first we need a brute-force method
		string connectionParam = "server=148.61.131.80;uid=username;port=8889;pwd=password;database=test;";
		MySqlConnection connection = null;
		MySqlDataReader dataReader = null;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ViewInputLayout);
			Button goBack = FindViewById<Button>(Resource.Id.GoBackButton);
			goBack.Text = testText;

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

				string stm = "SELECT * FROM trax"; //String using query syntax
				MySqlCommand cmd = new MySqlCommand(stm, connection); //Query is placed alongside our MySQLConnection object to create a command object
				dataReader = cmd.ExecuteReader(); //Our data reader which was null is given our new command

				int count = dataReader.FieldCount;
				pubCount = count;
				while (dataReader.Read())
				{
					for (int i = 0; i < count; i++)
					{
						retrievedData.Add(dataReader.GetString(i));
						Button b = new Button(this);
						b.Text = retrievedData[i];
						b.Click += delegate
						{
						};
						//testText = dataReader.GetValue(i).ToString();

						//testText = (dataReader.GetInt32(i) + " : " + dataReader.GetString(1)).ToString();
					}
				}
				goBack.Text = testText;
			}

			catch (MySqlException error) //If at any point there's a connection or query error, we want to know what exactly is going on
			{
				goBack.Text = "Error: {0}" + error;
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
