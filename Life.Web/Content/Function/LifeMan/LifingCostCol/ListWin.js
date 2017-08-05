/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.ListWin", {
    extend: 'Ext.window.Window',
    requires: ['Ext.ux.NGrid'],
    title: '详细信息',
    modal: true,
    closeAction: 'hide',
    buttonAlign: 'center',
    layout: 'fit',
    width: 600,
    height:400,
    items: [{
        xtype: 'ngrid',
        al: false,
        border: 0,
        dataUrl: '/LifingCost/SelectByPage',
        rootValue: 'rows',
        multiSelect: true,
        pagging: true,
        modelArray: [
            { name: 'Id', type: 'String' },
            { name: 'Time', type: 'DateTime',
                convert: function (value) {
                    return Duanjt.Date.NumToDate(value);
                }
            },
            { name: 'Reason', type: 'String' },
            { name: 'Price', type: 'double' },
            { name: 'Notes', type: 'String' },

            { name: 'CostTypeName', type: 'string' }
        ],
        columns: [
            { text: '消费时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
            { text: '消费名称', dataIndex: 'Reason', flex: 1, minWidth: 0 },
            { text: '消费金额', dataIndex: 'Price', flex: 1, minWidth: 0, align: 'right', renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
            },
            { text: '消费类型', dataIndex: 'CostTypeName', flex: 1, minWidth: 0 },
            { text: '备注', dataIndex: 'Notes', flex: 1, minWidth: 0,
                renderer: function (value) {
                    return '<div title="' + value + '">' + value + '</div>';
                }
            }
        ]
    }],
    getButtons: function () {
        var me = this;
        return [{
            text: '关闭',
            handler: function () {
                me.hide();
            }
        }];
    },
    initComponent: function () {
        var me = this;
        me.buttons = me.getButtons();
        this.callParent(arguments);
    }
});

