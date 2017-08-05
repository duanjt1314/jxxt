//定义角色表模型
Ext.regModel('Roles', {
    fields: [
        { name: 'RoleId', type: 'String' },
        { name: 'RoleName', type: 'String' },
        { name: 'Notes', type: 'String' }
    ]
});

/*角色表数据store*/
var rolesGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Roles",
    proxy: {
        type: 'ajax',
        url: '/Roles/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //rolesGridStore.proxy.url = "/Roles/SelectByParentId?parentId="+parentId;
        }
    }
});

/*新增模块*/
var rolesWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '角色信息',
    layout:'fit',
    items: [{
        xtype: "form",
        layout: 'form',
        bodyStyle:'padding-right:5px',
        id: "rolesForm",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '角色编号', name: 'RoleId', xtype: 'hidden' },
            { fieldLabel: '角色名称', name: 'RoleName', xtype: 'textfield' },
            { fieldLabel: '备注', name: 'Notes', xtype: 'textfield' }
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
            var form = Ext.getCmp("rolesForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Roles/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            rolesWin.hide();
                            rolesGrid.store.load();
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
            rolesWin.hide();
        }
    }]
});

/*rolesGrid展示界面*/
var rolesGrid = Ext.create('Ext.grid.Panel', {
    //title: '角色信息',
    store: rolesGridStore,
    multiSelect: true,
    columns: [
        { text: '角色名称', dataIndex: 'RoleName', flex: 1, minWidth: 0 },
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
                rolesWin.show();
                //清空操作                                 
                var form = Ext.getCmp("rolesForm").getForm();
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
        store: rolesGridStore,
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
                    iconCls: 'editicon',
                    handler: function () {
                        EditData();
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'deleteicon',
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
    var records = rolesGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            rolesWin.show();
            var form = Ext.getCmp("rolesForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = rolesGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("RoleId"));
        });
        Ext.Ajax.request({
            url: '/Roles/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    rolesGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [rolesGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    rolesGrid.store.load();
});

