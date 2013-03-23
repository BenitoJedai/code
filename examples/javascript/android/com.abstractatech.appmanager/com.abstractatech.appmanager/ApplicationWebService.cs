using android.content;
using android.content.pm;
using android.graphics;
using android.graphics.drawable;
using android.os;
using java.io;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Threading;
using System.Diagnostics;

namespace com.abstractatech.appmanager
{
    class AtWebServiceDiscoveryArguments
    {
        public int port;
    }

    // http://developer.android.com/reference/android/content/BroadcastReceiver.html
    [IntentFilter(Action = "ScriptCoreLib.Android.CoreAndroidWebServiceActivity.AtWebServiceDiscovery")]
    public class AtWebServiceDiscovery : BroadcastReceiver
    {
        public static Thread XCallback;

        public override void onReceive(Context c, Intent data)
        {
            //            I/System.Console(17864): setResult { port = 5781 }
            //I/System.Console(18183): AtWebServiceDiscovery: { port = 5781, extras = Bundle[{port=5781}] }

            var port = 0;
            var CallbackToken = 0;

            Bundle extras = null;

            if (data != null)
            {
                extras = data.getExtras();

                //I/System.Console(14280): startActivityForResult: { packageName = com.abstractatech.adminshell, name = com.abstractatech.adminshell.ApplicationWebServiceActivity }
                //I/System.Console(14280): ActivityResult: { packageName = com.abstractatech.adminshell, port = 0, resultCode = 0, requestCode = 1, extras =  }

                if (extras != null)
                {
                    if (extras.containsKey("port"))
                        port = extras.getInt("port");

                    if (extras.containsKey("CallbackToken"))
                        CallbackToken = extras.getInt("CallbackToken");
                }
            }


            //System.Console.WriteLine("AtWebServiceDiscovery");

            // I/System.Console(18971): AtWebServiceDiscovery: { port = 16846, CallbackToken = 1554743048 }

            System.Console.WriteLine(
                "AtWebServiceDiscovery: " + new { port, CallbackToken }
            );

            if (XCallback != null)
            {
                XCallback.Start(
                    new AtWebServiceDiscoveryArguments { port = port }
                );
                XCallback = null;
            }
        }
    }

    public delegate void yield_ACTION_MAIN(
        string packageName,
        string name,
        string IsCoreAndroidWebServiceActivity = "",

        string label = ""



    );





    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        AtWebServiceDiscovery ref0;

