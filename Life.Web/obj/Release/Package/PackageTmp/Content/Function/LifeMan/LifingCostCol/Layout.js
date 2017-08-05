/**
* 作者：段江涛
* 时间：2014-10-30
* 描述：门禁进出记录-布局
*/
Ext.onReady(function () {
    var yearMonth = "";
    var dayWindow = Ext.create("Life.LifeMan.LifingCostCol.ListWin");
    var incomeWindow = Ext.create("Life.LifeMan.LifingCostCol.IncomeWin");

    function LoadDayData(time) {
        dayWindow.show();
        var parm = monthGrid.getParm();
        parm.startTime = time;
        parm.endTime = time;
        dayWindow.down("ngrid").getStore().proxy.extraParams = parm;
        dayWindow.down("ngrid").getStore().loadPage(1);
        dayWindow.setTitle(time + "消费详细");
    }

    //月份汇总表格
    var monthGrid = Ext.create("Life.LifeMan.LifingCostCol.MonthGrid", {
        region: 'center',
        border: 0,
        style: 'border-right:1px solid #99BCE8',
        listeners: {
            selectionchange: function (model, records) {
                var data = monthGrid.GetGridSelectedArrayData();
                var parm = monthGrid.getParm();
                if (data) {
                    var time = [];
                    for (var i = 0; i < data.length; i++) {
                        time.push(data[i].Time);
                    }
                    parm.time = time.join(',');
                } else {
                    parm.time = "";
                }
                yearMonth = parm.time;
                dayGrid.getStore().proxy.extraParams = parm;
                dayGrid.getStore().reload();

                typeGrid.getStore().proxy.extraParams = parm;
                typeGrid.getStore().reload();
            },
            cellclick: function (grid, td, cellIndex, record, tr, rowIndex, e) {
                var t = e.getTarget();
                var control = t.className;
                if (control != "col")
                    return;

                var parm = monthGrid.getParm();
                yearMonth = record.get('Time');
                parm.yearMonth = yearMonth;
                incomeWindow.show(monthGrid);
                incomeWindow.down("ngrid").getStore().proxy.extraParams = parm;
                incomeWindow.down("ngrid").getStore().loadPage(1);
                incomeWindow.setTitle(yearMonth + "收入详细");

            },
            render: function (grid) {
                grid.beginSearch();
            }
        }
    });

    //日期汇总表格
    var dayGrid = Ext.create("Life.LifeMan.LifingCostCol.DayGrid", {
        region: 'center',
        split: true,
        border: 0,
        style: 'border-left:1px solid #99BCE8;border-bottom:1px solid #99BCE8',
        listeners: {
            "itemcontextmenu": function (view, record, item, index, e, eOpts) {
                e.preventDefault();
                view.getSelectionModel().select(index);
                Ext.create("Ext.menu.Menu", {
                    items: [{
                        text: '详细',
                        iconCls: 'i_edit',
                        handler: function () {
                            var records = dayGrid.getSelectionModel().getSelection();
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

    //类型汇总表格
    var typeGrid = Ext.create("Life.LifeMan.LifingCostCol.TypeGrid", {
        region: 'south',
        split: true,
        height: 300,
        border: 0,
        style: 'border-left:1px solid #99BCE8;border-top:1px solid #99BCE8',
        listeners: {
            "itemdblclick": function (View, record, item, index, e, options) {
                var parm = monthGrid.getParm();
                parm.yearMonth = yearMonth;
                parm.CostTypeName = record.get('costTypeName');
                parm.CostTypeId = null;
                dayWindow.show();
                dayWindow.down("ngrid").getStore().proxy.extraParams = parm; //{ yearMonth: yearMonth, CostTypeName: record.get('costTypeName') };
                dayWindow.down("ngrid").getStore().loadPage(1);
                dayWindow.setTitle(record.get('costTypeName') + "-消费详细");
            }
        }
    });

    //右边Panel
    var rightPanel = Ext.create("Ext.panel.Panel", {
        layout: 'border',
        region: 'east',
        width: 300,
        split: true,
        border: 0,
        items: [dayGrid, typeGrid]
    });

    Ext.create("Ext.container.Viewport", {
        layout: 'border',
        items: [monthGrid, rightPanel],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    })

});