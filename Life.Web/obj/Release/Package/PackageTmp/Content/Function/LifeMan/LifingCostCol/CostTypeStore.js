/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.CostTypeStore", {
    extend: 'Ext.data.Store',
    alias: 'widget.costtypestore',
    autoLoad: true,
    fields: ["Id", "Name"],
    proxy: {
        type: 'ajax',
        url: '/Diction/SelectByParentId',
        extraParams: { parentId: "1000300000" },
        reader: {
            type: 'json'
        }
    },
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});

