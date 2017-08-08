/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按日汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.DayGrid", {
    extend: 'Ext.ux.NGrid',
    alias: 'widget.lifingcostcoldaygrid',
    title: '消费详细',
    region: "center",
    multiSelect: true,
    al: false,
    isSelect:false,
    pagging: false,
    dataUrl: '/LifingCost/GetCollectionByDay',
    rootValue: 'rows',
    columns: [
        { text: '消费时间', dataIndex: 'time', flex: 1, minWidth: 0 },
        { text: '消费金额', dataIndex: 'price', flex: 1, minWidth: 0, align: 'right',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        }
    ],
    modelArray: [
        { name: 'time', type: 'string',
            convert: function (value) {
                return value.substr(0, 10);
            }
         },
        { name: 'price', type: 'float' }
    ],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});

