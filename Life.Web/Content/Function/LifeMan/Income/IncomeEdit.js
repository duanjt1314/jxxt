Ext.define("Life.Income.IncomeEdit", {
    extend: 'Ext.ux.NEdit',
    alias: 'widget.incomeedit',
    formItems: [{
        xtype: "form",
        layout: 'form',
        id: "incomeForm",
        bodyStyle: "padding-right:5px",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '操作时间', name: 'Time', xtype: 'datefield', format: 'Y-m-d' },
            { fieldLabel: '操作金额', name: 'Price', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Note', xtype: 'textarea' },
            { fieldLabel: '创建者', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改者', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
        ]
    }],
    initComponent: function () {
        this.callParent(arguments);
    }
});