        public void queryIntentActivities(
            yield_ACTION_MAIN yield,
            // jsc please start supporting int :)
            //int take = 10,
            string skip = "0",
            string take = "10",
            Action yield_done = null)
        {
#if Android
            // http://stackoverflow.com/questions/2695746/how-to-get-a-list-of-installed-android-applications-and-pick-one-to-run
            // https://play.google.com/store/apps/details?id=com.flopcode.android.inspector




            //            Caused by: java.net.SocketException: Broken pipe
            //       at org.apache.harmony.luni.platform.OSNetworkSystem.write(Native Method)
            //       at dalvik.system.BlockGuard$WrappedNetworkSystem.write(BlockGuard.java:284)
            //       at org.apache.harmo
            //ny.luni.net.PlainSocketImpl.write(PlainSocketImpl.java:472)
            //       at org.apache.harmony.luni.net.SocketOutputStream.write(SocketOutputStream.java:57)

            var context = ThreadLocalContextReference.CurrentContext;
            var pm = context.getPackageManager();

            lock (ApplicationPackageManagerLock)
                context
                    .GetLaunchers()
                    .OrderByDescending(r =>
                        {

                            return r.IsCoreAndroidWebServiceActivity();
                        }
                    )
                    .ThenBy(
                        r => (string)(object)pm.getApplicationLabel(r.activityInfo.applicationInfo))

                    .Skip(int.Parse(skip))
                    .Take(int.Parse(take))
                    .WithEach(
                    r =>
                    {
                        // http://stackoverflow.com/questions/6344694/get-foreground-application-icon-convert-to-base64

                        var label = (string)(object)pm.getApplicationLabel(r.activityInfo.applicationInfo);

                        yield(
                            r.activityInfo.applicationInfo.packageName,
                            r.activityInfo.name,

                            IsCoreAndroidWebServiceActivity: Convert.ToString(r.IsCoreAndroidWebServiceActivity()),
                            label: label
                        );
                    }
                );
#else

            var icon_base64 = "iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAYAAADimHc4AAAABHNCSVQICAgIfAhkiAAAIABJREFUeJzlfXm8HUWV//dUdfdd3paXjSwQILgggVFAQVCGp+LG6Kg/fRlUXMYFFPfdn8vkMi6DOI47I64MLjPkgYKAMgrkiY7IKiIJsoUkBLK8vP1u3V11zu+Pqu7bLyZ5AUkyM7/6fO57ffv27a4659T3rFUX+G/QRED/Pz77gDSRmgKAHVJ77rrGO3/2w9sHTgOAmj+/f/syqAHgIfnQu26ffuPl373lmOX7uy/B/npQp9VEpKb+49b/+lS5Gp4cUPSUz13x5Gd9GLUtEKgagffmLh3JFcAfEkH2therZVATDdn75O2nPjT5yPnr791a3ninXg/g/RgeVsDe9eMvbftd6oaGBhVRjdfdMfVvWx+ekCDUhy1ZvOR8IshRQ4O7hQORmhJZrUVqgciaABACBEQkjvBCnc9Xa3f9ruFFBLQOQyLy+a6JevNftj4yWrr3zrHxe+4wPwWAtSML9pqR//OagGriGH/evz/zsu/916ky9IcXybd/9aw3AMDq1Q4WROAJuma3s/Q+eXHpQamV18hAebePy5nSYcZqDz23t9903jUbXy5f+MmJ8taPP/UfAGDQP39/tf0PQQRBDSQCev9nqu8OS41nlErBIdWuynlf/c8Tblj5wqEHb731rBC40BKRBQCRgSDGS5cHmPMMgTpakBwOyEKBVAghLcXrqC2vawMyzqBNFnxPjOlbHoZdS7SykT16jdQCYBjPoSGzLn37adtHR9/30IOjeHh9+ze//OHk+TWBqtHQfoGeDjkOUFu9elCvXDlkP/mvJ5+x4BD+/oIlPUHcjq+ubr7hZStXwgJAKl87SaHyAoZ5qUHraItmySIBIBAP9wYxEkyDISCEIAQgRCCULSF6yLK+xkjw89Hph244tv9LEwAwJh/pWze+6debN+w45q5bdzTuuKHxrKt+cO8fBlcP6qGVQ3Z/0uGAmmC1NQNB7TnD9lMXnfCtgw6J3lyqlLBtdPydH3rJ2RtSoQ8xtU9OMBEyYmhEUCgzEDABEDAETG2MYgIbYNACQ8SCwRASKK1QguJuaO4BpPKH1IaXNpuLv8K4ubZjdPx9636/HX+6vfXxiz5312cPBPGBA8wACEhQ0295y429i575yLWHPannaccducwctfAk3cI2BVipYIEFQmXQUCmmwTB5xxmAhUGMaSRogCFwDLCwYBhOxbJhaxmShpq4F1p1bRkZ2d73pzu2Vu7/Y+Oqf12FVw2uPsoOrRxiYO+tqMerHQAztNMENU1UMxvkzNLElqfsCOdswsJKVU9hA/VgqVEo6Qa2BCmmEKEbFcxDiB5olAFoAAKDGBN4CCk2AUj9fQERABISWGlihhVrk2QbwrCyuHuO2K7egPrnz7sUuCE5bbwcrhYwHQBxPIAe6GpNtNJuknNeMA9HXhhCH7YVN3OPHKa66WBMYSOa2IYK5qEXhyBEL4AQjvCZ9cxIMIYxbEATo7BIYWFhYMFiYKyBMRbWWlhjYa3AWitsBUnMaE4FSasRfOr1J131WRHC0NCgWvm/XQc4c9BJ/mZ531nz8YQvWbQrW+UOnk9HqQg92IG1AIBeLEMX5nuJz4jPuQIGBE2MYAKbkKDhYAfGsYANjE1hrIU1BsYYWMuwqYUxjCSxkiaWkpZCu07/tnHr/LNqK4cSEdCjcej+0nYAGFALiGpmo7znIwvxxPNStGQrfi8LsEIFqGA71iFCFVUsRAndCFGFQgCDNlK0YJFAYH3XNRI0EWPay72TfwMDwwbWpjCe+O5lYVM3I4wRmJTFpNa0Ghy2G7g6peCMkYHhJs4FarX94wnvVwZkxL9f3vmuhTjiKwDsw7iJ+nCYqmIBRnA3QlQQesK7DhIEKVK0veUjeccFBPZKN1O9FgZG/AwwBtZmxE9hUptDkkkZ1jJMwjDGps26DdsNvnxkatnfrRocSoFHF9p4rG2/hSLWeOLfLW96ZQ8WfxkI+RHcoRSqKsQc7MB6CEJYKKSwiNFCA2OYwjbUMY4EbVgYZ3xCYCGe4OyPHRusCFgAtgAzgS0g7I5FCCKAMAGiIEwQEUAo1AGlpOXlvdGGLxKAc4cH9P6IlO4Xt1ukpg6nmr0lfsXRffqwH1cxrzKK9WjIdlWlJUgQo4kxAAoMBkEjRRMGsSc14FxoAQM5EzqvbB4ILDOYnXSznXlsLIMtYC0jSVtgFoA1rLVgK5pZjDV84sAdi0Y+8ZIbb1qxYlAPDa3bp7Ngn88AJ0VraY0MlKt66ddC9M6tY5wn7RYlNkIKwYTZhHZsYJIq6vE0GjyGNjeRSorUo7uB8ccGaf5KkeaYz97ScQS3pvNiK7BGIJaQpgmSdooIi8FpiFY85WcHAJDWmkQpfPaT3zvmmJUrh2yttm9ptM/9gCEMqpU0ZG9uvuYdYbnn1BTGNGRHECdNBNSFCbsJXXwUDo3ORETzULcPYkP725BgHFqVQcpJCRGD3BEA7zEJwCIQETAXJd6CmWEMe9NTwJaQmhSae3H8wR/CvOpT0E7HcMfmb+P+7ddAUxdAlohglFa9pVB9/qwLj38pttxmIV4V7YO2TyFIBLQC6+S4N5x40Jyew34Yhl1dMTdVIx6lNGkhtU2otBdHd52Lil4CRSVU9RK00wmMpbdDowIWBrOARcAsDk7YQc1Mibdg621+m32WfW7BFmjFk1jSNYCnLHw1tI5QCediad9J2Ljj16i3toEQwlqrhJU1CZ4YJO07P/3BkbWr9yEU7dPpNYwBTQSZ07vsXSTRoiRtc6s9Re32NIxhtNrT6KYViNQcsKQQcT5QmsZIE2expImBSQ3SxCJJUnecGnfeWH+cnbdIU5tbO8Zw4T+DLaGZjAIAhC0sxwiDKuZXjkWctAAPRSJMSgeioD82WFsRDQ7uuwjpPmNATWrqOTRsLttwymLY0uusYWm26tRojiOOm7CpI8hUYztEAEUBFIVoJtvw8PQwYCMkSeoJ7wmeOoakSYo0ST1DUk9403n5a03qPWDDMMZAoYpHJm/GQ+O/gVIhtIrAKbBu7X0QVrBWIAyIsCIiAeHYpd32JUSQfZUn2GcMOBXDCgCqPOd0ZloWJwm3WpOq1Zp0hEwNlFQw0rgddz9yOaw12DZ1B27cvAqNdCvYKKRp6iXYwpgUaZoiNcYxxLiZkaTGnU/d+0z602wWpJkitmBP4F/f+ync9dClaLTH8YsbL8KmHb9FqLpgjHGWkQQAiAERIpzpRjS0T+i0z+zczKW/6t6XXl2q9LyYCJykE5rRzh/rlCqj1bQIMQ8pJqC0RSnsgdICrTVABAIVuppZLPA2PMAs/hx7KRawMMQr5sw0dUraQVy73YKk3ZiYmEC5XEIQBC6IxwpsFJLYcqNeV1MTyUhr0p70zU/f/0CtBvV4e8j7xArKiL/6jpcsNcyn6NSQ5bYy3AIAEGWJdAsihagUwphRBBRBUQRjU2goMAuInOWTsUyyrwo8Xot/+WNmiMARnJ11JOyYIF6JAyFKUYiUEvTP7UWaZjdTECGn8IUViKwwL7CCkwE8AAwoYPi/PwOGhgYVMGS11s82adxDaLPhpgIsBMrJcz73GEQEkgDMFkyOexABKV+cIOTOwdGJiHKiI2dCkRnipN1bTpIxw3aYwlYAIYi4NIBjmo+YGnKfMwkIwiJ/DeD7jzfxgX3EgAULRggAhHF8ahMwGxakChCQysbgiOr8246EZwwRIsB6H5jIV5+QJ5rMYIKwuy7zCYQ94TMGSIcBIugwwmYwlr13/gJzNoOgREA24WcMDg7qWu3xD1XvEwaM+LIOttEK540mAAGkHA2JPKIXpD2bEpTV+XjsJ7gCFPcnYxcKesD9ceqgSHzkDlqHEeIVcee67LybJcjPO//BkIet5S38YRGAh5EDYKeJSD6fyeHrXrfHxAAXXsilU3b+jGjIrnmwVt4+cvcCtgFYEqKcAeRpTZ4ZBemm4kzoXJdzIX+Sj4n62eCe62cBz4Qi8U6ccOe9iHRmiz/mnGHScd5YSIRgWaqszWIAD9dqoFoN4oh+rgbWSla9AQBrZCAYwQJZSXs3Wx4VA0RWa2CdENW4aECtlkG9DiukRjU+FzUCajK2bfN8IeoXGxpjQUQMUgQi8f87eoByGktG+gIsZQzxCCSACPljnmERZVK/s15wpmWB0Jn0i2cYu/t2dIZnLisBlBHLimEPAXDr2rUgV1VHFnAJapEfzwOWBEDQJnr6pDsHOvdc0GxW014xwGexiGilr9NZU57Gvd09WJ4Cp9VdZ4awevWgHkSNawAmJyMKSmEkWgIIAaoTDga72I6T8g7OdyS+QP5MIVPel5whyGBHOsd/rpTRYUAGMRnhvV/gjuHD0z7TQARhoUoFYf+CLmzd0AYA9J92vFpJQ6nID3rHsOVNFvHp23D74jbWBG1utW9rv+6BifHWJUSXDnm52GOGbVY/QKSmnMQDU/KFv7Ucr0xVfIIFz4s5jhPDG5oJXTPd1D845aDz1tdqrrC1VqvxFy5+4+lc3vgm6PG/UpqfGFU0SmWNINBQSuX4L7ITYYvdkswH8HqiiP3Zf68U8nMZ3BT0Qnae2cNXBk2eQcwuYprEjKQtEjcVtabNDqvsLTYOrx9be+cXV6+uCVGN70k+d0Kgp74TKnt0CV0IUMYW+SOm2iNIWkCznmJ8JL5iYrr9tnMGbtl2LkC7q3ndIwMy7j0o751TxcKvMdqvtWBE6IVA4aH4dkzXx5G2NFr1cPPYdvuevz/1pz+uSU25tF6N3/+Vpx8bpX1fi+uVk4ms6HKTdNSGLqWodBMq1RBRWSGMFIKIoDWgFMGb/1AFXz2bBbzzUArS7t5KIdkCb9W4JI01LjpqEkfsuCVoNy1aDYGNI9i4BNOMJG4qmpwe27x16+hHf3fd+h86egj9+NY3H17tb9xQmdNYOrdvWdqrFxFE1Fj8CCbr26VRb0qr2cb0RByMbU9/edWdPaevWTVsdzcLZoGg1eo++XXQTstfa8jW18bcTjUqKhSjWBhxHCFtdcn09JRN28nBU1NTP/rAPz/ptBrVfgMAv97x1nMmN/d8/c4bR/Dghimsv38L7RiZRBwbpEkC0oJSOUBUIpQqGuWqQrlKCMuEIAhQKmuEkUZUViASKKURhhpB5MS+oJNzWbJGkCaAtRbCgjQRJLFF3HIxpSS2SFqMdlMQtwzSWBC3LdKEoRBAKYVSKaJFCxejd86cg5ct7v9Bz8vKLx/jiTcQUfPzlwyc1Tu/uXSh6U1Ne0e4IxgFs0UaM9qtGK1WglYjRbPOpjHFzz9Kb3k5ES4dXA095Cv+9ooBTtGstFfd87KTg1LljFY8Yoh1AFIkBCiEAAdIY0ONBqvpqUYyOd4s6bDyEQC/+frqFd1J3HrnkcccjkWHSHrXfevDIzYKtm6MsPUhix1bgbHtbUyMxhjZEaPVsEhjIEkANngUxeE7T+KCq7yrpgCtgSAkBBFQLocoVTS6e0N09QSo9Ch096Xo7n8AXd1aenv7qDxpX2UetP8M4KYtmyeOTYwI0FQTUYIwigBBJzYVp2i3DZKW0MSOVLY/0nwqgEtH1u0abXbLgHXDzpka3xYd3Uwe0oLYEGkipUHQEHYOi0ktkrZBs5kE7SbL5DgfDqC04NCTFmxa/2Dv1PQW9PcdEiw7fCGOeLJFpdQPYUGjNYV2yyBuMdoNoD6ZojGdoDVt0aynaNQTNOsJ4rZF0ma02y4y6oJrPsnCFpYzJe7pqwAdEHSgEAQKQejgrVTWiMpOB4UlQqmsUa5qlMrKMSMkBKHDO5M6iGpMM7EYa1lIU+nJAG5K21JpNlKqT8YURAZaOx9HBGDDSFN2eqTJSNpErQbv0S+Y1QoS7i636kriNIFWAUBpHofJcNWkjLht0ZhKaXo8BgAx03N6puroi+NxjI3UoZSCDgikRhFFIYJAI4wCRKUQPb0lLD6kFzrQiHQIpTSU0k4HQBfsTwKgcgsn5cSFL8RVhVprcuhx1pH4pIzNvV9rHRSZ1NUIpQkjjh2EOAXMSGLHgFbDlbZMT8aKWc0FAGPSAuQVrDHuGBPCAHtXidSeA86zMoCgJ0RAzOL9UP/wXLkVbGcCSAXdxx67qK+iu6fi2DSN2O4o0ULEpEOC1oS2slAq84a9+Zk7Z9lLQeXOmQIpgiKVpyJR6Edmw3esH3ecRUGlkEVj6xwt8Q6bS1eKjwEJrAFM4vWIUTAGZI3AJGYMANhyIEywLFAsUG6ZSMHczSTX3V/NUlkxux9gdSHold2786Cc6wIXSzGsormsSPVYMYG1WmBSAZGAmWAVzXDEcoKrjKTimdKJGe0M6dnzCK68xLJBFlDLJgtQMEfhpTLzB2zBGbPZf+QlLCIKYJoxZgq0Pwr6WKzrT06GAj12oom1u1NGe8sADRCp/IZuoJ3IZMfz7DCnUW8AKMMaEGmG1q63ROTr+J2p6QjmmAOeSWWnSov+QDESQSBoJKaJJI0RBV0IdAgWiyIlxDPB9dU7YgVP2M2cjpmah7gZeWgiG2eeDiMo6XRkBi06VcGAiLiv5xg0AGD4z8g7a0ZMQ81k64zRuXfMnlw+Qtnwa1JyZWmdhFkuSJ9lHxTbCcoKRMhjNd5pAhQgGnHSxtjkNgSyAAfPeTZMQpiY2g5jDCBBLu05kXkm8TtxI5+CLBCfGTnQij8WIVjrWECZH1cM5GV+yAwy+ayHEk+NPyc+8ChiQTm9PTIU4/GuQy7+LiCqVpzoisC5+75zygLs6msdpvv7UCFBkEls9igC5UmZdtxAq9XEnOpyHPvks3D8E1eiqzwXIxP346Y/fR9/evgaNMx2VMq9CHTkY0XcISSjQLAC0QuOXC4Uwjl0QSSfAcbwTJeqCI2FKKz4B1hrpvdE171jQObGSYb/lA+CvTQpCkjBgICuvvlzu3TY3e50xsGLCEAZfrPPd6gMawsg6pmnVQBmRpxOIU0N5nY9CScfsxIrDn0xuqvzckFY2P8EvPSkc3HC+Jm44/4f465NV2KyuR2lqIoorHjimxnYXJTcLMKRhzWyeHce9AMyELKcskjgTHEvWDQDHwu3YMBa9RcqYaWnvBQU4s5ZT5FLi+sgARCtiHWJI8mkCfn0zGaN1wVEIJadfC7lcsViMd0Yg7DCkrnH4a8O+z9YceiLEYUVAACLhSLVScyAcVD/E/HCZ3wEz3zKG3DbfUNYu/EqjE6tRxiUUAq7PKPtDJMxG4NIZuoWGDGDsAoiQm/52JNLgMsz6IIVloOzPy7qkL+IAUqh6QaJ3OHIeCAsnfws8s8lTROJVUIioGyw8Hlg9x1vevrvkQKECUQaSdpCO24goC4cvvB5OO4JK7F88bOgdZATnqCgqFMl4jJr2sENBH3di/DcY9+FZzz51bh74y9x+31D2DK2FiBBOeoBkYaImWE8eLOqAEMuJJ5HWMExAC1QPZ7hJKJzy6vIgY5CLijv4cfIAOFA5dJe6GTe81x6OMfvQIdqYf/SNoA4w9hsVmcJGGGAlECI3CyyBpPT29FXORhPO+wVOPaIV2HJ/BU5t53E6xmE37kRKS8PAmGLnup8nPCUV+P4J70K6zZch9vvuwz3P/xbpKaBnq65Od2LShkoBDdcP8ll0pLx+++HJgeiTkHllKZcd2Tfy+5t+S90xJRSkiXEgUxJ0swpnCsdBwdaizrhyOe1rv7Vp1rFAXYwM8NcZ2iyMKKgF6885ZNYNv9E9HUt8eNw6URFao+E37kRCKSCHJq0DnHMES/CisNfiG3jd+PO9Vfg5j+tdt422xw28pyDG6iDEW8TGQMkCYQglKX3ZihzdK71s4CIBFrzGAAsWLBrf2BWM5SIqUjwmUrMPZJ8ytA5VSKpSgSAIlZUNCmLdnameIUJ1iaYmhrHyCZCd+SIz2LBLJ7ws6YtdtN350VbayBgKEWoBsuw6W7C6KiLYqJox/smAigKEQaVnPFBWft+zby2Y5gU6OI/YgZSY9I99XG3DDjKJ9ZFVBMCKfqjxYfkDEHO+GoF3VUAVumoMzsK0pIPWAiJaaGvugyHLvhrnP+d1+OMs4/D1ddcCrCGVg7X+c8SAHvXLFsQEbQO0Jhu4rs/+DJOP+NJ+MbFn8GRS0+HRgXGGhQNCl+aiDDsRrU8PydRgAAbN/6cmO3McRdmgPsgExanqP88AD2zzToD2CIGiIuZf3//giR0RIJAYdBFYefkTK+5mBCHV7oL+47Ga077Ks7/2JU4eHk/PnXBIM764PPwm99e52JCSnlGzJ7nzgJwIgKtNBqNJi69/CKc+Y5n4isXvRcnnHgC/u1r1+ONL/0XaO6DMUlHevLuunoha9LcjzAGqNfrOdiiIO3Z+5nCpRDo8mz03wsdQJoyK6jjrBQfVJAEAEQiZJWHevYwA8/qIsgWOuyV31896VSc/9Gf4Te3D+FHV34W//C1F+Fp//kSvOZlH8HTj3smiJxUK8qcs5mNmUEEaK1hDePn1w7hkiu/iAce+R2edswz8cF3X46Tj3splFKYnBpHY9qAQ9f3LBSRN6EZ3SQiGRlZFxGpKDNnnWfudR8KqMCu+i7Q0awzYFYGBGGxU0WFipnE9wNgFqRp6i4pzJCs8kAyMMsvUGjFkwAAywZhUMJzTjgTJxzzN7j2pu/gp9d9FZ/4ynNx/JMH8eq/fT+OXvFUT2y3UlIp5aDGH0OA63/1c/z7FV/APZuvx5FHHoVPnvFNPOeEM1EuVdxMEgaRQhIzmMTV4halF0CGuMJZOFNjBNu7CdQFzIRgd1DwI/w0smyEwBN/EQMA5NJWxPCctDkGsnfIOl8TX9wgOw+uYD0B8NFMQCnvXQqjq9KPlw18ECc/dRDX3fxNXD38LXzo/Mtx4orX4PWv+gCWL3+C+661rogXwI033YCLLvkc7nrwGhzxpGV439mfx6lPfy3m9i0C4JhG5MLaAHxMqpOoz81rJxd5R0UgYiiuohAUyASvYLOKH5OLAikiEKe2GQPAihWQoV0UWM/uB0i5KYIGgN6Mubk0F6/z75zrnhLc5OsEYwtODumZCmxGBRCRd5QcIxb0H4ozXvgZnHLsmbj25m/iqmsvwlv+74/w/BPPwmteeQ4OPfRw/OGu2/CN730WN935EzzhyCV4x1v+Ac8/6U1YMPeQnMHaJ3mK3qlLKM2wIDxRCRBfH+pE27ZNazJNDOV99JZhZgXNMEcZEGZhVhAJHlsoYt3gCgGGECplAbLONOuYYc7BEhSnIPlslYoCRURJ7V9Pa2Rjy8MrO5lxM0y6Quswwnm3Sxc+BW94yRcxcPwbcPWvvo7Lf/ZVDN80hMOXHItb7vwF5h5Uxnvf8Um88JQ34qB5h3sCeytI7V7O8ogoOkIl7MkOV0NUqCQu2IIdV76IWtn4MiGaTcZ3/+m5xWd6SwYo4AkKOiCTfjf3Qim5VdYKHSXkTQUphCPg7yd7sDJz79Yz4tDFT8M5Z3wLLzrlbfj20Edw0y1X4pWvfC3e+urPYEH/wQAAZgMin9bcQ8sMBC5OZxHkNTHojDUKgQYKY/BMKH5NfEbMoYCQsAgz7dGG3r0ZuspfQEx5bVee3MieOuM/MUOICFG5WgZ8dUN+SceC6phxBWGapZH3hlksjE2xfOnxOPXpf4elh1fxihedhQX9B8OYBCIMpQIUvfdd368zrNyq89DaETgnZW68EZojG2Ft5gdYLxiSjyVz8zMzFADbuLlHJTyrH2DDSgqh1Bk6heKngkJ2WSULlsSFYsnOBJadYCcfXBF397Ip0j5Dx5iebCFNGMYmEBEorXdpnu5yXD6Bn9vzhU7kaxG4Q9UkBdA18x7ZzJkhh34mZADBnO7REJ21tz3lqKFIGgRy2aCisoHrOPnOZg82qVNWWoUzdUU22KIfgccSaHArZ5gBYwRaU6fKei++CwDtdhPNZt3BlBR1m+S6rJh0KhGJTZXu2EG77ndnTC7ImCTRHnuzF+JShiCgTspNck4X9QA7c8Y3x3TaKRfxZz4EMEOCHm3L+/NYbuCzJsVxdfQq5Z3zQk6IQpRQnkNEgQ+I0ozgQGEmZ91hAVUr4R5pPCsDkjidEe4GOlM2sxyy8WSd7kD/TOoW4YYz8+gxkZ92c/womuzim5IBjgIwA5YEqW5rxaSoY5DMyB3nusCH5kSBIGkjadUBoFbb9UBnZUBULacgmJmWUIeYueT48pEZJmZxVDM42MkPdKbTo2+dxPNj+3JeHuNPZGNSpLO+Z3c2jfZ4vW2FmKUjM7KLx0uWdiWwlXRqtN3aUzd2y4BVqAkALCgf0oSoBjymzZhqGaRklQadaetpIy0pzI280wXp2pk3e9d2Is9jmQQFIcoWbWRSL0JgNvnyJgf6VShNQjl07dwdKqKyf09UivYs5HtlMlAO5hnedtg+Q7nmSsuBo4KYYsd2mtbuJPvO+nW9j7o9BuIzM0ACpTLZ991hpzyDoIxQV1EKu9xs4OxRSuU8z/RFQRg7guhmU6CCuLtvSbKnvsyuA3pKTD6kULQMZiYhMIMn2bIXEZmRySmasB0fgGBM24edNax1VW57bsUM3Wwj6DQXqjauTlUHYDHemir0Da5mKQy7UC71AaKzpa2kSTXJR3WdP1MIR/hZ4KRNgSgQiG6Obojqe+rTHhjg7nh8/2l1gWoD3hDKd53K1lfJjCyRy9c5D1Tpkq9acPO2yCzAeaylqAubtv0B/37lZ7F9xxaffCeXxdoL86Z4v91f4whPRAiCCA9uugdfuvBjmG6MIQyjPOHT0W+MNG0jNSkIJbAoCbTRT1j4gg2cRg/YFBARw35RePHla1CN4m5SKN23YcOGBIKd7ZjZGUBEUqsNaCKycVM2sVXEbNmyc15meJoCsAibRBC3pGFbahMAVMrdAgSA6OyeMwiXvQKtcMWaz+PdnzgN31/9JTQa09DaebN7k4TZEwxlhNc6wPYdW/ClCz+Ot77v+Vjz20tRqVRcDiFbPlvoW5bOLAV9AJNUykFw9tlnp5JupZVsAAANQklEQVT2/ENzWkPM3CCig1IlfRbcw+BuFlO1abuUULwkrE/ZViMe+ycAPLhycLd0ngWCBhgAmhPRFyZ3qEZ9QoemVU2bdbL1KcPNacP1Kbb1STJTO0IZ3Uq6WZcLrvvpPXcCQFel33ZV5kEVgmFEHfMtGy0pwvInHoLSnElcfMXHcfaHB3D5zy5Cq9XKI5h2BiMyJbxr0c+yYgCgdYDRse347g8/j79/96m49GdfRu984IlHLkEYZcq3k3By5lHHDwhUN6wFKVURALj4y7+7JE3V309Ppo+QmRd2hUfo7ugJqkyHKUkX6LTVE8Uts7Hd3vG6//j6ndfValBDQ7tfsrpHBtRqNQZAn3j7j25uTAavHt/O2+qjXSE3DtbxxAJVH+1TjdFePT3SFdQnSDXq5oINv9/8yVptIAAAHQTS17UMikJkpXq51YoCbkIQlQVLD+3HsScuR9C3Dd+45N149ydfhF8OXwYRQCvtwh3s6+Azhnb4ASCLgLqsWKvdxI8uuwDnfPTFuPgn/4jeBSmOftqhOGhpN6p9nQUZOwelZmyP5RjTmG436wAwOAj93c/+8aJWMjawY3T9RyenRn7ebtfvStPWH41p/8zYxofG6rf/9bfOu/WyWq026+Yee5OQkcHVg/o7K4eufP17VpwwNv7w28Kg79lalRYJ69AYO5qm7btj0/retas3Xi8COnd4QAPDIMJ4BjtEzrzLF8YjjxoCcNXSQSRQWrD00LlYtHQutm25H1+6+C24+vqLsPIl78HJJ5wGAEhN0skheLMwW6gR6BBJEuMXay7DJT/9Gh4ZXYfFB8/BU5cfDlJ+20shCJRbDJjvQVE0bTthDYEAjGSyxQkADA2Ba7WBoPbJ4fsAfA7A+cef5eh42zeRWxBua4ParPi5VxmxoZVDdnBwUF/85aFNAD4GQD339VgcqTlhuzGxY3gImbeniCBr1mSdV+0suVFMzmefZlM98+MygrAVUAgcvGwBFi0RbHn4NvzTha/Gil88D2e87L142jHP9EzNwh0EpTRIFK7/9U/xox9/FesfvgWLlvbi6GOXwen1bH+JjG+dhSEZ5ud+DjJ9Ra6KmyRuq3ZeXlKrDRu3md+AWrVqjSWiLAdL5577nAAY5r3dV2Kvq6M9jtFAbUAP14bt9RfjYcBFWgdqA8EAhjmbbr/KSJzyfa7u362wUd549mkl5+RQtjtEZ9G2WzsgELFQSnDIYQtx0CKLTRuuxye+MIzjjvobvHHlh6H8YjvLBneuvQUXXvwp3HXfrzF/UQXHHHcIwpKCwO4k6ejEfHLLDXkoGkC+dRlAbAwrYdmw+cbNceeb2c66w1yrzcjmCVCIxDyeDMhoOlwbNgAo286xVnPnhotX/coxYmoy+QkC+QRzEGRZJlK+7JzdgrqOQi4MRAHkqxIySygsEY540lI0mwnuuv8n+MC5N6B/bh8OXrYUF136j7jtpntR6U1w1FOXoNIVQGmG0uIS9flGIAW4LzCAWVx9qmR+hYJYADaQ6fokKcWXAeCBAQTDw4+OwLO1xxjJmr05BVTjT3/jDd9utre8OZYtaRByqANHeLXTUqWdiQIUbfyd1mBJgJFt02i3DJYs68PEeB3taYXe/gqUtn6FZAYxhWdkuO59l9w5FLd8Kk1TkFRQ0osBUzbT09PB1pH166DxnJ9844GRrFuPJ5327catAurWcz4OU74nboRhq8Fp3BIksYJJya1MTNxiarfVMDrVaewBmmZMcQ9ZBnMXdOGgpb0gJZg3rwfzF1UQhIwg1AgCt6iPlP9PBEB1kunINsJx2xcbo5DEAuJulNRSwFQ5jtvBVGMzl8rqwz/5xgPbBwd9iPRxbvts39Dh4WGpUU199AOfqz/39BNvSNPW6c1mfa411hC0AFWlUIFYBWsBYQVhhWyP53xvZ18mnq8bIPfK6oDcclYFpRWUCtw+FDrwS10DV9vpF1RAQgi792IDsNWwRiFNCDZVEFvlOI5tvbVdN+ORVIA3/+jL91w6OAg9NDRrkdtjavsMgrKW/TZL7YtnHLZjeuO/tOPxVwgMwqgk1XI/Vas90AHAnMCK25remaycWymdFZQo4BV5hsCnKN1QXHEW+ZU3foeWrOohWztm/Y66qdvGnlmYrXBq44BtDAivDcLofRf+4x2/3Bcb9RXbPmcA0NEHAPC+zw68od4cO8dy6wQdaJTCHlRL81Ap9yAIAwgMWGJYTsBiIOTXjqpOZ0kpZGvHsuMcYkh5be8ZwFni3VdA2M4CO7ZuH3ZjYxjTguX0XkXqku55C7943jlXj+9Lyc/afmEA4Jiwdm2NhoZgr7j2Rwf9Yvii+7aN3t3T1RNKVI6oUupGKepDOexFEFRcZbSLTbp4EFmI20+OyYXa3QRRjvhKKXjV7hQvdG7Lz4ieigVzCmNjJGlDGo06sU1/owP5rkTmmi+977YtgHOk9hRCeLzafmMA4MI+K4cG1TnLT+u/6udX3/2HP94436QiYaSp2h2hqztCpauEcqWEUqmCKKwgDCsIdAlahdAqBDNApKG18rU/nvg+/BAEOo9POOYZWE6RJG202w00mw3Up6cxMT4JSXpse1rrzZvXP+/m67ddD4DOuvD44Jtn3WaK0Yh92fb7rygNrRyy59x6GkoVUN/cyO/VYzC6rYkdW1og5TbZCEsaYUkjihSCIEC5UoEOIHP6u28LIj2pKZhDpCsqcDkSTYGkCS9vNutVaxhJmuRbGCexQRwnSGLjN/ywSFMjc/uqFOkStAYNrh7U/deuV988+7YUZ+8/ehyQn7GKK5rDspJKV4AgIEQlhdTvz2ASV2rSbjJa026RgzGCUmjRjpvWNna87fabtty2YhBRuR+yfBy0vh/ykdO+MueKX1523dp7f38MQbNSpHSgEQQBtNbQgZs1SmlEJQUdEqISkRJJ4jRtuHDL/qfFAWHAk6vPa95aunqq2hPMTyNCVPa7V7UFaQxYkylLAlsFaxhhoCCk0UpDAYC1Q0gByG3+nk98+ylp+KsrpFQK3cbcASEIFHQQeML7LdLggoLeKSMQmMjsF7jZVduvP2eblTQddthhNgjIRJFCqer27al0BejuDdHdF6DS5aQ0CAg6IChNedIkbzWH9NkedfV6nZRyeV7naQuUptzrVsrdRwVO+oPQb58zWw3jPm77/feEfSMdKApChVLJb6RU0Sj7V7Vbo9IdoFTRCEJHOK3d/53bqlWrBAA9+9nPbghR0zNMtFbQ2m3EpENCWFKIygpRpBCVFMIS+XvD/UTZAWoH7KcMdUDQoYKw26pYKYENAKUFpNzKyyxu5PbdcVIc7p5YVik2QehWyWQ7ZQWR22dOB5mp6gJOSrudsow9oBPgwDFAkYJSAIuXQjio0B4+EgUgFnBJYK2GGMrLSHbTSAVErk7US3vJbfrnwhMq/z4zQ0ghDDUkAcIDOAUOGAPIb08JXwgFZAE4F+N37xlg5Xa4UhqqvUcGSBBECCNn8bgdF90syKTfMUDArCBw5w8YCPt2wB4faJ91IpeuJNVRtNnGe2FJIYicMg4iDa0Joceg2i7uGUUKUTlw+5CW/PdDQhgSwihTvE75ZjpFKZLgAP6m7IF6NAtoolMiQT7O5mL3SjszUWsgjBSSmESpkHQQ2enWxDRQLHbtzIqoVEJUCkHKdnRAoAr4781P44nvf6Djz9ZA78d2oGaACGAVZbt1+xQ7SWcWaG86KgUdAEFYQqCqMtUYiYs3IoL4WD0rHbTDMJAgJAkjjSB0SlgHqiDxKjdPtVYgjZaRuAm4lYz7mxD7nQEucU82jc395DYf4Wwbrs4vBmRVEwSQQAda2skYG65vOig6eGTne65YMUBEJETm1lI5oiAk0QEc0fPsm/K+gPcLtLIsVlhaD46Yxka4NOv/fgasPWqQAKA+Hn+73aIEHGlIZLPVQK50RYNUAB1E0LrMge4yzKKCMP36jTdubg0O+p/T9q22atgCQLkcXFCphFu6u/rCrurcNIzKEgQlRFEZUVRBqdSFcrkbpXIXl0s9aMXjFMvIBeuvxeS+ynjN1vbLj3kW27qhdVKrQa360L0bjn/20umkbV9UjRarcjCPrdFWOGBChTX1cEhzoNCtkjZrk8bforEHVq1bB6xbtxOhznUzq/bRdROnvGTZH0jhBX09i/vmzjmYKuUerlb6ubt7rnRX50m5NIdISiqNU2VM+1Pf/+d7v1CrQV1wwf75/eCd235nAAAMD0NqNajzV2353VHP6P19ux0/haSyKECv1uhRGt1KU5cS1hQnzS3teOoT7c1/XLViBWR4GMAuJDW753kff+SBZ50+50pj4l6t9RPK5d5StdynylG30ipSxqSmHU/dEqfT7/lG7Y4LajWo2ipIcVnu/mwH1A3MMk5vfCPKcf/igUpl7smlqO+wUIdagIfTpHnraH3LtUNf3DxW6OseYSJLgQLAOZ9eckhX99KTy5W+w6OgXGJrH5lsT9z60K9vvHNoCHZfpxv/RzSP57NcM/iodm1yhK3tUb/tq58mfLTt/wEEiBDIGW601AAAAABJRU5ErkJggg==";

            Thread.Sleep(1700);

            for (int i = 0; i < int.Parse(take) - 1; i++)
            {
                yield("foo", "foo", icon_base64, "foo" + i);

            }
#endif

            if (yield_done != null)
                yield_done();
        }



