//定义系统配置模型
Ext.regModel('SysConfig', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Name', type: 'String' },
        { name: 'SysKey', type: 'String' },
        { name: 'SysValue', type: 'String' },
        { name: 'Remark', type: 'String' },
        { name: 'GroupNo', type: 'String' },
        { name: 'IsVisible', type: 'bool' },
        { name: 'OrderId', type: 'int' },
        { name: 'CreateBy', type: 'String' },
        { name: 'CreateTime', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'UpdateBy', type: 'String' },
        { name: 'UpdateTime', type: 'DateTime' }
    ]
});

/*系统配置数据store*/
var sysConfigGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "SysConfig",
    proxy: {
        type: 'ajax',
        url: '/SysConfig/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //sysConfigGridStore.proxy.url = "/SysConfig/SelectByParentId?parentId="+parentId;
        }
    }
});

/*新增系统配置*/
var sysConfigWin = Ext.create("Ext.window.Window", {
    heigth: 300,
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '编辑页面',
    layout:'fit',
    items: [{
        xtype: "form",
        layout:'form',
        id: "sysConfigForm",
        border: 0,
        bodyStyle: "padding-right:5px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '唯一标识', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '名称', name: 'Name', xtype: 'textfield' },
            { fieldLabel: '键', name: 'SysKey', xtype: 'textfield' },
            { fieldLabel: '值', name: 'SysValue', xtype: 'textfield' },            
            { fieldLabel: '分组标识', name: 'GroupNo', xtype: 'textfield' },
            { fieldLabel: '是否显示', name: 'IsVisible', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'name', valueField: 'value', queryMode: 'local', width: 220,
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'value'],
                    data: [{ name: '是', value: true }, { name: '否', value: false}]
                })
            },
            { fieldLabel: '排序序号', name: 'OrderId', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Remark', xtype: 'textarea' },
            { fieldLabel: '创建人编号', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改人编号', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
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
            //Ext.getCmp("ParentId").setValue(parentId);//赋值
            var form = Ext.getCmp("sysConfigForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/SysConfig/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            sysConfigWin.hide();
                            sysConfigGrid.store.load();
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
            sysConfigWin.hide();
        }
    }]
});

/*sysConfigGrid展示界面*/
var sysConfigGrid = Ext.create('Ext.grid.Panel', {
    title: '系统配置信息',
    //region: "center",
    store: sysConfigGridStore,
    multiSelect: true,
    columns: [
        { text: '名称', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '键', dataIndex: 'SysKey', flex: 1, minWidth: 0 },
        { text: '值', dataIndex: 'SysValue', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Remark', flex: 1, minWidth: 0 },
        { text: '分组标识', dataIndex: 'GroupNo', flex: 1, minWidth: 0 },
        { text: '是否显示', dataIndex: 'IsVisible', flex: 1, minWidth: 0 },
        { text: '排序序号', dataIndex: 'OrderId', flex: 1, minWidth: 0 }
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
                sysConfigWin.show();
                //清空操作                                 
                var form = Ext.getCmp("sysConfigForm").getForm();
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
        store: sysConfigGridStore,
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
    var records = sysConfigGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要修改的数据");
    } else {
        var record = records[0];
        if (record != null) {
            sysConfigWin.show();
            var form = Ext.getCmp("sysConfigForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = sysConfigGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });
        Ext.Ajax.request({
            url: '/SysConfig/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    sysConfigGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [sysConfigGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    sysConfigGrid.store.load();
});

