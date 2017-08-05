//定义银行卡操作记录表模型
Ext.regModel('BankCard', {
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Time', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'Price', type: 'double' },
        { name: 'SaveType', type: 'decimal' },
        { name: 'Balance', type: 'double' },
        { name: 'BankType', type: 'decimal' },
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
        { name: 'ImgUrl', type: 'string' },

        { name: 'BankTypeName', type: 'string' },
        { name: 'SaveName', type: 'string' },
        { name: 'CreateName', type: 'string' },
        { name: 'UpdateName', type: 'string' }
    ]
});

/*银行卡操作记录表数据store*/
var bankCardGridStore = Ext.create("Ext.data.Store", {
    //autoLoad: { start: 0, limit: 20 }, //10表示每页10条，0表示开始的索引
    model: "BankCard",
    proxy: {
        type: 'ajax',
        url: '/BankCard/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20,
    listeners: {
        "beforeload": function (store, operation, eOpts) {
            //bankCardGridStore.proxy.url = "/BankCard/SelectByParentId?parentId="+parentId;
        }
    }
});

/*操作类型的store*/
var opeTypeStore = Ext.create("Ext.data.Store", {
    autoLoad: true,
    fields: ["Id", "Name"],
    proxy: {
        type: 'ajax',
        url: '/Diction/SelectByParentId',
        extraParams: { parentId: "1000200000" },
        reader: {
            type: 'json'
        }
    }
});

/*银行卡类型的store*/
var bankTypeStore = Ext.create("Ext.data.Store", {
    autoLoad: true,
    fields: ["Id", "Name"],
    proxy: {
        type: 'ajax',
        url: '/Diction/SelectByParentId',
        extraParams: { parentId: "1000100000" },
        reader: {
            type: 'json'
        }
    }
});

/*新增银行卡操作记录表*/
var bankCardWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '银行卡信息',
    layout: 'fit',
    items: [{
        xtype: "form",
        id: "bankCardForm",
        border: 0,
        bodyStyle: "padding-right:5px",
        layout: 'form',
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '操作时间', name: 'Time', xtype: 'datefield', format: 'Y-m-d' },
            { fieldLabel: '操作金额', name: 'Price', xtype: 'numberfield', minValue: 0 },
            { fieldLabel: '操作类型', name: 'SaveType', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', store: opeTypeStore,
                listeners: {
                    "change": function (combo, newValue, oldValue, eOpts) {
                        ChangeState(newValue);
                    }
                }
            },
            { fieldLabel: '余额', name: 'Balance', xtype: 'hidden', minValue: 0 },
            { fieldLabel: '银行卡名称', name: 'BankType', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', store: bankTypeStore },
            { fieldLabel: '图片', name: 'ImgUrl', xtype: 'filefield', buttonText: '选择' },
            { fieldLabel: '备注', name: 'Note', xtype: 'textarea' },
            {
                xtype: 'fieldcontainer',
                fieldLabel: '收入类型',
                hidden: true,
                name: 'flIncome',
                defaultType: 'checkboxfield',
                items: [
                    {
                        boxLabel: '纯收入',
                        name: 'Income',
                        inputValue: true,
                        value: true,
                        listeners: {
                            change: function (cb, newValue, oldValue, eOpts) {
                                var pDetail = bankCardWin.down("panel[name=pDetail]");
                                if (newValue == true) {
                                    pDetail.setVisible(true);
                                } else {
                                    pDetail.setVisible(false);
                                }
                            }
                        }
                    }
                ]
            },
            {
                xtype: 'fieldcontainer',
                fieldLabel: '收入类型',
                name: 'flCost',
                hidden: true,
                defaultType: 'checkboxfield',
                items: [
                    {
                        boxLabel: '消费',
                        name: 'Cost',
                        inputValue: true,
                        value: true,
                        listeners: {
                            change: function (cb, newValue, oldValue, eOpts) {
                                var costType = bankCardWin.down("combo[name=CostTypeId]");
                                var pDetail = bankCardWin.down("panel[name=pDetail]");
                                if (newValue == true) {
                                    pDetail.setVisible(true);
                                    costType.setVisible(true);
                                } else {
                                    pDetail.setVisible(false);
                                    costType.setVisible(false);
                                }
                            }
                        }
                    }
                ]
            },
            { fieldLabel: '消费类型', name: 'CostTypeId', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', width: 230, value: 1000300001,
                store: Ext.create("Life.LifeMan.LifingCost.LifeTypeStore"), hidden: true
            },
            {
                xtype: 'panel',
                layout: 'hbox',
                hidden: true,
                name: 'pDetail',
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
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            //Ext.getCmp("ParentId").setValue(parentId);//赋值
            var form = Ext.getCmp("bankCardForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/BankCard/Save",
                    waitMsg: "正在提交请稍后...",
                    waitTitle: "消息",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            bankCardWin.hide();
                            bankCardGrid.store.load();
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
            bankCardWin.hide();
        }
    }]
});