        public void Launch(
         string packageName,
         string name,

         string ExtraKey = "Float",
         string ExtraValue = "Float",

            string DisableCallbackToken = "false",


            Action<string> yield_port = null
         )
        {
#if Android
            // http://stackoverflow.com/questions/12504954/how-to-start-an-intent-from-a-resolveinfo
            var c = new ComponentName(packageName, name);
            Intent i = new Intent(Intent.ACTION_MAIN);

            i.addCategory(Intent.CATEGORY_LAUNCHER);
            i.setFlags(
                Intent.FLAG_ACTIVITY_NEW_TASK
                | Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED
                | Intent.FLAG_ACTIVITY_EXCLUDE_FROM_RECENTS
            );

            i.setComponent(c);

            // http://stackoverflow.com/questions/11860074/start-activity-for-result
            // http://stackoverflow.com/questions/2844440/passing-arguments-from-loading-activity-to-main-activity
            i.putExtra(ExtraKey, ExtraValue);


            var bDisableCallbackToken = Convert.ToBoolean(DisableCallbackToken);

            var CallbackToken = new Random().Next();

            if (bDisableCallbackToken)
            {

            }
            else
            {
                i.putExtra("CallbackToken", CallbackToken);
            }

            var context = ThreadLocalContextReference.CurrentContext;

            (context as CoreAndroidWebServiceActivity).With(
               a =>
               {


                   System.Console.WriteLine(
                               "startActivityForResult: " + new
                               {
                                   packageName,
                                   name
                               }
                               );

                   var done = new EventWaitHandle(false, EventResetMode.AutoReset);
                   AtWebServiceDiscoveryArguments value = null;

                   // tested by X:\jsc.svn\examples\java\CLRJVMThreadAsCallback\CLRJVMThreadAsCallback\Program.cs
                   var yield = new Thread(
                       (object e) =>
                       {
                           value = (AtWebServiceDiscoveryArguments)e;
                           done.Set();
                       }
                   );

                   if (bDisableCallbackToken)
                   { }
                   else
                   {
                       AtWebServiceDiscovery.XCallback = yield;
                   }

                   var intent = new Intent(context, typeof(foo.NotifyService).ToClass());

                   //intent.putExtra("data0", AuthorizationLiteralCredentials.user + " has logged into this device...");
                   intent.putExtra("data0", "Loading " + packageName + "...");

                   context.startService(intent);

                   // http://developer.android.com/reference/android/app/Activity.html#startActivityForResult(android.content.Intent, int)
                   // http://stackoverflow.com/questions/1984233/onactivityresult-doesnt-work
                   a.startActivityForResult(i, 0);


                   if (bDisableCallbackToken)
                   { }
                   else
                   {
                       System.Console.WriteLine("waiting for callback...");

                       // how long shall we wait? what if the app is not a jsc app?
                       done.WaitOne(500);

                       System.Console.WriteLine("waiting for callback... done!");

                       if (value != null)
                       {
                           yield_port("" + value.port);
                       }
                   }
               }
            );



#endif

        }


