window.Api = {
    root: "/api",
    useFakeApi: false,
	ignore403: false,
    GetTask: function (type, level) {
	    var url = "/task/get?level=" + level + "&type=" + type;
	    if (Api.useFakeApi) {
            return mockDef(url, mockApi.GetTask_Result);
        }
	    level = level - 1;
	    return this._DoApiCall(url, 'POST');
    },

    GetCards: function (level) {
	    level = level - 1;
	    var url = "/task/card?level=" + level;
	    if (Api.useFakeApi) {
            return mockDef(url, mockApi.GetCards_Result);
        }
	    return this._DoApiCall(url, 'POST');
    },

    SubmitTaskResults: function (level, score) {
	    level = level - 1;
	    var url = "/taskResult/post?score=" + score + "&level=" + level;
	    if (Api.useFakeApi) {
            return mockDef(url);
        }
	    return this._DoApiCall(url, 'POST');
    },

    GetUserInfo: function () {
	    var url = "/userInfo/get";
	    if (Api.useFakeApi) {
            return mockDef(url, mockApi.GetUserInfo_Result);
        }
	    return this._DoApiCall(url, 'GET');
    },

    GetUserProgress: function () {
	    var url = "/userProgress/get";
	    if (Api.useFakeApi) {
            return mockDef(url, mockApi.GetUserProgress_Result);
        }
	    return this._DoApiCall(url, 'get');
    },

    _DoApiCall: function (url, methodType) {
        return $.ajax(Api.root + url, {
            method: methodType
        }).fail(function (jqXHR) {
        	if (jqXHR.status == 403 && !Api.ignore403) {
		        window.location.hash = 'login';
	        }
            console.log("Error: " + JSON.stringify(jqXHR));
        });
    }
};

window.mockDef = function(url, result) {
    var $def = $.Deferred();
    $def.resolve(result);
	if (result) {
		console.log("Mock def result for url " + url + " will be following:");
		console.log(result);
	} else {
		console.log("Mock def result for url " + url + " was called.");
	}
	return $def.promise();
};

window.mockApi = {
    GetCards_Result: [
        {
            "Id": "0318927b-b955-40dc-9513-2373138e0d40",
            "Type": "Card",
            "Data": {
                "Text": [
                    { "Text": "Ни для кого не секрет", "Type": "Stop" },
                    { "Text": ", что причина комплексов взрослого кроется в глубоком детстве.", "Type": "Normal" }]
            }
        }, {
            "Id": "726c8e80-b955-40dc-9513-2373138e0d40",
            "Type": "Card",
            "Data": {
                "Text": [
                    { "Text": "Общеизвестно", "Type": "Stop" },
                    { "Text": ", что крысы покидают судно, если в трюмах поднимается вода.", "Type": "Normal" }]
            }
        }, {
            "Id": "85218c97-9767-4d68-90b5-af782e69f096",
            "Type": "Card",
            "Data": {
                "Text": [
                    { "Text": "Новогоднюю ёлку, ", "Type": "Normal" },
                    { "Text": "впрочем", "Type": "Stop" }, { "Text": ", выносят до майских праздников.", "Type": "Normal" }]
            }
        }],
    GetUserInfo_Result: {
        "Name": "Максим Ильяхов",
        "PictureSrc": "/Content/resources/avatar.png",
        "Score": 250
    },
    GetTask_Result: {
        "Id": "4a42d787-f7fc-4ed5-8c60-d2a286962704",
        "Type": "CrawlLine",
        "Data": {
            "Text": [
                { "Text": "Безмятежный пуховый дятел живет, ", "Type": "Normal" },
                { "Text": "как правило", "Type": "Stop" },
                { "Text": ", в зарослях ландышей и незабудок. Он хитёр!", "Type": "Normal" }
            ]
        }
    },
    GetUserProgress_Result: { "CurrentLevel": 3 }
}

window.TaskTypes = {
    CrawlLine: "CrawlLine"
};

window.TextTypes = {
    Normal: "Normal",
    Stop : "Stop"
};