window.CrawlLine = Base.extend({
    constructor: function(opt) {
        this.data = opt.data;
        this.$container = opt.$container;
    },

    init: function () {
        this.$el = $("<div>").addClass("marquee");
        var $scroller = $("<span>").addClass("content").appendTo(this.$el);
        for (var i = 0; i < this.data.length; i++) {
            var $word = this.buildWord(this.data[i]);
            $scroller.append($word);
        }
        this.$container.append(this.$el);
    },

    buildWord: function(word) {
        var $elem = $("<paper-button>/>").text(word);
        return $elem;
    }


});