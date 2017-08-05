/**
* 作者：段江涛
* 时间：2014-12-09
* 描述：银行卡类型
*/
Ext.define("Life.LifeMan.BankType.Grid", {
    extend: 'Ext.ux.NGrid',
    alias: 'widget.banktypegrid',
    multiSelect: true,
    al: false,
    isSelect:false,
    pagging: false,
    isToolbar: true,
    dataUrl: '/BankType/SelectByPage',
    rootValue: 'rows',
    columns: [
        { text: '银行卡名称', dataIndex: 'BankName', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Remark', flex: 1, minWidth: 0}
    ],
    modelArray: [
        { name: 'Id', type: 'String' },
        { name: 'BankName', type: 'String' },
        { name: 'Remark', type: 'String' },
        { name: 'CreateBy', type: 'String' },
        { name: 'CreateTime', type: 'String',
            convert: function (value) {
                return Duanjt.Date.NumToDateTime(value);
            }
        },
        { name: 'UpdateBy', type: 'String' },
        { name: 'UpdateTime', type: 'String' },
        { name: 'IsUse', type: 'String' }
    ],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});

