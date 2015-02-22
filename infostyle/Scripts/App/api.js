window.Api = {
    root: "http://norris.kontur:4444/api",
    GetTask: function (type, subject) {
        return $.get(Api.root + "/task/get", { type: type, level: 0 })
            .fail(function(data) { console.log("Error: " + data); });
    },

    GetCards: function () {
//        return mockDef({
//            Id: "mockId",
//            Data: [
//                [
//                    { Text: "1В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
//                    { Text: "свыше ", Type: TextTypes.Stop },
//                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
//                ],
//                [
//                    { Text: "2В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
//                    { Text: "свыше ", Type: TextTypes.Stop },
//                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
//                ],
//                [
//                    { Text: "3В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
//                    { Text: "свыше ", Type: TextTypes.Stop },
//                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
//                ],
//            ]
//        });
        return $.post(Api.root + "/task/card?level=0")
            .fail(function (data) { console.log("Error: " + JSON.stringify(data)); });
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