package md5a0988c28110328bccc0a2b31338c0ba8;


public class UserMarkerInfo
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.InfoWindowAdapter
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getInfoContents:(Lcom/google/android/gms/maps/model/Marker;)Landroid/view/View;:GetGetInfoContents_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IInfoWindowAdapterInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_getInfoWindow:(Lcom/google/android/gms/maps/model/Marker;)Landroid/view/View;:GetGetInfoWindow_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IInfoWindowAdapterInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("TaximooTestApp.Droid.UserMarkerInfo, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserMarkerInfo.class, __md_methods);
	}


	public UserMarkerInfo () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserMarkerInfo.class)
			mono.android.TypeManager.Activate ("TaximooTestApp.Droid.UserMarkerInfo, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public UserMarkerInfo (android.view.LayoutInflater p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserMarkerInfo.class)
			mono.android.TypeManager.Activate ("TaximooTestApp.Droid.UserMarkerInfo, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.LayoutInflater, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.view.View getInfoContents (com.google.android.gms.maps.model.Marker p0)
	{
		return n_getInfoContents (p0);
	}

	private native android.view.View n_getInfoContents (com.google.android.gms.maps.model.Marker p0);


	public android.view.View getInfoWindow (com.google.android.gms.maps.model.Marker p0)
	{
		return n_getInfoWindow (p0);
	}

	private native android.view.View n_getInfoWindow (com.google.android.gms.maps.model.Marker p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
