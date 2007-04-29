///////////////////////////////////////////////////////////////////////////////
//
//  aghost.js
//
//  February 2007 Community Technology Preview
//
//  This file is provided by Microsoft as a helper file for websites that
//  incorporate pre-release WPF/E objects.  The 1.29.2007 version of the file
//  is configured to check for WPF/E version 0.8.5.0.  You may modify this file
//  for testing purposes.
//
//  copyright 2007, Microsoft Corporation
//
///////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////
//
//  sample code for detecting WPF/e and instantiating the correct HTML
//
///////////////////////////////////////////////////////////////////////////////
function agHost(hostElementId, id, width, height, backgroundColor, sourceElement, source, isWindowlessMode, framerate, errorHandler, reqMajorVer, reqMinorVer, reqBuildVer)
{
    var agHostHelper = new Object();

    agHostHelper.uaString = navigator.userAgent;
    agHostHelper.hostElementId = hostElementId;
    agHostHelper.id = id;
    agHostHelper.width = width;
    agHostHelper.height = height;
    agHostHelper.backgroundColor = backgroundColor;
    agHostHelper.sourceElement = sourceElement;
    agHostHelper.source = source;
    agHostHelper.isWindowlessMode = isWindowlessMode;
    agHostHelper.framerate = framerate;

    // if not set, the version defaults to "0.8.5.0"
    agHostHelper.reqMajorVer = (reqMajorVer != null) ? reqMajorVer : 0;
    agHostHelper.reqMinorVer = (reqMinorVer != null) ? reqMinorVer : 8;
    agHostHelper.reqBuildVer = (reqBuildVer != null) ? reqBuildVer : 5;

    // assign error handler
    if(errorHandler == null)
    {
        errorHandler = function(line, column, errorCode, errorstring)
        {
            if(line != 0 && column != 0)
            {
                var errorString = "(" + line + "," + column + "): " + errorstring + "\n";
                errorString += "ERROR CODE: " + errorCode;
                alert(errorString);
            }
        }
    }
    else
    {
        agHostHelper.errorHandler = errorHandler;
    }

    var wpfePluginHTML = "";

    // detect the browser to see if it's a supported version & that the correct version of WPF/e is installed
    if (browserIsSupportedVersion(agHostHelper))
    {
        if (supportedVersionIsInstalled(agHostHelper))
        {
            wpfePluginHTML = buildHTMLOutput(agHostHelper);
        }
        else
        {
            wpfePluginHTML = buildHTMLOutputToDownloadCurrentVersion(agHostHelper);
        }
    }
    else
    {
        wpfePluginHTML = buildHTMLOutputForUnsupportedBrowser(agHostHelper);
    }

    // insert the HTML into the requested host element
    document.getElementById(hostElementId).innerHTML = wpfePluginHTML;
}


///////////////////////////////////////////////////////////////////////////////
//
//  detect to see if this is a supported browser version
//
///////////////////////////////////////////////////////////////////////////////
function browserIsSupportedVersion(agHostHelper)
{
    var supportedBrowser = false;

    // detection for Internet Explorer 6.0+
    if (agHostHelper.uaString.indexOf('MSIE') != -1)
    {
        var tempVersion = agHostHelper.uaString.split("MSIE");
        browserMajorVersion = parseInt(tempVersion[1]);
        if (browserMajorVersion >= 6.0)
        {
            supportedBrowser = true;
        }
    }
    // detection for Firefox 1.5+ and 2.0
    else if (navigator.userAgent.indexOf("Firefox") != -1)
    {
        var tempVersion = agHostHelper.uaString.split("Firefox/");
        tempVersion = tempVersion[1].split(".");
        browserMajorVersion = parseFloat(tempVersion[0]);
        browserMinorVersion = parseFloat(tempVersion[1]);

        if (browserMajorVersion >= 2)
        {
            supportedBrowser = true;
        }
        else
        {
            if ((browserMinorVersion >= 5))
            {
                supportedBrowser = true;
            }
        }
    }
    else if (navigator.userAgent.indexOf("Safari") != -1)
    {
        supportedBrowser = true;
    }

    return supportedBrowser;
}



