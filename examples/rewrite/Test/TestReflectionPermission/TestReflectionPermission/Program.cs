using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TestReflectionPermission
{
    [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
    [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201406/20140609/mysql

        static void Main(string[] args)
        {

        }
    }
}
