window.Api = {
    address: "http://K1307004.kontur:4444/api",
    GetTask: function(type) {
        return mockDef(["пыщь","бздыщь","херыщь"]);
        //return $.get(Api.address + "/GetTask", { type: type });
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