var treeStore = Ext.create("Ext.data.TreeStore", {
    fields: ["id", "text", "Tobject", "leaf"],
    root: {
        expanded: true,
        text: '配置类型',
        id: 0
    },
    proxy: {
        type: 'ajax',
        url: '/Module/SelectAll'
    }
});

var centerPanel = Ext.create("Ext.tab.Panel", {
    region: "center",    
    items: [
        {
            title: '首页',
            html: "<iframe width='100%' height='100%' frameborder='0' src='/HTML/index.htm'></iframe>"
        },
    ]
});

var myMask = new Ext.LoadMask(centerPanel, { msg: '正在努力加载...' });

var tree = Ext.create("Ext.ux.MainMenu", {
    width: 220,
    region: 'west',
    split: true,
    collapsed: false,
    collapsible: true,
    title: '菜单',
    listeners: {
        itemclick: function (record) {
            var data = record.get("Tobject");
            AddShow(data.ModuleName, data.ModuleUrl,data.IconUrl);
        }
    }
});

var southPanel = Ext.create("Ext.panel.Panel", {
    region: "south",
    height: 50,
    border: 1,
    margin:'5 0 0 0',
    html: "<div style='height:50px;text-align:center;line-height:50px;background-color:#6C9DFC;color:white'>Copyright © 2011-2014 段江涛 </div>" 
});

var northPanel = Ext.create("Ext.panel.Panel", {
    region: "north",
    border: 1,
    margin:'0 0 5 0',
    height: 50,
    loader: {
        url: '/HTML/top.htm',
        autoLoad: true
    }
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        padding: '5',
        items: [tree, centerPanel, southPanel, northPanel],
        listeners: {
            "render": function () {
                GetCurrentUser();
            }
        }
    });
});

//增加选项卡
function AddShow(name, url,icon) {
    var tab = Ext.getCmp('tab_' + url);
    if (!tab) {
        myMask.show();
        var b = centerPanel.add({ id: "tab_" + url,icon:icon, title: name, closable: true, html: "<iframe width='100%' height='100%' frameborder='0' src='" + url + "'></iframe>" });
        b.show();
    } else {
        Ext.getCmp('tab_' + url).show();
    }

}

//获得当前用户
function GetCurrentUser() {
    Ext.Ajax.request({
        url: '/Manage/GetCurrentUser',
        success: function (response) {
            var data = Ext.JSON.decode(response.responseText);
            Ext.get("labCurrentName").dom.innerHTML = data.Name;
        }
    })
}

//退出登录
function ExitLogin() {
    Ext.Ajax.request({
        url: '/Manage/ExitLogin',
        success: function (response) {
            var data = Ext.JSON.decode(response.responseText);
            if (data.success) {
                //清空cookie，跳转页面
                Duanjt.Cookie.Remove("LifeLogin", "/"); //清空cookie的方法就是将cookie的失效期设置为当前日期之前
                location.href = "/Manage/Index";
            } else
                Ext.Msg.alert("提示", data.msg);
        }
    })
}
/***************修改密码******************/
//修改密码
function UpdatePass() {
    Ext.getCmp("updatePassForm").getForm().reset();
    updatePassWin.show();
}