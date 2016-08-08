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
		//List<Button> ButtonViewList = new List<Button>();
		string time = "";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			//MySqlConnection mysqlConn = new MySqlConnection(

				string cs = "server=148.61.131.80;uid=username;port=8889;pwd=password;database=test;";
			//mysqlConn.Open();

			//string text = Intent.GetStringExtra("MyData") ?? "Data not available"; //The contents of the "MyData" string is retrieved from the main activity
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ViewInputLayout);
			Button goBack = FindViewById<Button>(Resource.Id.GoBackButton);
			goBack.Text = time;

			goBack.Click += (sender, e) =>
			{
				var MainActivityIntent = new Intent(this, typeof(Trax.MainActivity));
				StartActivity(MainActivityIntent);
			};

			MySqlConnection conn = null;
			MySqlDataReader rdr = null;

			try
			{
				conn = new MySqlConnection(cs);
				conn.Open();

				string stm = "SELECT * FROM trax";
				MySqlCommand cmd = new MySqlCommand(stm, conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					goBack.Text = (rdr.GetInt32(0) + ": "
						+ rdr.GetString(1));
				}

			}
			catch (MySqlException ex)
			{
				goBack.Text = "Error: {0}" + ex;

			}
			finally
			{
				if (rdr != null)
				{
					rdr.Close();
				}

				if (conn != null)
				{
					conn.Close();
				}

			}
		}
	}
}

