//定义生活费操作管理模型
Ext.regModel('LifingCost', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Time', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'Reason', type: 'String' },
        { name: 'Price', type: 'double' },
        { name: 'CostTypeId', type: 'decimal' },
        { name: 'Notes', type: 'String' },
        { name: 'ImgUrl', type: 'String' },
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
        { name: 'IsMark', type: 'bool' },
        { name: 'FamilyPay', type: 'bool' },

        { name: 'CostTypeName', type: 'string' },
        { name: 'CreateName', type: 'string' },
        { name: 'UpdateName', type: 'string' },
        { name: 'CusGroup', type: 'string' }
    ]
});

/*生活费操作管理数据store*/
var lifingCostGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引    
    model: "LifingCost",
    proxy: {
        type: 'ajax',
        url: '/LifingCost/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //lifingCostGridStore.proxy.url = "/LifingCost/SelectByParentId?parentId="+parentId;
        }
    }
});

/*生活费类型的store*/
var lifeTypedStore = Ext.create("Ext.data.Store", {
    autoLoad: true,
    fields: ["Id", "Name"],
    proxy: {
        type: 'ajax',
        url: '/Diction/SelectByParentId',
        extraParams: { parentId: "1000300000" },
        reader: {
            type: 'json'
        }
    }
});

var ds = Ext.create('Ext.data.Store', {
    //pageSize: 10,
    fields: ['Reason'],
    proxy: {
        type: 'ajax',
        url: '/LifeMan/LifingCost/GetReasons',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    }
});

/*新增生活费操作管理*/
var lifingCostWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 300,
    title: '生活费管理',
    saveUrl: '/LifingCost/Save',
    formItems: [{
        xtype: "form",
        id: "lifingCostForm",
        border: 0,
        layout: 'form',
        bodyStyle: "padding-right:5px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '消费时间', name: 'Time', xtype: 'datefield', format: 'Y-m-d', allowBlank: false, width: 230 },
            { fieldLabel: '消费名称', name: 'Reason', xtype: 'combo', allowBlank: false, width: 230,
                store: ds,
                displayField: 'Reason',
                typeAhead: false,
                hideLabel: false,
                hideTrigger: true,
                valueField: 'Reason',
                queryParam: 'key',
                minChars: 1,
                listConfig: {
                    loadingText: '查找中...',
                    emptyText: '没有数据',

                    // Custom rendering template for each item
                    getInnerTpl: function () {
                        return '{Reason}';
                    }
                }
            },
            { fieldLabel: '消费金额', name: 'Price', xtype: 'numberfield', allowBlank: false, minValue: 0, width: 230 },
            { fieldLabel: '消费类型', name: 'CostTypeId', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', width: 230, store: lifeTypedStore, value: 1000300001 },
            { fieldLabel: '备注', name: 'Notes', xtype: 'textarea', width: 230 },
            { fieldLabel: '图片', name: 'ImgUrl', xtype: 'filefield', buttonText: '选择' },
            {
                xtype: 'panel',
                layout: 'hbox',
                border: 0,
                defaults: {
                    labelAlign: 'right',
                    labelWidth: 70
                },
                items: [
                    { fieldLabel: '特殊标识', name: 'IsMark', xtype: 'checkbox', value: false, flex: 1, inputValue: true },
                    { fieldLabel: '家庭内支出', name: 'FamilyPay', xtype: 'checkbox', value: false, flex: 1, inputValue: true }
                ]
            },
            { fieldLabel: '创建者', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改者', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' },
            { fieldLabel: '自定义分组', name: 'CusGroup', xtype: 'hidden' }

        ]
    }],
    IsUse: true,
    listeners: {
        "success": function (data) {
            lifingCostWin.hide();
            lifingCostGrid.getStore().load();
        },
        "use": function (data) {
            lifingCostGrid.getStore().load(); //刷新  
            var form = lifingCostWin.down("form").getForm();
            form.setValues({
                Reason: '',
                Price: '',
                Notes: '',
                ImgUrl: ''
            });

            form.findField("Reason").focus();
        },
        "render": function () {
            var map = new Ext.util.KeyMap({
                target: 'lifingCostForm', //Id
                key: 13, // or Ext.EventObject.ENTER
                fn: function (e) {
                    var button = lifingCostWin.down("button[action=use]");
                    if (button) {
                        button.fireEvent("click");
                    }
                },
                scope: this
            });
        }
    }
});

