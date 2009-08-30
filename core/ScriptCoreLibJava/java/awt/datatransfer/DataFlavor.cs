// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;
using java.io;
using java.lang;

namespace java.awt.datatransfer
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/datatransfer/DataFlavor.html
	[Script(IsNative = true)]
	public class DataFlavor
	{
		/// <summary>
		/// Constructs a new <code>DataFlavor</code>.
		/// </summary>
		public DataFlavor()
		{
		}

		/// <summary>
		/// Constructs a <code>DataFlavor</code> that represents a Java class.
		/// </summary>
		public DataFlavor(Class @representationClass, string @humanPresentableName)
		{
		}

		/// <summary>
		/// Constructs a <code>DataFlavor</code> from a <code>mimeType</code> string.
		/// </summary>
		public DataFlavor(string @mimeType)
		{
		}

		/// <summary>
		/// Constructs a <code>DataFlavor</code> that represents a
		/// <code>MimeType</code>.
		/// </summary>
		public DataFlavor(string @mimeType, string @humanPresentableName)
		{
		}

		/// <summary>
		/// Constructs a <code>DataFlavor</code> that represents a
		/// <code>MimeType</code>.
		/// </summary>
		public DataFlavor(string @mimeType, string @humanPresentableName, ClassLoader @classLoader)
		{
		}

		/// <summary>
		/// Returns a clone of this <code>DataFlavor</code>.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Tests a <code>DataFlavor</code> to this <code>DataFlavor</code> for
		/// equality.
		/// </summary>
		public bool Equals(DataFlavor @that)
		{
			return default(bool);
		}

		/// <summary>
		/// Tests an arbitrary <code>Object</code> to this <code>DataFlavor</code>
		/// for equality.
		/// </summary>
		public override bool Equals(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As inconsistent with <code>hashCode()</code> contract,
		/// use <code>isMimeTypeEqual(String)</code> instead.</I>
		/// </summary>
		public bool Equals(string @s)
		{
			return default(bool);
		}

		/// <summary>
		/// 
		/// </summary>
		public Class getDefaultRepresentationClass()
		{
			return default(Class);
		}

		/// <summary>
		/// 
		/// </summary>
		public string getDefaultRepresentationClassAsString()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the human presentable name for the data format that this
		/// <code>DataFlavor</code> represents.
		/// </summary>
		public string getHumanPresentableName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the MIME type string for this <code>DataFlavor</code>.
		/// </summary>
		public string getMimeType()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the human presentable name for this <code>DataFlavor</code>
		/// if <code>paramName</code> equals "humanPresentableName".
		/// </summary>
		public string getParameter(string @paramName)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the primary MIME type for this <code>DataFlavor</code>.
		/// </summary>
		public string getPrimaryType()
		{
			return default(string);
		}

		/// <summary>
		/// Gets a Reader for a text flavor, decoded, if necessary, for the expected
		/// charset (encoding).
		/// </summary>
		public Reader getReaderForText(Transferable @transferable)
		{
			return default(Reader);
		}

		/// <summary>
		/// Returns the <code>Class</code> which objects supporting this
		/// <code>DataFlavor</code> will return when this <code>DataFlavor</code>
		/// is requested.
		/// </summary>
		public Class getRepresentationClass()
		{
			return default(Class);
		}

		/// <summary>
		/// Returns the sub MIME type of this <code>DataFlavor</code>.
		/// </summary>
		public string getSubType()
		{
			return default(string);
		}

		/// <summary>
		/// Returns a <code>DataFlavor</code> representing plain text with Unicode
		/// encoding, where:
		/// </summary>
		public DataFlavor getTextPlainUnicodeFlavor()
		{
			return default(DataFlavor);
		}

		/// <summary>
		/// Returns hash code for this <code>DataFlavor</code>.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if the <code>DataFlavor</code> specified represents
		/// a list of file objects.
		/// </summary>
		public bool isFlavorJavaFileListType()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the <code>DataFlavor</code> specified represents
		/// a remote object.
		/// </summary>
		public bool isFlavorRemoteObjectType()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the <code>DataFlavor</code> specified represents
		/// a serialized object.
		/// </summary>
		public bool isFlavorSerializedObjectType()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether this <code>DataFlavor</code> is a valid text flavor for
		/// this implementation of the Java platform.
		/// </summary>
		public bool isFlavorTextType()
		{
			return default(bool);
		}

		/// <summary>
		/// Compares the <code>mimeType</code> of two <code>DataFlavor</code>
		/// objects.
		/// </summary>
		public bool isMimeTypeEqual(DataFlavor @dataFlavor)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the string representation of the MIME type passed in
		/// is equivalent to the MIME type of this <code>DataFlavor</code>.
		/// </summary>
		public bool isMimeTypeEqual(string @mimeType)
		{
			return default(bool);
		}

		/// <summary>
		/// Does the <code>DataFlavor</code> represent a serialized object?
		/// </summary>
		public bool isMimeTypeSerializedObject()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the representation class for this
		/// <code>DataFlavor</code> is <code>java.nio.ByteBuffer</code> or a
		/// subclass thereof.
		/// </summary>
		public bool isRepresentationClassByteBuffer()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the representation class for this
		/// <code>DataFlavor</code> is <code>java.nio.CharBuffer</code> or a
		/// subclass thereof.
		/// </summary>
		public bool isRepresentationClassCharBuffer()
		{
			return default(bool);
		}

		/// <summary>
		/// Does the <code>DataFlavor</code> represent a
		/// <code>java.io.InputStream</code>?
		/// </summary>
		public bool isRepresentationClassInputStream()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether the representation class for this
		/// <code>DataFlavor</code> is <code>java.io.Reader</code> or a subclass
		/// thereof.
		/// </summary>
		public bool isRepresentationClassReader()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the representation class is <code>Remote</code>.
		/// </summary>
		public bool isRepresentationClassRemote()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the representation class can be serialized.
		/// </summary>
		public bool isRepresentationClassSerializable()
		{
			return default(bool);
		}

		/// <summary>
		/// Tests a <code>DataFlavor</code> to this <code>DataFlavor</code> for
		/// equality.
		/// </summary>
		public bool match(DataFlavor @that)
		{
			return default(bool);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I></I>
		/// </summary>
		public string normalizeMimeType(string @mimeType)
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I></I>
		/// </summary>
		public string normalizeMimeTypeParameter(string @parameterName, string @parameterValue)
		{
			return default(string);
		}

		/// <summary>
		/// Restores this <code>DataFlavor</code> from a Serialized state.
		/// </summary>
		public void readExternal(ObjectInput @is)
		{
		}

		/// <summary>
		/// Selects the best text <code>DataFlavor</code> from an array of <code>
		/// DataFlavor</code>s.
		/// </summary>
		public DataFlavor selectBestTextFlavor(DataFlavor[] @availableFlavors)
		{
			return default(DataFlavor);
		}

		/// <summary>
		/// Sets the human presentable name for the data format that this
		/// <code>DataFlavor</code> represents.
		/// </summary>
		public void setHumanPresentableName(string @humanPresentableName)
		{
		}

		/// <summary>
		/// String representation of this <code>DataFlavor</code> and its
		/// parameters.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Tries to load a class from: the bootstrap loader, the system loader,
		/// the context loader (if one is present) and finally the loader specified.
		/// </summary>
		public Class tryToLoadClass(string @className, ClassLoader @fallback)
		{
			return default(Class);
		}

		/// <summary>
		/// Serializes this <code>DataFlavor</code>.
		/// </summary>
		public void writeExternal(ObjectOutput @os)
		{
		}

	}
}

