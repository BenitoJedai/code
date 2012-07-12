using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.util;

namespace DelaunayExample.Java
{

	[Script]
	public class DelaunayTriangulation : Triangulation
	{
		private Simplex mostRecent = null;       // Most recently inserted triangle
		public bool debug = false;            // Used for debugging

		/**
		 * Constructor.
		 * All sites must fall within the initial triangle.
		 * @param triangle the initial triangle
		 */
		public DelaunayTriangulation(Simplex triangle) : base(triangle)
		{
			mostRecent = triangle;
		}

		/**
		 * Locate the triangle with point (a Pnt) inside (or on) it.
		 * @param point the Pnt to locate
		 * @return triangle (Simplex<Pnt>) that holds the point; null if no such triangle
		 */
		public Simplex locate(Pnt point) {
        Simplex triangle = mostRecent;
        if (!this.contains(triangle)) triangle = null;
        
        // Try a directed walk (this works fine in 2D, but can fail in 3D)
        Set visited = new HashSet();
        while (triangle != null) {
            if (visited.contains(triangle)) { // This should never happen
                Console.WriteLine("Warning: Caught in a locate loop");
                break;
            }
            visited.add(triangle);
            // Corner opposite point
            Pnt corner = point.isOutside((Pnt[]) triangle.toArray(new Pnt[0]));
            if (corner == null) return triangle;
            triangle = this.neighborOpposite(corner, triangle);
        }
        // No luck; try brute force
        Console.WriteLine("Warning: Checking all triangles for " + point);
        for (Iterator it = this.iterator(); it.hasNext();) {
            Simplex tri = (Simplex) it.next();
            if (point.isOutside((Pnt[]) tri.toArray(new Pnt[0])) == null) return tri;
        }
        // No such triangle
		Console.WriteLine("Warning: No triangle holds " + point);
        return null;
    }

		/**
		 * Place a new point site into the DT.
		 * @param site the new Pnt
		 * @return set of all new triangles created
		 */
		public Set delaunayPlace(Pnt site) {
        Set newTriangles = new HashSet();
        Set oldTriangles = new HashSet();
        Set doneSet = new HashSet();
        LinkedList waitingQ = new LinkedList();
        
        // Locate containing triangle
        if (debug) Console.WriteLine("Locate");
        Simplex triangle = locate(site);
        
        // Give up if no containing triangle or if site is already in DT
		var triangle_null = triangle == null;
        if (triangle_null || triangle.contains(site)) return newTriangles;
        
        // Find Delaunay cavity (those triangles with site in their circumcircles)
        if (debug) Console.WriteLine("Cavity");
        waitingQ.add(triangle);
        while (!waitingQ.isEmpty()) {
            triangle = (Simplex) waitingQ.removeFirst();      
            if (site.vsCircumcircle((Pnt[]) triangle.toArray(new Pnt[0])) == 1) continue;
            oldTriangles.add(triangle);
            Iterator it = this.neighbors(triangle).iterator();
            for (; it.hasNext();) {
                Simplex tri = (Simplex) it.next();
                if (doneSet.contains(tri)) continue;
                doneSet.add(tri);
                waitingQ.add(tri);
            }
        }
        // Create the new triangles
        if (debug) Console.WriteLine("Create");
        for (Iterator it = Simplex.boundary(oldTriangles).iterator(); it.hasNext();) {
            Set facet = (Set) it.next();
            facet.add(site);
            newTriangles.add(new Simplex(facet));
        }
        // Replace old triangles with new triangles
		if (debug) Console.WriteLine("Update");
        this.update(oldTriangles, newTriangles);
        
        // Update mostRecent triangle
        if (!newTriangles.isEmpty()) mostRecent = (Simplex) newTriangles.iterator().next();
        return newTriangles;
    }

		/**
		 * Main program; used for testing.
		 */
		public static void main(String[] args) {
        Simplex tri = new Simplex(new Pnt[] {new Pnt(-10,10), new Pnt(10,10), new Pnt(0,-10)});
       Console.WriteLine("Triangle created: " + tri);
        DelaunayTriangulation dt = new DelaunayTriangulation(tri);
		Console.WriteLine("DelaunayTriangulation created: " + dt);
        dt.delaunayPlace(new Pnt(0,0));
        dt.delaunayPlace(new Pnt(1,0));
        dt.delaunayPlace(new Pnt(0,1));
        Console.WriteLine("After adding 3 points, the DelaunayTriangulation is a " + dt);
        dt.printStuff();
    }
	}
}