/*导入Excel*/
var ImportWin = Ext.create("Ext.window.Window", {
    width: 280,
    modal: true,
    layout: 'fit',
    closeAction: 'hide',
    buttonAlign: 'center',
    title: '生活费导入',
    //plain: true,
    constrain: true,
    layout: 'fit',
    items: [{
        xtype: "form",
        id: "ImportForm",
        border: 0,
        layout: 'form',
        bodyStyle: "padding-right:5px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '导入文件', name: 'impExcel', xtype: 'filefield', buttonText: "选择", width: 230 }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            //Ext.getCmp("ParentId").setValue(parentId);//赋值
            var form = Ext.getCmp("ImportForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/LifingCost/ImportExcel",
                    success: function (form, action) {
                        ExtjsUtil.ShowInfoMsg("提示", action.result.msg);
                        if (action.result.success) {
                            ImportWin.hide();
                            lifingCostGrid.store.load();
                        }
                    },
                    failure: function (form, action) {
                        ExtjsUtil.ShowInfoMsg("提示", action.result.msg);
                    }
                });
            }
        }
    }, {
        text: '取消',
        handler: function () {
            ImportWin.hide();
        }
    }]
});

var outInWin = Ext.create("Life.LifeMan.LifingCost.LifingOutInWin", {
    listeners: {
        success: function (action) {
            console.log(action);
            ExtjsUtil.ShowInfoMsg("提示", action.result.msg);
            if (action.result.success) {
                outInWin.hide();
                lifingCostGrid.getStore().load();
            }
        }
    }
});

