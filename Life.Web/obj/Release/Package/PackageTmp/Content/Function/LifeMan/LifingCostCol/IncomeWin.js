/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.IncomeWin", {
    extend: 'Ext.window.Window',
    requires: ['Ext.ux.NGrid'],
    title: '收入信息',
    modal: true,
    closeAction: 'hide',
    buttonAlign: 'center',
    layout: 'fit',
    width: 600,
    height: 400,
    items: [{
        xtype: 'ngrid',
        al: false,
        border: 0,
        dataUrl: '/Income/SelectByPage',
        rootValue: 'rows',
        multiSelect: false,
        isSelect:false,
        pagging: true,
        modelArray: [
            { name: 'Time', type: 'DateTime',
                convert: function (value) {
                    return Duanjt.Date.NumToDate(value);
                }
            },
            { name: 'Price', type: 'float' },
            { name: 'Note', type: 'String' },
            { name: 'CreateName', type: 'String' }
        ],
        columns: [
            { text: '时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
            { text: '金额', dataIndex: 'Price', flex: 1, minWidth: 0, align: 'right',
                renderer: function (value) {
                    return "￥" + Duanjt.Float.ToFloat(value, 2);
                }
            },
            { text: '创建人', dataIndex: 'CreateName', flex: 1, minWidth: 0 },
            { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0,
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

