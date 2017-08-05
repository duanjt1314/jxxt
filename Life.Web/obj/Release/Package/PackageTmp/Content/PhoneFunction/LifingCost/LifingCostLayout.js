/**
*   引用地址：
*   创建人：樊小平
*   创建时间：2013/10/31
*   说明：数据字典 主布局
*/
Ext.define("Life.LifingCost.LifingCostLayout", {
    extend: 'Ext.Container',
    requires: ['Life.LifingCost.LifingCostList', 'Life.LifingCost.LifingCostEdit'],
    alias: 'widget.lifingcostlayout',
    config: {
        fullscreen: true,
        layout: 'card',
        items: [{
            xtype: 'lifingcostlist'
        }, {
            xtype: 'lifingcostedit'
        }]
    },
    constructor: function (config) {
        this.callParent(arguments);
    }
});