///////////////////////////////////////////////////////////////////////////////
//
//  detect plug-in version on Mozilla and Safari, and control version
//  on Internet Explorer.
//
///////////////////////////////////////////////////////////////////////////////
function detectAgControlVersion(agHostHelper)
{
    agVersion = -1;

    if ((navigator.plugins != null) && (navigator.plugins.length > 0))
    {
        if (navigator.plugins["WPFe Plug-In"])
        {
             agVersion = navigator.plugins["WPFe Plug-In"].description;
        }
    }
    else if ((agHostHelper.uaString.indexOf('Windows') != -1) && (navigator.appVersion.indexOf('MSIE') != -1) )
    {
        try
        {
            var AgControl = new ActiveXObject("AgControl.AgControl");
            agVersion = AgControl.version;
        }
        catch (e)
        {
            agVersion = -1;
        }
    }
    return agVersion;
}

///////////////////////////////////////////////////////////////////////////////
//
//  detect version needed, if not present returns false
//
///////////////////////////////////////////////////////////////////////////////
function supportedVersionIsInstalled(agHostHelper)
{
    var versionStr = detectAgControlVersion(agHostHelper);

    if (versionStr == -1 )
    {
        return false;
    }
    else if (versionStr != 0)
    {
        versionArray = versionStr.split(".");

        var versionMajor = versionArray[0];
        var versionMinor = versionArray[1];
        var versionBuild = versionArray[2];

        if (versionMajor > parseFloat(agHostHelper.reqMajorVer))
        {
            return true;
        }
        else if (versionMajor == parseFloat(agHostHelper.reqMajorVer))
        {
            if (versionMinor > parseFloat(agHostHelper.reqMinorVer))
            {
                return true;
            }
            else if (versionMinor == parseFloat(agHostHelper.reqMinorVer))
            {
                if (versionBuild >= parseFloat(agHostHelper.reqBuildVer))
                {
                    return true;
                }
            }
        }
        return false;
    }
}


///////////////////////////////////////////////////////////////////////////////
//
//  create HTML that points to the download center
//
///////////////////////////////////////////////////////////////////////////////
function buildHTMLOutputToDownloadCurrentVersion(agHostHelper)
{
    var downloadLocation = "";

    // set download location
    {
        if (agHostHelper.uaString.indexOf('Macintosh') != -1)
        {
            downloadLocation = "http://go.microsoft.com/fwlink/?linkid=77793&clcid=0x409";
        }
        else if (agHostHelper.uaString.indexOf('Windows') != -1)
        {
            downloadLocation = "http://go.microsoft.com/fwlink/?linkid=77792&clcid=0x409";
        }
    }

    var wpfePluginHTML = "";
    wpfePluginHTML += '<div style="width: 300px; padding: 6px 10px 2px 10px; margin: 6px; background-color: #E7E8D1;border: 4px solid #D4D3AA;font-family: Arial, Helvetica, sans-serif;">';
    wpfePluginHTML += '  <div><strong style="font-size: 20px;font-weight: bold;color: #444444;position: relative;top: 5px;">Install WPF/E</strong></div>';
    wpfePluginHTML += '  <div style="font-size: 14px;margin-bottom: 5px;color: #444444;margin-top: 7px;">';
    wpfePluginHTML += '    You must install the current version of WPF/E (codename) in order to view this content.&nbsp;';
    wpfePluginHTML += '    Get WPF/E <a href="' + downloadLocation + '" style="text-decoration: underline;color: #901708;">here</a>.';
    wpfePluginHTML += '    <div style="margin-top: 25px;text-align: center;font-size: 14px;background-color: #FBF9E6;">';
    wpfePluginHTML += '      <a href="http://www.microsoft.com/wpfe/" style="text-decoration: underline;color: #901708;">Learn More About WPF/E</a>';
    wpfePluginHTML += '    </div>';
    wpfePluginHTML += '  </div>';
    wpfePluginHTML += '</div>';

    return wpfePluginHTML;
}


///////////////////////////////////////////////////////////////////////////////
//
//  create HTML that points to the requirements page
//
///////////////////////////////////////////////////////////////////////////////
function buildHTMLOutputForUnsupportedBrowser(agHostHelper)
{
    var wpfePluginHTML = ""

    wpfePluginHTML += '<div style="width: 300px; padding: 6px 10px 2px 10px; margin: 6px; background-color: #E7E8D1;border: 4px solid #D4D3AA;font-family: Arial, Helvetica, sans-serif;">';
    wpfePluginHTML += '  <div><strong style="font-size: 20px;font-weight: bold;color: #444444;position: relative;top: 5px;">Check WPF/E requirements</strong></div>';
    wpfePluginHTML += '  <div style="font-size: 14px;margin-bottom: 5px;color: #444444;margin-top: 7px;">';
    wpfePluginHTML += '    Your current browser does not support WPF/E (codename).&nbsp;';
    wpfePluginHTML += '    <a href="http://www.microsoft.com/wpfe/" style="text-decoration: underline;color: #901708;">Click here</a> for more details on WPF/E-supported browsers and platforms.';
    wpfePluginHTML += '    <div style="margin-top: 25px;text-align: center;font-size: 14px;background-color: #FBF9E6;">';
    wpfePluginHTML += '      <a href="http://www.microsoft.com/wpfe/" style="text-decoration: underline;color: #901708;">Learn More About WPF/E</a>';
    wpfePluginHTML += '    </div>';
    wpfePluginHTML += '  </div>';
    wpfePluginHTML += '</div>';

    return wpfePluginHTML;
}



