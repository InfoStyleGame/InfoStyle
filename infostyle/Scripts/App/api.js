window.Api = {
    root: "http://norris.kontur:4444/api",
    GetTask: function (type, level) {
        level = level - 1;
        return $.post(Api.root + "/task/get?level="+ level +"&type=" + type)
            .fail(function(data) { console.log("Error: " + data); });
    },

    GetCards: function (level) {
        level = level - 1;
       return $.post(Api.root + "/task/card?level=" + level)
            .fail(function (data) { console.log("Error: " + JSON.stringify(data)); });
    },

    SubmitTaskResults: function (level, score) {
        level = level - 1;
        return $.post(Api.root + "/taskResult/post?score=" + score + "&level=" + level);
    },

    GetUserInfo: function() {
        return $.get(Api.root + "/userInfo/get");
    },

    GetUserProgress: function() {
        return $.get(Api.root + "/userProgress/get");
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