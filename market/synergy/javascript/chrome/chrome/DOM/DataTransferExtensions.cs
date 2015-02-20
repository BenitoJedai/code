using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Description("Chrome Web Browser package?")]
    public static class DataTransferExtensions
    {
        // X:\jsc.svn\market\synergy\javascript\chrome\chrome\DOM\DataTransferExtensions.cs

        [Description("is this only for chrome?")]
        public static void setDownloadURL(this DataTransfer dataTransfer,
            string path,
            byte[] bytes
            )
        {
            // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

            var data64 = System.Convert.ToBase64String(
                bytes
            );

            dataTransfer.setData(
                format: "DownloadURL",
                data: "application/octet-stream:" + path
                + ":data:application/octet-stream;base64," + data64
            );
        }
    }
}
