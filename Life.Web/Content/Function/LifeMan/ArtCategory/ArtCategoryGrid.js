//定义文章类型表模型
Ext.regModel('ArtCategory', {
    fields: [
        { name: 'CatId', type: 'String' },
        { name: 'CatName', type: 'String' },
        { name: 'CatRemark', type: 'String' },
        { name: 'CatOrder', type: 'int' },
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
        "CreateName", "UpdateName"
    ]
});

/*文章类型表数据store*/
var artCategoryGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "ArtCategory",
    proxy: {
        type: 'ajax',
        url: '/ArtCategory/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //artCategoryGridStore.proxy.url = "/ArtCategory/SelectByParentId?parentId="+parentId;
        }
    }
});

/*新增文章类型表*/
var artCategoryWin = Ext.create("Ext.window.Window", {
    heigth: 300,
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '编辑页面',
    items: [{
        xtype: "form",
        layout:'form',
        id: "artCategoryForm",
        border: 0,
        bodyStyle: "padding-right:2px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '类型编号', name: 'CatId', xtype: 'hidden' },
            { fieldLabel: '类型名称', name: 'CatName', xtype: 'textfield' },
            { fieldLabel: '类型备注', name: 'CatRemark', xtype: 'textfield' },
            { fieldLabel: '排序', name: 'CatOrder', xtype: 'hidden', value: 0 },
            { fieldLabel: '创建人', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改人', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            //Ext.getCmp("ParentId").setValue(parentId);//赋值
            var form = Ext.getCmp("artCategoryForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/ArtCategory/Save",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            artCategoryWin.hide();
                            artCategoryGrid.store.load();
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
            artCategoryWin.hide();
        }
    }]
});

/*artCategoryGrid展示界面*/
var artCategoryGrid = Ext.create('Ext.grid.Panel', {
    title: '文章类型表信息',
    //region: "center",
    store: artCategoryGridStore,
    multiSelect: true,
    viewConfig: {
        plugins: {
            ptype: 'gridviewdragdrop',
            dragText: '拖动到目标位置放开'
        },
        listeners: {
            drop: function (node, data, overModel, dropPosition, dropHandlers, eOpts) {
                var tempData = artCategoryGrid.getStore().data.items;
                var resData = [];
                for (var i = 0; i < tempData.length; i++) {
                    tempData[i].data.CatOrder = i;
                    resData.push(tempData[i].data);
                }
                
                Ext.Ajax.request({
                    url: '/ArtCategory/UpdateOrder',
                    params: { dataStr: JSON.stringify(resData) },
                    success: function (response) {
                        //执行成功，不做任何处理
                    }
                })
            }
        }
    },
    columns: [
        { text: '类型名称', dataIndex: 'CatName', flex: 1, minWidth: 0 },
        { text: '类型备注', dataIndex: 'CatRemark', flex: 1, minWidth: 0 },        
        { text: '创建人', dataIndex: 'CreateName', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreateTime', flex: 1, minWidth: 0 },
        { text: '修改人', dataIndex: 'UpdateName', flex: 1, minWidth: 0 },
        { text: '修改时间', dataIndex: 'UpdateTime', flex: 1, minWidth: 0 }
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                artCategoryWin.show();
                //清空操作                                 
                var form = Ext.getCmp("artCategoryForm").getForm();
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
        store: artCategoryGridStore,
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
    var records = artCategoryGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要修改的数据");
    } else {
        var record = records[0];
        if (record != null) {
            artCategoryWin.show();
            var form = Ext.getCmp("artCategoryForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = artCategoryGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("CatId"));
        });
        Ext.Ajax.request({
            url: '/ArtCategory/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    artCategoryGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [artCategoryGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    artCategoryGrid.store.load();
});

