var rolesTreeStore = Ext.create("Ext.data.TreeStore", {
    autoLoad: false,
    fields: ["id", "text", "Tobject", "leaf"],
    root: {
        //expanded: true,
        text: '角色信息',
        id: 0
    },
    proxy: {
        type: 'ajax',
        url: '/RoleToModule/Search'
    }
});

/*角色的树界面*/
var rolesTree = Ext.create("Ext.tree.Panel", {
    width: 220,
    region: 'west',
    split: true,
    collapsed: false,
    collapsible: true,
    store: rolesTreeStore,
    rootVisible: false,
    title: '角色信息',
    listeners: {
        "selectionchange": function (model, selected, eOpts) {
            //节点选中改变事件
            var records = model.getSelection();
            if (records.length > 0) {
                var changMask = new Ext.LoadMask(Ext.getBody(), { msg: '正在加载...' });
                changMask.show();

                roleId = records[0].get("id");
                //控制子节点未选中
                var node = moduleGrid.getRootNode();
                node.cascade(function (n) {
                    n.set("checked", false);
                    return true;
                });

                //加载所有的模块并选中
                Ext.Ajax.request({
                    url: '/RoleToModule/SearchModule',
                    params: { roleId: roleId },
                    success: function (response) {
                        changMask.hide();
                        var data = Ext.JSON.decode(response.responseText);
                        for (var i = 0; i < data.length; i++) {
                            node = moduleGrid.store.getNodeById(data[i].ModuleId);
                            node.set("checked", true);
                        }
                    }
                })
            }
        },
        "load": function () {
            //加载时选中第一条数据
            var node = rolesTree.getRootNode().firstChild;
            if (node != null) {
                var path = node.getPath();
                rolesTree.selectPath(path);
            } else
                rolesGrid.store.removeAll();
        }
    }
});


