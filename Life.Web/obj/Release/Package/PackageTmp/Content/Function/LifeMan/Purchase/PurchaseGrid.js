/*新增收入记录表*/
var PurchaseWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 300,
    title: '采购信息管理',
    saveUrl: '/Purchase/Save',
    formItems: [{
        xtype: "form",
        layout: 'form',
        bodyStyle: "padding-right:5px",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '购买时间', name: 'PurTime', xtype: 'datefield', format: 'Y-m-d',value:new Date() },
            { fieldLabel: '数量(斤)', name: 'Number', xtype: 'numberfield',
                listeners: {
                    blur: function () {
                        calcTotalPrice();
                    }
                }
            },
            { fieldLabel: '单价', name: 'UnitPrice', xtype: 'numberfield',
                listeners: {
                    blur: function () {
                        calcTotalPrice();
                    }
                }
            },
            { fieldLabel: '总价', name: 'TotalPrice', xtype: 'numberfield' },
            { fieldLabel: '备注', name: 'Note', xtype: 'textarea' },
            { fieldLabel: '创建人', name: 'CreateBy', xtype: 'hidden' },
            { fieldLabel: '创建时间', name: 'CreateTime', xtype: 'hidden' },
            { fieldLabel: '修改人', name: 'UpdateBy', xtype: 'hidden' },
            { fieldLabel: '修改时间', name: 'UpdateTime', xtype: 'hidden' }
        ]
    }],
    listeners: {
        success: function (data) {
            PurchaseWin.hide();
            PurchaseGrid.store.load();
        }
    }
});

function calcTotalPrice() {
    var a = parseFloat(PurchaseWin.down("textfield[name='Number']").getValue());
    var b = parseFloat(PurchaseWin.down("textfield[name='UnitPrice']").getValue());
    if (a && a != "" && b && b != "")
        PurchaseWin.down("textfield[name='TotalPrice']").setValue(a * b);
}

/*PurchaseGrid展示界面*/
var PurchaseGrid = Ext.create('Ext.ux.NGrid', {
    border: false,
    al: true, //是否自动加载    
    dataUrl: '/Purchase/SelectByPage',
    rootValue: 'rows',
    isToolbar: true,
    modelArray: [
        { name: 'Id', type: 'String' },
        { name: 'PurTime', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'Number', type: 'double' },
        { name: 'UnitPrice', type: 'double' },
        { name: 'TotalPrice', type: 'double' },
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
        }
    ],
    columns: [
        { text: '购买时间', dataIndex: 'PurTime', flex: 1, minWidth: 0 },
        { text: '数量(斤)', dataIndex: 'Number', flex: 1, minWidth: 0 },
        { text: '单价', dataIndex: 'UnitPrice', flex: 1, minWidth: 0 ,
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        },
        { text: '总价', dataIndex: 'TotalPrice', flex: 1, minWidth: 0 ,
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
        },
        { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0 },
        { text: '创建人', dataIndex: 'CreateBy', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreateTime', flex: 1, minWidth: 0 },
        { text: '修改人', dataIndex: 'UpdateBy', flex: 1, minWidth: 0 },
        { text: '修改时间', dataIndex: 'UpdateTime', flex: 1, minWidth: 0 }
    ],
    listeners: {
        "Insert": function () {
            PurchaseWin.show();
            var form = PurchaseWin.down("form").getForm();
            form.reset();
        },
        "Modify": function (grid, data) {
            EditData();
        },
        "Delete": function (grid, data) {
            DeleteData();
        }
    }
});

/*编辑数据*/
function EditData() {
    var records = PurchaseGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            PurchaseWin.show();
            var form = PurchaseWin.down("form").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
};
/*删除数据*/
function DeleteData() {
    var records = PurchaseGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/Purchase/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                ExtjsUtil.ShowInfoMsg("提示", data.msg);
                if (data.success) {
                    PurchaseGrid.store.load();
                }
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
};

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "fit",
        items: [PurchaseGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });
});

