/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.MonthGrid", {
    extend: 'Ext.ux.NGrid',
    alias: 'widget.lifingcostcolmonthgrid',
    al: false,
    border: 0,
    dataUrl: '/LifingCost/GetCollectionByMonth',
    rootValue: 'rows',
    multiSelect: true,
    pagging: false,
    features: [{
        ftype: 'summary'
    }],
    getParm: function () {
        var me = this;
        var data = {};
        data.beginTime = me.down("toolbar>datefield[name=beginTime]").getValue();
        if (data.beginTime)
            data.beginTime = data.beginTime.format('yyyy-MM-dd');
        data.endTime = me.down("toolbar>datefield[name=endTime]").getValue();
        if (data.endTime)
            data.endTime = data.endTime.format('yyyy-MM-dd');

        data.CostTypeId = me.down("toolbar>combo[name=CostTypeId]").getValue();
        data.IsMark = me.down("toolbar>checkbox[name=IsMark]").getValue();
        data.family = me.down("toolbar>checkbox[name=family]").getValue();

        if (data.CostTypeId)
            data.CostTypeId = data.CostTypeId.join(',');
        return data;
    },
    getBeginDate: function () {
        var _beginDate = new Date();
        _beginDate.setTime(_beginDate.getTime() - 365 * 24 * 60 * 60 * 1000);
        return _beginDate;
    },
    beginSearch: function () {
        var me = this;
        me.getStore().proxy.extraParams = me.getParm();
        me.getStore().reload();
    },
    getToolBar: function () {
        var me = this;
        return Ext.create("Ext.toolbar.Toolbar", {
            defaults: {
                labelAlign: 'right',
                labelWidth: 60
            },
            layout: {
            //overflowHandler: 'menu'
        },
        items: [{
            fieldLabel: '开始月份',
            xtype: 'datefield',
            name: 'beginTime',
            maxValue: new Date(),
            value: me.getBeginDate(),
            format: 'Y-m-d',
            width: 160
        }, {
            fieldLabel: '结束月份',
            xtype: 'datefield',
            name: 'endTime',
            maxValue: new Date(),
            value: new Date(),
            format: 'Y-m-d',
            width: 160
        }, {
            fieldLabel: '消费类型',
            name: 'CostTypeId',
            xtype: 'combo',
            multiSelect: true,
            displayField: 'Name',
            valueField: 'Id',
            queryMode: 'local',
            width: 250,
            store: Ext.create("Life.LifeMan.LifingCostCol.CostTypeStore")
        }, '-', {
            xtype: 'checkbox',
            boxLabel: '特殊标识',
            name: 'IsMark',
            inputValue: true
        }, {
            xtype: 'checkbox',
            boxLabel: '家庭内收支',
            name: 'family',
            inputValue: true
        }, {
            xtype: 'button',
            text: '查询',
            iconCls: 'i_search',
            handler: function () {
                me.beginSearch();
            }
        }]
    })
},
modelArray: [
        { name: 'Time', type: 'string' },
        { name: 'Pay', type: 'float' },
        { name: 'Income', type: 'float' },
        { name: 'Balance', type: 'float' }
    ],
columns: [
        { text: '月份', dataIndex: 'Time', width: 150, align: 'center',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "总和";
            }
        },
        { text: '收入', dataIndex: 'Income', width: 130, summaryType: 'sum', align: 'right',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            },
            renderer: function (value) {
                return "<span style='text-decoration:underline; color:Blue;cursor:pointer;' class='col'>￥" + value + "</span>";
            }
        },
        { text: '支出', dataIndex: 'Pay', flex: 1, minWidth: 100, summaryType: 'sum', align: 'right',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            },
            renderer: function (value) {
                return "￥" + value;
            }
        },
        { text: '结余', dataIndex: 'Balance', flex: 1, minWidth: 100, summaryType: 'sum', align: 'right',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            },
            renderer: function (value) {
                return "￥" + value;
            }
        }
    ],
initComponent: function () {
    var me = this;
    me.tbar = me.getToolBar();
    this.callParent(arguments);
}
});

