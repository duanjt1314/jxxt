Ext.Loader.require([
    "Ext.ux.RichText"
]);

//定义文章表模型
Ext.regModel('Article', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Title', type: 'String' },
        { name: 'Content', type: 'String' },
        { name: 'CateId', type: 'String' },
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
        }
    ]
});

/*文章表数据store*/
var articleGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "Article",
    proxy: {
        type: 'ajax',
        url: '/Article/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //articleGridStore.proxy.url = "/Article/SelectByPage?parentId="+parentId;
        }
    }
});

//文章窗体
var articleWin;

/*articleGrid展示界面*/
var articleGrid = Ext.create('Ext.grid.Panel', {
    title: '文章表信息',
    //region: "center",
    store: articleGridStore,
    multiSelect: true,
    columns: [
        { text: '文章标题', dataIndex: 'Title', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreateTime', width: 150 },
        { text: '最后修改时间', dataIndex: 'UpdateTime', width: 150 }
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
                //parent.AddShow("新增文章", "/Article/UpdateView");
                articleWin.show();
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
        }, '-', {
            text: '类别',
            xtype: 'splitbutton',
            iconCls: 'i_delete',
            handler: function () {
                ArticleSearch("");
            },
            listeners: {
                "render": function (but) {
                    var menu = Ext.create('Ext.menu.Menu', {});
                    //查询所有类别
                    Ext.Ajax.request({
                        url: '/ArtCategory/SelectAll',
                        async: false,
                        success: function (response) {
                            var data = Ext.JSON.decode(response.responseText);
                            for (var i = 0; i < data.length; i++) {
                                menu.add(
                                { text: data[i].CatName, id: data[i].CatId,
                                    handler: function (but) {
                                        ArticleSearch(but.id);
                                    }
                                });
                            }
                            but.menu = menu;
                        }
                    });
                }
            }
        }, '-',
        { xtype: 'button', text: '查看文章', iconCls: 'i_search',
            handler: function () {
                ArticleDetail();
            }
        }, '->', {
            text: '查询',
            xtype: 'searchfield',
            store: articleGridStore
        }
    ],
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: articleGridStore,
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
                }, '-', {
                    text: '文章查看',
                    iconCls: 'i_search',
                    handler: function () {
                        ArticleDetail();
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
    var records = articleGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        parent.AddShow("文章编辑", "/Article/UpdateView?id=" + record.get("Id"));
    }
}
/*删除数据*/
function DeleteData() {
    var records = articleGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });
        Ext.Ajax.request({
            url: '/Article/Delete',
            params: { ids: ids.join(',') },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    articleGrid.store.load();
            }
        })
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}
/*查看文章*/
function ArticleDetail() {
    var records = articleGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        parent.AddShow("文章查看", "/Article/Detail?id=" + record.get("Id"));
    }
}
/*文章类别*/
function ArticleSearch(cateId) {
    articleGridStore.proxy.extraParams.cateId = cateId;
    articleGridStore.load();
}

Ext.onReady(function () {
    //初始化弹出窗体，必须写在onReady里面
    articleWin = Ext.create("Ext.window.Window", {
        height: 400,
        width: 530,
        modal: true,
        closeAction: 'hide',
        title: '编辑页面',
        items: [{
            xtype: "form",
            id: "articleForm",
            border: 0,
            bodyStyle: "padding-top:2px",
            defaults: {
                labelAlign: 'right',
                labelWidth: 70
            },
            items: [
            { fieldLabel: '文章编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '文章标题', name: 'Title', xtype: 'textfield',width:500 },
            { fieldLabel: '文章内容', name: 'Content', xtype: 'richtext', width: 500,height:260 },
            { fieldLabel: '文章类型', name: 'CateId', xtype: 'textfield' },
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
                var form = Ext.getCmp("articleForm").getForm();
                if (form.isValid()) {
                    form.submit({
                        url: "/Article/Save",
                        success: function (form, action) {
                            Ext.Msg.alert('提示', action.result.msg);
                            if (action.result.success) {
                                articleWin.hide();
                                articleGrid.store.load();
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
                articleWin.hide();
            }
        }]
    });
    
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [articleGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    articleGrid.store.load();
});

