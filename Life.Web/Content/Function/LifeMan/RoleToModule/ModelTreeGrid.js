//定义模块表模型
Ext.regModel('Module', {
    fields: [
        { name: 'Tobject.ModuleId', type: 'String' },
        { name: 'Tobject.ModuleName', type: 'String' },
        { name: 'Tobject.ModuleUrl', type: 'String' },
        { name: 'Tobject.IconUrl', type: 'String' },
        { name: 'Tobject.ParentId', type: 'String' },
        { name: 'Tobject.OrderId', type: 'decimal' },
        { name: 'Tobject.Notes', type: 'String' },
        { name: 'Tobject.Status', type: 'decimal' }//,
        //{ name: 'id',type:'string'}
    ]
});

/*模块表数据store*/
var moduleGridStore = Ext.create("Ext.data.TreeStore", {
    autoLoad: true,
    model: "Module",
    root: {
        //expanded: true,
        text: '父级菜单',
        id: 0
    },
    proxy: {
        type: 'ajax',
        url: '/RoleToModule/Select'
    }
});

/*moduleGrid展示界面*/
var moduleGrid = Ext.create('Ext.tree.Panel', {
    title: '模块表信息',
    region: "center",
    store: moduleGridStore,
    multiSelect: true,
    rootVisible: false,
    columns: [
        { text: '模块名称', dataIndex: 'Tobject.ModuleName', xtype: 'treecolumn', width: 150 },
        { text: '模块路径', dataIndex: 'Tobject.ModuleUrl', flex: 1, minWidth: 0 },
        { text: '父级模块', dataIndex: 'Tobject.ParentId', flex: 1, minWidth: 0 },
        { text: '序号', dataIndex: 'Tobject.OrderId', flex: 1, minWidth: 0 },
        { text: '备注', dataIndex: 'Tobject.Notes', flex: 1, minWidth: 0 },
        { text: '状态', dataIndex: 'Tobject.Status', flex: 1, minWidth: 0 }
    ],
    tbar: [
        { xtype: 'button', text: '保存', iconCls: 'i_save',
            handler: function () {
                var saveMask = new Ext.LoadMask(Ext.getBody(), { msg: '正在保存...' });
                saveMask.show();
                //获得编号集合
                var nodes = moduleGrid.getChecked();
                var ids = [];
                Ext.Array.each(nodes, function (node) {
                    ids.push(node.get("Tobject.ModuleId"));
                });
                var idStr = ids.join(',');
                if (idStr.indexOf(',') == 0) {
                    idStr = idStr.substr(1);
                }

                //保存到数据库
                Ext.Ajax.request({
                    url: '/RoleToModule/Save',
                    params: { roleId: roleId, moduleIds: idStr },
                    success: function (response) {
                        saveMask.hide();
                        var data = Ext.JSON.decode(response.responseText);
                        Ext.Msg.alert("提示", data.msg);
                    }
                })
            }
        }
    ],
    listeners: {
        "checkchange": function (node, state) {//选择父节点勾选所有子节点；勾除所有子节点取出父节点勾选  
            if (node.parentNode != null) {
                //控制子节点
                node.cascade(function (n) {
                    n.set("checked", state);
                    return true;
                });
                //控制父节点
                var pNode = node.parentNode;
                if (state == true) {
                    while (pNode != null) {
                        pNode.set("checked", state);
                        pNode = pNode.parentNode;
                    }
                } else {
                    var _miss = false;
                    for (var i = 0; i < pNode.childNodes.length; i++) {
                        if (pNode.childNodes[i].checked != state) _miss = true;
                    }
                    if (!_miss) {
                        pNode.set("checked", state);
                    }
                }
            }
        }
    }
});
