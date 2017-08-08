//生活费消费信息store
Ext.define("Life.LifeMan.LifingCost.LifeTypeStore", {
    extend: 'Ext.data.Store',
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

});