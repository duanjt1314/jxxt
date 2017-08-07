/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.JiaoXue.Student.SearchPanel", {
    extend: 'Ext.panel.Panel',
    alias: 'widget.incomesearchpanel',
    title: '查询条件',
    border: 0,
    layout: 'fit',
    buttonAlign: 'center',
    title: '查询条件',
    layout: 'fit',
    collapsed: true,
    collapsible: true,
    items: [{
        xtype: "form",
        id: "searchForm",
        border: 0,
        layout: {
            type: 'table',
            columns: 4
        },
        bodyStyle: "padding:5px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '身份证号', name: 'CardNo', xtype: 'textfield', width: 230 },
            { fieldLabel: '姓名', name: 'Name', xtype: 'textfield', width: 230 }            
        ]
    }],
    getButtons: function () {
        var me = this;
        return [{
            text: '查询',
            handler: function () {
                var parm = Ext.getCmp("searchForm").getValues();
                me.SetTotalPrice();
                me.fireEvent('search', me, parm);
            }
        }];
    },
    SetTotalPrice: function () {
        var me = this;
    },
    initComponent: function () {
        var me = this;
        me.buttons = me.getButtons();
        //注册事件
        this.addEvents('search');

        this.callParent(arguments);
    }
});