/*lifingCostGrid展示界面*/
var lifingCostGrid = Ext.create('Ext.grid.Panel', {
    //title: '生活费操作管理信息',
    region: "center",
    store: lifingCostGridStore,
    multiSelect: true,
    plugins: [
        Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1
        })
    ],
    selType: 'checkboxmodel',
    columns: [
        { text: '消费时间', dataIndex: 'Time', width: 80 },
        { text: '消费名称', dataIndex: 'Reason', width: 130 },
        { text: '消费金额', dataIndex: 'Price', width: 70, align: 'right', summaryType: 'sum', renderer: function (value) {
            return "￥" + Duanjt.Float.ToFloat(value, 2);
        }
        },
        { text: '消费类型', dataIndex: 'CostTypeName', width: 100 },
        { text: '备注', dataIndex: 'Notes', flex: 1, minWidth: 150 },
        { text: '创建者', dataIndex: 'CreateName', width: 70 },
        { text: '创建时间', dataIndex: 'CreateTime', width: 140 },
        { text: '修改者', dataIndex: 'UpdateName', width: 70, hidden: true },
        { text: '修改时间', dataIndex: 'UpdateTime', width: 140, hidden: true },
        { text: '图标', dataIndex: 'ImgUrl', width: 50,
            renderer: function (value) {
                if (value != "" && value)
                    return "<img onclick=\"ShowImg('" + value + "')\" src='/Content/Images/icon16/@asset.gif'/>";
            }
        },
        { text: '特殊标识', dataIndex: 'IsMark', width: 80, align: 'center',
            xtype: 'actioncolumn',
            items: [{
                iconCls: 'i_star_red',
                tooltip: '单击修改',
                getClass: function (v, mete, record) {
                    if (record.get("IsMark"))
                        return 'i_star_red';
                    else
                        return "i_star_white";
                },
                handler: function (grid, rowIndex, colIndex) {
                    var rec = lifingCostGrid.getStore().getAt(rowIndex);
                    rec.data.IsMark = rec.data.IsMark == false;

                    Ext.Ajax.request({
                        url: '/LifeMan/LifingCost/Save',
                        params: rec.data,
                        success: function (response, opts) {
                            rec.commit();
                        },
                        failure: function (response, opts) {

                        }
                    });


                }
            }]
        },
        { text: '家庭内支出', dataIndex: 'FamilyPay', width: 70, align: 'center',
            xtype: 'actioncolumn',
            items: [{
                iconCls: 'i_star_red',
                tooltip: '单击修改',
                getClass: function (v, mete, record) {
                    if (record.get("FamilyPay"))
                        return 'i_star_red';
                    else
                        return "i_star_white";
                },
                handler: function (grid, rowIndex, colIndex) {
                    var rec = lifingCostGrid.getStore().getAt(rowIndex);
                    rec.data.FamilyPay = !rec.data.FamilyPay;

                    Ext.Ajax.request({
                        url: '/LifeMan/LifingCost/Save',
                        params: rec.data,
                        success: function (response, opts) {
                            rec.commit();
                        },
                        failure: function (response, opts) {

                        }
                    });
                }
            }]
        },
        { text: '标识', dataIndex: 'CusGroup', width: 100,
            editor: {
                xtype: 'textfield',
                allowBlank: true
            }
        }
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                lifingCostWin.show();
                //清空操作                                 
                var form = lifingCostWin.down("form").getForm();
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
        }, '-',
        { xtype: 'button', text: '批量修改标识', iconCls: 'i_edit',
            handler: function () {
                var records = lifingCostGrid.getSelectionModel().getSelection();
                if (records.length > 0) {
                    var ids = [];
                    Ext.Array.each(records, function (record) {
                        ids.push(record.get("Id"));
                    });

                    if (!lifingCostGrid._edit) {
                        lifingCostGrid._edit = Ext.create("Life.LifeMan.LifingCost.CusGroupWin", {
                            listeners: {
                                success: function () {
                                    lifingCostGrid._edit.hide();
                                    lifingCostGrid.store.load();
                                }
                            }
                        });
                    }

                    lifingCostGrid._edit.show();
                    var form = lifingCostGrid._edit.down("form").getForm();
                    //清空
                    form.reset();
                    //赋值
                    form.setValues({ Ids: ids.join(',') });
                } else {
                    Ext.Msg.alert("提示", "请至少选择一行");
                }
            }
        }, '-',
        { xtype: 'button', text: '导入Excel', iconCls: 'i_undo',
            handler: function () {
                ImportWin.show();
            }
        }, '-',
        { xtype: 'button', text: 'Excel模版', iconCls: 'i_redo',
            handler: function () {
                window.open("/LifeMan/LifingCost/GetTemp");
            }
        }, '-', {
            xtype: 'button', text: '家庭内收支', iconCls: 'i_add',
            handler: function () {
                var form = outInWin.down("form").getForm();
                form.reset();
                outInWin.show();
            }
        }, '->', {
            xtype: 'button', text: '帮助', iconCls: 'i_save',
            handler: function () {
                var me = this;
                if (!this.win) {
                    var allGroup = "";
                    Ext.Ajax.request({
                        url: '/LifingCost/GetAllCusGroup',
                        async: false,
                        success: function (response) {
                            var result = Ext.decode(response.responseText);
                            for (var i = 0; i < result.length; i++) {
                                if (allGroup) {
                                    allGroup += ",";
                                }
                                allGroup += result[i];
                            }
                        }
                    });

                    this.win = Ext.create('Ext.window.Window', {
                        title: '说明',
                        height: 200,
                        width: 400,
                        closeAction: 'hide',
                        buttonAlign: 'center',
                        buttons: [{
                            text: '确定',
                            handler: function () {
                                me.win.hide();
                            }
                        }],
                        html: '<div style="font-size:14px"><span style="color:red">特殊标识</span>:指公司出差消费(可以报销),给朋友代购商品(后面会给钱)的情况<br/>'
                            + '<span style="color:red">家庭内支出</span>:指从家庭成员中的一人把钱给家庭成员的其它人的情况(双方分别记录收入和支出)<br/>'
                            + '<span style="color:red">所有标识</span>:' + allGroup + '</div>'
                    });
                }
                this.win.show();
            }
        }
    ],
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: lifingCostGridStore,
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
                },'-', {
                    text: '复制',
                    iconCls: 'i_add',
                    handler: function () {
                        CopyData();
                    }
                } ,'-', {
                    text: '查看图片',
                    iconCls: 'i_search',
                    handler: function () {
                        var records = lifingCostGrid.getSelectionModel().getSelection();
                        if (records.length <= 0) {
                            Ext.Msg.alert("提示", "请选择要修改的数据");
                        } else {
                            var record = records[0];
                            ShowImg(record.get("ImgUrl"));
                        }


                    }
                }]
            }).showAt(e.getXY());
        },
        "edit": function (editor, e) {
            if (e.value != e.originalValue) {
                Ext.Ajax.request({
                    url: '/LifingCost/Save',
                    params: e.record.data,
                    success: function (response) {
                        var result = Ext.JSON.decode(response.responseText);
                        if (result.success) {
                            e.record.commit();
                        } else {
                            Ext.Msg.alert("提示", result.msg);
                            e.record.reject();
                        }
                    }
                });
            }
        }
    }
});

