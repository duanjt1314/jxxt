var ExtjsUtil = {
    //错误信息
    ShowErrorMsg: function (title, msg) {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            buttons: Ext.MessageBox.OK,
            icon: Ext.MessageBox.ERROR
        })
    },
    //警告信息
    ShowWarningMsg: function (title, msg) {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            buttons: Ext.MessageBox.OK,
            icon: Ext.MessageBox.WARNING
        })
    },
    //感叹号
    ShowInfoMsg: function (title, msg) {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            buttons: Ext.MessageBox.OK,
            icon: Ext.MessageBox.INFO
        })
    },
    //成功
    ShowCheckMsg: function (title, msg) {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            buttons: Ext.MessageBox.OK,
            icon: Ext.baseCSSPrefix + ' i_check'
        })
    },
    //定义图标
    ShowCheckMsg: function (title, msg, iconCls) {
        Ext.MessageBox.show({
            title: title,
            msg: msg,
            buttons: Ext.MessageBox.OK,
            icon: Ext.baseCSSPrefix + iconCls
        })
    },
    //获得数据字典对象
    GetDicRecord: function (dirId) {
        var record = [];
        Ext.Ajax.request({
            url: '/Admin/DataDictionary/SelectById',
            async: false, //关键(同步加载)
            params: {
                id: dirId
            },
            success: function (response) {
                record = Ext.JSON.decode(response.responseText);
            }
        });
        return record;
    },
    //是否删除
    DoDelete: function (obj) {
        Ext.MessageBox.confirm('操作确认', '确定要删除选择的项吗?', function (btn) {
            if (btn == 'yes') {
                Ext.Ajax.request({
                    url: obj.url,
                    async: false,
                    params: obj.params,
                    success: function (response) {
                        var result = Ext.JSON.decode(response.responseText);
                        if (Ext.isDefined(result)) {
                            obj.success(result);
                        };
                    }
                });
            }
        }, this);
    }
};