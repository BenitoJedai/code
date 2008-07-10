using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/XMLList.html
    [Script(IsNative = true)]
    public sealed class XMLList
    {
        #region Methods
        /// <summary>
        /// Calls the attribute() method of each XML object and returns an XMLList object of the results.
        /// </summary>
        public XMLList attribute(object attributeName)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Calls the attributes() method of each XML object and returns an XMLList object of attributes for each XML object.
        /// </summary>
        public XMLList attributes()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Calls the child() method of each XML object and returns an XMLList object that contains the results in order.
        /// </summary>
        public XMLList child(object propertyName)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Calls the children() method of each XML object and returns an XMLList object that contains the results.
        /// </summary>
        public XMLList children()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Calls the comments() method of each XML object and returns an XMLList of comments.
        /// </summary>
        public XMLList comments()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Checks whether the XMLList object contains an XML object that is equal to the given value parameter.
        /// </summary>
        public bool contains(XML value)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns a copy of the given XMLList object.
        /// </summary>
        public XMLList copy()
        {
            return default(XMLList);
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
        /// Calls the elements() method of each XML object.
        /// </summary>
        public XMLList elements(object name)
        {
            return default(XMLList);
        }

        /// <summary>
        /// Calls the elements() method of each XML object.
        /// </summary>
        public XMLList elements()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Checks whether the XMLList object contains complex content.
        /// </summary>
        public bool hasComplexContent()
        {
            return default(bool);
        }

        /// <summary>
        /// Checks for the property specified by p.
        /// </summary>
        public bool hasOwnProperty(string p)
        {
            return default(bool);
        }

        /// <summary>
        /// Checks whether the XMLList object contains simple content.
        /// </summary>
        public bool hasSimpleContent()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the number of properties in the XMLList object.
        /// </summary>
        public int length()
        {
            return default(int);
        }

        /// <summary>
        /// Merges adjacent text nodes and eliminates empty text nodes for each of the following: all text nodes in the XMLList, all the XML objects contained in the XMLList, and the descendants of all the XML objects in the XMLList.
        /// </summary>
        public XMLList normalize()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Returns the parent of the XMLList object if all items in the XMLList object have the same parent.
        /// </summary>
        public object parent()
        {
            return default(object);
        }

        /// <summary>
        /// If a name parameter is provided, lists all the children of the XMLList object that contain processing instructions with that name.
        /// </summary>
        public XMLList processingInstructions(string name)
        {
            return default(XMLList);
        }

        /// <summary>
        /// If a name parameter is provided, lists all the children of the XMLList object that contain processing instructions with that name.
        /// </summary>
        public XMLList processingInstructions()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Checks whether the property p is in the set of properties that can be iterated in a for..in statement applied to the XMLList object.
        /// </summary>
        public bool propertyIsEnumerable(string p)
        {
            return default(bool);
        }

        /// <summary>
        /// Calls the text() method of each XML object and returns an XMLList object that contains the results.
        /// </summary>
        public XMLList text()
        {
            return default(XMLList);
        }

        /// <summary>
        /// Returns a string representation of all the XML objects in an XMLList object.
        /// </summary>
        public string toXMLString()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the XMLList object.
        /// </summary>
        public XMLList valueOf()
        {
            return default(XMLList);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new XMLList object.
        /// </summary>
        public XMLList(object value)
        {
        }

        #endregion


        public  XML this[int i]
        {
            get
            {
                return default(XML);
            }
        }
    }
}