        public void Remove(string packageName, string name)
        {
            // http://stackoverflow.com/questions/6813322/install-uninstall-apks-programmatically-packagemanager-vs-intents
            var context = ThreadLocalContextReference.CurrentContext;
            // http://stackoverflow.com/questions/8228365/how-do-i-remove-any-app-from-a-device-using-my-app-in-android
            // http://stackoverflow.com/questions/6049622/action-delete-android
            Intent intent = new Intent(Intent.ACTION_DELETE);
            intent.setData(global::android.net.Uri.parse("package:" + packageName));
            context.startActivity(intent);
        }


        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            // http://tools.ietf.org/html/rfc2617#section-3.2.1

            var Authorization = h.Context.Request.Headers["Authorization"];

            var AuthorizationLiteralEncoded = Authorization.SkipUntilOrEmpty("Basic ");
            var AuthorizationLiteral = Encoding.ASCII.GetString(
                Convert.FromBase64String(AuthorizationLiteralEncoded)
            );

            var AuthorizationLiteralCredentials = new
            {
                user = AuthorizationLiteral.TakeUntilOrEmpty(":"),
                password = AuthorizationLiteral.SkipUntilOrEmpty(":"),
            };

            var Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");

            var path = h.Context.Request.Path;

            System.Console.WriteLine(
                new
                {
                    AuthorizationLiteralCredentials,
                    Host,
                    //h.Context.Request.UserHostAddress,
                }.ToString());

