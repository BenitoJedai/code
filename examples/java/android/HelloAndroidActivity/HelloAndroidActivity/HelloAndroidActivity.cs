using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;

namespace HelloAndroidActivity
{
    public class HelloAndroidActivity : Activity
    {
        protected override void onCreate(android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
        }
    }
}
