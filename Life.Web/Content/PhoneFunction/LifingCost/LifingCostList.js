Ext.define('Life.LifingCost.LifingCostList', {
    extend: 'Ext.List',
    alias: "widget.lifingcostlist",
    config: {
        fullscreen: true,
        itemTpl: '<div>{Reason} {Time}</div><div>{CreateName}</div>',
        plugins: [{
            xclass: 'Ext.ux.PullRefresh',
            pullText: '下拉可以更新',
            lastUpdatedText: '上次更新时间',
            releaseText: '松开开始更新',
            lastUpdatedDateFormat: 'Y-m-d h:iA',
            loadingText: '正在刷新...',
            refreshFn: function (pull) {
                var store = this.getList().getStore();
                store.removeAll();
                store.loadPage(1);
            }
        }, {
            xclass: 'Ext.plugin.ListPaging',
            autoPaging: true,
            noMoreRecordsText: '没有更多信息了',
            loadMoreText: '更多...'
        }],
        store: Ext.create("Ext.data.Store", {
            fields: [
                { name: 'Id', type: 'String' },
                { name: 'Time', type: 'DateTime', convert: function (value) {
                    return Duanjt.Date.MsAjaxTime(value).format('yyyy-MM-dd');
                }
                },
                { name: 'Reason', type: 'String' },
                { name: 'Price', type: 'double' },
                { name: 'CostTypeId', type: 'decimal' },
                { name: 'Notes', type: 'String' },
                { name: 'ImgUrl', type: 'String' },
                { name: 'CreateBy', type: 'String' },
                { name: 'CreateTime', type: 'DateTime', convert: function (value) {
                    return Duanjt.Date.MsAjaxTime(value).format('yyyy-MM-dd');
                }
                },
                { name: 'UpdateBy', type: 'String' },
                { name: 'UpdateTime', type: 'DateTime', convert: function (value) {
                    return Duanjt.Date.MsAjaxTime(value).format('yyyy-MM-dd');
                }
                },
                { name: 'CostTypeName', type: 'string' },
                { name: 'CreateName', type: 'string' },
                { name: 'UpdateName', type: 'string' }    
            ],
            pageSize: 15,
            proxy: {
                type: "ajax",
                url: "/LifingCost/SelectByPage",
                reader: {
                    type: "json",
                    rootProperty: "rows"
                }
            },
            autoLoad: true
        }),
        listeners: {
            itemtaphold: function (list, index, target, record, e, eOpts) {
                //修改菜单
                this.getMenu().show();
            },
            itemtap: function (view, index, target) {

            },
            flexchange: function (re, value, oldValue, eOpts) {
                alert("1");
            }
        }
    },
    initialize: function () {
        var me = this;
        this.callParent(arguments);
        this.add(this.getToolbar());
        me.add(this.getMenu());
    },
    getToolbar: function () {
        var me = this;
        if (!this._headerBar) {
            this._headerBar = Ext.create("Ext.Toolbar", {
                title: "生活费信息",
                docked: 'top',
                items: [{
                    //iconCls: 'arrow_left',
                    html: "返回",
                    action: 'back',
                    handler: function () {
                        Ext.Viewport.animateActiveItem(0, { type: 'slide', direction: 'right' });
                    }
                }, {
                    xtype: 'spacer'
                }, {
                    //iconCls: 'add',
                    html: "新增",
                    action: 'add',
                    handler: function () {
                        var layout = Ext.ComponentQuery.query('lifingcostlayout')[0];
                        layout.animateActiveItem(1, 'slide');
                        var form = Ext.ComponentQuery.query('lifingcostedit')[0];
                        form.reset();
                    }
                }]
            });
        }
        return this._headerBar;
    },
    getMenu: function () {
        var me = this;
        if (!this._sheet) {
            this._sheet = Ext.create('Ext.ActionSheet', {
                items: [
                    {
                        text: '修改',
                        ui: 'decline',
                        handler: function () {
                            var record = me.getSelection()[0];
                            var layout = Ext.ComponentQuery.query('lifingcostlayout')[0];
                            layout.animateActiveItem(1, 'slide');
                            var form = Ext.ComponentQuery.query('lifingcostedit')[0];
                            record.data.Time = new Date(record.data.Time);
                            form.setValues(record.data);
                            me.getMenu();
                        }
                    },
                    {
                        text: '删除',
                        handler: function () {
                            alert("");
                        }
                    },
                    {
                        text: '取消',
                        ui: 'confirm',
                        handler: function () {
                            me.getMenu().hide();
                        }
                    }
                ]
            });
        }
        this._sheet.hide();
        return this._sheet;
    }
});