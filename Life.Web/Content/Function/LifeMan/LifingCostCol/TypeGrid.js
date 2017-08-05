/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCostCol.TypeGrid", {
    extend: 'Ext.ux.NGrid',
    alias: 'widget.lifingcostcoltypegrid',
    title: '类型汇总',
    al: false,
    border: 0,
    dataUrl: '/LifingCost/GetCollectionType',
    rootValue: 'rows',
    isSelect: false,
    multiSelect: true,
    pagging: false,
    modelArray: [
        { name: 'costTypeName', type: 'string' },
        { name: 'price', type: 'float' }
    ],
    columns: [
        { text: '类型', dataIndex: 'costTypeName', flex: 1 },
        { text: '金额', dataIndex: 'price', flex: 1, align: 'right',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            } 
        }
    ],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});

