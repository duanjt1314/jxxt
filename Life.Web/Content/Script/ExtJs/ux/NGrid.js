/**
*   Extjs 4
*   gridPanel重新封装，方便使用，使用ux.FunctionGrid 需要遵守读取数据规范
*   xtype:functionGrid
*   使用只需要简单配置就能实现（增、删、改）数据读取和分页功能
*   所属类别：【公共】
*/
Ext.define("Ext.ux.NGrid", {
    extend: 'Ext.grid.Panel',
    alias: 'widget.ngrid',
    border: false,
    al: false, //是否自动加载
    viewConfig: {
        stripeRows: true
    },
    forceFit: false, //自动填充panel空白处
    multiSelect: true, //运行多选
    isSelect: true, //是否添加选择框（default:true）
    isEdit: false, //是否能编辑行
    //字段集合
    fields: new Ext.util.MixedCollection(),
    modelArray: [], //store的fields字段集
    storeParams: {}, // 参数
    pagging: true, //是否分页显示
    modelName: '', //model集合key
    columnName: '', //column集合key
    dataUrl: '', //store地址
    modelUrl: '', //model地址
    columnUrl: '', //列标头地址
    columns: [], //列标头
    rootValue: 'rows',
    isFoot: false,
    pageSize: 20,
    groupStr: '', //分组字段
    isToolbar: false, //工具栏，开启后默认为（增、删、改）
    isInsert: true,
    isModify: true,
    isDelete: true,
    pushItems: [], //工具栏按钮添加，需要开启isToolbar为true时才能生效
    columnsLoad: function (columnUrl, columnName) {
        this.reconfigure('', this.fieldsStore(columnUrl, columnName));
    },
    fieldsStore: function (url, modelName) {
        var me = this;
        //1.如果该字段集已经加载过，就从集合中直接取出
        if (this.fields.containsKey(modelName)) {
            return this.fields.get(modelName);
        } else {
            //ajax拿到字段集合
            var fields = [];
            Ext.Ajax.request({
                url: url,
                method: 'POST',
                timeout: 4000,
                async: false, //关键(同步加载)
                success: function (response, opts) {
                    fields = eval(response.responseText);
                }
            });
            this.fields.add(modelName, fields);
            return fields;
        }
    },
    //选中的第一行数据
    GetGridSelectedRowData: function () {
        var me = this;
        var selModel = me.getSelectionModel();
        if (Ext.isDefined(selModel.getSelection()[0]))
            return selModel.getSelection()[0];
        else
            return null;
    },
    //得到选中的数据数组
    GetGridSelectedArrayData: function () {
        var me = this;
        var selRecords = me.getSelectionModel().getSelection();
        if (selRecords.length > 0) {
            var arr = new Array();
            for (var i = 0; i < selRecords.length; i++) {
                arr.push(selRecords[i].data);
            }
            return arr;
        }
        else
            return null;
    },
    initComponent: function () {
        var me = this;
        this.addEvents('Insert', 'Modify', 'Delete', 'Refresh', 'Beforeload');
        if (me.modelUrl && me.modelArray == "") {
            me.modelArray = me.fieldsStore(me.modelUrl, me.modelName);
        }
        if (me.columnUrl && me.columns == "") {
            me.columns = me.fieldsStore(me.columnUrl, me.columnName);
        }
        this.store = Ext.create('Ext.data.Store', {
            autoLoad: me.al,
            fields: me.modelArray,
            groupField: me.groupStr,
            pageSize: me.pageSize,
            proxy: {
                type: 'ajax',
                url: me.dataUrl,
                extraParams: me.storeParams,
                reader: {
                    type: 'json',
                    root: me.rootValue,
                    totalProperty: 'total'
                }
            },
            listeners: {
                "beforeload": function (store, operation, options) {
                    me.fireEvent('storeBeforeload');
                }
            }
        });
        if (me.isSelect) {
            me.selType = 'checkboxmodel'; //设定选择模式
        }
        if (me.pagging) {
            me.dockedItems = [{
                xtype: 'pagingtoolbar',
                store: me.store,
                dock: 'bottom',
                displayInfo: true
            }]
        }
        if (me.isEdit) {
            me.plugins = [
				Ext.create("Ext.grid.plugin.CellEditing", {
				    clicksToEdit: 1
				})
			]
        }
        if (me.isFoot) {
            me.addListener("render", function (Component, options) {

            });
        }
        if (me.isToolbar) {
            var toolBar = [];
            if (me.isInsert) {
                if (toolBar.length > 0) {
                    toolBar.push('-');
                }

                toolBar.push({
                    text: '新增',
                    iconCls: 'i_add',
                    listeners: {
                        scope: this,
                        click: function () {
                            this.fireEvent('Insert', me);
                        }
                    }
                });
            }

            if (me.isModify) {
                if (toolBar.length > 0) {
                    toolBar.push('-');
                }

                toolBar.push({
                    text: '修改',
                    iconCls: 'i_edit',
                    listeners: {
                        scope: this,
                        click: function () {
                            var records = me.getSelectionModel().getSelection();
                            this.fireEvent('Modify', me, records);
                        }
                    }
                });
            }

            if (me.isDelete) {
                if (toolBar.length > 0) {
                    toolBar.push('-');
                }

                toolBar.push({
                    text: '删除',
                    iconCls: 'i_delete',
                    listeners: {
                        scope: this,
                        click: function () {
                            var records = me.getSelectionModel().getSelection();
                            this.fireEvent('Delete', me, records);
                        }
                    }
                });
            }
            me.tbar = toolBar;
                        
            me.addListener("itemcontextmenu", function (view, record, item, index, e, options) {
                e.preventDefault();
                view.getSelectionModel().select(index);
                var menuItems = [];

                if (me.isModify) {
                    if (menuItems.length > 0) {
                        menuItems.push('-');
                    }

                    menuItems.push({
                        text: '修改',
                        iconCls: 'i_edit',
                        handler: function () {
                            var records = me.getSelectionModel().getSelection();
                            me.fireEvent('Modify', me, records);
                        }
                    });
                }

                if (me.isDelete) {
                    if (menuItems.length > 0) {
                        menuItems.push('-');
                    }

                    menuItems.push({
                        text: '删除',
                        iconCls: 'i_delete',
                        handler: function () {
                            var records = me.getSelectionModel().getSelection();
                            me.fireEvent('Delete', me, records);
                        }
                    });
                }

                Ext.create("Ext.menu.Menu", {
                    items: menuItems
                }).showAt(e.getXY());
            })

            for (var i = 0; i < me.pushItems.length; i++) {
                me.tbar.push(me.pushItems[i]);
            }
        }
        this.callParent(arguments);
    }
})