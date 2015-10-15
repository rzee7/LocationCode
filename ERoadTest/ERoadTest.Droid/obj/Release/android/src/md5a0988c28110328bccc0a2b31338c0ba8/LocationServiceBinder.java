package md5a0988c28110328bccc0a2b31338c0ba8;


public class LocationServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TaximooTestApp.Droid.LocationServiceBinder, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocationServiceBinder.class, __md_methods);
	}


	public LocationServiceBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocationServiceBinder.class)
			mono.android.TypeManager.Activate ("TaximooTestApp.Droid.LocationServiceBinder, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public LocationServiceBinder (md5a0988c28110328bccc0a2b31338c0ba8.LocationService p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocationServiceBinder.class)
			mono.android.TypeManager.Activate ("TaximooTestApp.Droid.LocationServiceBinder, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "TaximooTestApp.Droid.LocationService, TaximooTestApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

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
