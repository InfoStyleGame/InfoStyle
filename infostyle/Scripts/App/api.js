window.Api = {
    root: "http://localhost:666/api",
    GetTask: function (type, level) {
        return $.post(Api.root + "/task/get?type=" + type + "&level=" + level)
            .fail(function(data) { console.log("Error: " + data); });
    },

    GetCards: function() {
        return $.post(Api.root + "/task/card?level=0")
            .fail(function (data) { console.log("Error: " + data); });
    },

    SubmitTaskResults: function(score, taskId) {
        return $.post(Api.root + "/taskResult/post?score=" + score + "&taskId=" + taskId);
    },

    GetUserInfo: function() {
        return $.get(Api.root + "/userInfo");
    },

    GetUserProgress: function() {
        return $.get(Api.root + "/userProgress");
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