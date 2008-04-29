using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/XML.html

    /// <summary>
    /// The XML class contains methods and properties for working with XML objects. The XML class (along with the XMLList, Namespace, and QName classes) implements the powerful XML-handling standards defined in ECMAScript for XML (E4X) specification (ECMA-357 edition 2).
    /// </summary>
    [Script(IsNative=true)]
    public sealed class XML
    {
        #region Properties
        /// <summary>
        /// [static] Determines whether XML comments are ignored when XML objects parse the source XML data.
        /// </summary>
        public bool ignoreComments { get; set; }

        /// <summary>
        /// [static] Determines whether XML processing instructions are ignored when XML objects parse the source XML data.
        /// </summary>
        public bool ignoreProcessingInstructions { get; set; }

        /// <summary>
        /// [static] Determines whether white space characters at the beginning and end of text nodes are ignored during parsing.
        /// </summary>
        public bool ignoreWhitespace { get; set; }

        /// <summary>
        /// [static] Determines the amount of indentation applied by the toString() and toXMLString() methods when the XML.prettyPrinting property is set to true.
        /// </summary>
        public int prettyIndent { get; set; }

        /// <summary>
        /// [static] Determines whether the toString() and toXMLString() methods normalize white space characters between some tags.
        /// </summary>
        public bool prettyPrinting { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a namespace to the set of in-scope namespaces for the XML object.
        /// </summary>
        public XML addNamespace(object ns)
        {
            return default(XML);
        }

        /// <summary>
        /// Appends the given child to the end of the XML object's properties.
        /// </summary>
        public XML appendChild(object child)
        {
            return default(XML);
        }

        /// <summary>
        /// Returns the XML value of the attribute that has the name matching the attributeName parameter.
        /// </summary>
        public XMLList attribute(object attributeName)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Returns a list of attribute values for the given XML object.
        /// </summary>
        public XMLList attributes()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Lists the children of an XML object.
        /// </summary>
        public XMLList child(object propertyName)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Identifies the zero-indexed position of this XML object within the context of its parent.
        /// </summary>
        public int childIndex()
        {
            return default(int);
        }

        /// <summary>
        /// Lists the children of the XML object in the sequence in which they appear.
        /// </summary>
        public XMLList children()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Lists the properties of the XML object that contain XML comments.
        /// </summary>
        public XMLList comments()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Compares the XML object against the given value parameter.
        /// </summary>
        public bool contains(XML value)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns a copy of the given XML object.
        /// </summary>
        public XML copy()
        {
            return default(XML);
        }

        /// <summary>
        /// [static] Returns an object with the following properties set to the default values: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
        /// </summary>
        public static object defaultSettings()
        {
            return default(object);
        }

        /// <summary>
        /// Returns all descendants (children, grandchildren, great-grandchildren, and so on) of the XML object that have the given name parameter.
        /// </summary>
        public XMLList descendants(object name)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Returns all descendants (children, grandchildren, great-grandchildren, and so on) of the XML object that have the given name parameter.
        /// </summary>
        public XMLList descendants()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Lists the elements of an XML object.
        /// </summary>
        public XMLList elements(object name)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Lists the elements of an XML object.
        /// </summary>
        public XMLList elements()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Checks to see whether the XML object contains complex content.
        /// </summary>
        public bool hasComplexContent()
        {
            return default(bool);
        }

        /// <summary>
        /// Checks to see whether the object has the property specified by the p parameter.
        /// </summary>
        public bool hasOwnProperty(string p)
        {
            return default(bool);
        }

        /// <summary>
        /// Checks to see whether the XML object contains simple content.
        /// </summary>
        public bool hasSimpleContent()
        {
            return default(bool);
        }

        /// <summary>
        /// Lists the namespaces for the XML object, based on the object's parent.
        /// </summary>
        public Array inScopeNamespaces()
        {
            return default(Array);
        }

        /// <summary>
        /// Inserts the given child2 parameter after the child1 parameter in this XML object and returns the resulting object.
        /// </summary>
        public object insertChildAfter(object child1, object child2)
        {
            return default(object);
        }

        /// <summary>
        /// Inserts the given child2 parameter before the child1 parameter in this XML object and returns the resulting object.
        /// </summary>
        public object insertChildBefore(object child1, object child2)
        {
            return default(object);
        }

        /// <summary>
        /// For XML objects, this method always returns the integer 1.
        /// </summary>
        public int length()
        {
            return default(int);
        }

        /// <summary>
        /// Gives the local name portion of the qualified name of the XML object.
        /// </summary>
        public object localName()
        {
            return default(object);
        }

        /// <summary>
        /// Gives the qualified name for the XML object.
        /// </summary>
        public object name()
        {
            return default(object);
        }

        /// <summary>
        /// If no parameter is provided, gives the namespace associated with the qualified name of this XML object.
        /// </summary>
        public object @namespace(string prefix)
        {
            return default(object);
        }

        /// <summary>
        /// If no parameter is provided, gives the namespace associated with the qualified name of this XML object.
        /// </summary>
        public object @namespace()
        {
            return default(object);
        }

        /// <summary>
        /// Lists namespace declarations associated with the XML object in the context of its parent.
        /// </summary>
        public Array namespaceDeclarations()
        {
            return default(Array);
        }

        /// <summary>
        /// Specifies the type of node: text, comment, processing-instruction, attribute, or element.
        /// </summary>
        public string nodeKind()
        {
            return default(string);
        }

        /// <summary>
        /// For the XML object and all descendant XML objects, merges adjacent text nodes and eliminates empty text nodes.
        /// </summary>
        public XML normalize()
        {
            return default(XML);
        }

        /// <summary>
        /// Returns the parent of the XML object.
        /// </summary>
        public object parent()
        {
            return default(object);
        }

        /// <summary>
        /// Inserts a copy of the provided child object into the XML element before any existing XML properties for that element.
        /// </summary>
        public XML prependChild(object value)
        {
            return default(XML);
        }

        /// <summary>
        /// If a name parameter is provided, lists all the children of the XML object that contain processing instructions with that name.
        /// </summary>
        public XMLList processingInstructions(string name)
        {
            return default(XMLList);
        }

        /// <summary>
        /// If a name parameter is provided, lists all the children of the XML object that contain processing instructions with that name.
        /// </summary>
        public XMLList processingInstructions()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Checks whether the property p is in the set of properties that can be iterated in a for..in statement applied to the XML object.
        /// </summary>
        public bool propertyIsEnumerable(string p)
        {
            return default(bool);
        }

        /// <summary>
        /// Removes the given namespace for this object and all descendants.
        /// </summary>
        public XML removeNamespace(Namespace ns)
        {
            return default(XML);
        }

        /// <summary>
        /// Replaces the properties specified by the propertyName parameter with the given value parameter.
        /// </summary>
        public XML replace(object propertyName, XML value)
        {
            return default(XML);
        }

        /// <summary>
        /// Replaces the child properties of the XML object with the specified set of XML properties, provided in the value parameter.
        /// </summary>
        public XML setChildren(object value)
        {
            return default(XML);
        }

        /// <summary>
        /// Changes the local name of the XML object to the given name parameter.
        /// </summary>
        public void setLocalName(string name)
        {
        }

        /// <summary>
        /// Sets the name of the XML object to the given qualified name or attribute name.
        /// </summary>
        public void setName(string name)
        {
        }

        /// <summary>
        /// Sets the namespace associated with the XML object.
        /// </summary>
        public void setNamespace(Namespace ns)
        {
        }

        /// <summary>
        /// [static] Sets values for the following XML properties: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
        /// </summary>
        public static void setSettings(/* params */ object rest)
        {
        }

        /// <summary>
        /// [static] Retrieves the following properties: ignoreComments, ignoreProcessingInstructions, ignoreWhitespace, prettyIndent, and prettyPrinting.
        /// </summary>
        public static object settings()
        {
            return default(object);
        }

        /// <summary>
        /// Returns an XMLList object of all XML properties of the XML object that represent XML text nodes.
        /// </summary>
        public XMLList text()
        {
            return default(XMLList);
        }



        /// <summary>
        /// Returns a string representation of the XML object.
        /// </summary>
        public string toXMLString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the XML object.
        /// </summary>
        public XML valueOf()
        {
            return default(XML);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new XML object.
        /// </summary>
        public XML(object value)
        {
        }

        #endregion

    }
}
