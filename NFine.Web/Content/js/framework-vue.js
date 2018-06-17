
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

//数字显示千分符保留2位小数<span v-number="item.value"></span>，结果:<span>1.00</span>
Vue.directive('number', {
    bind: function (el, binding, vnode) {
        
    },
    update: function (el, binding, vnode) {
        var value = binding.value;
        if (!!isNaN(parseFloat(value))) {
            el.innerHTML = value;
        } else {
            el.innerHTML = parseFloat(value).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        }
    }
})
