$(document).ready(function() {
    if (window.Polymer === undefined)
	    window.location.href = "/Static/BadBrowser.html";
    else {
    	if (!window.DOMTokenList) // IE9 detection (Polymer is defined there, but won't work)
    		window.location.href = "/Static/BadBrowser.html";
    }
});