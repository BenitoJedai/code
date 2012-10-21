using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Net;
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
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = h.Context.Request.Headers["Host"].SkipUntilIfAny(":")
            };

#if DEBUG
            Console.WriteLine();
            Console.WriteLine(h.Context.Request.HttpMethod + " " + h.Context.Request.Path);

            h.Context.Request.Headers.AllKeys.WithEach(
                k => Console.WriteLine(k + ": " + h.Context.Request.Headers[k])
            );
#endif

            #region /nuget
            if (h.Context.Request.Path == "/nuget")
            {
                h.Context.Response.ContentType = "application/xml";
                h.Context.Response.Write(@"

<service xmlns:atom='http://www.w3.org/2005/Atom' xmlns:app='http://www.w3.org/2007/app' xmlns='http://www.w3.org/2007/app' xml:base='http://localhost:59019/nuget/'>
<workspace>
<atom:title>Default</atom:title>
<collection href='Packages'>
<atom:title>Packages</atom:title>
</collection>
</workspace>
</service>
                ".Replace("localhost:59019", HostUri.Host + ":" + HostUri.Port));
                h.CompleteRequest();

                return;
            }
            #endregion

            #region /nuget/$metadata
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
            #endregion

            #region /nuget/Search()/$count
            if (h.Context.Request.Path == "/nuget/Search()/$count")
            {
                /*
GET /nuget/Search()/$count?$filter=IsAbsoluteLatestVersion&searchTerm=''&targetFramework='net40'&includePrerelease=true HTTP/1.1
DataServiceVersion: 2.0;NetFx
MaxDataServiceVersion: 2.0;NetFx
User-Agent: NuGet Add Package Dialog/2.0.30625.9003 (Microsoft Windows NT 6.1.7601 Service Pack 1)
Accept-Charset: UTF-8
Accept: text/plain
Host: localhost:29019
Accept-Encoding: gzip, deflate


?< 0007 0x0103 bytes
HTTP/1.1 200 OK
Server: ASP.NET Development Server/11.0.0.0
Date: Sun, 21 Oct 2012 12:42:05 GMT
X-AspNet-Version: 4.0.30319
DataServiceVersion: 2.0;
Content-Length: 1
Cache-Control: no-cache
Content-Type: text/plain;charset=utf-8
Connection: Close


?< 0007 0x0001 bytes
1
            */

                h.Context.Response.ContentType = "text/plain";

                h.Context.Response.Write("1");
                h.CompleteRequest();
                return;
            }
            #endregion


            #region /nuget/Search()
            if (h.Context.Request.Path == "/nuget/Search()")
            {
                /*
?> 0008 0x01cf bytes
GET /nuget/Search()?$filter=IsAbsoluteLatestVersion&$orderby=DownloadCount%20desc,Id&$skip=0&$top=30&searchTerm=''&targetFramework='net40'&includePrerelease=true HTTP/1.1
DataServiceVersion: 1.0;NetFx
MaxDataServiceVersion: 2.0;NetFx
User-Agent: NuGet Add Package Dialog/2.0.30625.9003 (Microsoft Windows NT 6.1.7601 Service Pack 1)
Accept: application/atom+xml,application/xml
Accept-Charset: UTF-8
Host: localhost:29019
Accept-Encoding: gzip, deflate


?< 0007 0x0001 bytes
1
            */

                h.Context.Response.ContentType = "application/atom+xml";

                h.Context.Response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>

<feed xml:base=""http://localhost:59019/nuget/"" xmlns:d=""http://schemas.microsoft.com/ado/2007/08/dataservices"" xmlns:m=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"" xmlns=""http://www.w3.org/2005/Atom"">
  <title type=""text"">Search</title>
  <id>http://localhost:59019/nuget/Search</id>
  <updated>2012-10-21T12:42:05Z</updated>
  <link rel=""self"" title=""Search"" href=""Search"" />
  <entry>
    <id>http://localhost:59019/nuget/Packages(Id='TestNuGetSupport.Foo',Version='0.0.0.1')</id>
    <title type=""text"">TestNuGetSupport.Foo</title>
    <summary type=""text""></summary>
    <updated>2012-10-21T12:25:55Z</updated>
    <author>
      <name>John Doe</name>
    </author>
    <link rel=""edit-media"" title=""Package"" href=""Packages(Id='TestNuGetSupport.Foo',Version='0.0.0.1')/$value"" />
    <link rel=""edit"" title=""Package"" href=""Packages(Id='TestNuGetSupport.Foo',Version='0.0.0.1')"" />
    <category term=""NuGet.Server.DataServices.Package"" scheme=""http://schemas.microsoft.com/ado/2007/08/dataservices/scheme"" />
    <content type=""application/zip"" src=""http://localhost:59019/api/v2/package/testnugetsupport.foo/0.0.0.1"" />
    <m:properties xmlns:m=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"" xmlns:d=""http://schemas.microsoft.com/ado/2007/08/dataservices"">
      <d:Version>0.0.0.1</d:Version>
      <d:Title m:null=""true""></d:Title>
      <d:IconUrl m:null=""true""></d:IconUrl>
      <d:LicenseUrl m:null=""true""></d:LicenseUrl>
      <d:ProjectUrl m:null=""true""></d:ProjectUrl>
      <d:DownloadCount m:type=""Edm.Int32"">0</d:DownloadCount>
      <d:RequireLicenseAcceptance m:type=""Edm.Boolean"">false</d:RequireLicenseAcceptance>
      <d:Description>TestNuGetSupport.Foo !!</d:Description>
      <d:ReleaseNotes m:null=""true""></d:ReleaseNotes>
      <d:Published m:type=""Edm.DateTime"">2012-10-21T10:55:45.4825886Z</d:Published>
      <d:Dependencies></d:Dependencies>
      <d:PackageHash>H0LvR29MtBUeY8bPDkTW6lHhQfixAGjxScIIC97XUJyu7wPmrdQQmbV3cgAelyBEWpn38kTtg8xVMe0ujhChGg==</d:PackageHash>
      <d:PackageHashAlgorithm>SHA512</d:PackageHashAlgorithm>
      <d:PackageSize m:type=""Edm.Int64"">4495</d:PackageSize>
      <d:Copyright m:null=""true""></d:Copyright>
      <d:Tags m:null=""true""></d:Tags>
      <d:IsAbsoluteLatestVersion m:type=""Edm.Boolean"">true</d:IsAbsoluteLatestVersion>
      <d:IsLatestVersion m:type=""Edm.Boolean"">true</d:IsLatestVersion>
      <d:Listed m:type=""Edm.Boolean"">false</d:Listed>
      <d:VersionDownloadCount m:type=""Edm.Int32"">0</d:VersionDownloadCount>
      <d:Summary m:null=""true""></d:Summary>
    </m:properties>
  </entry>
</feed>

".Replace("localhost:59019", HostUri.Host + ":" + HostUri.Port)
                );

                h.CompleteRequest();
                return;
            }
            #endregion


            #region TestNuGetSupport.Foo.0.0.0.1.nupkg
            var x = "/api/v2/package/testnugetsupport.foo/0.0.0.1";

            if (h.Context.Request.Path == x)
            {
                var f = "assets/TestNuGetSupport.FeedServer/TestNuGetSupport.Foo.0.0.0.1.nupkg";

                // Could not find a part of the path 'V:\staging.net.debug\api\v2\package\testnugetsupport.foo\assets\TestNuGetSupport.FeedServer\TestNuGetSupport.Foo.0.0.0.1.nupkg'.
                // WebDev is relativet to request.
                h.Context.Response.WriteFile("/" + f);
                h.CompleteRequest();
                return;
            }
            #endregion


            if (h.IsDefaultPath)
            {
                h.Context.Response.Redirect("/jsc");
                h.CompleteRequest();
                return;
            }

            if (h.Context.Request.Path == "/jsc")
                return;

            h.Context.Response.StatusCode = 500;
            h.CompleteRequest();

        }
    }
}
