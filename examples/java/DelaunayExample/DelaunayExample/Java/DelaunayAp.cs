using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.swing;
using java.awt;
using java.awt.@event;
using java.util;

namespace DelaunayExample.Java
{
	[Script]
	public class DelaunayAp : javax.swing.JApplet, java.lang.Runnable
	{

		/**
		* Initialize the applet.
		* As recommended, the actual use of Swing components takes place in the
		* event-dispatching thread.
		*/
		public void init()
		{
			try
			{
				SwingUtilities.invokeAndWait(this);
			}
			catch //(Exception e) 
			{
				//System.err.println("Initialization failure");
			}
		}

		/**
		 * Set up the applet's GUI.
		 * As recommended, the init method executes this in the event-dispatching 
		 * thread.
		 */
		public void run()
		{
			Container pane = getContentPane();
			pane.setLayout(new BorderLayout());

			// Build the button controls
			JRadioButton voronoiButton = new JRadioButton("Voronoi Diagram");
			voronoiButton.setActionCommand("voronoi");
			JRadioButton delaunayButton = new JRadioButton("Delaunay Triangulation");
			delaunayButton.setActionCommand("delaunay");
			JButton clearButton = new JButton("Clear");
			clearButton.setActionCommand("clear");
			ButtonGroup group = new ButtonGroup();
			group.add(voronoiButton);
			group.add(delaunayButton);
			JPanel buttonPanel = new JPanel();
			buttonPanel.add(voronoiButton);
			buttonPanel.add(delaunayButton);
			buttonPanel.add(clearButton);
			pane.add(buttonPanel, "North");

			// Build the mouse-entry switches
			JLabel circleLabel = new JLabel("Show Empty Circles");
			circleLabel.setName("circles");
			JLabel delaunayLabel = new JLabel("Show Delaunay Edges");
			delaunayLabel.setName("delaunay");
			JLabel voronoiLabel = new JLabel("Show Voronoi Edges");
			voronoiLabel.setName("voronoi");
			JPanel switchPanel = new JPanel();
			switchPanel.add(circleLabel);
			switchPanel.add(new Label("     "));
			switchPanel.add(delaunayLabel);
			switchPanel.add(new Label("     "));
			switchPanel.add(voronoiLabel);
			pane.add(switchPanel, "South");

			// Build the graphics panel
			DelaunayPanel graphicsPanel = new DelaunayPanel();
			graphicsPanel.setBackground(Color.gray);
			pane.add(graphicsPanel, "Center");

			// Register the listeners
			voronoiButton.addActionListener(graphicsPanel);
			delaunayButton.addActionListener(graphicsPanel);
			clearButton.addActionListener(graphicsPanel);
			graphicsPanel.addMouseListener(graphicsPanel);
			circleLabel.addMouseListener(graphicsPanel);
			delaunayLabel.addMouseListener(graphicsPanel);
			voronoiLabel.addMouseListener(graphicsPanel);

			// Initialize the radio buttons
			voronoiButton.doClick();
		}

		/**
		 * Main program (used when run as application instead of applet).
		 */
		public static void main(String[] args)
		{
			DelaunayAp applet = new DelaunayAp();  // Create applet
			applet.init();                         // Perform applet initialization
			JFrame dWindow = new JFrame();         // Create window
			dWindow.setSize(700, 500);             // Set window size
			dWindow.setTitle("Voronoi/Delaunay Window");
			// Set window title
			dWindow.getContentPane().setLayout(new BorderLayout());
			// Specify layout manager
			dWindow.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			// Specify closing behavior
			dWindow.getContentPane().add(applet, "Center");
			// Place applet into window
			dWindow.setVisible(true);              // Show the window
		}
	}

	/**
	 * Graphics Panel for DelaunayAp.
	 */
	[Script]
	class DelaunayPanel : JPanel, ActionListener, MouseListener
	{

