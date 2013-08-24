This ZIP file contains a set of map images for the 21 multiplayer maps
available in Blizzard's Warcraft I.  They were generated using the Stratlas
RTS map imaging tool ( http://stratlas.sourceforge.net ) against a set of
network save games.

Settings used were the defaults - 2 Peasants per army.

Warcraft doesn't support maps in the modern RTS sense: they are not editable
by end users, and somewhat generated at run-time, so there is no start
position information.  To produce these I restarted the same map repeatedly
and saved the game, then ran the outputs through Stratlas, and finally built a
composite image showing all the different start locations.

Interesting observations:
 * There are four start locations on Forest and Swamp maps, and three in
     Dungeon maps.
 * Contrary to some reports Warcraft doesn't actually have a Random Map
     Generator.  The Random category gives five options: Forest, Swamp,
     Dungeon, Random Town and Random Map.  The first three choose one of the
     seven maps from the type you select.  Random Town chooses a map from the
     collection of Forest and Swamp (i.e. a map where you can build a town),
     and Random Map picks one of the 21 at random.  Of course start locations
     are chosen at random too from the fixed possibilities.
 * The game plays quite well in DosBOX... : )

If you find these useful I'd love to hear it!  Also, check out the stratlas
tool listed above if you are interested - it can generate full-size map images
from a variety of 2D RTS games.

Enjoy!

-Greg Kennedy
kennedy.greg@gmail.com