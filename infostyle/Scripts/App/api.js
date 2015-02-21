window.Api = {
    address: "http://norris.kontur:4444/api",
    GetTask: function(type) {
        return mockDef({
            Type: TaskTypes.CrawlLine,
            Data: {
                Text: [
                    { Text: "В рамках", Type: "Stop" },
                    { Text: "ежегодного фестиваля техники", Type: "Normal" },
                    { Text: "продемонстрировали", Type: "Normal" },
                    { Text: "свыше", Type: "Stop" },
                    { Text: "ста", Type: "Normal" },
                    { Text: "совершенно новых", Type: "Stop" },
                    { Text: "экспонатов", Type: "Normal" }
                ]
            }
        });
        //return $.get(Api.address + "/task/get", { type: type });
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