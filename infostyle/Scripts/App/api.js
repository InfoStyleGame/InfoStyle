window.Api = {
    root: "http://norris.kontur:4444/api",
    GetTask: function (type, subject) {
        return $.get(Api.root + "/task", { type: type, subject: subject })
            .fail(function(data) { console.log("Error: " + data); });
    },

    GetCards: function() {
        return mockDef({
            Id: "mockId",
            Data: [
                [
                    { Text: "1В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
                    { Text: "свыше ", Type: TextTypes.Stop },
                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
                ],
                [
                    { Text: "2В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
                    { Text: "свыше ", Type: TextTypes.Stop },
                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
                ],
                [
                    { Text: "3В рамках ежегодного фестиваля техники продемонстрировали ", Type: TextTypes.Normal },
                    { Text: "свыше ", Type: TextTypes.Stop },
                    { Text: "ста совершенно новых экспанатов", Type: TextTypes.Normal }
                ],
                
            ]
    })
    },

    SubmitTaskResults: function(score, taskId) {
        return $.post(Api.root + "/taskResult?score=" + score + "&taskId=" + taskId);
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

window.Subjects = {
    Stop: "Stop"
};

window.TextTypes = {
    Normal: "Normal",
    Stop : "Stop"
}