using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://help.adobe.com/en_US/FlashPlatform/beta/reference/actionscript/3/flash/net/NetGroup.html
    [Script(IsNative = true)]
    public class NetGroup : EventDispatcher
    {
        #region Methods
        /// <summary>
        /// Adds objects from startIndex through endIndex, to the set of objects this node advertises to neighbors as objects for which it fulfills requests.
        /// </summary>
        public void addHaveObjects(double startIndex, double endIndex)
        {
        }

        /// <summary>
        /// Manually adds a record specifying that peerID is a member of the group.
        /// </summary>
        public bool addMemberHint(string peerID)
        {
            return default(bool);
        }

        /// <summary>
        /// Manually adds a neighbor by immediately connecting directly to the specified peerID, which must already be in this group.
        /// </summary>
        public bool addNeighbor(string peerID)
        {
            return default(bool);
        }

        /// <summary>
        /// Adds objects from startIndex through endIndex, to the set of objects to retrieve.
        /// </summary>
        public void addWantObjects(double startIndex, double endIndex)
        {
        }

        /// <summary>
        /// Disconnect from the group and close this NetGroup.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Converts a peerID to a group address suitable for use with the sendToNearest() method.
        /// </summary>
        public string convertPeerIDToGroupAddress(string peerID)
        {
            return default(string);
        }

        /// <summary>
        /// Denies a request received in a NetStatusEvent NetGroup.Replication.Request for an object previously advertised with addHaveObjects().
        /// </summary>
        public void denyRequestedObject(int requestID)
        {
        }

        /// <summary>
        /// Sends a message to all members of a group.
        /// </summary>
        public string post(object message)
        {
            return default(string);
        }

        /// <summary>
        /// Removes objects from startIndex through endIndex, from the set of objects this node advertises to neighbors as objects for which it fulfills requests.
        /// </summary>
        public void removeHaveObjects(double startIndex, double endIndex)
        {
        }

        /// <summary>
        /// Removes objects from startIndex through endIndex, from the set of objects to retrieve.
        /// </summary>
        public void removeWantObjects(double startIndex, double endIndex)
        {
        }

        /// <summary>
        /// Sends a message to all neighbors.
        /// </summary>
        public string sendToAllNeighbors(object message)
        {
            return default(string);
        }

        /// <summary>
        /// Sends a message to the neighbor (or local node) nearest to the specified group address.
        /// </summary>
        public string sendToNearest(object message, string groupAddress)
        {
            return default(string);
        }

        /// <summary>
        /// Sends a message to the neighbor specified by the sendMode parameter.
        /// </summary>
        public string sendToNeighbor(object message, string sendMode)
        {
            return default(string);
        }

        /// <summary>
        /// Satisfies the request as received by NetStatusEvent NetGroup.Replication.Request for an object previously advertised with the addHaveObjects() method.
        /// </summary>
        public void writeRequestedObject(int requestID, object @object)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a NetGroup on the specified NetConnection object and joins it to the group specified by groupspec.
        /// </summary>
        public NetGroup(NetConnection connection, string groupspec)
        {
        }

        #endregion


        #region Properties
        /// <summary>
        /// [read-only] Specifies the estimated number of members of the group, based on local neighbor density and assuming an even distribution of group addresses.
        /// </summary>
        public double estimatedMemberCount { get; private set; }

        /// <summary>
        /// [read-only] Returns a NetGroupInfo object whose properties provide Quality of Service statistics about this NetGroup's RTMFP peer-to-peer data transport.
        /// </summary>
        public NetGroupInfo info { get; private set; }

        /// <summary>
        /// [read-only] Specifies the start of the range of group addresses for which this node is the "nearest" and responsible.
        /// </summary>
        public string localCoverageFrom { get; private set; }

        /// <summary>
        /// [read-only] Specifies the end of the range of group addresses for which this node is the "nearest" and responsible.
        /// </summary>
        public string localCoverageTo { get; private set; }

        /// <summary>
        /// [read-only] Specifies the number of group members to which this node is directly connected.
        /// </summary>
        public double neighborCount { get; private set; }

        /// <summary>
        /// Specifies this node's routing receive mode as one of values in the NetGroupReceiveMode enum class.
        /// </summary>
        public string receiveMode { get; set; }

        /// <summary>
        /// Specifies the object replication fetch strategy.
        /// </summary>
        public string replicationStrategy { get; set; }

        #endregion

        #region Events
        /// <summary>
        /// Dispatched when a NetGroup object is reporting its status or error condition.	NetGroup
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<NetStatusEvent> netStatus;

        #endregion


    }
}
