/*
* 富文本控件
* 说明：需要手动引用对应的css和js文件
*  <link href="/Content/Script/kindeditor-4.1.4/themes/default/default.css" rel="stylesheet" type="text/css" />
*  <link href="/Content/Script/kindeditor-4.1.4/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
*  <script src="/Content/Script/kindeditor-4.1.4/kindeditor.js" type="text/javascript"></script>
*  <script src="/Content/Script/kindeditor-4.1.4/lang/zh_CN.js" type="text/javascript"></script>
*  <script src="/Content/Script/kindeditor-4.1.4/plugins/code/prettify.js" type="text/javascript"></script>
*/
Ext.define("Ext.ux.RichText", {
    extend: 'Ext.form.FieldContainer',
    alternateClassName: 'Ext.form.RichText',
    mixins: {
        field: 'Ext.form.field.Field'
    },
    alias: 'widget.richtext',
    html: "<textarea id='_editor_id' name='_content' style='width:100%'></textarea>",
    _editor: null,
    height:300,
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    },
    getHtml: function () {
        var me = this;
        return me._editor.html();
    },
    getValue: function () {
        var me = this;
        return me._editor.html();
    },
    listeners: {
        afterrender: function (richText) {
            richText._editor = KindEditor.create('#_editor_id', {
                //cssPath: '../plugins/code/prettify.css',
                uploadJson: '/AJ/UpdImage/upload_json.ashx',//图片上传路径
                fileManagerJson: '/AJ/UpdImage/file_manager_json.ashx',//图片查看路径
                allowFileManager: true,
                height: richText.height,
                afterCreate: function () {
                    var self = this;
                    KindEditor.ctrl(document, 13, function () {
                        self.sync();
                        KindEditor('form[name=example]')[0].submit();
                    });
                    KindEditor.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        KindEditor('form[name=example]')[0].submit();
                    });
                }
            });

        }
    }
});