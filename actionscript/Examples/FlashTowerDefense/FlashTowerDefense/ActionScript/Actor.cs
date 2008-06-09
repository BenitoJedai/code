using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashTowerDefense.ActionScript
{
    [Script]
    public class Actor : Sprite
    {
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


        public void AddDamage(double e)
        {
            health -= e;

            if (health < 0)
                Die();
        }

        readonly List<Bitmap> Footsteps = new List<Bitmap>();

        public Actor(Bitmap[] frames, Bitmap corpse, Bitmap blood, Sound death)
        {
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

                               if (CorpseAndBloodGone != null)
                                   CorpseAndBloodGone();

                           }
                       );

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

                     for (int i = 0; i < frames.Length; i++)
                     {
                         var v = frames[i];

                         if (t.currentCount % frames.Length == i)
                         {
                             v.x = -v.width / 2;
                             v.y = -v.height / 2;
                             v.AttachTo(this);
                             
                             if (this.Moved != null)
                                 this.Moved();

                             UpdateFootsteps();
                         }
                         else
                             v.Orphanize();
                     }
                 }
             );
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

            var n = Assets.footsteps.ToBitmapAsset();

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


    }

}
