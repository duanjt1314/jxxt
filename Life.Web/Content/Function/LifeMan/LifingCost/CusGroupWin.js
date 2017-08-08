/**
* 作者：段江涛
* 时间：2014-11-03
* 描述：生活费信息 按月份汇总表格
*/
Ext.define("Life.LifeMan.LifingCost.CusGroupWin", {
    extend: 'Ext.ux.NEdit',    
    title: '标识修改',
    heigth: 300,
    width: 300,
    saveUrl: '/LifingCost/ModifyCusGroup',
    formItems: [{
        xtype: "form",
        layout: 'form',        
        bodyStyle: "padding-right:5px",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 50
        },
        items: [
            { fieldLabel: '编号', name: 'Ids', xtype: 'hidden' },
            { fieldLabel: '标识', name: 'Value', xtype: 'textarea' }        
        ]
    }],
    initComponent: function () {
        var me = this;        
        this.callParent(arguments);
    }
});

