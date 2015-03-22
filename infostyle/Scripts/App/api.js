window.Api = {
    root: "/api",
    GetTask: function (type, level) {
        level = level - 1;
        return this._DoApiCall("/task/get?level=" + level + "&type=" + type, 'POST');
    },

    GetCards: function (level) {
        level = level - 1;
        return this._DoApiCall("/task/card?level=" + level, 'POST');
    },

    SubmitTaskResults: function (level, score) {
        level = level - 1;
        return this._DoApiCall("/taskResult/post?score=" + score + "&level=" + level, 'POST');
    },

    GetUserInfo: function() {
        return this._DoApiCall("/userInfo/get", 'GET');
    },

    GetUserProgress: function() {
        return this._DoApiCall("/userProgress/get", 'get');
    },

    _DoApiCall: function (url, methodType) {
        return $.ajax(Api.root + url, {
            method: methodType
        }).fail(function (jqXHR) {
            if (jqXHR.status == 403) {
                //todo Valera: here it is, 'event' on unauthorized
                window.requireAuthorization = 1;
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

window.TaskTypes = {
    CrawlLine: "CrawlLine"
};

window.TextTypes = {
    Normal: "Normal",
    Stop : "Stop"
}