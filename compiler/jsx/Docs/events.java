// http://weblogs.java.net/blog/tball/archive/2006/08/will_java_final.html
// sun: http://java.sun.com/docs/white/delegates.html
// ms: http://msdn.microsoft.com/archive/default.asp?url=/archive/en-us/dnarvj/html/msdn_deltruth.asp

public interface IListener
{
	void Invoke1(T t, S s);
}


// they call it the Adapter?
public abstract class VirtualIListener extends IListener
{
	public void Invoke1(T t, S s)
	{
		// nop;
	}
}


public class Control
{
	public void add_Event1(IListener e)
	{
	}

	public void remove_Event1(IListener e)
	{
	}

	IListener[] _Event1;

	public void raise_Event1(T t, S s)
	{
		_Event1.Invoke1(t, s);
	}
}

class Program
{
	void Main()
	{
		Control c = new Control();

		c.add_Event1(
				new VirtualIListener ()
				{
					public void Invoke1(T t, S s)
					{
						// nop;
					}					
				}
			);
	}
}