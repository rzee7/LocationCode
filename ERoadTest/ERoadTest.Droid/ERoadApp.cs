using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace ERoadTest.Droid
{
	[Application]
	public class ERoadApp : Application
	{
		#region Constructor

		public ERoadApp (IntPtr Handle, JniHandleOwnership transfer) : base (Handle, transfer)
		{

		}

		#endregion

		#region Evet Argument

		public event EventHandler<ServiceConnectedEventArgs> LocationServiceConnected = delegate { };

		#endregion

		#region Fields Member

		protected static LocationServiceConnection locationServiceConnection;

		#endregion

		#region This Instance

		public static ERoadApp Current {
			get;
			set;
		}

		#endregion

		#region Location Service

		public LocationService LocationService {
			get {
				if (locationServiceConnection.Binder == null)
					throw new Exception ("Service not bound yet");
				return locationServiceConnection.Binder.Service;
			}
		}

		#endregion

		#region OnCreate

		public override void OnCreate ()
		{
			base.OnCreate ();
			Current = this;
			locationServiceConnection = new LocationServiceConnection (null);
			locationServiceConnection.ServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
				this.LocationServiceConnected (this, e);
			};
		}

		#endregion

		#region Start Location Service

		public static void StartLocationService ()
		{
			new Task (() => {
				Android.App.Application.Context.StartService (new Intent (Android.App.Application.Context, typeof(LocationService)));
				Intent locationServiceIntent = new Intent (Android.App.Application.Context, typeof(LocationService));
				Android.App.Application.Context.BindService (locationServiceIntent, locationServiceConnection, Bind.AutoCreate);
			}).Start ();
		}

		#endregion

		#region Stop Location Service

		public static void StopLocationService ()
		{
			if (locationServiceConnection != null) {
				Android.App.Application.Context.UnbindService (locationServiceConnection);
			}
			if (Current.LocationService != null) {
				Current.LocationService.StopSelf ();
			}
		}

		#endregion
	}
}