		private DelaunayTriangulation dt;     // The Delaunay triangulation
		private Simplex initialTriangle;      // The large initial triangle
		private int initialSize = 10000;      // Controls size of initial triangle
		private bool isVoronoi;            // True iff VoD instead of DT
		private bool showCircles = false;  // True iff showing empty circles
		private bool showDelaunay = false; // True iff showing Delaunay edges
		private bool showVoronoi = false;   // True iff showing Voronoi edges

		public bool debug = false;         // True iff printing info for debugging

		public Color voronoiColor = Color.magenta;
		public Color delaunayColor = Color.green;
		public int pointRadius = 3;
		private Graphics g;                   // Stored graphics context

		/**
		 * Create and initialize the DT.
		 */
		public DelaunayPanel()
		{
			initialTriangle = new Simplex(new Pnt[] {
            new Pnt(-initialSize, -initialSize),
            new Pnt( initialSize, -initialSize),
            new Pnt(           0,  initialSize)});
			dt = new DelaunayTriangulation(initialTriangle);
		}

		/* Events */

		/**
		 * Actions for button presses.
		 * @param e the ActionEvent
		 */
		public void actionPerformed(ActionEvent e)
		{
			String command = e.getActionCommand();
			if (debug) Console.WriteLine(command);
			if (command == "voronoi") isVoronoi = true;
			else if (command == "delaunay") isVoronoi = false;
			else if (command == "clear")
				dt = new DelaunayTriangulation(initialTriangle);
			repaint();
		}

		/**
		 * Mouse press.
		 * @param e the MouseEvent
		 */
		public void mousePressed(MouseEvent e)
		{
			if (e.getComponent() != this) return;
			Pnt point = new Pnt(e.getX(), e.getY());
			if (debug) Console.WriteLine("Click " + point);
			dt.delaunayPlace(point);
			repaint();
		}

		/**
		 * MouseEnter events.
		 * @param e the MouseEvent
		 */
		public void mouseEntered(MouseEvent e)
		{
			if (e.getComponent() == this) return;
			String name = e.getComponent().getName();
			if (debug) Console.WriteLine("Entering " + name);
			showCircles = (name == "circles");
			showDelaunay = (name == "delaunay");
			showVoronoi = (name == "voronoi");
			repaint();
		}

		/**
		 * MouseExit events.
		 * @param e the MouseEvent
		 */
		public void mouseExited(MouseEvent e)
		{
			if (e.getComponent() == this) return;
			if (debug) Console.WriteLine("Exiting");
			showCircles = false;
			showDelaunay = false;
			showVoronoi = false;
			repaint();
		}

		/**
		 * MouseClick event (not used, but needed for MouseListener).
		 */
		public void mouseClicked(MouseEvent e) { }

		/** 
		 * MouseRelease event (not used, but needed for MouseListener).
		 */
		public void mouseReleased(MouseEvent e) { }

		/* Basic Drawing Methods */

		/**
		 * Draw a point.
		 * @param point the Pnt to draw
		 */
		public void draw(Pnt point)
		{
			int r = pointRadius;
			int x = (int)point.coord(0);
			int y = (int)point.coord(1);
			g.fillOval(x - r, y - r, r + r, r + r);
		}

		/**
		 * Draw a line segment.
		 * @param endA one endpoint
		 * @param endB the other endpoint
		 */
		public void draw(Pnt endA, Pnt endB)
		{
			g.drawLine((int)endA.coord(0), (int)endA.coord(1),
					   (int)endB.coord(0), (int)endB.coord(1));
		}

		/**
		 * Draw a circle.
		 * @param center the center of the circle
		 * @param radius the circle's radius
		 * @param fillColor; null implies no fill
		 */
		public void draw(Pnt center, double radius, Color fillColor)
		{
			int x = (int)center.coord(0);
			int y = (int)center.coord(1);
			int r = (int)radius;
			if (fillColor != null)
			{
				Color temp = g.getColor();
				g.setColor(fillColor);
				g.fillOval(x - r, y - r, r + r, r + r);
				g.setColor(temp);
			}
			g.drawOval(x - r, y - r, r + r, r + r);
		}

