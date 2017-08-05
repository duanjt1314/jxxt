/**
* 作者：段江涛
* 时间：2014-10-30
* 描述：
*/
Ext.onReady(function () {
    var grid = Ext.create("Life.LifeMan.BankCardCalc.Grid");

    Ext.create("Ext.container.Viewport", {
        layout: 'fit',
        items: [grid],
        listeners: {
            "afterrender": function () {
                parent.myMask.hide();
            }
        }
    })

});