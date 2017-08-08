/*全局变量*/
var yearMonth = "";


/*****chart*****/
var store = Ext.create('Ext.data.JsonStore', {
    fields: ['time', 'price', 'interPrice'],
    autoLoad: false,
    proxy: {
        type: 'ajax',
        url: '/LifingCost/GetCollection'
    }
});

var chart = Ext.create('Ext.chart.Chart', {
    region: 'center',
    xtype: 'chart',
    border: 1,
    animate: true,
    store: store,
    axes: [
        {
            type: 'Numeric',
            position: 'left',
            fields: ['price', 'interPrice'],
            label: {
                renderer: Ext.util.Format.numberRenderer('0,0')
            },
            title: '消费金额',
            grid: true            
        },
        {
            type: 'Category',
            position: 'bottom',
            fields: ['time'],
            title: '月份',
            label: {
                rotate: {
                    degrees: 315
                }
            }
        }
    ],
    series: [{
        type: 'column',
        axis: 'left',
        highlight: true,
        tips: {
            trackMouse: true,
            width: 140,
            height: 40,
            renderer: function (storeItem, item) {
                if (item.yField == "price")
                    this.setTitle(storeItem.get('time') + '</br>消费:￥' + storeItem.get('price'));
                else
                    this.setTitle(storeItem.get('time') + '</br>收入:￥' + storeItem.get('interPrice'));


            }
        },
        label: {
            display: 'insideEnd',
            'text-anchor': 'middle',
            field: ['price', 'interPrice'],
            renderer: Ext.util.Format.numberRenderer('0'),
            orientation: 'vertical',
            color: '#333'
        },
        listeners: {
            itemclick: function (item) {
                if (item.yField == "price") {
                    yearMonth = item.storeItem.get('time');

                    monthGrid.store.load({ params: { time: yearMonth} });
                    monthGrid.setTitle(yearMonth + "消费详细");

                    typeStore.load({ params: { time: yearMonth} });
                } else {
                    yearMonth = item.storeItem.get('time');

                    interWindow.show(monthGrid);
                    interStore.proxy.extraParams = { yearMonth: yearMonth };
                    interStore.loadPage(1);
                    interWindow.setTitle(yearMonth + "收入详细");
                }

            }
        },
        xField: 'time',
        yField: ['price', 'interPrice']
    }]
});