///////////////////////////////////////////////////////////////////////////////
//
//  create HTML that instantiates the control
//
///////////////////////////////////////////////////////////////////////////////
function buildHTMLOutput(agHostHelper)
{
    var wpfePluginHTML = "";
    if (agHostHelper.uaString.indexOf('MSIE') != -1)
    {
        wpfePluginHTML = buildHTMLForIE(agHostHelper);
    }
    else if ((agHostHelper.uaString.indexOf('Firefox') != -1) || (agHostHelper.uaString.indexOf('Safari') != -1))
    {
        wpfePluginHTML = buildHTMLForFirefoxOrSafari(agHostHelper);
    }

    return wpfePluginHTML;
}



///////////////////////////////////////////////////////////////////////////////
//
//  IE version of the control code
//
///////////////////////////////////////////////////////////////////////////////
function buildHTMLForIE(agHostHelper)
{
    var wpfePluginHTML = '<object id="'+agHostHelper.id+'" width="'+agHostHelper.width+'" height="'+agHostHelper.height+'" classid="CLSID:32C73088-76AE-40F7-AC40-81F62CB2C1DA">';

    if (agHostHelper.sourceElement != null)
    {
        wpfePluginHTML += ' <param name="SourceElement" value="'+agHostHelper.sourceElement+'" />';
    }
    if (agHostHelper.source != null)
    {
        wpfePluginHTML += ' <param name="Source" value="'+agHostHelper.source+'" />';
    }
    if (agHostHelper.framerate != null)
    {
        wpfePluginHTML += ' <param name="MaxFrameRate" value="'+agHostHelper.framerate+'" />';
    }
    if (agHostHelper.errorHandler != null)
    {
        wpfePluginHTML += ' <param name="OnError" value="'+agHostHelper.errorHandler+'" />';
    }
    if (agHostHelper.backgroundColor != null)
    {
        wpfePluginHTML += ' <param name="BackgroundColor" value="'+agHostHelper.backgroundColor+'" />';
    }
    if (agHostHelper.isWindowlessMode != null)
    {
        wpfePluginHTML += ' <param name="WindowlessMode" value="'+agHostHelper.isWindowlessMode+'" />';
    }
    wpfePluginHTML += '<\/object>';

    return wpfePluginHTML;
}


///////////////////////////////////////////////////////////////////////////////
//
//  Firefox / Safari version of the control code
//
///////////////////////////////////////////////////////////////////////////////
function buildHTMLForFirefoxOrSafari(agHostHelper)
{

    var wpfePluginHTML = '<embed id="' + agHostHelper.id + '" width="' + agHostHelper.width + '" height="' + agHostHelper.height + '"';

    if (agHostHelper.source != null)
    {
        wpfePluginHTML += '" Source="'+agHostHelper.source;
    }
    if (agHostHelper.sourceElement != null)
    {
        wpfePluginHTML += '" SourceElement="'+agHostHelper.sourceElement;
    }
    if (agHostHelper.framerate != null)
    {
        wpfePluginHTML += '" MaxFrameRate="'+agHostHelper.framerate;
    }
    if (agHostHelper.errorHandler != null)
    {
        wpfePluginHTML +='" OnError="'+agHostHelper.errorHandler;
    }
    if (agHostHelper.backgroundColor != null)
    {
        wpfePluginHTML += '" BackgroundColor="'+agHostHelper.backgroundColor;
    }
    if (agHostHelper.isWindowlessMode != null)
    {
        wpfePluginHTML += '" WindowlessMode="'+ agHostHelper.isWindowlessMode;

    }
    wpfePluginHTML += '" type="application/ag-plugin"/>';

    if (agHostHelper.uaString.indexOf("Safari") != -1)
    {
        // disable Safari caching
        // for more information, see http://developer.apple.com/internet/safari/faq.html#anchor5
        wpfePluginHTML += "<iframe style='visibility:hidden;height:0;width:0'/>";
    }

    return wpfePluginHTML;
}
