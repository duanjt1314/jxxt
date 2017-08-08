/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按日汇总表格
*/
Ext.define("Life.LifeMan.BankCardCalc.Grid", {
    extend: 'Ext.ux.NGrid',
    alias: 'widget.bankcardcalcgrid',
    region: "center",
    multiSelect: true,
    al: true,
    isSelect: false,
    pagging: false,
    dataUrl: '/BankCard/SelectCalc',
    rootValue: 'rows',
    features: [{
        ftype: 'summary'
    }],
    isToolbar: true,
    isInsert: false,
    isModify: false,
    isDelete: false,
    pushItems: ['时间', {
        xtype: 'datefield',
        format: 'Y-m-d'
    }, {
        xtype: 'button',
        text: '查询',
        handler: function (but) {
            var grid = but.up("bankcardcalcgrid");
            var time = grid.down("datefield").getValue();
            if (time)
                time = time.format('yyyy-MM-dd');
            grid.getStore().proxy.extraParams = { time: time };
            grid.getStore().load();
        }
    }],
    columns: [
        { text: '时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
        { text: '银行卡名称', dataIndex: 'BankTypeName', flex: 1, minWidth: 0,
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "合计";
            }
        },
        { text: '当前余额', dataIndex: 'Balance', flex: 1, minWidth: 0, align: 'left',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            },
            summaryType: 'sum',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        }
    ],
    modelArray: [
        { name: 'Time', type: 'String',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'BankTypeName', type: 'string' },
        { name: 'Balance', type: 'float' }
    ],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});

