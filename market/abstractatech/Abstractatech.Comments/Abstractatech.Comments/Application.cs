using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Abstractatech.Comments;
using Abstractatech.Comments.Design;
using Abstractatech.Comments.HTML.Pages;

namespace Abstractatech.Comments
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            AddNewCommentsDiv(page.NewCommentContainer, page.CommentsContainer, this);
        }

        public static void RefreshComments(IHTMLDiv commentDiv, ApplicationWebService service)
        {
            commentDiv.Clear();

            Action refresh = async delegate
            {
                var comments = await service.GetAllViewComments(Native.document.location.hash);
                if (comments != null)
                {
                    for (var r = 0; r < comments.Rows.Count; r++)
                    {
                        var row = (global::Abstractatech.Comments.Schema.CommentCommentTableRow)comments.Rows[r];
                        var container = new CommentRow();
                        container.name.innerText = row.Name;
                        container.email.innerText = row.Email;
                        container.time.innerText = row.Timestamp.ToString("dd.MM.yyyy HH:mm:ss");
                        //container.content.innerHTML = row.Comment;
                        container.content.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
                        container.content.innerText = row.Comment;

                        container.AttachTo(commentDiv);
                    }
                }
            };
            refresh();
        }
        public static void AddNewCommentsDiv(IHTMLDiv newCommentDiv, IHTMLDiv commentsDiv, ApplicationWebService service)
        {
            RefreshComments(commentsDiv, service);
            var newcommentContainer = new NewComment().AttachTo(newCommentDiv);

            newcommentContainer.Submit.onclick += async delegate
            {
                if (newcommentContainer.name.value != "")
                {
                    if (newcommentContainer.email.value != "")
                    {
                        if (newcommentContainer.commentarea.value != "")
                        {
                            var rep = newcommentContainer.commentarea.value; //.Replace("\n", "<br />");

                            await service.InsertNewComment(Native.document.location.hash, newcommentContainer.name.value,
                                newcommentContainer.email.value, rep);

                            RefreshComments(commentsDiv, service);

                            newcommentContainer.email.value = "";
                            newcommentContainer.name.value = "";
                            newcommentContainer.commentarea.value = "";
                        }
                    }
                }
            };

        }

    }
}