            #region /a
            var a = h.Applications.FirstOrDefault(k => k.TypeName == "a");

            if (h.Context.Request.Path == "/a")
            {
                var OK = false;


                if (Host == h.Context.Request.UserHostAddress)
                    OK = true;

                if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                    if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.password))
                        OK = true;

                if (OK)
                {
#if Android
                    if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                    {
                        var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

                        var intent = new Intent(c, typeof(foo.NotifyService).ToClass());

                        intent.putExtra("data0", AuthorizationLiteralCredentials.user + " has logged into this device...");
                        intent.putExtra("data1", "... from device " + h.Context.Request.UserHostAddress);

                        c.startService(intent);
                    }
#endif



                    h.Context.Response.ContentType = "text/javascript";
                    h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


                    //Implementation not found for type import :
                    //type: System.Web.HttpResponse
                    //method: Void AppendCookie(System.Web.HttpCookie)
                    // not working on android?
                    h.Context.Response.SetCookie(
                        new HttpCookie("foo", "bar")
                    );

                    h.WriteSource(a);
                    h.CompleteRequest();
                    return;
                }

                h.Context.Response.StatusCode = 401;
                h.Context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Android\""
                );

                // flush?
                h.Context.Response.Write(" ");
                h.CompleteRequest();

                return;
            }
            #endregion

            const string icon = "/icon/";

            var is_icon = path.StartsWith(icon);

            if (is_icon)
            {

                // package will be a keyword and cannot be used as a field name in a closure!
                // jsc could do some magic!
                //var package = path.SkipUntilIfAny(icon);
                var packageName = path.SkipUntilIfAny(icon);
                var context = ThreadLocalContextReference.CurrentContext;
                var pm = context.getPackageManager();

                lock (ApplicationPackageManagerLock)
                    context.GetLaunchers().Where(r => r.activityInfo.applicationInfo.packageName == packageName).FirstOrDefault().With(
                        r =>
                        {

                            try
                            {
                                var dicon = pm.getApplicationIcon(r.activityInfo.applicationInfo);

                                if (dicon != null)
                                {

                                    ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
                                    // bitmap.compress(CompressFormat.PNG, 0, outputStream); 

                                    BitmapDrawable bitDw = ((BitmapDrawable)dicon);
                                    Bitmap bitmap = bitDw.getBitmap();
                                    ByteArrayOutputStream stream = new ByteArrayOutputStream();
                                    bitmap.compress(Bitmap.CompressFormat.PNG, 100, stream);
                                    var bytes = (byte[])(object)stream.toByteArray();

                                    h.Context.Response.ContentType = "image/png";
                                    h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");
                                    h.Context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                                    h.CompleteRequest();

                                    //icon_base64 = Convert.ToBase64String(bitmapByte);

                                    //bitmapByte = Base64.encode(bitmapByte,Base64.DEFAULT);
                                    //System.out.println("..length of image..."+bitmapByte.length);
                                }
                            }
                            catch
                            {

                            }

                        }
                    );
            }
        }

        //#89 java.lang.RuntimeException: Package manager has died
        //       at android.app.ApplicationPackageManager.queryIntentActivitiesAsUser(ApplicationPackageManager.java:486)
        //       at android.app.ApplicationPackageManager.queryIntentActivities(ApplicationPackageManager.java:472)
        //       at com.abstractatech.appmanager.X.queryIntentActivitiesEnumerable(X.java:41)
        //       at com.abstractatech.appmanager.X.GetLaunchers(X.java:33)

        public static object ApplicationPackageManagerLock = new object();




        #region poll_oninstall
        int sync_SelectContentUpdates_timeout = 5000;
        int sync_SelectContentUpdates_waitmin = 100;
        int sync_SelectContentUpdates_waitrandom = 300;


        // jsc could upgrade this method to use EventSource?
        // async yield?
        public void poll_oninstall(string last_id, yield_ACTION_MAIN yield, Action<string> yield_last_id)
        {
            System.Console.WriteLine("enter poll_oninstall " + new { last_id });

            if (last_id == "")
            {
                yield_last_id("" + AtInstall.History.Count);
                return;
            }



            var int_last_id = int.Parse(last_id);

            var sw = new Stopwatch();
            sw.Start();

            var random = new Random();


            // will this compile?


            while (sw.IsRunning)
            {
                var id = int_last_id;

                //sync.SelectTransaction(
                //    nid => id = (int)nid
                //);

                id = AtInstall.History.Count;


                //                type: System.Random
                //method: Int32 Next(Int32)
                var wait = sync_SelectContentUpdates_waitmin + random.Next(0, sync_SelectContentUpdates_waitrandom);

                //Console.WriteLine("SelectTransaction " + new { id, int_last_id, sw.ElapsedMilliseconds });
                if (id == int_last_id)
                {
                    Thread.Sleep(wait);
                }
                else
                {
                    // dont stop reading...
                    //sw.Stop();

                    //var value = new PointerSyncQueries.SelectContentUpdates
                    //{
                    //    FromTransaction = int_last_id,
                    //    ToTransaction = (int)id
                    //};

                    //sync.SelectContentUpdates(
                    //    value: value,
                    //    yield: message =>
                    //    {

                    //        yield(XElement.Parse(message));
                    //    }
                    //);

                    System.Console.WriteLine("raise oninstall " + new { int_last_id, id });

                    AtInstall.History.ToArray().Skip(int_last_id).Take(id - int_last_id).WithEach(
                        packageName =>
                        {
                            //var xml = new XElement("oninstall", new XAttribute("packageName", packageName));

                            //// raise oninstall { int_last_id = 1, id = 2 }

                            System.Console.WriteLine("yield " + new { packageName });


                            var context = ThreadLocalContextReference.CurrentContext;
                            var pm = context.getPackageManager();

                            lock (ApplicationPackageManagerLock)
                                context
                                    .GetLaunchers()
                                    .WithEach(
                                    r =>
                                    {
                                        if (r.activityInfo.applicationInfo.packageName != packageName)
                                            return;

                                        // http://stackoverflow.com/questions/6344694/get-foreground-application-icon-convert-to-base64

                                        var label = (string)(object)pm.getApplicationLabel(r.activityInfo.applicationInfo);

                                        yield(
                                            r.activityInfo.applicationInfo.packageName,
                                            r.activityInfo.name,

                                            IsCoreAndroidWebServiceActivity: Convert.ToString(r.IsCoreAndroidWebServiceActivity()),
                                            label: label
                                        );
                                    }
                                );

                        }
                    );

                    int_last_id = (int)id;


                    // return early
                    sw.Stop();
                }

                if (sw.ElapsedMilliseconds >= sync_SelectContentUpdates_timeout)
                    sw.Stop();
            }

            yield_last_id("" + int_last_id);
        }
        #endregion
    }

    static class X
    {

        public static bool IsCoreAndroidWebServiceActivity(this ResolveInfo r)
        {
            if (r.activityInfo.applicationInfo.metaData != null)
                return r.activityInfo.applicationInfo.metaData.containsKey("CoreAndroidWebServiceActivity");


            return false;
        }

        public static IEnumerable<ResolveInfo> GetLaunchers(this  Context c)
        {
            var mainIntent = new Intent(Intent.ACTION_MAIN, null);
            mainIntent.addCategory(Intent.CATEGORY_LAUNCHER);

            var context = ThreadLocalContextReference.CurrentContext;


            var pm = context.getPackageManager();
            return pm.queryIntentActivitiesEnumerable(mainIntent);
        }

        public static IEnumerable<ResolveInfo> queryIntentActivitiesEnumerable(this  PackageManager pm, Intent mainIntent, int arg1 = 0)
        {
            // http://imogene-map.googlecode.com/svn-history/r40/trunk/org.imogene.map/src/org/imogene/map/app/Supplier.java
            var pkgAppsList = pm.queryIntentActivities(mainIntent, PackageManager.GET_META_DATA);

            //for (int i = 0; i < pkgAppsList.size(); i++)
            //{
            //    yield return (ResolveInfo)pkgAppsList.get(i);
            //}

            return Enumerable.Range(0, pkgAppsList.size()).Select(i => (ResolveInfo)pkgAppsList.get(i));

        }
    }


    //[DefaultEvent("oninstall")]
    public static class ApplicationWebServiceClientSideExtensions
    {

        public static void oninstall(this ApplicationWebService service, yield_ACTION_MAIN value)
        {
            {
                // based on X:\jsc.svn\examples\javascript\android\MultiMouse\com.abstractatech.multimouse\ApplicationWebService.sync_SelectContentUpdates.cs

                System.Console.WriteLine("ApplicationWebService_oninstall");

                // start polling!    

                // empty string means skip to the end
                var last_id = "";

                #region async_poll_oninstall
                var loop_index = 0;
                Action loop = null;

                loop = delegate
                {
                    loop_index++;

                    //talk.innerText = "#" + loop_index + " " + new { last_id };

                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            service.poll_oninstall(
                                last_id: last_id,
                                yield: value,

                                //{
                                //    System.Console.WriteLine("async_poll_oninstall yield " + new { xml });

                                //    var packageName = xml.Attribute("packageName").Value;

                                //    //                             Caused by: android.content.ActivityNotFoundException: Unable to find explicit activity class {NASDAQSNA.Activities/NASDAQSNA.Activities}; have you declared this activity in your AndroidManifest.xml?
                                //    //at android.app.Instrumentation.checkStartActivityResult(Instrumentation.java:1618)
                                //    //at android.app.Instrumentation.execStartActivity(Instrumentation.java:1417)
                                //    //at android.app.Activity.startActivityForResult(Activity.java:3370)
                                //    //at android.app.Activity.startActivityForResult(Activity.java:3331)
                                //    //at com.abstractatech.appmanager.ApplicationWebService___c__DisplayClassb._Launch_b__9(ApplicationWebService___c__DisplayClassb.java:53)

                                //    value(packageName);
                                //},
                                yield_last_id:
                                    id =>
                                    {
                                        // in stream mode this make a while to reach here
                                        last_id = id;

                                        // this would cause stackoverflow, yet since we are in 
                                        // clent-server "tail" call it aint.
                                        loop();
                                    }
                            );
                        }
                    ).StartTimeout(150);
                };

                loop();
                #endregion
            }

        }



    }
}
