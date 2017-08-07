/*新增收入记录表*/
var studentWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 450,
    title: '教员管理',
    saveUrl: '/JiaoXue/Teacher/Save',
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
            {
                fieldLabel: '编号',
                name: 'Id',
                xtype: 'hidden',
                allowBlank: false
            },
            {
                fieldLabel: '姓名',
                name: 'Name',
                xtype: 'textfield',
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
            {
                fieldLabel: '籍贯',
                name: 'NativePlace',
                xtype: 'textfield',
                allowBlank: false
            },
            {
                fieldLabel: '联系电话',
                name: 'Phone',
                xtype: 'textfield',
                allowBlank: false
            },
            {
                fieldLabel: 'QQ',
                name: 'Qq',
                xtype: 'textfield',
                allowBlank: true
            },
            {
                fieldLabel: '电子邮箱',
                name: 'Email',
                xtype: 'textfield',
                allowBlank: true
            },
            {
                fieldLabel: '毕业院校',
                name: 'GraduateSchool',
                xtype: 'textfield',
                allowBlank: false
            },
            {
                fieldLabel: '所读专业',
                name: 'Professional',
                xtype: 'textfield',
                allowBlank: true
            },
            {
                fieldLabel: '擅长科目',
                name: 'GoodSubjects',
                xtype: 'textarea',
                allowBlank: true
            },
            {
                fieldLabel: '自我评价',
                name: 'SelfAssessment',
                xtype: 'textarea',
                allowBlank: true
            },
            {
                fieldLabel: '创建人',
                name: 'CreateBy',
                xtype: 'hidden',
                allowBlank: true
            },
            {
                fieldLabel: '创建时间',
                name: 'CreateTime',
                xtype: 'hidden',
                allowBlank: true
            },
            {
                fieldLabel: '修改人',
                name: 'UpdateBy',
                xtype: 'hidden',
                allowBlank: true
            },
            {
                fieldLabel: '修改时间',
                name: 'UpdateTime',
                xtype: 'hidden',
                allowBlank: true
            }
        ]
    }],
    listeners: {
        success: function (data) {
            studentWin.hide();
            teacherGrid.store.load();
        }
    }
});

/*teacherGrid展示界面*/
var teacherGrid = Ext.create('Ext.ux.NGrid', {
    border: 1,
    region: 'center',
    al: true, //是否自动加载    
    dataUrl: '/JiaoXue/Teacher/SelectByPage',
    rootValue: 'rows',
    isToolbar: true,
    isEdit: true,
    pushItems: [],
    modelArray: [
        { name: 'Id', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'Sex', type: 'int' },
        { name: 'NativePlace', type: 'string' },
        { name: 'Phone', type: 'string' },
        { name: 'Qq', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'GraduateSchool', type: 'string' },
        { name: 'Professional', type: 'string' },
        { name: 'GoodSubjects', type: 'string' },
        { name: 'SelfAssessment', type: 'string' },
        { name: 'CreateBy', type: 'string' },
        {
            name: 'CreateTime', type: 'date',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        },
        { name: 'UpdateBy', type: 'string' },
        {
            name: 'UpdateTime', type: 'date',
            convert: function (value) {
                return Duanjt.Date.NumToDate(value);
            }
        }
    ],
    columns: [
        { text: '姓名', dataIndex: 'Name', flex: 1, minWidth: 0 },
        {
            text: '性别', dataIndex: 'Sex', flex: 1, minWidth: 0, renderer: function (value) {
                if (value == 1) {
                    return "男";
                } else {
                    return "女";
                }
            }
        },
        { text: '籍贯', dataIndex: 'NativePlace', flex: 1, minWidth: 0 },
        { text: '联系电话', dataIndex: 'Phone', flex: 1, minWidth: 0 },
        { text: 'QQ', dataIndex: 'Qq', flex: 1, minWidth: 0 },
        { text: '电子邮箱', dataIndex: 'Email', flex: 1, minWidth: 0 },
        { text: '毕业院校', dataIndex: 'GraduateSchool', flex: 1, minWidth: 0 },
        { text: '所读专业', dataIndex: 'Professional', flex: 1, minWidth: 0 },
        { text: '擅长科目', dataIndex: 'GoodSubjects', flex: 1, minWidth: 0, hidden: true },
        { text: '自我评价', dataIndex: 'SelfAssessment', flex: 1, minWidth: 0, hidden: true }
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
                    url: '/JiaoXue/Teacher/Save',
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
    var records = teacherGrid.getSelectionModel().getSelection();
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
    var records = teacherGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/JiaoXue/Teacher/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                ExtjsUtil.ShowInfoMsg("提示", data.msg);
                if (data.success) {
                    teacherGrid.store.load();
                }
            }
        });
    } else {
        Ext.Msg.alert("提示", "请至少选择一行");
    }
};

var searchPanel = Ext.create("Life.JiaoXue.Teacher.SearchPanel", {
    region: 'north',
    border: 1,
    listeners: {
        "search": function (panel, parm) {
            teacherGrid.store.proxy.extraParams = parm;
            teacherGrid.store.loadPage(1);
        }
    }
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [teacherGrid, searchPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
                searchPanel.SetTotalPrice();
            }
        }
    });
});

