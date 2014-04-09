using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Abstractatech.Comments.Schema;
using System.Data.SQLite;

namespace Abstractatech.Comments
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public void ReferenceDeclaration()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }

        public Task<DataTable> GetAllViewComments(string viewHash)
        {
            return new CommentService().GetAllViewComments(viewHash);
        }

        public Task<DataTable> GetAllComments()
        {
            return new CommentService().GetAllComments();
        }

        public Task<DataTable> GetTopViewComments(string viewHash, int amount)
        {
            return new CommentService().GetTopViewComments(viewHash, amount);
        }

        public Task InsertNewComment(string viewHash, string name, string email, string comment)
        {
            return new CommentService().InsertNewComment(viewHash,name,email,comment);
        }

        public Task<bool> CheckIpBlacklist(string ip)
        {
            return new CommentService().CheckIpBlacklist(ip);
        }

        public Task InsertNewIpToBlacklist(string ip)
        {
            return new CommentService().InsertNewIpToBlacklist(ip);
        }

    }

    public interface ICommentInterface
    {
        Task<DataTable> GetAllViewComments(string viewHash);
        Task<DataTable> GetAllComments();
        Task<DataTable> GetTopViewComments(string viewHash, int amount);
        Task InsertNewComment(string viewHash, string name, string email, string comment);
        Task<bool> CheckIpBlacklist(string ip);
        Task InsertNewIpToBlacklist(string ip);
    }

    public class CommentService : ICommentInterface
    {
        public async Task<DataTable> GetAllViewComments(string viewHash)
        {
            return (from c in new Comment.CommentTable()
                    where c.View == viewHash
                    orderby c.Key descending
                    select c).AsDataTable();
        }

        public async Task<DataTable> GetAllComments()
        {
            return (from c in new Comment.CommentTable()
                    orderby c.Key descending
                    select c).AsDataTable();
        }

        public async Task<DataTable> GetTopViewComments(string viewHash, int amount)
        {
            return (from c in new Comment.CommentTable()
                    where c.View == viewHash
                    orderby c.Key descending
                    select c).Take(amount).AsDataTable();
        }

        public async Task InsertNewComment(string viewHash, string name, string email, string comment)
        {
            var row = new CommentCommentTableRow { Email = email, Name = name, Comment = comment, View = viewHash};
            var table = new Comment.CommentTable();
            table.Insert(row);
        }

        public async Task<bool> CheckIpBlacklist(string ip)
        {
            var res = (from c in new Comment.IpBlacklist()
                       where c.ip == ip
                       select c).FirstOrDefault();

            if (res == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task InsertNewIpToBlacklist(string ip)
        {
            var row = new CommentIpBlacklistRow { ip = ip };
            var table = new Comment.IpBlacklist();
            table.Insert(row);
        }
    }

}
