/*全局变量*/
var yearMonth = "";


/*****chart*****/
var store = Ext.create('Ext.data.JsonStore', {
    fields: ['time', 'moneyInsert', 'moneyOut'],
    autoLoad: true,
    proxy: {
        type: 'ajax',
        url: '/BankCard/GetCollectionByMonth'
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
            fields: ['moneyInsert', 'moneyOut'],
            label: {
                renderer: Ext.util.Format.numberRenderer('0,0')
            },
            title: '金额',
            grid: true,
            //maximum: 8000,
            minimum: 0
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
                if (item.yField == "moneyInsert")
                    this.setTitle(storeItem.get('time') + '</br>存入:￥' + storeItem.get('moneyInsert'));
                else
                    this.setTitle(storeItem.get('time') + '</br>取出:￥' + storeItem.get('moneyOut'));
            }
        },
        label: {
            display: 'insideEnd',
            'text-anchor': 'middle',
            field: ['moneyInsert', 'moneyOut'],
            renderer: Ext.util.Format.numberRenderer('0'),
            orientation: 'vertical',
            color: '#333'
        },
        listeners: {
            itemclick: function (item) {
                yearMonth = item.storeItem.get('time');

                monthGrid.store.load({ params: { yearMonth: yearMonth} });
                monthGrid.setTitle(yearMonth + "操作详细");
            }
        },
        xField: 'time',
        yField: ['moneyInsert', 'moneyOut']
    }]
});

/*****grid*****/
var monthStore = Ext.create('Ext.data.JsonStore', {
    fields: [
        { name: 'Time', type: 'date', convert: function (value) {
            return Duanjt.Date.NumToDate(value);
        }
        },
        { name: 'SaveName', type: 'string' },
        { name: 'Price', type: 'float' },
        { name: 'Note', type: 'string' }
    ],
    groupField: 'SaveName',
    autoLoad: false,
    proxy: {
        type: 'ajax',
        url: '/BankCard/SelectByPage',
        reader: {
            type: 'json',
            root: 'rows',
            totalProperty: 'total'
        }
    },
    pageSize: 20
});

var monthGrid = Ext.create('Ext.grid.Panel', {
    title: '消费详细',
    region: "east",
    width: 250,
    store: monthStore,
    multiSelect: true,
    columns: [
        { text: '消费时间', dataIndex: 'Time', flex: 1, minWidth: 0 },
        { text: '操作方式', dataIndex: 'SaveName', flex: 1, minWidth: 0 },
        { text: '操作金额', dataIndex: 'Price', flex: 1, minWidth: 0, align: 'right',
            renderer: function (value) {
                return "￥" + Duanjt.Float.ToFloat(value, 2);
            },
            summaryType: 'sum',
            summaryRenderer: function (value) {
                return Ext.String.format('￥' + value);
            }
        }
    ],
    features: [{
        groupHeaderTpl: '{name}',
        ftype: 'groupingsummary'
    }],
    dockedItems: [{
        xtype: 'pagingtoolbar',
        store: monthStore,
        dock: 'bottom',
        displayInfo: true
    }]
});

/*鼠标移上去显示备注*/
monthGrid.getView().on('render', function (view, obj) {
    view.tip = Ext.create('Ext.tip.ToolTip', {
        // The overall target element.
        target: view.el,
        // Each grid row causes its own separate show and hide.
        delegate: view.itemSelector,
        // Moving within the row should not hide the tip.
        trackMouse: true,
        // Render immediately so that tip.body can be referenced prior to the first show.
        renderTo: Ext.getBody(),
        //layout: 'fit',
        maxWidth: 200,
        maxHeight: 200,
        listeners: {
            // Change content dynamically depending on which element triggered the show.
            beforeshow: function updateTipBody(tip) {
                tip.update(view.getRecord(tip.triggerElement).get('Note'));
            }
        }
    });
});

Ext.onReady(function () {
    Ext.create("Ext.container.Viewport", {
        layout: "border",
        items: [chart, monthGrid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    });

});
