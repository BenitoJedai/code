using ScriptCoreLib.PHP.IO;
using ScriptCoreLib.Shared;
using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Runtime;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.PHP.Net
{
    [Script]
    public class SKLDAPClient : LDAPClient, IDisposable
    {

        public void ToConsole()
        {
            int i = 0;

            foreach (LDAPClient.Entry v in this.Entries)
            {
                i++;

                //Native.Dump("#" + i, var.entry);
                Native.Message("#" + i,
                    "givenname: " + v.givenname + "\n" +
                    "dn: " + v.dn + "\n" +
                    "cn: " + v.cn + "\n" +
                    "sn: " + v.sn + "\n" +
                    "id: " + SKLDAPClient.GetID(v) + "\n" +
                    "gender: " + SKLDAPClient.GetGender(v) + "\n" +
                    "birth: " + SKLDAPClient.GetDateOfBirth(v) + "\n" +
                    "mail: " + v.mail + "\n"
                    );
            }

        }

        public SKLDAPClient()
        {
            Connect();
        }

        public void Connect()
        {
            base.Connect("ldap.sk.ee");
            base.Bind();
        }

        //public void Close()
        //{
        //    base.Close();
        //}
    
        #region IDisposable Members

        public void  Dispose()
        {
            this.Close();
        }

        #endregion

        public static string GetID(Entry v)
        {
            string[] u = v.cn.Split(',');

            return u[2];
        }

        public static int GetGender(Entry v)
        {
            return int.Parse( GetID(v).Substring(0, 1));
        }

        public static string GetDateOfBirth(Entry v)
        {
            return GetID(v).Substring(1, 6);

        }
        public void FindPeople(string forename, string surname)
        {
            base.Search("ou=Authentication,o=ESTEID,c=EE", "(&(sn=" + surname + ")(givenname=" + forename + "))");
            base.GetEntries();
        }
        public void FindPeople(string forename, string surname, string cn)
        {
            base.Search("ou=Authentication,o=ESTEID,c=EE", "(&(sn=" + surname + ")(givenname=" + forename + ")(cn=" + cn + "))");
            base.GetEntries();

        }
    }

    [Script]
    public class LDAPClient
    {
        public static bool IsSupported
        {
            get
            {
                return Native.API.function_exists("ldap_connect");
            }
        }

        [Script(IsNative=true)]
        static class API
        {
       

            #region API import
            #region string ldap_8859_to_t61 ( string value )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value">string</param>
            [Script(IsNative = true)]
            public static string ldap_8859_to_t61(string value)
            {
                return default(string);
            }
            #endregion
            #region bool ldap_add ( resource link_identifier, string dn, array entry )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure. 
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="entry">array</param>
            [Script(IsNative = true)]
            public static bool ldap_add(object link_identifier, string dn, object entry)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_bind ( resource link_identifier [, string bind_rdn [, string bind_password]] )
            /// <summary>
            /// Binds to the LDAP directory with specified RDN and password. Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            [Script(IsNative = true)]
            public static bool ldap_bind(object link_identifier)
            {
                return default(bool);
            }
            /// <summary>
            /// Binds to the LDAP directory with specified RDN and password. Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="bind_rdn">string</param>
            [Script(IsNative = true)]
            public static bool ldap_bind(object link_identifier, string bind_rdn)
            {
                return default(bool);
            }
            /// <summary>
            /// Binds to the LDAP directory with specified RDN and password. Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="bind_rdn">string</param>
            /// <param name="bind_password">string</param>
            [Script(IsNative = true)]
            public static bool ldap_bind(object link_identifier, string bind_rdn, string bind_password)
            {
                return default(bool);
            }
            #endregion
            #region mixed ldap_compare ( resource link_identifier, string dn, string attribute, string value )
            /// <summary>
            /// Returns TRUE if value matches otherwise returns FALSE. Returns -1 on error. 
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="attribute">string</param>
            /// <param name="value">string</param>
            [Script(IsNative = true)]
            public static object ldap_compare(object link_identifier, string dn, string attribute, string value)
            {
                return default(object);
            }
            #endregion
            #region resource ldap_connect ( [string hostname [, int port]] )
            /// <summary>
            /// Returns a positive LDAP link identifier on success, or FALSE on error. When OpenLDAP 2.x.x is used, ldap_connect() will always return a resource as it does not actually connect but just initializes the connecting parameters. The actual connect happens with the next calls to ldap_* funcs, usually with ldap_bind(). 
            /// </summary>
            [Script(IsNative = true)]
            public static object ldap_connect()
            {
                return default(object);
            }
            /// <summary>
            /// Returns a positive LDAP link identifier on success, or FALSE on error. When OpenLDAP 2.x.x is used, ldap_connect() will always return a resource as it does not actually connect but just initializes the connecting parameters. The actual connect happens with the next calls to ldap_* funcs, usually with ldap_bind(). 
            /// </summary>
            /// <param name="hostname">string</param>
            [Script(IsNative = true)]
            public static object ldap_connect(string hostname)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a positive LDAP link identifier on success, or FALSE on error. When OpenLDAP 2.x.x is used, ldap_connect() will always return a resource as it does not actually connect but just initializes the connecting parameters. The actual connect happens with the next calls to ldap_* funcs, usually with ldap_bind(). 
            /// </summary>
            /// <param name="hostname">string</param>
            /// <param name="port">int</param>
            [Script(IsNative = true)]
            public static object ldap_connect(string hostname, int port)
            {
                return default(object);
            }
            #endregion
            #region int ldap_count_entries ( resource link_identifier, resource result_identifier )
            /// <summary>
            /// Returns number of entries in the result or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_identifier">resource</param>
            [Script(IsNative = true)]
            public static int ldap_count_entries(object link_identifier, object result_identifier)
            {
                return default(int);
            }
            #endregion
            #region bool ldap_delete ( resource link_identifier, string dn )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            [Script(IsNative = true)]
            public static bool ldap_delete(object link_identifier, string dn)
            {
                return default(bool);
            }
            #endregion
            #region string ldap_dn2ufn ( string dn )
            /// <summary>
            /// ldap_dn2ufn() function is used to turn a DN, specified by dn, into a more user-friendly form, stripping off type names. 
            /// </summary>
            /// <param name="dn">string</param>
            [Script(IsNative = true)]
            public static string ldap_dn2ufn(string dn)
            {
                return default(string);
            }
            #endregion
            #region string ldap_err2str ( int errno )
            /// <summary>
            /// Returns string error message.
            /// </summary>
            /// <param name="errno">int</param>
            [Script(IsNative = true)]
            public static string ldap_err2str(int errno)
            {
                return default(string);
            }
            #endregion
            #region int ldap_errno ( resource link_identifier )
            /// <summary>
            /// Return the LDAP error number of the last LDAP command for this link.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            [Script(IsNative = true)]
            public static int ldap_errno(object link_identifier)
            {
                return default(int);
            }
            #endregion
            #region string ldap_error ( resource link_identifier )
            /// <summary>
            /// Returns string error message.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            [Script(IsNative = true)]
            public static string ldap_error(object link_identifier)
            {
                return default(string);
            }
            #endregion
            #region array ldap_explode_dn ( string dn, int with_attrib )
            /// <summary>
            /// ldap_explode_dn() function is used to split the DN returned by ldap_get_dn() and breaks it up into its component parts. Each part is known as Relative Distinguished Name, or RDN. ldap_explode_dn() returns an array of all those components. with_attrib is used to request if the RDNs are returned with only values or their attributes as well. To get RDNs with the attributes (i.e. in attribute=value format) set with_attrib to 0 and to get only values set it to 1.
            /// </summary>
            /// <param name="dn">string</param>
            /// <param name="with_attrib">int</param>
            [Script(IsNative = true)]
            public static object ldap_explode_dn(string dn, int with_attrib)
            {
                return default(object);
            }
            #endregion
            #region string ldap_first_attribute ( resource link_identifier, resource result_entry_identifier, int &ber_identifier )
            /// <summary>
            /// Returns the first attribute in the entry on success and FALSE on error. 
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            /// <param name="ber_identifier">int</param>
            [Script(IsNative = true)]
            public static string ldap_first_attribute(object link_identifier, object result_entry_identifier, int ber_identifier)
            {
                return default(string);
            }
            #endregion
            #region resource ldap_first_reference ( resource link, resource result )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            [Script(IsNative = true)]
            public static object ldap_first_reference(object link, object result)
            {
                return default(object);
            }
            #endregion
            #region bool ldap_free_result ( resource result_identifier )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="result_identifier">resource</param>
            [Script(IsNative = true)]
            public static bool ldap_free_result(object result_identifier)
            {
                return default(bool);
            }
            #endregion
            #region array ldap_get_attributes ( resource link_identifier, resource result_entry_identifier )
            /// <summary>
            /// Returns a complete entry information in a multi-dimensional array on success and FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            [Script(IsNative = true)]
            public static object ldap_get_attributes(object link_identifier, object result_entry_identifier)
            {
                return default(object);
            }
            #endregion
            #region string ldap_get_dn ( resource link_identifier, resource result_entry_identifier )
            /// <summary>
            /// Returns the DN of the result entry and FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            [Script(IsNative = true)]
            public static string ldap_get_dn(object link_identifier, object result_entry_identifier)
            {
                return default(string);
            }
            #endregion
            #region bool ldap_get_option ( resource link_identifier, int option, mixed &retval )
            /// <summary>
            /// Sets retval to the value of the specified option. Returns TRUE on success or FALSE on failure. 
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="option">int</param>
            /// <param name="retval">mixed</param>
            [Script(IsNative = true)]
            public static bool ldap_get_option(object link_identifier, int option, object retval)
            {
                return default(bool);
            }
            #endregion
            #region array ldap_get_values_len ( resource link_identifier, resource result_entry_identifier, string attribute )
            /// <summary>
            /// Returns an array of values for the attribute on success and FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            /// <param name="attribute">string</param>
            [Script(IsNative = true)]
            public static object ldap_get_values_len(object link_identifier, object result_entry_identifier, string attribute)
            {
                return default(object);
            }
            #endregion
            #region array ldap_get_values ( resource link_identifier, resource result_entry_identifier, string attribute )
            /// <summary>
            /// Returns an array of values for the attribute on success and FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            /// <param name="attribute">string</param>
            [Script(IsNative = true)]
            public static object ldap_get_values(object link_identifier, object result_entry_identifier, string attribute)
            {
                return default(object);
            }
            #endregion
            #region resource ldap_list ( resource link_identifier, string base_dn, string filter [, array attributes [, int attrsonly [, int sizelimit [, int timelimit [, int deref]]]]] )
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter, object attributes)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter, object attributes, int attrsonly)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            /// <param name="deref">int</param>
            [Script(IsNative = true)]
            public static object ldap_list(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit, int deref)
            {
                return default(object);
            }
            #endregion
            #region bool ldap_mod_del ( resource link_identifier, string dn, array entry )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="entry">array</param>
            [Script(IsNative = true)]
            public static bool ldap_mod_del(object link_identifier, string dn, object entry)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_mod_replace ( resource link_identifier, string dn, array entry )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="entry">array</param>
            [Script(IsNative = true)]
            public static bool ldap_mod_replace(object link_identifier, string dn, object entry)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_modify ( resource link_identifier, string dn, array entry )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="entry">array</param>
            [Script(IsNative = true)]
            public static bool ldap_modify(object link_identifier, string dn, object entry)
            {
                return default(bool);
            }
            #endregion
            #region resource ldap_next_entry ( resource link_identifier, resource result_entry_identifier )
            /// <summary>
            /// Returns entry identifier for the next entry in the result whose entries are being read starting with ldap_first_entry(). If there are no more entries in the result then it returns FALSE.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="result_entry_identifier">resource</param>
            [Script(IsNative = true)]
            public static object ldap_next_entry(object link_identifier, object result_entry_identifier)
            {
                return default(object);
            }
            #endregion
            #region resource ldap_next_reference ( resource link, resource entry )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="entry">resource</param>
            [Script(IsNative = true)]
            public static object ldap_next_reference(object link, object entry)
            {
                return default(object);
            }
            #endregion
            #region bool ldap_parse_reference ( resource link, resource entry, array &referrals )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="entry">resource</param>
            /// <param name="referrals">array</param>
            [Script(IsNative = true)]
            public static bool ldap_parse_reference(object link, object entry, object referrals)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_parse_result ( resource link, resource result, int &errcode [, string &matcheddn [, string &errmsg [, array &referrals]]] )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            /// <param name="errcode">int</param>
            [Script(IsNative = true)]
            public static bool ldap_parse_result(object link, object result, int errcode)
            {
                return default(bool);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            /// <param name="errcode">int</param>
            /// <param name="matcheddn">string</param>
            [Script(IsNative = true)]
            public static bool ldap_parse_result(object link, object result, int errcode, string matcheddn)
            {
                return default(bool);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            /// <param name="errcode">int</param>
            /// <param name="matcheddn">string</param>
            /// <param name="errmsg">string</param>
            [Script(IsNative = true)]
            public static bool ldap_parse_result(object link, object result, int errcode, string matcheddn, string errmsg)
            {
                return default(bool);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            /// <param name="errcode">int</param>
            /// <param name="matcheddn">string</param>
            /// <param name="errmsg">string</param>
            /// <param name="referrals">array</param>
            [Script(IsNative = true)]
            public static bool ldap_parse_result(object link, object result, int errcode, string matcheddn, string errmsg, object referrals)
            {
                return default(bool);
            }
            #endregion
            #region resource ldap_read ( resource link_identifier, string base_dn, string filter [, array attributes [, int attrsonly [, int sizelimit [, int timelimit [, int deref]]]]] )
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter, object attributes)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter, object attributes, int attrsonly)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            /// <param name="deref">int</param>
            [Script(IsNative = true)]
            public static object ldap_read(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit, int deref)
            {
                return default(object);
            }
            #endregion
            #region bool ldap_rename ( resource link_identifier, string dn, string newrdn, string newparent, bool deleteoldrdn )
            /// <summary>
            /// The entry specified by dn is renamed/moved. The new RDN is specified by newrdn and the new parent/superior entry is specified by newparent. If the parameter deleteoldrdn is TRUE the old RDN value(s) is removed, else the old RDN value(s) is retained as non-distinguished values of the entry. Returns TRUE on success or FALSE on failure. 
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="dn">string</param>
            /// <param name="newrdn">string</param>
            /// <param name="newparent">string</param>
            /// <param name="deleteoldrdn">bool</param>
            [Script(IsNative = true)]
            public static bool ldap_rename(object link_identifier, string dn, string newrdn, string newparent, bool deleteoldrdn)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_sasl_bind ( resource link [, string binddn [, string password [, string sasl_mech [, string sasl_realm [, string sasl_authz_id [, string props]]]]]] )
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            /// <param name="password">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn, string password)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            /// <param name="password">string</param>
            /// <param name="sasl_mech">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn, string password, string sasl_mech)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            /// <param name="password">string</param>
            /// <param name="sasl_mech">string</param>
            /// <param name="sasl_realm">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn, string password, string sasl_mech, string sasl_realm)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            /// <param name="password">string</param>
            /// <param name="sasl_mech">string</param>
            /// <param name="sasl_realm">string</param>
            /// <param name="sasl_authz_id">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn, string password, string sasl_mech, string sasl_realm, string sasl_authz_id)
            {
                return default(bool);
            }
            /// <summary>
            /// Warning This function is currently not documented; only the argument list is available.
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="binddn">string</param>
            /// <param name="password">string</param>
            /// <param name="sasl_mech">string</param>
            /// <param name="sasl_realm">string</param>
            /// <param name="sasl_authz_id">string</param>
            /// <param name="props">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sasl_bind(object link, string binddn, string password, string sasl_mech, string sasl_realm, string sasl_authz_id, string props)
            {
                return default(bool);
            }
            #endregion
            #region resource ldap_search ( resource link_identifier, string base_dn, string filter [, array attributes [, int attrsonly [, int sizelimit [, int timelimit [, int deref]]]]] )
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter, object attributes)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter, object attributes, int attrsonly)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit)
            {
                return default(object);
            }
            /// <summary>
            /// Returns a search result identifier or FALSE on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="base_dn">string</param>
            /// <param name="filter">string</param>
            /// <param name="attributes">array</param>
            /// <param name="attrsonly">int</param>
            /// <param name="sizelimit">int</param>
            /// <param name="timelimit">int</param>
            /// <param name="deref">int</param>
            [Script(IsNative = true)]
            public static object ldap_search(object link_identifier, string base_dn, string filter, object attributes, int attrsonly, int sizelimit, int timelimit, int deref)
            {
                return default(object);
            }
            #endregion
            #region bool ldap_set_option ( resource link_identifier, int option, mixed newval )
            /// <summary>
            /// Sets the value of the specified option to be newval. Returns TRUE on success or FALSE on failure. on error.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            /// <param name="option">int</param>
            /// <param name="newval">mixed</param>
            [Script(IsNative = true)]
            public static bool ldap_set_option(object link_identifier, int option, object newval)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_set_rebind_proc ( resource link, callback callback )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="callback">callback</param>
            [Script(IsNative = true)]
            public static bool ldap_set_rebind_proc(object link, object callback)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_sort ( resource link, resource result, string sortfilter )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            /// <param name="result">resource</param>
            /// <param name="sortfilter">string</param>
            [Script(IsNative = true)]
            public static bool ldap_sort(object link, object result, string sortfilter)
            {
                return default(bool);
            }
            #endregion
            #region bool ldap_start_tls ( resource link )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="link">resource</param>
            [Script(IsNative = true)]
            public static bool ldap_start_tls(object link)
            {
                return default(bool);
            }
            #endregion
            #region string ldap_t61_to_8859 ( string value )
            /// <summary>
            /// 
            /// </summary>
            /// <param name="value">string</param>
            [Script(IsNative = true)]
            public static string ldap_t61_to_8859(string value)
            {
                return default(string);
            }
            #endregion
            #region bool ldap_unbind ( resource link_identifier )
            /// <summary>
            /// Returns TRUE on success or FALSE on failure.
            /// </summary>
            /// <param name="link_identifier">resource</param>
            [Script(IsNative = true)]
            public static bool ldap_unbind(object link_identifier)
            {
                return default(bool);
            }
            #endregion
            #endregion

            [Script(IsNative = true)]
            public static void ldap_close(object handle)
            {
            }


            [Script(IsNative = true)]
            public static object ldap_get_entries(object handle, object sr)
            {
                return default(object);
            }
        }

        object handle;

        public bool Empty
        {
            get
            {
                return Entries == null || Entries.Length == 0;
            }
        }

        public void Connect(string host)
        {
            handle = API.ldap_connect(host);
        }

        public void Bind()
        {
            API.ldap_bind(handle);
        }

        object sr;

    //=        - matches exact value
    //=*xxx  - matches values ending xxx
    //=xxx*  - matches values beginning xxx
    //=*xxx* - matches values containing xxx
    //=*      - matches all values (if set - NULLS are not returned)

    //>=xxx  - matches everthing from xxx to end of directory
    //<=xxx  - matches everything up to xxx in directory

    //~=xxx      - matches similar entries (not all systems)

    //Boolean operators for constructing complex search

    //&(term1)(term2)  - matches term1 AND term2
    //| (term1)(term2)  - matches term1 OR term2
    //!(term1)                - matches NOT term1
    //&(|(term1)(term2))(!(&(term1)(term2)) - matches XOR term1 term2

        public void Search(string base_dn, string filter)
        {
            sr = API.ldap_search(handle, base_dn, filter);
        }

        public object info;

        [Script]
        public class Entry
        {
            public ReadOnlyDualArray entry;

            public Entry(ReadOnlyDualArray e)
            {
                entry = e;
            }

            public string givenname
            {
                get
                {
                    return entry.GetChild("givenname").GetString(0);
                }
            }

            public string dn
            {
                get
                {
                    return entry.GetString("dn");
                }
            }

            public string cn
            {
                get
                {
                    return entry.GetChild("cn").GetString(0);
                }
            }

            public string sn
            {
                get
                {
                    return entry.GetChild("sn").GetString(0);
                }
            }

            public string mail
            {
                get
                {
                    return entry.GetChild("mail").GetString(0);
                }
            }
        }

        Entry[] _entries;

        public Entry[] Entries
        {
            get
            {
                return _entries;
            }
        }

        [Script]
        public class ReadOnlyDualArray
        {
            public IArray<object, object> _array;

            public ReadOnlyDualArray(object e)
            {
                _array = (IArray<object, object>)e;
            }

            public int GetInteger(string key)
            {
                return Convert.ToInt32( _array[key] );
            }


            public ReadOnlyDualArray GetChild(string key)
            {
                return new ReadOnlyDualArray(_array[key]);
            }

            public ReadOnlyDualArray GetChild(int index)
            {
                return new ReadOnlyDualArray(_array[index]);
            }

            public string GetString(string p)
            {
                return Convert.ToString(_array[p]);
            }

            public string GetString(int p)
            {
                return Convert.ToString( _array[p] );
            }
        }

        public int CountEntries()
        {
            return API.ldap_count_entries(handle, sr);
        }

        public void GetEntries()
        {
            info = API.ldap_get_entries(handle, sr);
            _entries = null;

            LDAPClient.ReadOnlyDualArray dual = new LDAPClient.ReadOnlyDualArray(this.info);

            List<LDAPClient.Entry> u = new List<Entry>();

            for (int i = 0; i < dual.GetInteger("count"); i++)
            {
                LDAPClient.Entry v = new LDAPClient.Entry(dual.GetChild(i));

                u.Add(v);
            }

            _entries = u.ToArray();

        }

        public void Close()
        {
            API.ldap_close(handle);
        }
    }
}