var searchPanel = Ext.create("Ext.panel.Panel", {
    width: 280,
    region: "north",
    layout: 'fit',
    buttonAlign: 'center',
    title: '查询条件',
    layout: 'fit',
    collapsed: true,
    collapsible: true,
    items: [{
        xtype: "form",
        id: "searchForm",
        border: 0,
        layout: {
            type: 'table',
            columns: 4
        },
        bodyStyle: "padding:5px",
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '开始时间', name: 'startTime', xtype: 'datefield', format: 'Y-m-d', width: 230 },
            { fieldLabel: '结束时间', name: 'endTime', xtype: 'datefield', format: 'Y-m-d', width: 230 },
            { fieldLabel: '关键信息', name: 'key', xtype: 'textfield', width: 230 },
            { fieldLabel: '消费类型', name: 'CostTypeId', xtype: 'combo', multiSelect: true, displayField: 'Name', valueField: 'Id', queryMode: 'local', width: 230, store: lifeTypedStore },
            { fieldLabel: '包含标识', name: 'cusGroup', xtype: 'textfield', width: 230, emptyText: '多个参数用逗号(,)分隔' },
            { fieldLabel: '排除标识', name: 'cusGroupNo', xtype: 'textfield', width: 230, emptyText: '多个参数用逗号(,)分隔' },
            {
                xtype: 'radiogroup',
                fieldLabel: '特殊标识',
                columns: 3,
                width: 230,
                vertical: true,
                items: [
                    { boxLabel: '全部', name: 'searchMark', inputValue: '', checked: true },
                    { boxLabel: '是', name: 'searchMark', inputValue: '1' },
                    { boxLabel: '否', name: 'searchMark', inputValue: '0' }
                ]
            },
            {
                xtype: 'radiogroup',
                fieldLabel: '家庭内支出',
                columns: 3,
                width: 230,
                vertical: true,
                items: [
                    { boxLabel: '全部', name: 'searchFamily', inputValue: '', checked: true },
                    { boxLabel: '是', name: 'searchFamily', inputValue: '1' },
                    { boxLabel: '否', name: 'searchFamily', inputValue: '0' }
                ]
            },
            { fieldLabel: '金额', xtype: 'fieldcontainer', layout: 'hbox', width: 230,
                items: [{
                    name: 'minPrice',
                    xtype: 'numberfield',
                    width: 60
                }, {
                    xtype: 'displayfield',
                    value: '到',
                    width: 18
                }, {
                    name: 'maxPrice',
                    xtype: 'numberfield',
                    width: 70
                }]
            }
        ]
    }],
    buttons: [{
        text: '查询',
        handler: function () {
            lifingCostGrid.store.proxy.url = "/LifingCost/SelectByPage";
            var parm = Ext.getCmp("searchForm").getValues();
            if (parm.CostTypeId)
                parm.CostTypeId = parm.CostTypeId.join(',');
            lifingCostGrid.store.proxy.extraParams = parm;
            lifingCostGrid.store.loadPage(1);

            SetTotalPrice();
        }
    }, {
        xtype: 'displayfield',
        action: 'showPrice',
        value: '总金额:250'
    }]
});
/*编辑数据*/
function EditData() {
    var records = lifingCostGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要修改的数据");
    } else {
        var record = records[0];
        if (record != null) {
            lifingCostWin.show();
            var form = lifingCostWin.down("form").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = lifingCostGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/LifingCost/Delete",
            params: { ids: ids.join(',') },
            success: function (result) {
                lifingCostGrid.store.load();
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}
/*复制数据*/
function CopyData() {
    var records = lifingCostGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要复制的数据");
    } else {
        var record = records[0];
        if (record != null) {
            lifingCostWin.show();
            var form = lifingCostWin.down("form").getForm();
            console.log(record);
            record.set("Id", "");
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*查看图片*/
function ShowImg(url) {
    Ext.create("Ext.ux.ShowImg", {
        imgUrl: url
    }).show();
}
/*设置总金额*/
function SetTotalPrice() {
    var parm = Ext.getCmp("searchForm").getValues();
    if (parm.CostTypeId)
        parm.CostTypeId = parm.CostTypeId.join(',');

    Ext.Ajax.request({
        url: '/LifingCost/GetTotalPrice',
        params: parm,
        success: function (response) {
            var result = Ext.JSON.decode(response.responseText);
            var label = searchPanel.down("displayfield[action=showPrice]");
            if (result.success) {
                label.setValue("总金额:￥" + result.data);
            } else {
                label.setValue("查询错误");
            }
        }
    });

}

/*鼠标移上去显示图片*/
lifingCostGrid.getView().on('render', function (view, obj) {
    view.tip = Ext.create('Ext.tip.ToolTip', {
        // The overall target element.
        target: view.el,
        // Each grid row causes its own separate show and hide.
        delegate: view.itemSelector,
        // Moving within the row should not hide the tip.
        trackMouse: true,
        // Render immediately so that tip.body can be referenced prior to the first show.
        renderTo: Ext.getBody(),
        //layout: 'fit',
        maxWidth: 205, //考虑到边框
        maxHeight: 205,
        listeners: {
            // Change content dynamically depending on which element triggered the show.
            beforeshow: function updateTipBody(tip) {
                var url = view.getRecord(tip.triggerElement).get('ImgUrl');
                if (url) {
                    var image = new Image();
                    image.src = url;
                    image.onload = function () {
                        if (image.width > image.height && image.width > 200) {
                            image.height = 200 * image.height / image.width;
                            image.width = 200;
                        } else if (image.height > image.width && image.height > 200) {
                            image.width = 200 * image.width / image.height;
                            image.height = 200;
                        }
                        view.tip.setWidth(image.width + 2); //设置宽度
                        view.tip.setHeight(image.height + 5); //设置高度
                    }
                } else {
                    view.tip.setWidth(0); //设置宽度
                    view.tip.setHeight(0); //设置高度
                }

                if (url == "") {
                    tip.update("");
                }
                else {
                    tip.update("<img style='max-height:200px; max-width:200px;' src='" + url + "'/>");
                }

            }
        }
    });
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [lifingCostGrid, searchPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
                SetTotalPrice();
            }
        }
    });

    lifingCostGridStore.load();
});

