package
{
	import com.adobe.utils.*;

	import flash.display.*;
	import flash.display3D.*;
	import flash.display3D.textures.*;
	import flash.events.*;
	import flash.geom.*;
	import flash.text.*;
	import flash.utils.*;
	
	public class ProceduralShapes extends Sprite
	{
		/** Number of degrees to rotate per millisecond */
		private static const ROTATION_SPEED:Number = 1;
		
		/** Axis to rotate about */
		private static const ROTATION_AXIS:Vector3D = new Vector3D(0, 1, 0);
		
		/** UI Padding */
		private static const PAD:Number = 5;
		
		/** Distance between shapes */
		private static const SHAPE_SPACING:Number = 1.5;
		
		[Embed(source="earth.jpg")]
		private static const TEXTURE:Class;
		
		/** Temporary matrix to avoid allocation during drawing */
		private static const TEMP_DRAW_MATRIX:Matrix3D = new Matrix3D();
		
		/** 3D context to draw with */
		private var context3D:Context3D;
		
		/** Shader program to draw with */
		private var program:Program3D;
		
		/** Texture of all shapes */
		private var texture:Texture;
		
		/** Camera viewing the 3D scene */
		private var camera:Camera3D;
		
		/** Shapes to draw */
		private var shapes:Vector.<Shape3D> = new Vector.<Shape3D>();
		
		/** Current rotation of all shapes (degrees) */
		private var rotationDegrees:Number = 0;
		
		/** Number of rows of shapes */
		private var rows:uint = 5;
		
		/** Number of columns of shapes */
		private var cols:uint = 5;
		
		/** Number of layers of shapes */
		private var layers:uint = 1;
		
		/** Framerate display */
		private var fps:TextField = new TextField();
		
		/** Last time the framerate display was updated */
		private var lastFPSUpdateTime:uint;
		
		/** Time when the last frame happened */
		private var lastFrameTime:uint;
		
		/** Number of frames since the framerate display was updated */
		private var frameCount:uint;
		
		/** 3D rendering driver display */
		private var driver:TextField = new TextField();
		
		/** Simulation statistics display */
		private var stats:TextField = new TextField();
		
		/**
		* Entry point
		*/
		public function ProceduralShapes()
		{
			stage.align = StageAlign.TOP_LEFT;
			stage.scaleMode = StageScaleMode.NO_SCALE;
			stage.frameRate = 60;
			
			var stage3D:Stage3D = stage.stage3Ds[0];
			stage3D.addEventListener(Event.CONTEXT3D_CREATE, onContextCreated);
			stage3D.requestContext3D();
		}
		
		protected function onContextCreated(ev:Event): void
		{
			// Setup context
			var stage3D:Stage3D = stage.stage3Ds[0];
			stage3D.removeEventListener(Event.CONTEXT3D_CREATE, onContextCreated);
			context3D = stage3D.context3D;            
			context3D.configureBackBuffer(
				stage.stageWidth,
				stage.stageHeight,
				0,
				true
			);
			
			// Setup camera
			camera = new Camera3D(
				0.1, // near
				100, // far
				stage.stageWidth / stage.stageHeight, // aspect ratio
				40*(Math.PI/180), // vFOV
				2, 3, 5, // position
				2, 3, 0, // target
				0, 1, 0 // up dir
			);
			
			// Setup UI
			fps.background = true;
			fps.backgroundColor = 0xffffffff;
			fps.autoSize = TextFieldAutoSize.LEFT;
			fps.text = "Getting FPS...";
			addChild(fps);
			
			driver.background = true;
			driver.backgroundColor = 0xffffffff;
			driver.text = "Driver: " + context3D.driverInfo;
			driver.autoSize = TextFieldAutoSize.LEFT;
			driver.y = fps.height;
			addChild(driver);
			
			stats.background = true;
			stats.backgroundColor = 0xffffffff;
			stats.text = "Getting stats...";
			stats.autoSize = TextFieldAutoSize.LEFT;
			stats.y = driver.y + driver.height;
			addChild(stats);
			
			makeButtons(
				"Move Forward", "Move Backward", null,
				"Move Left", "Move Right", null,
				"Move Up", "Move Down", null,
				"Yaw Left", "Yaw Right", null,
				"Pitch Up", "Pitch Down", null,
				"Roll Left", "Roll Right", null,
				null,
				"-Rows", "+Rows", null,
				"-Cols", "+Cols", null,
				"-Layers", "+Layers"
			);
			
			var assembler:AGALMiniAssembler = new AGALMiniAssembler();
			
			// Vertex shader
			var vertSource:String = "m44 op, va0, vc0\nmov v0, va1\n";
			assembler.assemble(Context3DProgramType.VERTEX, vertSource);
			var vertexShaderAGAL:ByteArray = assembler.agalcode;
			
			// Fragment shader
			var fragSource:String = "tex oc, v0, fs0 <2d,linear,mipnone>";
			assembler.assemble(Context3DProgramType.FRAGMENT, fragSource);
			var fragmentShaderAGAL:ByteArray = assembler.agalcode;
			
			// Shader program
			program = context3D.createProgram();
			program.upload(vertexShaderAGAL, fragmentShaderAGAL);
			
			// Setup textures
			var bmd:BitmapData = (new TEXTURE() as Bitmap).bitmapData;
			texture = context3D.createTexture(
				bmd.width,
				bmd.height,
				Context3DTextureFormat.BGRA,
				true
			);
			texture.uploadFromBitmapData(bmd);
			
			makeShapes();
			
			// Start the simulation
			addEventListener(Event.ENTER_FRAME, onEnterFrame);
		}
		
		private function makeShapes(): void
		{
			for each (var shape:Shape3D in shapes)
			{
				shape.dispose();
			}
			shapes.length = 0;
			
			for (var row:int = 0; row < rows; ++row)
			{
				for (var col:int = 0; col < cols; ++col)
				{
					for (var layer:int = 0; layer < layers; ++layer)
					{
						var posX:Number = col*SHAPE_SPACING;
						var posY:Number = row*SHAPE_SPACING;
						var posZ:Number = -layer*SHAPE_SPACING;
						
						var rand:Number = Math.random();
						if (rand < 1/6)
						{
							shape = new Cylinder3D(20, context3D, posX, posY, posZ);
						}
						else if (rand < 2/6)
						{
							shape = new Sphere3D(20, 20, context3D, posX, posY, posZ);
						}
						else if (rand < 3/6)
						{
							shape = new Cube3D(context3D, posX, posY, posZ);
						}
						else if (rand < 4/6)
						{
							shape = new Pyramid3D(context3D, posX, posY, posZ);
						}
						else if (rand < 5/6)
						{
							shape = new Circle3D(20, context3D, posX, posY, posZ);
						}
						else
						{
							shape = new Quad3D(context3D, posX, posY, posZ);
						}
						shapes.push(shape);
					}
				}
			}
			
			var numShapes:uint = rows*cols*layers;
			stats.text = "Shapes: (rows=" + rows
				+ ", cols=" + cols
				+ ", layers=" + layers
				+ ", total=" + numShapes + ")";
		}
		
		private function makeButtons(...labels): Number
		{
			var curX:Number = PAD;
			var curY:Number = stage.stageHeight - PAD;
			for each (var label:String in labels)
			{
				if (label == null)
				{
					curX = PAD;
					curY -= button.height + PAD;
					continue;
				}
				
				var tf:TextField = new TextField();
				tf.mouseEnabled = false;
				tf.selectable = false;
				tf.defaultTextFormat = new TextFormat("_sans");
				tf.autoSize = TextFieldAutoSize.LEFT;
				tf.text = label;
				tf.name = "lbl";
				
				var button:Sprite = new Sprite();
				button.buttonMode = true;
				button.graphics.beginFill(0xF5F5F5);
				button.graphics.drawRect(0, 0, tf.width+PAD, tf.height+PAD);
				button.graphics.endFill();
				button.graphics.lineStyle(1);
				button.graphics.drawRect(0, 0, tf.width+PAD, tf.height+PAD);
				button.addChild(tf);
				button.addEventListener(MouseEvent.CLICK, onButton);
				if (curX + button.width > stage.stageWidth - PAD)
				{
					curX = PAD;
					curY -= button.height + PAD;
				}
				button.x = curX;
				button.y = curY - button.height;
				addChild(button);
				
				curX += button.width + PAD;
			}
			
			return curY - button.height;
		}
		
		public static function makeCheckBox(
            label:String,
            checked:Boolean,
            callback:Function,
            labelFormat:TextFormat=null): Sprite
        {
            var sprite:Sprite = new Sprite();
 
            var tf:TextField = new TextField();
            tf.autoSize = TextFieldAutoSize.LEFT;
            tf.text = label;
            tf.background = true;
            tf.backgroundColor = 0xffffff;
            tf.selectable = false;
            tf.mouseEnabled = false;
            tf.setTextFormat(labelFormat || new TextFormat("_sans"));
            sprite.addChild(tf);
 
            var size:Number = tf.height;
 
            var background:Shape = new Shape();
            background.graphics.beginFill(0xffffff);
            background.graphics.drawRect(0, 0, size, size);
            background.x = tf.width + PAD;
            sprite.addChild(background);
 
            var border:Shape = new Shape();
            border.graphics.lineStyle(1, 0x000000);
            border.graphics.drawRect(0, 0, size, size);
            border.x = background.x;
            sprite.addChild(border);
 
            var check:Shape = new Shape();
            check.graphics.lineStyle(1, 0x000000);
            check.graphics.moveTo(0, 0);
            check.graphics.lineTo(size, size);
            check.graphics.moveTo(size, 0);
            check.graphics.lineTo(0, size);
            check.x = background.x;
            check.visible = checked;
            sprite.addChild(check);
 
            sprite.addEventListener(
                MouseEvent.CLICK,
                function(ev:MouseEvent): void
                {
                    checked = !checked;
                    check.visible = checked;
                    callback(checked);
                }
            );
 
            return sprite;
        }
		
		private function onButton(ev:MouseEvent): void
		{
			var mode:String = TextField(Sprite(ev.target).getChildByName("lbl")).text;
			switch (mode)
			{
				case "Move Forward":
					camera.moveForward(1);
					break;
				case "Move Backward":
					camera.moveBackward(1);
					break;
				case "Move Left":
					camera.moveLeft(1);
					break;
				case "Move Right":
					camera.moveRight(1);
					break;
				case "Move Up":
					camera.moveUp(1);
					break;
				case "Move Down":
					camera.moveDown(1);
					break;
				case "Yaw Left":
					camera.yaw(-10);
					break;
				case "Yaw Right":
					camera.yaw(10);
					break;
				case "Pitch Up":
					camera.pitch(-10);
					break;
				case "Pitch Down":
					camera.pitch(10);
					break;
				case "Roll Left":
					camera.roll(10);
					break;
				case "Roll Right":
					camera.roll(-10);
					break;
				case "-Rows":
					if (rows > 1)
					{
						rows--;
						makeShapes();
					}
					break;
				case "+Rows":
					rows++;
					makeShapes();
					break;
				case "-Cols":
					if (cols > 1)
					{
						cols--;
						makeShapes();
					}
					break;
				case "+Cols":
					cols++;
					makeShapes();
					break;
				case "-Layers":
					if (layers > 1)
					{
						layers--;
						makeShapes();
					}
					break;
				case "+Layers":
					layers++;
					makeShapes();
					break;
			}
		}
		
		private function onEnterFrame(ev:Event): void
		{
			// Set up rendering
			context3D.setProgram(program);
			context3D.setTextureAt(0, texture);
			context3D.clear(0.5, 0.5, 0.5);
			
			// Draw shapes
			var worldToClip:Matrix3D = camera.worldToClipMatrix;
			var drawMatrix:Matrix3D = TEMP_DRAW_MATRIX;
			for each (var shape:Shape3D in shapes)
			{
				context3D.setVertexBufferAt(0, shape.positions, 0, Context3DVertexBufferFormat.FLOAT_3);
				context3D.setVertexBufferAt(1, shape.texCoords, 0, Context3DVertexBufferFormat.FLOAT_2);
				
				shape.modelToWorld.copyToMatrix3D(drawMatrix);
				drawMatrix.appendRotation(rotationDegrees, ROTATION_AXIS);
				drawMatrix.prepend(worldToClip);
				context3D.setProgramConstantsFromMatrix(
					Context3DProgramType.VERTEX,
					0,
					drawMatrix,
					false
				);
				context3D.drawTriangles(shape.tris);
			}
			context3D.present();
			
			rotationDegrees += ROTATION_SPEED;
			
			// Update stat displays
			frameCount++;
			var now:int = getTimer();
			var elapsed:int = now - lastFPSUpdateTime;
			if (elapsed > 1000)
			{
				var framerateValue:Number = 1000 / (elapsed / frameCount);
				fps.text = "FPS: " + framerateValue.toFixed(1);
				lastFPSUpdateTime = now;
				frameCount = 0;
			}
			lastFrameTime = now;
		}
	}
}
