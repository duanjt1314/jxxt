/*新增收入记录表*/
var incomeWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 300,
    title: '纯收入管理',
    saveUrl: '/Income/Save',
    formItems: [{
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
            { fieldLabel: '操作时间', name: 'Time', xtype: 'datefield', format: 'Y-m-d', value: new Date(), allowBlank: false },
            { fieldLabel: '操作金额', name: 'Price', xtype: 'numberfield', allowBlank: false },
            { fieldLabel: '备注', name: 'Note', xtype: 'textarea', allowBlank: false },
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
                    { fieldLabel: '家庭内收入', name: 'FamilyIncome', xtype: 'checkbox', value: false, flex: 1, inputValue: true }
                ]
            },
            { fieldLabel: '创建者', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改者', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' },
            { fieldLabel: '自定义分组', name: 'CusGroup', xtype: 'hidden' }
        ]
    }],
    listeners: {
        success: function (data) {
            incomeWin.hide();
            incomeGrid.store.load();
        }
    }
});

/*incomeGrid展示界面*/
var incomeGrid = Ext.create('Ext.ux.NGrid', {
    border: 1,
    region: 'center',
    al: true, //是否自动加载    
    dataUrl: '/Income/SelectByPage',
    rootValue: 'rows',
    isToolbar: true,
    isEdit: true,
    pushItems: [{ xtype: 'button', text: '批量修改标识', iconCls: 'i_edit',
        handler: function () {
            var records = incomeGrid.getSelectionModel().getSelection();
            if (records.length > 0) {
                var ids = [];
                Ext.Array.each(records, function (record) {
                    ids.push(record.get("Id"));
                });

                if (!incomeGrid._edit) {
                    incomeGrid._edit = Ext.create("Life.LifeMan.Income.CusGroupWin", {
                        listeners: {
                            success: function () {
                                incomeGrid._edit.hide();
                                incomeGrid.store.load();
                            }
                        }
                    });
                }

                incomeGrid._edit.show();
                var form = incomeGrid._edit.down("form").getForm();
                //清空
                form.reset();
                //赋值
                form.setValues({ Ids: ids.join(',') });
            } else {
                Ext.Msg.alert("提示", "请至少选择一行");
            }
        }
    }, '->', {
        xtype: 'button', text: '帮助', iconCls: 'i_save',
        handler: function () {
            var me = this;
            if (!me.win) {
                var allGroup = "";
                Ext.Ajax.request({
                    url: '/Income/GetAllCusGroup',
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
                
                me.win = Ext.create('Ext.window.Window', {
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
                    html: '<div style="font-size:14px"><span style="color:red">特殊标识</span>:指公司出差补贴(已经消费了),给朋友代购商品(之前已经支出了)的情况,对于多余的金额可以列为纯收入<br>'
                        + '<span style="color:red">家庭内支出</span>:指从家庭成员中的一人把钱给家庭成员的其它人的情况(双方分别记录收入和支出)<br>'
                        + '<span style="color:red">所有标识</span>:'+allGroup+'</div>'
                });
            }
            me.win.show();
        }
    }],
    modelArray: [
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
        { name: 'UpdateName', type: 'String' },
        { name: 'FamilyIncome', type: 'bool' },
        { name: 'IsMark', type: 'bool' },
        { name: 'CusGroup', type: 'String' }
    ],
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
        { text: '修改时间', dataIndex: 'UpdateTime', flex: 1, minWidth: 0 },
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
                    var rec = incomeGrid.getStore().getAt(rowIndex);
                    rec.data.IsMark = rec.data.IsMark == false;

                    Ext.Ajax.request({
                        url: '/LifeMan/Income/Save',
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
        { text: '家庭内收入', dataIndex: 'FamilyIncome', width: 80, align: 'center',
            xtype: 'actioncolumn',
            items: [{
                iconCls: 'i_star_red',
                tooltip: '单击修改',
                getClass: function (v, mete, record) {
                    if (record.get("FamilyIncome"))
                        return 'i_star_red';
                    else
                        return "i_star_white";
                },
                handler: function (grid, rowIndex, colIndex) {
                    var rec = incomeGrid.getStore().getAt(rowIndex);
                    rec.data.FamilyIncome = !rec.data.FamilyIncome;

                    Ext.Ajax.request({
                        url: '/LifeMan/Income/Save',
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
    listeners: {
        "Insert": function () {
            incomeWin.show();
            var form = incomeWin.down("form").getForm();
            form.reset();
        },
        "Modify": function (grid, data) {
            EditData();
        },
        "Delete": function (grid, data) {
            DeleteData();
        },
        "edit": function (editor, e) {
            if (e.value != e.originalValue) {
                Ext.Ajax.request({
                    url: '/Income/Save',
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

/*编辑数据*/
function EditData() {
    var records = incomeGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            incomeWin.show();
            var form = incomeWin.down("form").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
};
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
                ExtjsUtil.ShowInfoMsg("提示", data.msg);
                if (data.success) {
                    incomeGrid.store.load();
                }
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
};

var searchPanel = Ext.create("Life.LifeMan.Income.SearchPanel", {
    region: 'north',
    border: 1,
    listeners: {
        "search": function (panel, parm) {
            incomeGrid.store.proxy.extraParams = parm;
            incomeGrid.store.loadPage(1);
        }
    }
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [incomeGrid, searchPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
                searchPanel.SetTotalPrice();
            }
        }
    });
});

