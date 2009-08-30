using System;
using java.util;
using ScriptCoreLib;


namespace DelaunayExample.Java
{
	[Script]
	public class Triangulation
	{
		private HashMap _neighbors;  // Maps Simplex to Set of neighbors

		/**
		 * Constructor.
		 * @param simplex the initial Simplex.
		 */
		public Triangulation(Simplex simplex)
		{
			this._neighbors = new HashMap();
			this._neighbors.put(simplex, new HashSet());
		}

		/**
		 * String representation.
		 * Shows number of simplices currently in the Triangulation.
		 * @return a String representing the Triangulation
		 */
		public String toString()
		{
			return "Triangulation (with " + _neighbors.size() + " elements)";
		}

		/**
		 * Size (# of Simplices) in Triangulation.
		 * @return the number of Simplices in this Triangulation
		 */
		public int size()
		{
			return _neighbors.size();
		}

		/**
		 * True iff the simplex is in this Triangulation.
		 * @param simplex the simplex to check
		 * @return true iff the simplex is in this Triangulation
		 */
		public bool contains(Simplex simplex)
		{
			return this._neighbors.containsKey(simplex);
		}

		/**
		 * Iterator.
		 * @return an iterator for every Simplex in the Triangulation
		 */
		public Iterator iterator()
		{
			return Collections.unmodifiableSet(this._neighbors.keySet()).iterator();
		}

		/**
		 * Print stuff about a Triangulation.
		 * Used for debugging.
		 */
		public void printStuff()
		{
			var remember = Simplex.moreInfo;
			Console.WriteLine("Neighbor data for " + this);
			for (Iterator it = _neighbors.keySet().iterator(); it.hasNext(); )
			{
				Simplex simplex = (Simplex)it.next();
				Simplex.moreInfo = true;
				Console.WriteLine("    " + simplex + ":");
				Simplex.moreInfo = false;
				for (Iterator otherIt = ((Set)_neighbors.get(simplex)).iterator();
					 otherIt.hasNext(); )
					Console.WriteLine(" " + otherIt.next());
				Console.WriteLine();
			}
			Simplex.moreInfo = remember;
		}

		/* Navigation */

		/**
		 * Report neighbor opposite the given vertex of simplex.
		 * @param vertex a vertex of simplex
		 * @param simplex we want the neighbor of this Simplex
		 * @return the neighbor opposite vertex of simplex; null if none
		 * @throws IllegalArgumentException if vertex is not in this Simplex
		 */
		public Simplex neighborOpposite(Object vertex, Simplex simplex)
		{
			if (!simplex.contains(vertex))
				throw new InvalidOperationException("Bad vertex; not in simplex");

			for (Iterator it = ((Set)_neighbors.get(simplex)).iterator(); it.hasNext(); )
			{
				Simplex s = (Simplex)it.next();
				for (Iterator otherIt = simplex.iterator(); otherIt.hasNext(); )
				{
					var v = otherIt.next();
					if (v.Equals(vertex)) continue;
					if (!s.contains(v))
					{
						s = null;
						break;
					}
				}
				
				if (s == null)
					continue;

				return s;
			}
			return null;
		}

		/**
		 * Report neighbors of the given simplex.
		 * @param simplex a Simplex
		 * @return the Set of neighbors of simplex
		 */
		public Set neighbors(Simplex simplex)
		{
			return new HashSet((Set)this._neighbors.get(simplex));
		}

		/* Modification */

		/**
		 * Update by replacing one set of Simplices with another.
		 * Both sets of simplices must fill the same "hole" in the
		 * Triangulation.
		 * @param oldSet set of Simplices to be replaced
		 * @param newSet set of replacement Simplices
		 */
		public void update(Set oldSet,
							Set newSet)
		{
			// Collect all simplices neighboring the oldSet
			Set allNeighbors = new HashSet();
			for (Iterator it = oldSet.iterator(); it.hasNext(); )
				allNeighbors.addAll((Set)_neighbors.get((Simplex)it.next()));
			// Delete the oldSet
			for (Iterator it = oldSet.iterator(); it.hasNext(); )
			{
				Simplex simplex = (Simplex)it.next();
				for (Iterator otherIt = ((Set)_neighbors.get(simplex)).iterator();
					 otherIt.hasNext(); )
					((Set)_neighbors.get(otherIt.next())).remove(simplex);
				_neighbors.remove(simplex);
				allNeighbors.remove(simplex);
			}
			// Include the newSet simplices as possible neighbors
			allNeighbors.addAll(newSet);
			// Create entries for the simplices in the newSet
			for (Iterator it = newSet.iterator(); it.hasNext(); )
				_neighbors.put((Simplex)it.next(), new HashSet());
			// Update all the neighbors info
			for (Iterator it = newSet.iterator(); it.hasNext(); )
			{
				Simplex s1 = (Simplex)it.next();
				for (Iterator otherIt = allNeighbors.iterator(); otherIt.hasNext(); )
				{
					Simplex s2 = (Simplex)otherIt.next();
					if (!s1.isNeighbor(s2)) continue;
					((Set)_neighbors.get(s1)).add(s2);
					((Set)_neighbors.get(s2)).add(s1);
				}
			}
		}
	}
}
