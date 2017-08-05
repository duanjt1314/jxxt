/*
*   字典信息
*/
Ext.define("Ext.ux.DicCombo", {
    extend: 'Ext.form.ComboBox',
    alias: 'widget.diccombo',
    dataUrl: '/Diction/SelectByParentId',
    parentId: '', //字典父级编号
    editable: false,
    displayField: 'Name',
    valueField: 'Id',
    queryMode: 'local',    
    initComponent: function () {
        var me = this;
        me.store = Ext.create("Ext.data.Store", {
            autoLoad: true,
            fields: ["Id", "Name"],
            proxy: {
                type: 'ajax',
                url: me.dataUrl,
                extraParams: { parentId: me.parentId },
                reader: {
                    type: 'json'
                }
            }
        });
        this.callParent(arguments);
    }
});