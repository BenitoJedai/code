package  
{
	import Box2D.Collision.b2AABB;
	import Box2D.Collision.Shapes.b2PolygonDef;
	import Box2D.Collision.Shapes.b2PolygonShape;
	import Box2D.Collision.Shapes.b2Shape;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.b2Body;
	import Box2D.Dynamics.b2BodyDef;
	import Box2D.Dynamics.b2DebugDraw;
	import Box2D.Dynamics.b2World;
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.BlendMode;
	import flash.display.Shape;
	import flash.display.Sprite;
	import flash.events.Event;
	
	/**
	 * ...
	 * @author Salt
	 */
	public class Main extends Sprite
	{
		private const velocityBase:Number = 100;
		
		private const distanceToProjectShadows:Number = 1000;
		
		private var world:b2World;
		private var shadows:Shape = new Shape();
		private var debugSprite:Sprite = new Sprite();
		
		public function Main(shapesToAdd_:int) 
		{
			var shadowMap:Sprite = new Sprite();
			shadowMap.blendMode = BlendMode.LAYER;
			shadowMap.addChild(new Bitmap(new BitmapData(300, 300, false, 0xb0b0b0)));
			shadowMap.addChild(shadows);
			
			addChild(shadowMap);
			addChild(debugSprite);
			debugSprite.blendMode = BlendMode.LAYER;
			
			createWorld();
			for (var i:int = 0; i < shapesToAdd_; i++)
			{
				addRandomShape(int(3 + Math.random()*5), 7 + Math.random() * 20);
			}
			
			addEventListener(Event.ENTER_FRAME, function (e:Event):void
			{
				tickWorld();
				drawShadows();
			});
		}
		
		private function tickWorld():void
		{
			var dt:Number = 1 / 60;
			world.Step(dt, 1);
		}
		
		private function drawShadows():void
		{
			shadows.graphics.clear();
			shadows.graphics.lineStyle(1, 0, 0);
			var light:b2Vec2 = new b2Vec2(mouseX, mouseY);
			
			for (var body:b2Body = world.GetBodyList(); body; body = body.GetNext())
			{
				if (!body.IsStatic())
				{
					for (var shape:b2Shape = body.GetShapeList(); shape; shape = shape.GetNext())
					{
						if (shape is b2PolygonShape)
						{
							//move the vertices into a vector, because I like vectors.
							// also converting from local position to world position.
							var tempVertices:Array = (shape as b2PolygonShape).GetVertices();
							var vertices:Vector.<b2Vec2> = new Vector.<b2Vec2>((shape as b2PolygonShape).GetVertexCount(), true);
							var i:int;
							for (i = 0; i < vertices.length; i++)
							{
								vertices[i] = tempVertices[i];
								vertices[i] = body.GetWorldPoint(vertices[i]);
							}
							var startVertex:b2Vec2, endVertex:b2Vec2, edgeMidPoint:b2Vec2;
							for (i = 0; i < vertices.length; i++)
							{
								if (i == 0)
								{
									startVertex = vertices[vertices.length - 1];
								}
								else
								{
									startVertex = vertices[i - 1];
								}
								endVertex = vertices[i];
								edgeMidPoint = startVertex.Copy();
								edgeMidPoint.Add(endVertex);
								edgeMidPoint.Multiply(0.5);
								
								if (doesEdgeCastShadow(startVertex, endVertex, light))
								{
									var projectedPoint:b2Vec2;
									shadows.graphics.beginFill(0, 1);
									shadows.graphics.moveTo(startVertex.x, startVertex.y);
									projectedPoint = projectPoint(startVertex, light, distanceToProjectShadows);
									shadows.graphics.lineTo(projectedPoint.x, projectedPoint.y);
									projectedPoint = projectPoint(endVertex, light, distanceToProjectShadows);
									shadows.graphics.lineTo(projectedPoint.x, projectedPoint.y);
									shadows.graphics.lineTo(endVertex.x, endVertex.y);
									shadows.graphics.endFill();
								}
							}
						}
					}
				}
			}
		}
		private function doesEdgeCastShadow(start_:b2Vec2, end_:b2Vec2, light_:b2Vec2):Boolean
		{
			//var normal:b2Vec2 = new b2Vec2(start_.y - end_.y, end_.x - start_.x);
			
			var startToEnd:b2Vec2 = end_.Copy();
			startToEnd.Subtract(start_);
			
			var normal:b2Vec2 = new b2Vec2(startToEnd.y, -1 * startToEnd.x);
			
			var lightToStart:b2Vec2 = start_.Copy();
			lightToStart.Subtract(light_);
			
			if (dotProduct(normal, lightToStart) > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		// the first version of projectPoint, simply doubles the light-to-vertex vector (ignores projectDistance_ parameter)
		//private function projectPoint(point_:b2Vec2, light_:b2Vec2, projectDistance_:Number = 0):b2Vec2
		//{
			//var lightToPoint:b2Vec2 = point_.Copy();
			//lightToPoint.Subtract(light_);
			//lightToPoint.Add(point_);
			//return (lightToPoint);
		//}
		
		// more sensible version of projectPoint, places the projected vertex at a specified distance from the original
		private function projectPoint(point_:b2Vec2, light_:b2Vec2, projectDistance_:Number):b2Vec2
		{
			var lightToPoint:b2Vec2 = point_.Copy();
			lightToPoint.Subtract(light_);
			
			var vectorToAdd:b2Vec2 = lightToPoint.Copy();
			vectorToAdd.Normalize();
			vectorToAdd.Multiply(projectDistance_);
			
			var projectedPoint:b2Vec2 = point_.Copy();
			projectedPoint.Add(vectorToAdd);
			return projectedPoint;
		}
		
		private function dotProduct(vecA_:b2Vec2, vecB_:b2Vec2):Number
		{
			//dp = a.x*b.x + a.y*b.y;
			
			return (vecA_.x * vecB_.x + vecA_.y * vecB_.y);
		}
		
		private function createWorld():void
		{
			var worldAABB:b2AABB = new b2AABB();
			worldAABB.lowerBound.Set( -100, -100);
			worldAABB.upperBound.Set( +400, 400);
			
			var gravityVector:b2Vec2 = new b2Vec2(0, 0);
			
			world = new b2World(worldAABB, gravityVector, false);
			
			//debug draw stuff:
			//----------------------
			
			var dbgDraw:b2DebugDraw = new b2DebugDraw();
			
			dbgDraw.m_sprite = debugSprite;
			dbgDraw.m_drawScale = 1.0;
			dbgDraw.m_fillAlpha = 1.0;
			dbgDraw.m_lineThickness = 1.0;
			dbgDraw.m_drawFlags = b2DebugDraw.e_shapeBit; //use | to add extra bits.
			world.SetDebugDraw(dbgDraw);
			//---------------------
			
			//create borders:
			var shapeDef:b2PolygonDef;
			var bodyDef:b2BodyDef;
			var body:b2Body;
			
			//left
			shapeDef = new b2PolygonDef();
			shapeDef.SetAsBox(8, 200);
			shapeDef.density = 0;
			shapeDef.restitution = 1;
			bodyDef = new b2BodyDef();
			bodyDef.position.Set( -10, 150);
			body = world.CreateBody(bodyDef);
			body.CreateShape(shapeDef);
			
			//right
			shapeDef = new b2PolygonDef();
			shapeDef.SetAsBox(8, 200);
			shapeDef.density = 0;
			shapeDef.restitution = 1;
			bodyDef = new b2BodyDef();
			bodyDef.position.Set( 310, 150);
			body = world.CreateBody(bodyDef);
			body.CreateShape(shapeDef);
			
			//top
			shapeDef = new b2PolygonDef();
			shapeDef.SetAsBox(200, 8);
			shapeDef.density = 0;
			shapeDef.restitution = 1;
			bodyDef = new b2BodyDef();
			bodyDef.position.Set( 150, -9);
			body = world.CreateBody(bodyDef);
			body.CreateShape(shapeDef);
			
			//bottom
			shapeDef = new b2PolygonDef();
			shapeDef.SetAsBox(200, 8);
			shapeDef.density = 0;
			shapeDef.restitution = 1;
			bodyDef = new b2BodyDef();
			bodyDef.position.Set( 150, 310);
			body = world.CreateBody(bodyDef);
			body.CreateShape(shapeDef);
			
		}
		
		private function addRandomShape(sides_:int, radius_:Number):void
		{
			var shapeDef:b2PolygonDef;
			var bodyDef:b2BodyDef;
			var body:b2Body;
			
			shapeDef = new b2PolygonDef();
			shapeDef.vertexCount = sides_;
			shapeDef.vertices = new Array();
			
			var angle:Number;
			for (var i:int = 0; i < sides_; i++)
			{
				angle = -(Math.PI * 2 * (i + 1)) / sides_;
				shapeDef.vertices.push(new b2Vec2(radius_ * Math.sin(angle), radius_ * Math.cos(angle)));
			}
			
			shapeDef.density = 10;
			shapeDef.restitution = 1;
			
			bodyDef = new b2BodyDef();
			bodyDef.angle = Math.random() * Math.PI * 2;
			bodyDef.position.Set(50 + Math.random() * 200, 50 + Math.random() * 200);
			bodyDef.angularDamping = 0.6;
			
			body = world.CreateBody(bodyDef);
			body.CreateShape(shapeDef);
			body.SetMassFromShapes();
			
			var startVelocity:b2Vec2 = new b2Vec2( -velocityBase + Math.random() * 2 * velocityBase, -velocityBase + Math.random() * 2 * velocityBase);
			
			body.SetLinearVelocity(startVelocity);
		}
		
	}
	
}