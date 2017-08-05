//定义字典表模型
Ext.regModel('Diction', {
    fields: [
        { name: 'Id', type: 'decimal' },
        { name: 'Name', type: 'String' },
        { name: 'Note', type: 'String' },
        { name: 'ParentId', type: 'decimal' },
        { name: 'OrderId', type: 'decimal' },
        { name: 'CreateBy', type: 'String' },
        { name: 'CreateTime', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        },
        { name: 'UpdateBy', type: 'String' },
        { name: 'UpdateTime', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        }
    ]
});

var dictionTreeStore = Ext.create("Ext.data.TreeStore", {
    autoLoad: false,
    fields: ["id", "text", "Tobject", "leaf"],
    root: {
        //expanded: true,
        text: '父级菜单',
        id: 0
    },
    proxy: {
        type: 'ajax',
        url: '/Diction/Select'
    }
});

/*模块的树界面*/
var dictionTree = Ext.create("Ext.tree.Panel", {
    width: 220,
    region: 'west',
    split: true,
    collapsed: false,
    collapsible: true,
    store: dictionTreeStore,
    rootVisible: false,
    title: '字典类别',    
    listeners: {
        "selectionchange": function (model, selected, eOpts) {
            //节点选中改变事件
            var records = model.getSelection();
            if (records.length > 0) {
                parentId = records[0].get("id");
                dictionGrid.store.load();
            }
        },
        "load": function () {
            //加载时选中第一条数据
            var node = dictionTree.getRootNode().firstChild;
            if (node != null) {
                var path = node.getPath();
                dictionTree.selectPath(path);
            } else
                dictionGrid.store.removeAll();
        }
    }
});

