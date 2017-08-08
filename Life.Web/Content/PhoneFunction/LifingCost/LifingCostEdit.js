Ext.define('Life.LifingCost.LifingCostEdit', {
    extend: 'Ext.ux.NForm',
    alias: "widget.lifingcostedit",
    config: {
        fullscreen: true,
        items: [{
            xtype: 'fieldset',
            items: [
                { xtype: "datepickerfield", name: "Time", label: "消费时间", value: new Date(), dateFormat: "Y-m-d",
                    picker: {
                        doneButton: '确定',
                        cancelButton: '取消'
                    }
                },
                { xtype: "textfield", name: "Reason", label: "消费名称" },
                { xtype: "numberfield", name: "Price", label: "消费金额" },
                {
                    xtype: 'selectfield',
                    name: "CostTypeId",
                    label: '消费类型',
                    displayField: "Name",
                    valueField: "Id",
                    store: Ext.create("Ext.data.Store", {
                        autoLoad: true,
                        fields: ["Id", "Name"],
                        proxy: {
                            type: 'ajax',
                            url: '/Diction/SelectByParentId',
                            extraParams: { parentId: "1000300000" },
                            reader: {
                                type: 'json'
                            }
                        }
                    }),
                    picker: {
                        doneButton: '确定',
                        cancelButton: '取消'
                    }

                },
                { xtype: "textareafield", name: "Notes", label: "备注" },
                { xtype: "hiddenfield", name: "Id", label: "编号" },
                { xtype: "hiddenfield", name: "CreateBy", label: "创建人编号" },
                { xtype: "hiddenfield", name: "CreateTime", label: "创建时间" },
            ]
        }]
    },
    initialize: function () {
        this.callParent(arguments);
        this.add(this.getToolbar());
    },
    getToolbar: function () {
        var me = this;
        if (!this._headerBar) {
            this._headerBar = Ext.create("Ext.TitleBar", {
                title: "生活费信息",
                docked: 'top',
                items: [{
                    //iconCls: 'arrow_left',
                    action: 'back',
                    html: "返回",
                    align: 'left',
                    handler: function () {
                        var layout = Ext.ComponentQuery.query('lifingcostlayout')[0];
                        layout.animateActiveItem(0, { type: 'slide', direction: 'right' });
                    }
                }, {
                    //iconCls: 'arrow_left',
                    html: "保存",
                    align: 'right',
                    handler: function () {
                        //验证暂时未做
                        me.submit({
                            url: "/LifingCost/Save",
                            success: function (form, action) {
                                Ext.Msg.alert('提示', action.msg);
                                if (action.success) {
                                    var layout = Ext.ComponentQuery.query('lifingcostlayout')[0];
                                    layout.animateActiveItem(0, { type: 'slide', direction: 'right' });

                                    var list = Ext.ComponentQuery.query('lifingcostlist')[0];
                                    list.getStore().loadPage(1);
                                }
                            },
                            failure: function (form, action) {
                                Ext.Msg.alert("提示", action.result.msg);
                            }
                        });

                    }
                }]
            });
        }
        return this._headerBar;
    }
});