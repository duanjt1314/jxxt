//定义用户表模型
Ext.regModel('Users', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'LoginId', type: 'String' },
        { name: 'LoginPwd', type: 'String' },
        { name: 'Name', type: 'String' },
        { name: 'Phone', type: 'String' },
        { name: 'Mail', type: 'String' },
        { name: 'Address', type: 'String' },
        { name: 'Age', type: 'decimal' },
        { name: 'Notes', type: 'String' }
    ]
});

/*用户表数据store*/
var usersGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Users",
    proxy: {
        type: 'ajax',
        url: '/Users/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //usersGridStore.proxy.url = "/Users/SelectByParentId?parentId="+parentId;
        }
    }
});

/*用户表数据store*/
var roleStore = Ext.create("Ext.data.Store", {
    autoLoad: true,
    fields:["RoleId","RoleName"],
    proxy: {
        type: 'ajax',
        url: '/Users/GetAllRoles',
        reader: {
            type: 'json'
        }
    }
});

/*新增模块*/
var usersWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '用户管理',
    layout:'fit',
    items: [{
        xtype: "form",
        id: "usersForm",
        layout: 'form',
        bodyStyle:'padding-right:10px',
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '登录名', name: 'LoginId', xtype: 'textfield' },
            { fieldLabel: '密码', name: 'LoginPwd', xtype: 'hidden' },
            { fieldLabel: '真实姓名', name: 'Name', xtype: 'textfield' },
            { fieldLabel: '电话', name: 'Phone', xtype: 'textfield' },
            { fieldLabel: '邮件', name: 'Mail', xtype: 'textfield',allowBlank: false, vtype: "email" },
            { fieldLabel: '地址', name: 'Address', xtype: 'textfield' },
            { fieldLabel: '年龄', name: 'Age', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Notes', xtype: 'textarea' },
            { fieldLabel: '角色', name: 'Roles', xtype: 'combo',store:roleStore, editable: false, allowBlank: false,multiSelect:true,displayField: 'RoleName', valueField: 'RoleId' }
            /*文本框：textfield	文本域：textareafield|textarea	
            时间：timefield	 复选框：checkbox|checkboxfield	
            日期：datefield	 下拉框：combo|combobox
            文本：displayfield	 隐藏域：hidden|hiddenfield
            文件按钮:filebutton	文件：fileuploadfield|filefield
            HTML编辑:htmleditor 单选按钮：radio|radiofield
            数字：numberfield*/
            /*{ fieldLabel: '状态', name: 'Status', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'name', valueField: 'value', queryMode: 'local', width: 220,
            store: Ext.create('Ext.data.Store', {
            fields: ['name', 'value'],
            data: [{ name: '有效', value: '1' }, { name: '无效', value: '0'}]
            })
            }*/
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            //Ext.getCmp("ParentId").setValue(parentId);//父级模块赋值
            var form = Ext.getCmp("usersForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Users/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            usersWin.hide();
                            usersGrid.store.load();
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
            usersWin.hide();
        }
    }]
});

/*usersGrid展示界面*/
var usersGrid = Ext.create('Ext.grid.Panel', {
    //title: '用户信息',
    region: "center",
    store: usersGridStore,
    multiSelect: true,
    columns: [
        { text: '登录名', dataIndex: 'LoginId', flex: 1, minWidth: 0 },
        { text: '真实姓名', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '电话', dataIndex: 'Phone', flex: 1, minWidth: 0 },
        { text: '邮件', dataIndex: 'Mail', flex: 1, minWidth: 0 },
        { text: '地址', dataIndex: 'Address', flex: 1, minWidth: 0 },
        { text: '年龄', dataIndex: 'Age', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Notes', flex: 1, minWidth: 0 }
        /*{ text: '状态', dataIndex: 'Status', flex: 1, minWidth: 0, renderer: function (value) {
        if (value == "1")
        return "有效";
        else
        return "无效";
        }
        }*/
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                usersWin.show();
                //清空操作                                 
                var form = Ext.getCmp("usersForm").getForm();
                form.reset();
            }
        }, '-',
        { xtype: 'button', text: '修改', iconCls: 'i_edit',
            handler: function () {
                EditData();
            }
        }, '-',
        { xtype: 'button', text: '删除', iconCls: 'i_delete',
            handler: function () {
                DeleteData();
            }
        }
    ],
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: usersGridStore,
        dock: 'bottom',
        displayInfo: true
    }],
    listeners: {
        "itemcontextmenu": function (view, record, item, index, e, eOpts) {
            e.preventDefault();
            view.getSelectionModel().select(index);
            Ext.create("Ext.menu.Menu", {
                items: [{
                    text: '修改',
                    iconCls: 'i_edit',
                    handler: function () {
                        EditData();
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'i_delete',
                    handler: function () {
                        DeleteData();
                    }
                }]
            }).showAt(e.getXY());
        },
        "itemdblclick": function (View, record, item, index, e, options) {
            EditData();
        }
    }
});
/*编辑数据*/
function EditData() {
    var records = usersGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            usersWin.show();
            var form = Ext.getCmp("usersForm").getForm();
            form.loadRecord(record);

            //给角色赋值
            Ext.Ajax.request({
                url: '/Users/GetUserRole',
                params: { userId: record.get("Id") },
                success: function (response) {
                    var data = Ext.JSON.decode(response.responseText);
                    var roles = [];
                    for (var i = 0; i < data.length; i++) {
                        roles.push(data[i].RoleId);
                    }
                    form.findField("Roles").setValue(roles);
                }
            })
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = usersGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });
        Ext.Ajax.request({
            url: '/Users/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    usersGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [usersGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    usersGrid.store.load();
});

