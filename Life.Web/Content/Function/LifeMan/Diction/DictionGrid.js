//定义字典表模型
Ext.regModel('Diction', {
    fields: [
        { name: 'Id', type: 'decimal' },
        { name: 'Name', type: 'String' },
        { name: 'Note', type: 'String' },
        { name: 'ParentId', type: 'decimal' },
        { name: 'OrderId', type: 'decimal' },
        { name: 'CreateBy', type: 'String' },
        { name: 'CreateTime', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        },
        { name: 'UpdateBy', type: 'String' },
        { name: 'UpdateTime', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        }
    ]
});

/*字典表数据store*/
var dictionGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Diction",
    proxy: {
        type: 'ajax',
        url: '/Diction/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            dictionGridStore.proxy.url = "/Diction/SelectByPage?parentId="+parentId;
        }
    }
});

/*新增字典表*/
var dictionWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '编辑页面',
    layout:'fit',
    items: [{
        xtype: "form",
        id: "dictionForm",
        bodyStyle: "padding-right:5px;",
        layout:'form',
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '字典名称', name: 'Name', xtype: 'textfield' },
            { fieldLabel: '序号', name: 'OrderId', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Note', xtype: 'textfield' },
            { fieldLabel: '父级编号', name: 'ParentId', xtype: 'hidden', id: "ParentId" },
            { fieldLabel: '创建者', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改者', name: 'UpdateBy', xtype: 'hidden' },
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
            Ext.getCmp("ParentId").setValue(parentId); //赋值
            var form = Ext.getCmp("dictionForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Diction/Save",
                    waitTitle: '系统提示',
                    waitMsg: '正在提交...',
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            dictionWin.hide();
                            dictionGrid.store.load();
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
            dictionWin.hide();
        }
    }]
});

/*dictionGrid展示界面*/
var dictionGrid = Ext.create('Ext.grid.Panel', {
    title: '字典表信息',
    region: "center",
    store: dictionGridStore,
    //draggable:true,
    multiSelect: true,
    columns: [
        { text: '字典名称', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0 },
        { text: '序号', dataIndex: 'OrderId', flex: 1, minWidth: 0 },
        { text: '创建者', dataIndex: 'CreateBy', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreateTime', flex: 1, minWidth: 0 },
        { text: '修改者', dataIndex: 'UpdateBy', flex: 1, minWidth: 0 },
        { text: '修改时间', dataIndex: 'UpdateTime', flex: 1, minWidth: 0 }
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
                dictionWin.show();
                //清空操作                                 
                var form = Ext.getCmp("dictionForm").getForm();
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
        store: dictionGridStore,
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
    var records = dictionGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要修改的数据");
    } else {
        var record = records[0];
        if (record != null) {
            dictionWin.show();
            var form = Ext.getCmp("dictionForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = dictionGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });
        Ext.Ajax.request({
            url: '/Diction/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    dictionGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}


