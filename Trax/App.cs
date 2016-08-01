using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace Trax
{
	[Application]
	public class App : Application
	{
		public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			// Initialize the Parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseClient.Initialize(new ParseClient.Configuration
			{
				ApplicationId = "SF6ySem9jKCUPEhVrbI3jNkC0m87MQpKE7rzU2Bw",
				WindowsKey = "pPjU4zJofICNcOHIDAXn52JLmoUDhNEYH0LIcYZr",
				Server = "https://parseapi.back4app.com"
			});
		}
	}
}