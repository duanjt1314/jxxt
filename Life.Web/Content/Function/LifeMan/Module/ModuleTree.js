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

/*新增父级模块*/
var pModuleWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '模块信息',
    layout:'fit',
    items: [{
        xtype: "form",
        id: "pModuleForm",
        layout: 'form',
        bodyStyle:'padding-right:5px',
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '模块编号', name: 'ModuleId', xtype: 'hidden' },
            { fieldLabel: '模块名称', name: 'ModuleName', xtype: 'textfield' },
            { fieldLabel: '模块路径', name: 'ModuleUrl', xtype: 'hidden' },
            { fieldLabel: '图标', name: 'IconUrl', id: "PIconUrl", xtype: 'textfield', listeners: {
                focus: function () {
                    iconId = "PIconUrl";
                    checkImageWin.show();
                }
            }
            },
            { fieldLabel: '父级模块', name: 'ParentId', xtype: 'hidden', value: "0" },
            { fieldLabel: '序号', name: 'OrderId', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Notes', xtype: 'hidden' },
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
            var form = Ext.getCmp("pModuleForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/Module/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            pModuleWin.hide();
                            moduleTree.store.load();
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
            pModuleWin.hide();
        }
    }]
});

var moduleTreeStore = Ext.create("Ext.data.TreeStore", {
    autoLoad: false,
    fields: ["id", "text", "Tobject", "leaf"],
    root: {
        //expanded: true,
        text: '父级菜单',
        id: 0
    },
    proxy: {
        type: 'ajax',
        url: '/Module/Select'
    }
});

/*模块的树界面*/
var moduleTree = Ext.create("Ext.tree.Panel", {
    width: 220,
    region: 'west',
    split: true,
    collapsed: false,
    collapsible: true,
    store: moduleTreeStore,
    rootVisible: false,
    title: '父级模块',
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                Ext.getCmp("pModuleForm").getForm().reset();
                pModuleWin.show();
            }
        }, '-', { xtype: 'button', text: '刷新', iconCls: 'i_refresh',
            handler: function () {
                moduleTree.store.load();
            }
        }
    ],
    listeners: {
        "selectionchange": function (model, selected, eOpts) {
            var records = model.getSelection();
            if (records.length > 0) {
                parentId = records[0].get("id");
                moduleGrid.store.load();
            }
        },
        "load": function () {
            var node = moduleTree.getRootNode().firstChild;
            if (node != null) {
                var path = node.getPath();
                moduleTree.selectPath(path);
            } else
                moduleGrid.store.removeAll();
        },
        "itemcontextmenu": function (view, record, item, index, e, eOpts) {
            e.preventDefault();
            var menu=Ext.create("Ext.menu.Menu", {
                items: [{
                    text: '修改',
                    iconCls: 'editicon',
                    handler: function () {
                        //获得选中的数据
                        var data = moduleTree.getSelectionModel().lastSelected.data.Tobject;
                        var b = moduleTree.getSelectionModel().getSelection();
                        var record = Ext.create('Module', data);
                        Ext.getCmp("pModuleForm").loadRecord(record);
                        //单独赋值
                        pModuleWin.show();
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'deleteicon',
                    handler: function () {
                        //获得选中的数据
                        var record = moduleTree.getSelectionModel().lastSelected;

                        Ext.Ajax.request({
                            url: 'Delete',
                            async: false,
                            params: { ids: record.data.id },
                            success: function (response) {
                                var result = Ext.JSON.decode(response.responseText);
                                if (!result.success) {
                                    Ext.Msg.alert("提示", result.msg);
                                } else {
                                    moduleTree.store.load();
                                }
                            }
                        });
                    }
                }]
            }).showAt(e.getXY());
        }
    }
});