/**
* 时间:2014-03-23 14:01:24
* 作者:段江涛
* 描述:流程类型表
*/
var flowtypeWin = Ext.create("Ext.ux.NEdit", {
    heigth: 300,
    width: 300,
    title: 'FlowType',
    saveUrl: '/Flow/FlowType/Save',
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
            { fieldLabel: '流程名称', name: 'Name', xtype: 'textfield' },
            { fieldLabel: '描述', name: 'Description', xtype: 'textarea' }
        /*文本框：textfield	文本域：textareafield|textarea	
        时间：timefield	 复选框：checkbox|checkboxfield	
        日期：datefield	 下拉框：combo|combobox
        文本：displayfield	 隐藏域：hidden|hiddenfield
        文件按钮:filebutton	文件：fileuploadfield|filefield
        HTML编辑:htmleditor 单选按钮：radio|radiofield
        数字：numberfield*/
        /*{ fieldLabel: '状态', name: 'Status', xtype: 'combo', editable: false, allowBlank: false, flex: 1, displayField: 'name', valueField: 'value', queryMode: 'local', width: 220,
        store: Ext.create('Ext.data.Store', {
        fields: ['name', 'value'],
        data: [{ name: '有效', value: '1' }, { name: '无效', value: '0'}]
        })
        }*/
        ]
    }],
    listeners: {
        success: function (data) {
            flowtypeWin.hide();
            flowtypeGrid.store.load();
        }
    }
});

/*flowtypeGrid展示界面*/
var flowtypeGrid = Ext.create('Ext.ux.NGrid', {
    border: false,
    al: true, //是否自动加载    
    dataUrl: '/FlowType/SelectByPage',
    rootValue: 'rows',
    isToolbar: true,
    pushItems: [{ 
        xtype: 'button', text: '流程设计', iconCls: 'i_refresh',
            handler: function () {
                EditData();
            }
    }],
    modelArray: [
        { name: 'Id', type: 'String' },
        { name: 'Name', type: 'String' },
        { name: 'Description', type: 'String' }
    ],
    columns: [
        { text: '流程名称', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '描述', dataIndex: 'Description', flex: 1, minWidth: 0 }
    /*{ text: '状态', dataIndex: 'Status', flex: 1, minWidth: 0, renderer: function (value) {
    if (value == "1")
    return "有效";
    else
    return "无效";
    }
    }*/
    ],
    listeners: {
        "Insert": function () {
            flowtypeWin.show();
            var form = flowtypeWin.down("form").getForm();
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
    var records = flowtypeGrid.getSelectionModel().getSelection();
    if (records.length <= 0) {
        Ext.Msg.alert("提示", "请选择要删除的数据");
    } else {
        var record = records[0];
        if (record != null) {
            flowtypeWin.show();
            var form = flowtypeWin.down("form").getForm();
            form.loadRecord(record);
        } else {
            Ext.Msg.alert("提示", "请选择一行数据");
        }
    }
};
/*删除数据*/
function DeleteData() {
    var records = flowtypeGrid.getSelectionModel().getSelection();
    if (records.length > 0) {
        var ids = [];
        Ext.Array.each(records, function (record) {
            ids.push(record.get("Id"));
        });

        ExtjsUtil.DoDelete({
            url: "/FlowType/Delete",
            params: { ids: ids.join(',') },
            success: function (data) {
                ExtjsUtil.ShowInfoMsg("提示", data.msg);
                if (data.success) {
                    flowtypeGrid.store.load();
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
        items: [flowtypeGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });
});
