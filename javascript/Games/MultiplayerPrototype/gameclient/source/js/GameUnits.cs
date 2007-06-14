using ScriptCoreLib;
using ScriptCoreLib.JavaScript;

namespace gameclient.source.js
{
    [Script]
    public enum StandardDirections
    {
        North = 0,
        NorthWest = 4,
        West = 8,
        SouthWest = 12,
        South = 16,
        SouthEast = 20,
        East = 24,
        NorthEast = 28,

        PercisionConstant = 32
    }

    [Script]
    public class MyGameWorld
    {

    }

    [Script]
    public class MyGameUnit
    {

        /* what does a unit have
         * - direction
         * - ability deploy form a certain directon
         * - move into the direction
         */
    }
}
