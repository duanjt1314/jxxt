/*
*   图片展示窗体
*/
Ext.define("Ext.ux.ShowImg", {
    extend: 'Ext.window.Window',
    alias: 'widget.showimg',
    modal: true,
    layout: 'fit',
    closeAction: 'destroy',
    title: '图片显示',
    imgUrl: '', //图片路径
    maximizable: true,
    resizable: {
        preserveRatio: true
    },
    maxHeight: 450,
    maxWidth: 700,
    minHeight: 100,
    minWidth: 100,
    //bodyStyle:'text-align:center',
    tools: [{
        type: 'search',
        tooltip: '新窗口打开',
        tooltipType: 'title',
        handler: function (event, target, owner, tool) {
            var me = this;            
            window.open("/HTML/SearchImg.aspx?ImgUrl=" + owner.ownerCt.imgUrl);
        }
    }],
    initComponent: function () {
        var me = this;
        var changingImage = Ext.create('Ext.Img', {
            src: me.imgUrl,
            border: 0
        });

        var image = new Image();
        image.src = me.imgUrl;
        image.onload = function () {
            changingImage.setWidth(image.width);//设置宽度
            changingImage.setHeight(image.height);//设置高度
            me.center();//窗体居中
        }

        me.items = [changingImage];
        //me.html = "<img style='max-height:450px; max-width:700px;' src='" + me.imgUrl + "'/>";
        this.callParent(arguments);
    },
    buttonAlign: 'center',
    buttons: [
        {
            text: '关闭',
            handler: function (but) {
                but.ownerCt.ownerCt.close();
            }
        }
    ]
});