		/* Higher Level Drawing Methods */

		/**
		 * Handles painting entire contents of DelaunayPanel.
		 * Called automatically; requested via call to repaint().
		 * @param g the Graphics context
		 */
		protected override void paintComponent(Graphics g)
		{
			// fixme: jsc has trouble calling base...
			//base.paintComponent(g);
			this.g = g;

			// Flood the drawing area with a "background" color
			Color temp = g.getColor();
			if (!isVoronoi) g.setColor(delaunayColor);
			else if (dt.contains(initialTriangle)) g.setColor(this.getBackground());
			else g.setColor(voronoiColor);
			g.fillRect(0, 0, this.getWidth(), this.getHeight());
			g.setColor(temp);

			// Draw the appropriate picture
			if (isVoronoi)
			{
				drawAllVoronoi();
				drawAllSites();
			}
			else drawAllDelaunay();

			// Draw any extra info due to the mouse-entry switches
			temp = g.getColor();
			if (isVoronoi) g.setColor(delaunayColor);
			else g.setColor(voronoiColor);
			if (showCircles) drawAllCircles();
			if (showDelaunay) drawAllDelaunay();
			if (showVoronoi) drawAllVoronoi();
			g.setColor(temp);
		}

		/**
		 * Draw all the Delaunay edges.
		 */
		public void drawAllDelaunay()
		{
			// Loop through all the edges of the DT (each is done twice)
			for (Iterator it = dt.iterator(); it.hasNext(); )
			{
				Simplex triangle = (Simplex)it.next();
				for (Iterator otherIt = triangle.facets().iterator(); otherIt.hasNext(); )
				{
					Set facet = (Set)otherIt.next();
					Pnt[] endpoint = (Pnt[])facet.toArray(new Pnt[2]);
					draw(endpoint[0], endpoint[1]);
				}
			}
		}

		/**
		 * Draw all the Voronoi edges.
		 */
		public void drawAllVoronoi()
		{
			// Loop through all the edges of the DT (each is done twice)
			for (Iterator it = dt.iterator(); it.hasNext(); )
			{
				Simplex triangle = (Simplex)it.next();
				for (Iterator otherIt = dt.neighbors(triangle).iterator(); otherIt.hasNext(); )
				{
					Simplex other = (Simplex)otherIt.next();
					Pnt p = Pnt.circumcenter((Pnt[])triangle.toArray(new Pnt[0]));
					Pnt q = Pnt.circumcenter((Pnt[])other.toArray(new Pnt[0]));
					draw(p, q);
				}
			}
		}

		/**
		 * Draw all the sites (i.e., the input points) of the DT.
		 */
		public void drawAllSites()
		{
			// Loop through all sites of the DT
			// Each is done several times, about 6 times each on average; this
			// can be proved via Euler's formula.
			for (Iterator it = dt.iterator(); it.hasNext(); )
			{
				for (Iterator otherIt = ((Simplex)it.next()).iterator(); otherIt.hasNext(); )
					draw((Pnt)otherIt.next());
			}
		}

		/**
		 * Draw all the empty circles (one for each triangle) of the DT.
		 */
		public void drawAllCircles()
		{
		// Loop through all triangles of the DT
			for (Iterator it = dt.iterator(); it.hasNext(); )
			{
				Simplex triangle = (Simplex)it.next();
				for (Iterator otherIt = initialTriangle.iterator(); otherIt.hasNext(); )
				{
					Pnt p = (Pnt)otherIt.next();
					if (triangle.contains(p))
					{
						triangle = null;
						break;
					}
				}
				if (triangle != null)
				{
					Pnt c = Pnt.circumcenter((Pnt[])triangle.toArray(new Pnt[0]));
					double radius = c.subtract((Pnt)triangle.iterator().next()).magnitude();
					draw(c, radius, null);
				}
			}
		}

	}
}
