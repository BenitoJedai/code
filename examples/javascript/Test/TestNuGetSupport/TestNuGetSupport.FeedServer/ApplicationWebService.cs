using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Xml.Linq;

namespace TestNuGetSupport.FeedServer
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Handler(WebServiceHandler h)
        {
#if DEBUG
            Console.WriteLine();
            Console.WriteLine(h.Context.Request.HttpMethod + " " + h.Context.Request.Path);

            h.Context.Request.Headers.AllKeys.WithEach(
                k => Console.WriteLine(k + ": " + h.Context.Request.Headers[k])
            );
#endif

            if (h.Context.Request.Path == "/nuget")
            {
                h.Context.Response.ContentType = "application/xml";
                h.Context.Response.Write(@"

<service xmlns:atom='http://www.w3.org/2005/Atom' xmlns:app='http://www.w3.org/2007/app' xmlns='http://www.w3.org/2007/app' xml:base='/nuget/'>
<workspace>
<atom:title>Default</atom:title>
<collection href='Packages'>
<atom:title>Packages</atom:title>
</collection>
</workspace>
</service>
                ");
                h.CompleteRequest();

                return;
            }

            if (h.Context.Request.Path == "/nuget/$metadata")
            {
                h.Context.Response.ContentType = "application/xml";
                h.Context.Response.Write(@"

<edmx:Edmx xmlns:edmx='http://schemas.microsoft.com/ado/2007/06/edmx' Version='1.0'>
<edmx:DataServices xmlns:m='http://schemas.microsoft.com/ado/2007/08/dataservices/metadata' m:DataServiceVersion='2.0'>
<Schema xmlns:d='http://schemas.microsoft.com/ado/2007/08/dataservices' xmlns:m='http://schemas.microsoft.com/ado/2007/08/dataservices/metadata' xmlns='http://schemas.microsoft.com/ado/2006/04/edm' Namespace='NuGet.Server.DataServices'>
<EntityType Name='Package' m:HasStream='true'>
<Key>
<PropertyRef Name='Id'/>
<PropertyRef Name='Version'/>
</Key>
<Property Name='Id' Type='Edm.String' Nullable='false' m:FC_TargetPath='SyndicationTitle' m:FC_ContentKind='text' m:FC_KeepInContent='false'/>
<Property Name='Version' Type='Edm.String' Nullable='false'/>
<Property Name='Title' Type='Edm.String' Nullable='true'/>
<Property Name='Authors' Type='Edm.String' Nullable='true' m:FC_TargetPath='SyndicationAuthorName' m:FC_ContentKind='text' m:FC_KeepInContent='false'/>
<Property Name='IconUrl' Type='Edm.String' Nullable='true'/>
<Property Name='LicenseUrl' Type='Edm.String' Nullable='true'/>
<Property Name='ProjectUrl' Type='Edm.String' Nullable='true'/>
<Property Name='DownloadCount' Type='Edm.Int32' Nullable='false'/>
<Property Name='RequireLicenseAcceptance' Type='Edm.Boolean' Nullable='false'/>
<Property Name='Description' Type='Edm.String' Nullable='true'/>
<Property Name='Summary' Type='Edm.String' Nullable='true' m:FC_TargetPath='SyndicationSummary' m:FC_ContentKind='text' m:FC_KeepInContent='false'/>
<Property Name='ReleaseNotes' Type='Edm.String' Nullable='true'/>
<Property Name='Published' Type='Edm.DateTime' Nullable='false'/>
<Property Name='LastUpdated' Type='Edm.DateTime' Nullable='false' m:FC_TargetPath='SyndicationUpdated' m:FC_ContentKind='text' m:FC_KeepInContent='false'/>
<Property Name='Dependencies' Type='Edm.String' Nullable='true'/>
<Property Name='PackageHash' Type='Edm.String' Nullable='true'/>
<Property Name='PackageHashAlgorithm' Type='Edm.String' Nullable='true'/>
<Property Name='PackageSize' Type='Edm.Int64' Nullable='false'/>
<Property Name='Copyright' Type='Edm.String' Nullable='true'/>
<Property Name='Tags' Type='Edm.String' Nullable='true'/>
<Property Name='IsAbsoluteLatestVersion' Type='Edm.Boolean' Nullable='false'/>
<Property Name='IsLatestVersion' Type='Edm.Boolean' Nullable='false'/>
<Property Name='Listed' Type='Edm.Boolean' Nullable='false'/>
<Property Name='VersionDownloadCount' Type='Edm.Int32' Nullable='false'/>
</EntityType>
<EntityContainer Name='PackageContext' m:IsDefaultEntityContainer='true'>
<EntitySet Name='Packages' EntityType='NuGet.Server.DataServices.Package'/>
<FunctionImport Name='Search' EntitySet='Packages' ReturnType='Collection(NuGet.Server.DataServices.Package)' m:HttpMethod='GET'>
<Parameter Name='searchTerm' Type='Edm.String' Mode='In'/>
<Parameter Name='targetFramework' Type='Edm.String' Mode='In'/>
<Parameter Name='includePrerelease' Type='Edm.Boolean' Mode='In'/>
</FunctionImport>
<FunctionImport Name='FindPackagesById' EntitySet='Packages' ReturnType='Collection(NuGet.Server.DataServices.Package)' m:HttpMethod='GET'>
<Parameter Name='id' Type='Edm.String' Mode='In'/>
</FunctionImport>
<FunctionImport Name='GetUpdates' EntitySet='Packages' ReturnType='Collection(NuGet.Server.DataServices.Package)' m:HttpMethod='GET'>
<Parameter Name='packageIds' Type='Edm.String' Mode='In'/>
<Parameter Name='versions' Type='Edm.String' Mode='In'/>
<Parameter Name='includePrerelease' Type='Edm.Boolean' Mode='In'/>
<Parameter Name='includeAllVersions' Type='Edm.Boolean' Mode='In'/>
<Parameter Name='targetFrameworks' Type='Edm.String' Mode='In'/>
</FunctionImport>
</EntityContainer>
</Schema>
</edmx:DataServices>
</edmx:Edmx>
                ");
                h.CompleteRequest();

                return;
            }


            GET /nuget/Search()/$count
Accept: text/plain
Accept-Charset: UTF-8
Accept-Encoding: gzip, deflate
Host: 192.168.1.101:18292
User-Agent: NuGet Add Package Dialog/2.0.30625.9003 (Microsoft Windows NT 6.1.7601 Service Pack 1)
DataServiceVersion: 2.0;NetFx
MaxDataServiceVersion: 2.0;NetFx


            //GET /nuget/$metadata
            //Host: 192.168.1.101:1977
            //User-Agent: NuGet/2.0.30625.9003 (Microsoft Windows NT 6.1.7601 Service Pack 1)
            //< 0006 0x00d3 bytes

            h.Context.Response.StatusCode = 500;

            h.CompleteRequest();

        }
    }
}
