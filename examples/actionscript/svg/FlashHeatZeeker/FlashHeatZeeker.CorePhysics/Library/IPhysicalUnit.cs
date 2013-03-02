using Box2D.Dynamics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.Core.Library
{
    public class DriverSeat
    {
        public IPhysicalUnit driver;
    }

    public interface IPhysicalUnit
    {
        string Identity { get; set; }


        // jsc could convert XML comments to attributes!
        [Description("A driver can sit in the vehicle to drive it.")]
        DriverSeat driverseat { get; set; }

        /// <summary>
        /// The body from one of our physical simulated worlds.
        /// 
        /// body tells us where the unit thinks it is..
        /// 
        /// the air simulation only needs to know if collision happens.
        /// while ground units also need to show that.
        /// 
        /// 
        /// we could name the historic physics karma.
        /// if difference between past and now is small lets try to fix it
        /// otherwise kill the unit and respawn
        /// 
        /// (in Hinduism and Buddhism) The sum of a person's actions in this and previous states of existence, viewed as deciding their fate in...
        /// </summary>
        b2Body body { get; }

        double CameraRotation { get; set; }

        void ShowPositionAndAngle();
        void ApplyVelocity();

        //[Obsolete]
        //void SetVelocityFromInput(object[] __keyDown = null);

        void SetVelocityFromInput(KeySample __keyDown);


        void FeedKarma();


        void SetPositionAndAngle(double x, double y, double a = 0);

    }

}
