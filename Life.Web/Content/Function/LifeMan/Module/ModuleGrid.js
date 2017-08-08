
//定义模块表模型
Ext.regModel('Module', {
    fields: [
        { name: 'ModuleId', type: 'string' },
        { name: 'ModuleName', type: 'string' },
        { name: 'ModuleUrl', type: 'string' },
        { name: 'IconUrl', type: 'string' },
        { name: 'ParentId', type: 'string' },
        { name: 'OrderId', type: 'string' },
        { name: 'Notes', type: 'string' },
        { name: 'Status', type: 'string' }
    ]
});

/*moduleGrid数据*/
var moduleGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Module",
    proxy: {
        type: 'ajax',
        url: '/Module/SelectByParentId',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            moduleGridStore.proxy.url = "/Module/SelectByParentId?parentId="+parentId;
        }
    }
});

var checkImageStore = Ext.create('Ext.data.Store', {
    autoLoad: true,
    fields:["url"],
    proxy: {
        type: 'ajax',
        url: '/Manage/GetIcons',
        reader: {
            type: 'json'
        }
    }
});

//选择图片窗口
var checkImageWin = Ext.create('Ext.window.Window', {
    id: 'images-view',
    frame: true,
    collapsible: true,
    width: 535,
    height: 200,
    modal: true,
    closeAction: 'hide',
    autoScroll: true,
    title: '选择图片',
    items: [{
        xtype: 'dataview',
        store: checkImageStore,
        tpl: new Ext.XTemplate(
            '<tpl for=".">',
            '<div class="thumb-wrap"><img src="{url}" title="{url}" width="16" height="16"/></div>',
            '</tpl>'
        ),
        itemSelector: 'div.thumb-wrap',
        listeners: {
            "itemclick": function (view, record) {
                Ext.getCmp(iconId).setValue(record.get("url"));
                checkImageWin.hide();
            }
        }
    }]
});

/*新增模块*/
var moduleWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '模块信息',
    layout:'fit',
    items: [{
        xtype: "form",
        layout: 'form',
        bodyStyle:'padding-right:5px',
        id: "moduleForm",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '模块编号', name: 'ModuleId', xtype: 'hidden' },
            { fieldLabel: '模块名称', name: 'ModuleName', xtype: 'textfield' },
            { fieldLabel: '模块路径', name: 'ModuleUrl', xtype: 'textfield' },
            { fieldLabel: '图标', name: 'IconUrl', id: 'IconUrl', xtype: 'textfield', listeners: {
                focus: function () {
                    iconId = "IconUrl";
                    checkImageWin.show();
                }
            }
            },
            { fieldLabel: '父级模块', name: 'ParentId', xtype: 'hidden', id: 'ParentId' },
            { fieldLabel: '序号', name: 'OrderId', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Notes', xtype: 'textarea' },
            { fieldLabel: '状态', name: 'Status', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'name', valueField: 'value', queryMode: 'local', width: 220,
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'value'],
                    data: [{ name: '有效', value: '1' }, { name: '无效', value: '0'}]
                })
            }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            Ext.getCmp("ParentId").setValue(parentId); //父级模块赋值
            var form = Ext.getCmp("moduleForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Module/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            moduleWin.hide();
                            moduleGrid.store.load();
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
            moduleWin.hide();
        }
    }]
});

/*moduleGrid展示界面*/
var moduleGrid = Ext.create('Ext.grid.Panel', {
    title: '模块信息',
    region: "center",
    store: moduleGridStore,
    multiSelect: true,
    columns: [
        { text: '模块名称', dataIndex: 'ModuleName', flex: 1, minWidth: 0 },
        { text: '模块路径', dataIndex: 'ModuleUrl', flex: 1, minWidth: 0 },
        { text: '图标', dataIndex: 'IconUrl', flex: 1, minWidth: 0,renderer:function(value){
            return "<img src='"+value+"' width='16' />";
        }
        },
        { text: '序号', dataIndex: 'OrderId', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Notes', flex: 1, minWidth: 0 },
        { text: '状态', dataIndex: 'Status', flex: 1, minWidth: 0, renderer: function (value) {
            if (value == "1")
                return "有效";
            else
                return "无效";
        }
        }
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                if (parentId == '-1') {
                    Ext.Msg.alert("提示", "请选择一个父级模块");
                    return;
                }
                moduleWin.show();
                //清空操作                                 
                var form = Ext.getCmp("moduleForm").getForm();
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
        store: moduleGridStore,
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

function EditData() {
    var records = moduleGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            moduleWin.show();
            var form = Ext.getCmp("moduleForm").getForm();
            form.loadRecord(record);            
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}

function DeleteData() {
    var records = moduleGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("ModuleId"));
        });
        Ext.Ajax.request({
            url: '/Module/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    moduleGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}