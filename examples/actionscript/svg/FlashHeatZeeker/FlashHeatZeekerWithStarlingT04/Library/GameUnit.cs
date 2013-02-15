using Abstractatech.ActionScript.Audio;
using Box2D.Collision;
using Box2D.Common.Math;
using Box2D.Dynamics;
using Box2D.Dynamics.Contacts;
using FlashHeatZeekerWithStarlingT04.ActionScript.Images;
using FlashHeatZeekerWithStarlingT04.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using ScriptCoreLib.Shared.Lambda;
using starling.core;
using starling.display;
using starling.filters;
using starling.text;
using starling.textures;
using starling.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FlashHeatZeekerWithStarlingT04.Library
{
    [Description("Flash has the object, JavaScript can ask for details.")]
    public interface IGameUnit
    {
        // jsc supports trings only for now?
        string identity { get; set; }

        void AdjustHue(string delta);
        // what else does the GameUnitDiagnostics need to know about gameunit?
    }

    class GameUnit : IGameUnit
    {
        // in a network game, this defines our unit!
        public string identity { get; set; }

        public bool isdieselengine;
        public bool isjeepengine;
        public bool ishelicopterengine;

        public Func<double, double> zoomer_default = y => 1 + (1 - y) * 0.2;


        public double move_forward = 0.0;
        public double move_backward = 0.0;

        public double rot_left = 0.0;
        public double rot_right = 0.0;


        public Sprite loc;
        public Sprite rot;

        public Image shape;



        ColorMatrixFilter shape_filter;

        public void AdjustHue(string sdelta)
        {
            var delta = int.Parse(sdelta) / 240.0;

            Console.WriteLine(new { sdelta, delta });

            InternalAdjustHue(delta);
        }

        public void InternalAdjustHue(double delta)
        {
            if (shape_filter == null)
                shape_filter = new ColorMatrixFilter();

            shape_filter.adjustHue(delta);
            this.shape.filter = shape_filter;
        }



        // hit F2 to see the box2d physics
        public Car physics;

        public b2Body physics_body;

        // special shadow if any
        public DisplayObject shadow_loc;
        public Sprite shadow_rot;
        public bool shadow_rot_disable_rotation;

        public DisplayObject wings_rot;

        public __vec2 prevframe_loc = new __vec2();
        public double prevframe_rot = 0;

        public Queue<Sprite> tracks = new Queue<Sprite>();

        public double rotation
        {
            get
            {
                if (this.rot != null)
                    return this.rot.rotation;

                // we do not know whats our rotation? are we a building?
                return 0;
            }
            set
            {
                if (this.rot != null)
                {
                    // dont rotate physics if we dont have anything to rotate in visual world
                    this.rot.rotation = value;

                    if (this.physics_body != null)
                        this.physics_body.SetAngle(value);

                    if (!this.shadow_rot_disable_rotation)
                    {
                        if (this.shadow_rot != null)
                            this.shadow_rot.rotation = value;
                    }
                }
            }
        }

        public double scale
        {
            get
            {
                if (rot != null)
                    return rot.scaleX;

                return 1;
            }
            set
            {
                if (shadow_rot != null)
                {
                    // art too big
                    shadow_rot.scaleX = value;
                    shadow_rot.scaleY = value;
                }

                if (rot != null)
                {
                    rot.scaleX = value;
                    rot.scaleY = value;
                }
            }
        }

        // make th unit look like team lead
        public Action AddRank;

        public Action<double> ScrollTracks = delegate { };

        public DisplayObject guntower;
        public bool RemoteControlEnabled;

        public Action RenewTracks = delegate { };

        // not all units can be manned.
        public DriverSeat driverseat;

        // pedesterians can man the driverseat
        public bool isdriver;

        public bool input_enabled = true;

        public class DriverSeat
        {
            public GameUnit driver;
        }

        public GameUnit TeleportTo(GameUnit r, double dx, double dy)
        {
            TeleportTo(
                r.loc.x - dx,
                r.loc.y - dy
            );




            return this;
        }

        public GameUnit TeleportTo(double dx, double dy)
        {
            if (this.physics_body != null)
            {
                this.physics_body.SetPosition(
                    new b2Vec2(
                        (dx) / __b2debug_viewport.b2scale,
                        (dy) / __b2debug_viewport.b2scale
                    )
                );


            }

            if (this.physics != null)
            {

                this.physics.body.GetPosition().With(
                    pp =>
                    {

                        this.physics.body.SetPosition(
                            new b2Vec2(
                                dx / __b2debug_viewport.b2scale,
                               dy / __b2debug_viewport.b2scale
                            )
                        );
                    }
                );



            }

            TeleporVisiblePartTo(dx, dy);

            return this;
        }

        public GameUnit TeleporVisiblePartTo(double dx, double dy)
        {
            this.loc.x = (dx);
            this.loc.y = (dy);

            if (this.shadow_loc != null)
            {
                this.shadow_loc.x = this.loc.x;
                this.shadow_loc.y = this.loc.y;
            }

            return this;
        }

        public void TeleportBy(double dx, double dy)
        {
            if (this.physics_body != null)
            {

                this.physics_body.GetPosition().With(
                    pp =>
                    {

                        this.physics_body.SetPosition(
                            new b2Vec2(
                                pp.x + dx / __b2debug_viewport.b2scale,
                               pp.y + dy / __b2debug_viewport.b2scale
                            )
                        );
                    }
                );
            }

            if (this.physics != null)
            {

                this.physics.body.GetPosition().With(
                    pp =>
                    {

                        this.physics.body.SetPosition(
                            new b2Vec2(
                                pp.x + dx / __b2debug_viewport.b2scale,
                               pp.y + dy / __b2debug_viewport.b2scale
                            )
                        );
                    }
                );

                this.physics.wheels.WithEach(
                    w =>
                    {
                        w.body.GetPosition().With(
                           pp =>
                           {

                               w.body.SetPosition(
                                   new b2Vec2(
                                       pp.x + dx / __b2debug_viewport.b2scale,
                                      pp.y + dy / __b2debug_viewport.b2scale
                                   )
                               );
                           }
                       );
                    }
                );

            }


            this.loc.x += dx;
            this.loc.y += dy;



        }
    }

}
