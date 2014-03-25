using Abstractatech.JavaScript.Avatar.Design;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Abstractatech.Avatar.Notification
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IAvatarNotificationInterface
    {
        public void ReferenceDeclaration()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }

        public Task<global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row> GetLastUserImage()
        {
            return new AvatarNotificationService().GetLastUserImage();
        }
        public void DiagnosticInsertNewPicture(global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row y)
        {
            new AvatarNotificationService().DiagnosticInsertNewPicture(y);
        }
    }

    public interface IAvatarNotificationInterface
    {
        Task<global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row> GetLastUserImage();
        void DiagnosticInsertNewPicture(global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row y);

    }

    public class AvatarNotificationService : IAvatarNotificationInterface
    {
        public async Task<global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row> GetLastUserImage()
        {

            return
                (from c in new WebCamAvatars.Sheet1()
                 orderby c.Key descending
                 select c).FirstOrDefault();
        }

        public void DiagnosticInsertNewPicture(global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row y)
        {
            Console.WriteLine("enter InsertNewPicture");

            var avatars = new global::Abstractatech.JavaScript.Avatar.Design.WebCamAvatars.Sheet1();
            var key = avatars.Insert(y);

            Console.WriteLine("exit InsertNewPicture " + new { key });
        }
    }
}
