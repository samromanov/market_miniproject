package crc64515a9eb9e3682c11;


public class RockTrack
	extends crc64515a9eb9e3682c11.Track
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("market_miniproject.Classes.RockTrack, market_miniproject", RockTrack.class, __md_methods);
	}


	public RockTrack ()
	{
		super ();
		if (getClass () == RockTrack.class)
			mono.android.TypeManager.Activate ("market_miniproject.Classes.RockTrack, market_miniproject", "", this, new java.lang.Object[] {  });
	}

	public RockTrack (java.lang.String p0, java.lang.String p1, int p2)
	{
		super ();
		if (getClass () == RockTrack.class)
			mono.android.TypeManager.Activate ("market_miniproject.Classes.RockTrack, market_miniproject", "System.String, mscorlib:System.String, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

	private java.util.ArrayList refList;
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
