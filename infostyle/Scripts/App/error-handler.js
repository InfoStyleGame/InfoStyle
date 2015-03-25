window.onerror = function (msg, url, line, col, error) {
	window.location.hash = 'error';
	return true;
};

