// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.AttributeSet

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/AttributeSet.html
	[Script(IsNative = true)]
	public interface AttributeSet
	{
		/// <summary>
		/// Returns true if this set contains this attribute with an equal value.
		/// </summary>
		bool containsAttribute(object @name, object @value);

		/// <summary>
		/// Returns true if this set contains all the attributes with equal values.
		/// </summary>
		bool containsAttributes(AttributeSet @attributes);

		/// <summary>
		/// Returns an attribute set that is guaranteed not
		/// to change over time.
		/// </summary>
		AttributeSet copyAttributes();

		/// <summary>
		/// Fetches the value of the given attribute.
		/// </summary>
		object getAttribute(object @key);

		/// <summary>
		/// Returns the number of attributes contained in this set.
		/// </summary>
		int getAttributeCount();

		/// <summary>
		/// Returns an enumeration over the names of the attributes in the set.
		/// </summary>
		Enumeration getAttributeNames();

		/// <summary>
		/// Gets the resolving parent.
		/// </summary>
		AttributeSet getResolveParent();

		/// <summary>
		/// Checks whether the named attribute has a value specified in
		/// the set without resolving through another attribute
		/// set.
		/// </summary>
		bool isDefined(object @attrName);

		/// <summary>
		/// Determines if the two attribute sets are equivalent.
		/// </summary>
		bool isEqual(AttributeSet @attr);

	}
}
