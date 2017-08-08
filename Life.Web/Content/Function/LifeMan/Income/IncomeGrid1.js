//定义收入记录表模型
Ext.regModel('Income', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Time', type: 'DateTime', 
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            } 
        },
        { name: 'Price', type: 'double' },
        { name: 'Note', type: 'String' },
        { name: 'CreateBy', type: 'String' },
        { name: 'CreateTime', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDateTime(value);
            } 
        },
        { name: 'UpdateBy', type: 'String' },
        { name: 'UpdateTime', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDateTime(value);
            }
        },

        { name: 'CreateName', type: 'String' },
        { name: 'UpdateName', type: 'String' }
    ]
});

/*收入记录表数据store*/
var incomeGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Income",
    proxy: {
        type: 'ajax',
        url: '/Income/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
        }
    }
});

/*新增收入记录表*/
var incomeWin = Ext.create("Ext.window.Window", {
    heigth: 300,
    width: 300,
    modal: true,
    layout: 'fit',
    closeAction: 'hide',
    title: '编辑页面',
    items: [{
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
            var form = Ext.getCmp("incomeForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Income/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            incomeWin.hide();
                            incomeGrid.store.load();
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
            incomeWin.hide();
        }
    }]
});

/*incomeGrid展示界面*/
var incomeGrid = Ext.create('Ext.grid.Panel', {    
    //region: "center",
    store: incomeGridStore,
    selType: 'checkboxmodel',     
    multiSelect: true,
    columns: [        
        { text: '操作时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
        { text: '操作金额', dataIndex: 'Price', flex: 1, minWidth: 0, renderer: function (value) {
            return "￥" + Duanjt.Float.ToFloat(value, 2);
        } 
        },
        { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0 },
        { text: '创建者', dataIndex: 'CreateName', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreateTime', flex: 1, minWidth: 0 },
        { text: '修改者', dataIndex: 'UpdateName', flex: 1, minWidth: 0 },
        { text: '修改时间', dataIndex: 'UpdateTime', flex: 1, minWidth: 0 }
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                incomeWin.show();
                //清空操作                                 
                var form = Ext.getCmp("incomeForm").getForm();
                form.reset();
                form.findField("Time").setValue(new Date());
                form.findField("Time").focus();
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
        store: incomeGridStore,
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
    var records = incomeGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            incomeWin.show();
            var form = Ext.getCmp("incomeForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = incomeGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/Income/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    incomeGrid.store.load();
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [incomeGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    incomeGrid.store.load();
});

