var updatePassWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    layout:'fit',
    title: '密码修改',
    items: [{
        xtype: "form",
        id: "updatePassForm",
        bodyStyle: "padding-right:5px",
        layout: 'form',
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '旧密码', name: 'oldPass', xtype: 'textfield', inputType: 'password', allowBlank: false },
            { fieldLabel: '新密码', name: 'newPass', id: "newPass", xtype: 'textfield', inputType: 'password', allowBlank: false, vtype: "PwdFormat" },
            { fieldLabel: '重复密码', name: 'rNewPass', xtype: 'textfield', inputType: 'password', allowBlank: false, vtype: "Password", confirmTo: "newPass" }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            var form = Ext.getCmp("updatePassForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Manage/UpdatePass",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            updatePassWin.hide();
                        }
                    },
                    failure: function (form, action) {
                        Ext.Msg.alert("提示", action.result.msg);
                    }
                });
            }
        }
    }, {
        text: '取消',
        handler: function () {
            updatePassWin.hide();
        }
    }]
});