/******InterPrice*********/
var interStore = Ext.create('Ext.data.Store', {
    fields: [
        { name: 'Time', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        },
        { name: 'Price', type: 'double' },
        { name: 'Note', type: 'String' },
        { name: 'CreateName', type: 'String' }
    ],
    autoLoad: false,
    proxy: {
        type: 'ajax',
        url: '/Income/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 10
});

var interWindow = Ext.create("Ext.window.Window", {
    title: '收入详细',
    modal: true,
    layout: 'fit',
    closeAction: 'hide',
    buttonAlign: 'center',
    layout: 'fit',
    items: [{
        xtype: 'grid',
        width: 400,
        height: 300,
        store: interStore,
        multiSelect: true,
        columns: [
            { text: '时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
            { text: '金额', dataIndex: 'Price', flex: 1, minWidth: 0, align: 'right',
                renderer: function (value) {
                    return "￥" + Duanjt.Float.ToFloat(value, 2);
                }
            },
            { text: '时间', dataIndex: 'CreateName', flex: 1, minWidth: 0 },
            { text: '备注', dataIndex: 'Note', flex: 1, minWidth: 0,
                renderer: function (value) {
                    return '<div title="' + value + '">' + value + '</div>';
                }
            }
        ],
        dockedItems: [{
            xtype: 'pagingtoolbar',
            store: interStore,
            dock: 'bottom',
            displayInfo: true
        }]
    }],
    buttons: [{
        text: '关闭',
        handler: function () {
            interWindow.hide(monthGrid);
        }
    }]
})

/*****grid*****/
var monthStore = Ext.create('Ext.data.JsonStore', {
    fields: [
        { name: 'time', type: 'date', convert: function (value) {
            return value.substr(0, 10);
            //var time = new Date(value);
            //return time.getFullYear() + "-" + Duanjt.String.InsertSpace((time.getMonth() + 1), 2) + "-" + Duanjt.String.InsertSpace(time.getDate(), 2); //月份为0-11
        }
        },
        { name: 'price', type: 'float' }
    ],
    autoLoad: false,
    proxy: {
        type: 'ajax',
        url: '/LifingCost/GetCollectionByDay'
    }
});

var monthGrid = Ext.create('Ext.grid.Panel', {
    title: '消费详细',
    region: "center",
    store: monthStore,
    multiSelect: true,
    columns: [
        { text: '消费时间', dataIndex: 'time', flex: 1, minWidth: 0 },
        { text: '消费金额', dataIndex: 'price', flex: 1, minWidth: 0, align: 'right', renderer: function (value) {
            return "￥" + Duanjt.Float.ToFloat(value, 2);
        }
        }
    ],
    listeners: {
        "itemcontextmenu": function (view, record, item, index, e, eOpts) {
            e.preventDefault();
            view.getSelectionModel().select(index);
            Ext.create("Ext.menu.Menu", {
                items: [{
                    text: '详细',
                    iconCls: 'i_edit',
                    handler: function () {
                        var records = monthGrid.getSelectionModel().getSelection();
                        if (records.length <= 0) {
                            Ext.Msg.alert("提示", "请选择要查看的数据");
                        } else {
                            var record = records[0];
                            LoadDayData(record.get("time"));
                        }

                    }
                }]
            }).showAt(e.getXY());
        },
        "itemdblclick": function (View, record, item, index, e, options) {
            LoadDayData(record.get("time"));
        }
    }
});

function LoadDayData(time) {
    dayWindow.show(monthGrid);
    //dayWindow.items.get(0).store.proxy.extraParams = { startTime: time, endTime: time };
    //dayWindow.items.get(0).store.load({ params: { start: 0, limit: 10,foo: 'bar' } });
    dayStore.proxy.extraParams = { startTime: time, endTime: time };
    dayStore.loadPage(1);
    dayWindow.setTitle(time + "消费详细");
}

/*******dayWindow********/
var dayStore = Ext.create("Ext.data.Store", {
    autoLoad: false,
    fields: [
        { name: 'Id', type: 'String' },
        { name: 'Time', type: 'DateTime', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        },
        { name: 'Reason', type: 'String' },
        { name: 'Price', type: 'double' },
        { name: 'Notes', type: 'String' },

        { name: 'CostTypeName', type: 'string' },
    ],
    proxy: {
        type: 'ajax',
        url: '/LifingCost/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 10
});

var dayWindow = Ext.create("Ext.window.Window", {
    title: '详细信息',
    modal: true,
    layout: 'fit',
    closeAction: 'hide',
    buttonAlign: 'center',
    layout: 'fit',
    items: [{
        xtype: 'grid',
        width: 400,
        height: 300,
        store: dayStore,
        multiSelect: true,
        columns: [
            { text: '消费时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
            { text: '消费名称', dataIndex: 'Reason', flex: 1, minWidth: 0 },
            { text: '消费金额', dataIndex: 'Price', flex: 1, minWidth: 0, align: 'right', renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            }
            },
            { text: '消费类型', dataIndex: 'CostTypeName', flex: 1, minWidth: 0 },
            { text: '备注', dataIndex: 'Notes', flex: 1, minWidth: 0,
                renderer: function (value) {
                    return '<div title="' + value + '">' + value + '</div>';
                }
            }
        ],
        dockedItems: [{
            xtype: 'pagingtoolbar',
            store: dayStore,
            dock: 'bottom',
            displayInfo: true
        }]
    }],
    buttons: [{
        text: '关闭',
        handler: function () {
            dayWindow.hide(monthGrid);
        }
    }]
});

/*******typeWindow******/
var typeStore = Ext.create("Ext.data.Store", {
    fields: ['costTypeName', 'price'],
    autoLoad: false,
    proxy: {
        type: 'ajax',
        url: '/LifingCost/GetCollectionByType'
    }
});

var typePanel = Ext.create("Ext.panel.Panel", {
    title: '详细信息',
    region: 'south',
    height: 250,
    layout: 'fit',
    split: true,
    items: [{
        xtype: 'chart',
        animate: true,
        store: typeStore,
        legend: {
            position: 'right'
        },
        //theme: 'Base:gradients',
        series: [{
            type: 'pie',
            angleField: 'price',
            showInLegend: true,
            tips: {
                trackMouse: true,
                width: 140,
                height: 28,
                renderer: function (storeItem, item) {
                    // calculate and display percentage on hover
                    var total = 0;
                    typeStore.each(function (rec) {
                        total += rec.get('price');
                    });
                    this.setTitle(storeItem.get('costTypeName') + ': ￥' + storeItem.get('price') + ' <label style="color:red">' + Math.round(storeItem.get('price') / total * 100) + '%</label>');
                }
            },
            highlight: {
                segment: {
                    margin: 20
                }
            },
            label: {
                field: 'costTypeName',
                display: 'rotate',
                contrast: true,
                font: '12px Arial'
            },
            listeners: {
                itemclick: function (item) {
                    dayStore.proxy.extraParams = { yearMonth: yearMonth, CostTypeName: item.storeItem.get('costTypeName') };
                    dayStore.loadPage(1);
                    dayWindow.setTitle(item.storeItem.get('costTypeName') + "-消费详细");
                    dayWindow.show();
                }
            }
        }]
    }]
});

var rightPanel = Ext.create("Ext.panel.Panel", {
    layout: 'border',
    width: 300,
    border: 0,
    region: 'east',
    split: true,
    items: [
        monthGrid, typePanel
    ]
});

var _beginDate = new Date();
_beginDate.setTime(_beginDate.getTime() - 365 * 24 * 60 * 60 * 1000);
/*配置Panel*/
var configPanel = Ext.create("Ext.toolbar.Toolbar", {
    border: 0,
    region: 'north',
    split: false,
    items: [
        '最大值',
        {
            xtype: 'numberfield',
            name: 'maxValue',
            value: '8000',
            width: 70
        },
        '   开始月份',
        {
            xtype: 'datefield',
            name: 'beginTime',
            maxValue: new Date(),
            value: _beginDate,
            format: 'Y-m',
            width: 90
        },
        '结束月份',
        {
            xtype: 'datefield',
            name: 'endTime',
            maxValue: new Date(),
            value: new Date(),
            format: 'Y-m',
            width: 90
        },
        {
            text: '重新加载',
            handler: function () {
                var a = chart.axes.items[0];
                a.maximum = configPanel.down("textfield[name=maxValue]").getValue();

                var time1 = configPanel.down("datefield[name=beginTime]").rawValue;
                var time2 = configPanel.down("datefield[name=endTime]").rawValue;
                store.load({ params: { beginTime: time1, endTime: time2} });
                //store.reload();
                chart.redraw(true);
            }
        }
    ]
});

var leftPanel = Ext.create("Ext.panel.Panel", {
    layout: 'border',
    region: 'center',
    split: true,
    items: [
        configPanel, chart
    ]
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [leftPanel, rightPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();

                var time1 = configPanel.down("datefield[name=beginTime]").rawValue;
                var time2 = configPanel.down("datefield[name=endTime]").rawValue;
                store.load({ params: { beginTime: time1, endTime: time2} });
            }
        }
    });

});
