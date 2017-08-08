//家庭内用户收支功能
//支出和收入默认为家庭内，支出类型为“其它”
Ext.define("Life.LifeMan.LifingCost.LifingOutInWin", {
    extend: 'Ext.ux.NEdit',
    requires: ["Life.LifeMan.Users.UsersCombo"],
    title: '家庭内用户收支',
    heigth: 300,
    width: 300,
    saveUrl: '/LifingCost/OutIn',
    formItems: [{
        xtype: "form",
        layout: 'form',
        bodyStyle: "padding-right:5px",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 60
        },
        items: [
            { fieldLabel: '消费时间', name: 'time', xtype: 'datefield', allowBlank: false, format: 'Y-m-d', allowBlank: false },
            { fieldLabel: '支出用户', name: 'outUser', xtype: 'userscombo', allowBlank: false },
            { fieldLabel: '支出金额', name: 'outPrice', xtype: 'numberfield', allowBlank: false },
            { fieldLabel: '收入用户', name: 'inUser', xtype: 'userscombo', allowBlank: false },
            { fieldLabel: '备注', name: 'remark', xtype: 'textarea', allowBlank: false }
        ]
    }],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});