function ChangeState(newValue) {
    var flCost = bankCardWin.down("fieldcontainer[name=flCost]");
    var flIncome = bankCardWin.down("fieldcontainer[name=flIncome]");
    var costType = bankCardWin.down("combo[name=CostTypeId]");
    var pDetail = bankCardWin.down("panel[name=pDetail]");
    if (bankCardWin.mark == "edit") {
        pDetail.setVisible(false);
        costType.setVisible(false);
        flCost.setVisible(false);
        flIncome.setVisible(false);
    } else {
        if (newValue == '1000200001') {
            //存入
            pDetail.setVisible(false);
            costType.setVisible(false);
            flCost.setVisible(false);
            flIncome.setVisible(true);
        } else {
            //取出
            pDetail.setVisible(false);
            costType.setVisible(false);
            flIncome.setVisible(false);
            flCost.setVisible(true);
        }
        flIncome.down("checkboxfield[name=Income]").setValue(false);
        flCost.down("checkboxfield[name=Cost]").setValue(false);
    }
};

/*转账的表单*/
var transferWin = Ext.create("Ext.window.Window", {
    width: 300,
    modal: true,
    closeAction: 'hide',
    title: '转账信息',
    layout: 'fit',
    items: [{
        xtype: "form",
        id: "transferForm",
        border: 0,
        bodyStyle: "padding-right:5px",
        layout: 'form',
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '操作时间', name: 'Time', xtype: 'datefield', format: 'Y-m-d', value: new Date() },
            { fieldLabel: '操作金额', name: 'Price', xtype: 'numberfield', minValue: 0 },
            { fieldLabel: '操作类型', name: 'SaveType', xtype: 'hidden', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', store: opeTypeStore },
            { fieldLabel: '余额', name: 'Balance', xtype: 'hidden', minValue: 0 },
            { fieldLabel: '转出银行卡', name: 'BankType', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', store: bankTypeStore },
            { fieldLabel: '转入银行卡', name: 'inBankCard', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'Name', valueField: 'Id', queryMode: 'local', store: bankTypeStore },
            { fieldLabel: '备注', name: 'Note', xtype: 'textarea',
                listeners: {
                    focus: function (texarea) {
                        var str1 = transferWin.down("combo[name=BankType]").displayTplData[0].Name;
                        var str2 = transferWin.down("combo[name=inBankCard]").displayTplData[0].Name;
                        var str3 = "从" + str1 + "转入到" + str2;
                        texarea.setValue(str3);
                    }
                }
            },
            { fieldLabel: '创建者', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改者', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
        ]
    }],
    buttons: [{
        text: '确定',
        handler: function () {
            var form = Ext.getCmp("transferForm").getForm();
            if (form.isValid()) {
                form.submit({
                    url: "/BankCard/Transfer",
                    success: function (form, action) {
                        Ext.Msg.alert('提示', action.result.msg);
                        if (action.result.success) {
                            transferWin.hide();
                            bankCardGrid.store.load();
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
            transferWin.hide();
        }
    }]
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
                    url: "/BankCard/ImportExcel",
                    success: function (form, action) {
                        ExtjsUtil.ShowInfoMsg("提示", action.result.msg);
                        if (action.result.success) {
                            ImportWin.hide();
                            bankCardGrid.store.load();
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

/*bankCardGrid展示界面*/
var bankCardGrid = Ext.create('Ext.grid.Panel', {
    //title: '银行卡操作记录表信息',
    region: "center",
    store: bankCardGridStore,
    multiSelect: true,
    border: '1 1 1 1',
    columns: [
        { xtype: 'rownumberer', width: 30 },
        { text: '操作时间', dataIndex: 'Time', width: 80, align: 'center' },
        { text: '操作金额', dataIndex: 'Price', width: 70, align: 'right',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        },
        { text: '操作类型', dataIndex: 'SaveName', width: 60, align: 'center' },
        { text: '余额', dataIndex: 'Balance', width: 80, align: 'right',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        },
        { text: '银行卡名称', dataIndex: 'BankTypeName', width: 120 },
        { text: '图标', dataIndex: 'ImgUrl', width: 50,
            renderer: function (value) {
                if (value != "" && value)
                    return "<img onclick=\"ShowImg('" + value + "')\" src='/Content/Images/icon16/@asset.gif'/>";
            }
        },
        { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0,
            renderer: function (value) {
                return '<div title="' + value + '">' + value + '</div>';
            }
        },
        { text: '创建者', dataIndex: 'CreateName', width: 70 },
        { text: '创建时间', dataIndex: 'CreateTime', width: 140 },
        { text: '修改者', dataIndex: 'UpdateName', width: 70 },
        { text: '修改时间', dataIndex: 'UpdateTime', width: 140 }    
    ],
    tbar: [
        { xtype: 'button', text: '新增', iconCls: 'i_add',
            handler: function () {
                bankCardWin.show();
                bankCardWin.mark = "add";
                //清空操作                                 
                var form = Ext.getCmp("bankCardForm").getForm();
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
        { xtype: 'button', text: '计算余额', iconCls: 'i_home',
            handler: function () {
                Ext.Ajax.request({
                    url: '/BankCard/CalcAllBalance',
                    success: function (response) {
                        var data = Ext.JSON.decode(response.responseText);
                        Ext.Msg.alert("提示", data.msg)
                        if (data.success)
                            bankCardGrid.store.load();
                    }
                })
            }
        }, '-',
        { xtype: 'button', text: '转账', iconCls: 'i_refresh',
            handler: function () {
                var form = transferWin.down("form").getForm();
                form.reset();
                transferWin.show();
            }
        }, '-',
        { xtype: 'button', text: '导入Excel', iconCls: 'i_undo',
            handler: function () {
                ImportWin.show();
            }
        }, '-',
        { xtype: 'button', text: 'Excel模版', iconCls: 'i_redo',
            handler: function () {
                window.open("/LifeMan/BankCard/GetTemp");
            }
        }
    ],
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: bankCardGridStore,
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

/*查看图片*/
function ShowImg(url) {
    Ext.create("Ext.ux.ShowImg", {
        imgUrl: url
    }).show();
}

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
            { fieldLabel: '银行卡类型', name: 'BankType', xtype: 'diccombo', valueField: "Name", editable: true, parentId: '1000100000', width: 230 },
            { fieldLabel: '操作类型', name: 'SaveType', xtype: 'diccombo', valueField: "Name", editable: true, parentId: '1000200000', width: 230 }
        ]
    }],
    buttons: [{
        text: '查询',
        handler: function () {
            bankCardGrid.store.proxy.url = "/BankCard/SelectByPage";
            bankCardGrid.store.proxy.extraParams = Ext.getCmp("searchForm").getValues();
            bankCardGrid.store.load();
        }
    }]
});

/*编辑数据*/
function EditData() {
    var records = bankCardGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要修改的数据");
    } else {
        var record = records[0];
        if (record != null) {
            bankCardWin.show();
            bankCardWin.mark = "edit";
            ChangeState();
            var form = Ext.getCmp("bankCardForm").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
}
/*删除数据*/
function DeleteData() {
    var records = bankCardGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/BankCard/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                Ext.Msg.alert("提示", data.msg)
                if (data.success)
                    bankCardGrid.store.load();
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
}

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [bankCardGrid, searchPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

    bankCardGrid.store.load();
});