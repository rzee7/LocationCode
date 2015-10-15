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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Preferences;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ERoadTest.Droid
{
    [Activity(Label = "Eroad", MainLauncher = true)]
    public class HomeActivity : BaseActivity, IOnMapReadyCallback
    {
        #region Private Variables

        protected LocationManager LocManager = Android.App.Application.Context.GetSystemService(LocationService) as LocationManager;

        MapFragment mapFrag;
        GoogleMap map;
        Location currentLocation;
        TextView txtTimeZone;

        #endregion

        #region Override View Layout Resource

        public override int LayoutResource
        {
            get
            {
                return Resource.Layout.HomeLayout;
            }
        }

        #endregion

        #region On Create Method

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            txtTimeZone = FindViewById<TextView>(Resource.Id.txtUserAddress);

            //Check if location enabled
            if (!LocManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                ShowLocationDisableAlert();
            }

            //register location update service
            ERoadApp.Current.LocationServiceConnected += (object sender, ServiceConnectedEventArgs e) =>
            {
                ERoadApp.Current.LocationService.LocationChanged += HandleLocationChanged;
                ERoadApp.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
                ERoadApp.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
                ERoadApp.Current.LocationService.StatusChanged += HandleStatusChanged;
            };
            //Start service
            ERoadApp.StartLocationService();

            mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFrag.GetMapAsync(this);

        }

        #endregion

        #region Getting Timezone

       

        async void GetLocaleTimeZone(Location currLoc)
        {
            var source = currLoc.Latitude.ToString() + "," + currLoc.Longitude.ToString();
            string timeZoneUrl = string.Format(Constant.GoogleTimeZone, source, DateTime.UtcNow.ToTimestamp());
            //Get string route from Google map api
            string JSONTimezoneResponse = await HttpRequest(timeZoneUrl);
            if (!string.IsNullOrEmpty(JSONTimezoneResponse))
            {
                var objTime = JsonConvert.DeserializeObject<GoogleTimeZone>(JSONTimezoneResponse); 
                BindMapMarker(string.Format("Timezone {0} Utc {1} Loc {2}", objTime.timeZoneId, DateTime.UtcNow.TimeOfDay, DateTime.Now.TimeOfDay));
            }
        }

        #endregion

        #region Alert Location disabled

        public void ShowLocationDisableAlert()
        {
            RunOnUiThread(() =>
                {
                    AlertDialog.Builder builder;
                    builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Location Service Disabled");
                    builder.SetMessage("Please enable location services.");
                    builder.SetCancelable(false);
                    builder.SetPositiveButton("Enable", delegate
                        {
                            var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                            StartActivity(intent);
                        });
                    builder.Show();
                });
        }

        #endregion

        #region Map Ready CallBack & Map Marker Bind

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MyLocationEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            currentLocation = new Location("gps");
        }

        #endregion

        #region Bind Map Marker and create Region bound

        void BindMapMarker(string journey = "")
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(currentLocation.Latitude, currentLocation.Longitude));

            marker.SetTitle(string.Format("La {0} Lo {1}", currentLocation.Latitude.ToString("0.00"), currentLocation.Longitude.ToString("0.00")));
            marker.SetSnippet(journey);
            var markerNew = map.AddMarker(marker);
        }

        #endregion

        #region Location Service Method Handling

        private void HandleStatusChanged(object sender, Android.Locations.StatusChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandleProviderEnabled(object sender, Android.Locations.ProviderEnabledEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandleProviderDisabled(object sender, Android.Locations.ProviderDisabledEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void HandleLocationChanged(object sender, Android.Locations.LocationChangedEventArgs e)
        {
            if (currentLocation != null	&& currentLocation.Latitude != e.Location.Latitude)
            {
                currentLocation = e.Location;
                if (map != null)
                {
                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(new LatLng(e.Location.Latitude, e.Location.Longitude));
                    builder.Zoom(Constant.ZoomLevel);
                    CameraPosition cameraPosition = builder.Build();
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                    map.MapType = GoogleMap.MapTypeNormal;
                    map.AnimateCamera(cameraUpdate);
                    var loc = new Location(LocationManager.GpsProvider);
                    loc.Latitude = -36.722195;
                    loc.Longitude = 174.706167;
                   
                    GetLocaleTimeZone(currentLocation);
                    GetDuration(currentLocation, loc);
                    //BindMapMarker();
                }
            }
        }

        #endregion

        #region GetDuration map

        async void GetDuration(Location SourceLoc, Location DestinationLoc)
        {
            var source = SourceLoc.Latitude.ToString() + "," + SourceLoc.Longitude.ToString();
            var destination = DestinationLoc.Latitude.ToString() + "," + DestinationLoc.Longitude.ToString();
            //Create Google Map direction URL
            string fullDirectionURL = string.Format(Constant.GoogleDirectionUrl, source, destination);
            //Get string route from Google map api
            string JSONDirectionResponse = await HttpRequest(fullDirectionURL);
            if (!string.IsNullOrEmpty(JSONDirectionResponse))
            {
                //Pass the json string route to draw the map route
                SetDurationQuery(JSONDirectionResponse);
            }
        }

        WebClient webclient;
        //Download route from Google api url
        async Task<string> HttpRequest(string Uri)
        { 
            webclient = new WebClient();
            string resultData;
            try
            {
                resultData = await webclient.DownloadStringTaskAsync(new Uri(Uri));
                Console.WriteLine(resultData);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (webclient != null)
                {
                    webclient.Dispose();
                    webclient = null; 
                }
            }
            return resultData;
        }

        void SetDurationQuery(string JSONDirectionResponse)
        {
            Console.WriteLine("Getting route");
            var objRoutes = JsonConvert.DeserializeObject<GoogleDirection>(JSONDirectionResponse);  
            //--may be more then one 
            if (objRoutes != null && objRoutes.routes != null && objRoutes.routes.Count > 0)
            {
                Console.WriteLine("plotting route: " + objRoutes.routes);
                var route = objRoutes.routes[0];
                if (route.legs.Count > 0)
                {
                    var routeInfo = route.legs[0];
                    //BindMapMarker();
                    txtTimeZone.Text = string.Format("Distance {0} Duration {1}for ERoad", routeInfo.distance.text, routeInfo.duration.text);
                }
            }
        }

        #endregion
    }
}