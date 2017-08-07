/*新增收入记录表*/
var studentWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 300,
    title: '学生管理',
    saveUrl: '/JiaoXue/Student/Save',
    formItems: [{
        xtype: "form",
        layout: 'form',
        id: "studentForm",
        bodyStyle: "padding-right:5px",
        border: 0,
        defaults: {
            labelAlign: 'right',
            labelWidth: 70
        },
        items: [
            { fieldLabel: '编号', name: 'Id', xtype: 'hidden' },
            { fieldLabel: '姓名', name: 'Name', xtype: 'textfield', allowBlank: false },
            { fieldLabel: '身份证号', name: 'CardNo', xtype: 'textfield', allowBlank: false },
            {
                fieldLabel: '生日', name: 'BirthDay', xtype: 'datefield',
                format: 'Y-m-d',
                value: new Date(),
                allowBlank: false
            },
            {
                xtype: "combobox",
                fieldLabel: "性别",
                name: "Sex",
                editable: false,
                store: Ext.create('Ext.data.Store', {
                    fields: ['val', 'name'],
                    data: [
                        { "val": 1, "name": "男" },
                        { "val": 0, "name": "女" }
                    ]
                }),
                displayField: 'name',
                valueField: 'val',
                allowBlank: false
            },
            { fieldLabel: '家庭地址', name: 'Addr', xtype: 'textfield' },
            { fieldLabel: '备注', name: 'Remark', xtype: 'textfield' }
        ]
    }],
    listeners: {
        success: function (data) {
            studentWin.hide();
            studentGrid.store.load();
        }
    }
});

/*studentGrid展示界面*/
var studentGrid = Ext.create('Ext.ux.NGrid', {
    border: 1,
    region: 'center',
    al: true, //是否自动加载    
    dataUrl: '/JiaoXue/Student/SelectByPage',
    rootValue: 'rows',
    isToolbar: true,
    isEdit: true,
    pushItems: [],
    modelArray: [
        { name: 'Id', type: 'String' },
        { name: 'Name', type: 'String' },
        { name: 'CardNo', type: 'String' },
        {
            name: 'BirthDay', type: 'DateTime',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'Sex', type: 'int' },
        { name: 'Addr', type: 'String' },
        { name: 'Remark', type: 'String' }
    ],
    columns: [
        { text: '姓名', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '身份证号码', dataIndex: 'CardNo', flex: 1, minWidth: 0 },
        { text: '生日', dataIndex: 'BirthDay', flex: 1, minWidth: 0 },
        {
            text: '性别', dataIndex: 'Sex', flex: 1, minWidth: 0, renderer: function (value) {
                if (value == 1) {
                    return "男";
                } else {
                    return "女";
                }
            }
        },
        { text: '家庭地址', dataIndex: 'Addr', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Remark', flex: 1, minWidth: 0 }
    ],
    listeners: {
        "Insert": function () {
            studentWin.show();
            var form = studentWin.down("form").getForm();
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
                    url: '/JiaoXue/Student/Save',
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
    var records = studentGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            studentWin.show();
            var form = studentWin.down("form").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
};
/*删除数据*/
function DeleteData() {
    var records = studentGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/JiaoXue/Student/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                ExtjsUtil.ShowInfoMsg("提示", data.msg);
                if (data.success) {
                    studentGrid.store.load();
                }
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
};

var searchPanel = Ext.create("Life.JiaoXue.Student.SearchPanel", {
    region: 'north',
    border: 1,
    listeners: {
        "search": function (panel, parm) {
            studentGrid.store.proxy.extraParams = parm;
            studentGrid.store.loadPage(1);
        }
    }
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [studentGrid, searchPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
                searchPanel.SetTotalPrice();
            }
        }
    });
});

