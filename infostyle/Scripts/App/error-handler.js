window.onerror = function (msg, url, line, col, error) {
	if(window.handleScriptErrors) {
		window.location.href = "/Static/Error.html"
		return true;
	}
};

