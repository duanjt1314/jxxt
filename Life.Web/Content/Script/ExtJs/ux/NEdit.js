/**
*   编辑页面
*/
Ext.define("Ext.ux.NEdit", {
    extend: 'Ext.window.Window',
    alias: 'widget.nedit',
    heigth: 300, //高度
    width: 300, //宽度
    modal: true,
    layout: 'fit',
    closeAction: 'hide',
    title: '编辑页面',
    iconCls: 'i_edit',
    formItems: [], //表单结构
    saveUrl: '', //保存地址
    IsUse: false, //是否显示应用按钮
    getButtons: function () {
        var me = this;
        var buttons = [{
            text: '确定',
            action: 'ok',
            listeners: {
                "click": function () {
                    var form = me.down("form").getForm();
                    if (form.isValid()) {
                        form.submit({
                            submitEmptyText: false, //文本框emptyText的值是否提交到后台
                            url: me.saveUrl,
                            waitMsg: "正在提交请稍后...",
                            waitTitle: "消息",
                            success: function (form, action) {
                                me.fireEvent('success', action, me);
                            },
                            failure: function (form, action) {
                                Ext.Msg.alert("提示", action.result.msg);
                            }
                        });
                    }
                }
            }
        }, {
            text: '取消',
            handler: function () {
                me.hide();
            }
        }];

        if (me.IsUse) {
            buttons.push({
                text: '应用',
                action: 'use',
                listeners: {
                    click: function () {
                        var form = me.down("form").getForm();
                        if (form.isValid()) {
                            form.submit({
                                url: me.saveUrl,
                                waitMsg: "正在提交请稍后...",
                                waitTitle: "消息",
                                success: function (form, action) {
                                    me.fireEvent('use', action, me);
                                },
                                failure: function (form, action) {
                                    Ext.Msg.alert("提示", action.result.msg);
                                }
                            });
                        }
                    }
                }
            });
        }

        return buttons;
    },
    initComponent: function () {
        var me = this;
        //注册保存成功事件
        this.addEvents('success', 'use');

        this.buttons = me.getButtons();
        this.items = me.formItems;
        this.callParent(arguments);
    }
});