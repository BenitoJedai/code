using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.net
{
    [Script(IsNative = true)]
    public class GroupSpecifier
    {
        #region Methods
        /// <summary>
        /// Causes the associated NetStream or NetGroup to make an initial neighbor connection to the specified peerID.
        /// </summary>
        public void addBootstrapPeer(string peerID)
        {
        }

        /// <summary>
        /// Causes the associated NetStream or NetGroup to join the specified IP multicast group and listen to the specified UDP port.
        /// </summary>
        public void addIPMulticastAddress(string address, object port, string source)
        {
        }

        /// <summary>
        /// Causes the associated NetStream or NetGroup to join the specified IP multicast group and listen to the specified UDP port.
        /// </summary>
        public void addIPMulticastAddress(string address, object port)
        {
        }

        /// <summary>
        /// Causes the associated NetStream or NetGroup to join the specified IP multicast group and listen to the specified UDP port.
        /// </summary>
        public void addIPMulticastAddress(string address)
        {
        }

        /// <summary>
        /// Returns a string that represents passwords for IP multicast publishing and for posting.
        /// </summary>
        public string authorizations()
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents a bootstrap peerID.
        /// </summary>
        public static string encodeBootstrapPeerIDSpec(string peerID)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents an IP multicast socket address.
        /// </summary>
        public static string encodeIPMulticastAddressSpec(string address, object port, string source)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents an IP multicast socket address.
        /// </summary>
        public static string encodeIPMulticastAddressSpec(string address, object port)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents an IP multicast socket address.
        /// </summary>
        public static string encodeIPMulticastAddressSpec(string address)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents a posting password.
        /// </summary>
        public static string encodePostingAuthorization(string password)
        {
            return default(string);
        }

        /// <summary>
        /// [static] Encodes and returns a string that represents a multicast publishing password.
        /// </summary>
        public static string encodePublishAuthorization(string password)
        {
            return default(string);
        }

        /// <summary>
        /// Returns the opaque groupspec string, including authorizations, that can be passed to NetStream and NetGroup constructors.
        /// </summary>
        public string groupspecWithAuthorizations()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the opaque groupspec string, without authorizations, that can be passed to NetStream and NetGroup constructors.
        /// </summary>
        public string groupspecWithoutAuthorizations()
        {
            return default(string);
        }

        /// <summary>
        /// Adds a strong pseudorandom tag to the groupspec to make it unique.
        /// </summary>
        public void makeUnique()
        {
        }

        /// <summary>
        /// Specifies whether a password is required to post in the NetGroup.
        /// </summary>
        public void setPostingPassword(string password, string salt)
        {
        }

        /// <summary>
        /// Specifies whether a password is required to post in the NetGroup.
        /// </summary>
        public void setPostingPassword(string password)
        {
        }

        /// <summary>
        /// Specifies whether a password is required to post in the NetGroup.
        /// </summary>
        public void setPostingPassword()
        {
        }

        /// <summary>
        /// Specifies whether a password is required to publish a multicast stream in the NetStream.
        /// </summary>
        public void setPublishPassword(string password, string salt)
        {
        }

        /// <summary>
        /// Specifies whether a password is required to publish a multicast stream in the NetStream.
        /// </summary>
        public void setPublishPassword(string password)
        {
        }

        /// <summary>
        /// Specifies whether a password is required to publish a multicast stream in the NetStream.
        /// </summary>
        public void setPublishPassword()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new GroupSpecifier object.
        /// </summary>
        public GroupSpecifier(string name)
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// Specifies whether information about group membership can be exchanged on IP multicast sockets.
        /// </summary>
        public bool ipMulticastMemberUpdatesEnabled { get; set; }

        /// <summary>
        /// Specifies whether streaming is enabled for the NetGroup.
        /// </summary>
        public bool multicastEnabled { get; set; }

        /// <summary>
        /// Specifies whether object replication is enabled for the NetGroup.
        /// </summary>
        public bool objectReplicationEnabled { get; set; }

        /// <summary>
        /// Specifies whether peer-to-peer connections are disabled for the NetGroup or NetStream.
        /// </summary>
        public bool peerToPeerDisabled { get; set; }

        /// <summary>
        /// Specifies whether posting is enabled for the NetGroup.
        /// </summary>
        public bool postingEnabled { get; set; }

        /// <summary>
        /// Specifies whether directed routing methods are enabled for the NetGroup.
        /// </summary>
        public bool routingEnabled { get; set; }

        /// <summary>
        /// Specifies whether members of the NetGroup can open a channel to the server.
        /// </summary>
        public bool serverChannelEnabled { get; set; }

        #endregion

    }
}
