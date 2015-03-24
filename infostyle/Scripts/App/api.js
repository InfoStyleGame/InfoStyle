window.Api = {
    root: "/api",
    theCakeIsALie: false,
    GetTask: function (type, level) {
        if (Api.theCakeIsALie) {
            return mockDef(mockApi.GetTask_Result);
        }
        level = level - 1;
        return this._DoApiCall("/task/get?level=" + level + "&type=" + type, 'POST');
    },

    GetCards: function (level) {
        level = level - 1;
        if (Api.theCakeIsALie) {
            return mockDef(mockApi.GetCards_Result);
        }
        return this._DoApiCall("/task/card?level=" + level, 'POST');
    },

    SubmitTaskResults: function (level, score) {
        level = level - 1;
        if (Api.theCakeIsALie) {
            return mockDef();
        }
        return this._DoApiCall("/taskResult/post?score=" + score + "&level=" + level, 'POST');
    },

    GetUserInfo: function () {
        if (Api.theCakeIsALie) {
            return mockDef(mockApi.GetUserInfo_Result);
        }
        return this._DoApiCall("/userInfo/get", 'GET');
    },

    GetUserProgress: function () {
        if (Api.theCakeIsALie) {
            return mockDef(mockApi.GetUserProgress_Result);
        }
        return this._DoApiCall("/userProgress/get", 'get');
    },

    _DoApiCall: function (url, methodType) {
        return $.ajax(Api.root + url, {
            method: methodType
        }).fail(function (jqXHR) {
        	if (jqXHR.status == 403) {
		        window.location.hash = 'login';
	        }
            console.log("Error: " + JSON.stringify(jqXHR));
        });
    }
};

window.mockDef = function(result) {
    var $def = $.Deferred();
    $def.resolve(result);
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