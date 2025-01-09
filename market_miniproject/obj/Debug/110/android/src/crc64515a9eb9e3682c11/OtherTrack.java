package crc64515a9eb9e3682c11;


public class OtherTrack
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
		mono.android.Runtime.register ("market_miniproject.Classes.OtherTrack, market_miniproject", OtherTrack.class, __md_methods);
	}


	public OtherTrack ()
	{
		super ();
		if (getClass () == OtherTrack.class)
			mono.android.TypeManager.Activate ("market_miniproject.Classes.OtherTrack, market_miniproject", "", this, new java.lang.Object[] {  });
	}

	public OtherTrack (int p0, java.lang.String p1, java.lang.String p2, int p3, double p4)
	{
		super ();
		if (getClass () == OtherTrack.class)
			mono.android.TypeManager.Activate ("market_miniproject.Classes.OtherTrack, market_miniproject", "System.Int32, mscorlib:System.String, mscorlib:System.String, mscorlib:System.Int32, mscorlib:System.Double, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3, p4 });
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
