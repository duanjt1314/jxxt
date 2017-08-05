Ext.define("Ext.ux.MainMenu", {
    extend: 'Ext.panel.Panel',
    alias: 'widget.mainmenupanel',
    layout: 'accordion',
    menus: [],
    menuUrl: '/Module/SelectAll',
    initComponent: function () {
        var me = this;
        //注册事件
        me.addEvents("itemclick");

        var tempMenus = [];
        Ext.Ajax.request({
            url: me.menuUrl,
            params: { node: '0' },
            async: false,
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                me.menus = data;
            }
        })

        if (me.menus.toString() != "") {
            for (var i = 0; i < me.menus.length; i++) {
                tempMenus.push({
                    xtype: 'panel',
                    title: me.menus[i].text,
                    icon:me.menus[i].icon,
                    layout: 'fit',
                    items: [{
                        xtype: 'treepanel',
                        border:0,
                        rootVisible: false,
                        useArrows: true,
                        border: 0,
                        store: Ext.create('Ext.data.TreeStore', {
                            fields: ["id", "text", "iconCls", "icon", "leaf", "level", "treeid", "herf", "Checked", "Tobject", "children"],
                            root: {
                                expanded: true,
                                id: me.menus[i].id
                            },
                            proxy: {
                                type: 'ajax',
                                url: me.menuUrl
                            }
                        }),
                        listeners: {
                            itemclick: function (tree,record) {
                                me.fireEvent("itemclick",record);
                            }
                        }
                    }]
                });
            }
        }
        me.items = tempMenus;
        this.callParent(arguments);
    }
});