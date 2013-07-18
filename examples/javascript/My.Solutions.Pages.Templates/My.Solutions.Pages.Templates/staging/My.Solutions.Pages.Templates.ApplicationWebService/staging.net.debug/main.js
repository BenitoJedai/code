// http://stackoverflow.com/questions/16171474/filesystem-is-only-allowed-for-packaged-apps-and-this-is-a-legacy-packaged-ap

chrome.app.runtime.onLaunched.addListener(function () {
    chrome.app.window.create('index.htm', {
        bounds: {
            width: 500,
            height: 300
        }
    });
});