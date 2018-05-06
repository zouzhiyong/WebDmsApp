//VUE(v-select)
Vue.directive('select2', {
    inserted: function (el, binding, vnode) {
        var options = binding.value || {};
        var defaultOpt = {
            placeholder: "--请选择--",
            allowClear: true,
            minimumResultsForSearch: -1,
            language: "zh-CN",
        };
        options = $.extend(defaultOpt, options);
        $(el).select2(options).on("select2:select", function (e) {
            var event = document.createEvent('HTMLEvents');
            event.initEvent("change", true, true);
            el.dispatchEvent(event);
            //绑定选中选项的事件
            options && options.onSelect && options.onSelect(e);
        });

        //allowClear:清除选中
        $(el).select2(options).on("select2:unselecting", function (e) {
            //清空这个值，这个值即vuejs model绑定的值
            e.target.value = "";
            var event = document.createEvent('HTMLEvents');
            event.initEvent("change", true, true);
            el.dispatchEvent(event);
        });

        //绑定select2 dom渲染完毕时触发的事件
        options && options.onInit && options.onInit();
    },
    update: function (el, binding, vnode) {
        $(el).trigger("change");
    }
});

Vue.directive('autocomplete', {
    inserted: function (el, binding, vnode) {
        var options = binding.value || {};
        var defaultOpt = {
            minChars: 1,
            selectFirst: false,
            minLength: 1,
        };
        options = $.extend(defaultOpt, options);
        $(el).autocomplete(options).focus(function () {
            $(el).autocomplete("search");
            return false;
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
        .append("<div>" + item.value + "- " + item.label + "</div>")
        .appendTo(ul);
        };
    },
    update: function (el, binding, vnode) {
        
    }
});