using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTowerDefense.ActionScript.Actors
{
    [Script]
    public class Actor : Sprite
    {
        public string NetworkName;
        public int NetworkId;

        public string Description;

        public string ActorName;

        public int ScoreValue = 1;

        public bool IsAlive = true;

        public event Action Die;

        public event Action CorpseGone;
        public event Action CorpseAndBloodGone;

        public event Action Moved;

        public double health = 100;
        public double speed = 0.5;

        public bool IsBleeding;

        public Action PlayHelloSound;
        public Action PlayDeathSound;


        public void AddDamageFromDirection(double Damage, double Arc)
        {
            AddDamage(Damage);

            var DamageMovement = 2 * Damage / 100;
            var t = new Timer(1000 / 24, 10);

            t.timer +=
                delegate
                {
                    this.MoveToArc(Arc, DamageMovement);
                };

            t.start();
        }

        public void AddDamage(double e)
        {
            if (!IsAlive)
                return;

            health -= e;

            if (health <= 0)
                Die();
        }

        readonly List<Bitmap> Footsteps = new List<Bitmap>();

        public bool IsCorpseAndBloodGone;
        public bool IsCorpseGone;

        public bool RunAnimation = true;

        Bitmap[] _frames;

        public Actor(Bitmap[] frames, Bitmap corpse, Bitmap blood, Sound death)
        {
            this._frames = frames;
            this.mouseEnabled = false;

            PlayDeathSound = death.ToAction();




            Die = delegate
            {
                IsAlive = false;
                PlayDeathSound();

                foreach (var v in frames)
                    v.Orphanize();

                corpse.x = -corpse.width / 2;
                corpse.y = -corpse.height / 2;
                corpse.AttachTo(this);

                (10000 + 10000.Random().ToInt32()).AtDelay(
                    delegate
                    {
                        corpse.Orphanize();


                        blood.x = -blood.width / 2;
                        blood.y = -blood.height / 2;
                        blood.AttachTo(this);


                        (20000 + 10000.Random().ToInt32()).AtDelay(
                           delegate
                           {
                               blood.Orphanize();

                               IsCorpseAndBloodGone = true;

                               if (CorpseAndBloodGone != null)
                                   CorpseAndBloodGone();

                           }
                       );

                        IsCorpseGone = true;

                        if (CorpseGone != null)
                            CorpseGone();
                    }
                );
            };

            //this.Moved +=
            //    delegate
            //    {



            (1000 / 15).AtInterval(
                 t =>
                 {
                     if (!IsAlive)
                     {
                         t.stop();
                         return;
                     }

                     if (!RunAnimation)
                         return;


                     ShowFrame(t.currentCount);
                 }
             );

            ShowFrame(0);
        }

        private void ShowFrame(int Counter)
        {
            for (int i = 0; i < _frames.Length; i++)
            {
                var v = _frames[i];

                if (Counter % _frames.Length == i)
                {
                    v.MoveToCenter();
                    v.AttachTo(this);

                    if (this.Moved != null)
                        this.Moved();

                    UpdateFootsteps();
                }
                else
                    v.Orphanize();
            }
        }

        private void UpdateFootsteps()
        {
            if (Footsteps.Count == 0)
            {
                CreateFootsteps();
            }
            else
            {
                var o = Footsteps.Last();

                if ((o.x + o.width) < x)
                {
                    CreateFootsteps();
                }
            }
        }

        public bool CanMakeFootsteps = true;

        private void CreateFootsteps()
        {
            if (this.parent == null)
                return;

            if (!CanMakeFootsteps)
                return;

            var n = Assets.Images.footsteps.ToBitmapAsset();

            n.x = x - n.width / 2;
            n.y = y - n.height / 2;

            if (IsBleeding)
                n.filters = new[] { new GlowFilter(0xff0000) };

            n.AttachTo(this.parent).AddTo(Footsteps);


            (500).AtInterval(
                t =>
                {
                    n.alpha -= 0.1;

                    if (n.alpha < 0.1)
                    {
                        n.RemoveFrom(Footsteps).Orphanize();
                        t.stop();
                    }
                }
            );
        }

        double _TargetX;
        double _TargetY;

        Timer _TargetTimer;

        public void WalkTo(double _x, double _y)
        {
            _TargetX = _x;
            _TargetY = _y;

            var EgoMoveSpeed = 3;

            if (_TargetTimer == null)
                _TargetTimer =
                    (1000 / 30).AtInterval(
                        delegate
                        {
                            var p = new Point { x = _TargetX - this.x, y = _TargetY - this.y };

                            if (p.length < (EgoMoveSpeed * 4))
                            {
                                RunAnimation = false;

                                _TargetTimer.stop();
                                
                                return;
                            }

                            this.MoveToArc(p.GetRotation(), EgoMoveSpeed);
                        }
                    );

            RunAnimation = true;
            _TargetTimer.start();
        }

    }

}
