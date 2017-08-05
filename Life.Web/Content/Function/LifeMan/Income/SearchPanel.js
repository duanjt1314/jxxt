/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.Income.SearchPanel", {
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
            { fieldLabel: '开始时间', name: 'startTime', xtype: 'datefield', format: 'Y-m-d', width: 230 },
            { fieldLabel: '结束时间', name: 'endTime', xtype: 'datefield', format: 'Y-m-d', width: 230 },
            { fieldLabel: '关键信息', name: 'key', xtype: 'textfield', width: 230 },
            { fieldLabel: '包含标识', name: 'cusGroup', xtype: 'textfield', width: 230, emptyText: '多个参数用逗号(,)分隔' },
            { fieldLabel: '排除标识', name: 'cusGroupNo', xtype: 'textfield', width: 230, emptyText: '多个参数用逗号(,)分隔' },
            {
                xtype: 'radiogroup',
                fieldLabel: '特殊标识',
                columns: 3,
                width: 230,
                vertical: true,
                items: [
                    { boxLabel: '全部', name: 'searchMark', inputValue: '', checked: true },
                    { boxLabel: '是', name: 'searchMark', inputValue: '1' },
                    { boxLabel: '否', name: 'searchMark', inputValue: '0' }
                ]
            },
            {
                xtype: 'radiogroup',
                fieldLabel: '家庭内支出',
                columns: 3,
                width: 230,
                vertical: true,
                items: [
                    { boxLabel: '全部', name: 'searchFamily', inputValue: '', checked: true },
                    { boxLabel: '是', name: 'searchFamily', inputValue: '1' },
                    { boxLabel: '否', name: 'searchFamily', inputValue: '0' }
                ]
            },
            { fieldLabel: '金额', xtype: 'fieldcontainer', layout: 'hbox', width: 230,
                items: [{
                    name: 'minPrice',
                    xtype: 'numberfield',
                    width: 60
                }, {
                    xtype: 'displayfield',
                    value: '到',
                    width: 18
                }, {
                    name: 'maxPrice',
                    xtype: 'numberfield',
                    width: 70
                }]
            }
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
        }, {
            xtype: 'displayfield',
            action: 'showPrice',
            value: '总金额:250'
        }];
    },
    SetTotalPrice: function () {
        var me = this;
        var parm = Ext.getCmp("searchForm").getValues();

        Ext.Ajax.request({
            url: '/Income/GetTotalPrice',
            params: parm,
            success: function (response) {
                var result = Ext.JSON.decode(response.responseText);
                var label = me.down("displayfield[action=showPrice]");
                if (result.success) {
                    label.setValue("总金额:￥" + result.data);
                } else {
                    label.setValue("查询错误");
                }
            }
        });
    },
    initComponent: function () {
        var me = this;
        me.buttons = me.getButtons();
        //注册事件
        this.addEvents('search');

        this.callParent(arguments);
    }
});

