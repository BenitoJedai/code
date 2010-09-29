using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Amazon.EC2.Model;

namespace ScriptCoreLib.Amazon.EC2
{
    public class AmazonEC2Client
    {
        public AmazonEC2Client(string awsAccessKeyId, string awsSecretAccessKey)
        {

        }

        public InstanceStateChange[] StartInstance(string[] InstanceId)
        {
            return default(InstanceStateChange[]);
        }
    }
}
