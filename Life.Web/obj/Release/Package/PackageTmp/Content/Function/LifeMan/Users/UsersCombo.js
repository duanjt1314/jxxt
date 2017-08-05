//用户下拉框组将
Ext.define("Life.LifeMan.Users.UsersCombo", {
    extend: 'Ext.form.ComboBox',
    alias: 'widget.userscombo',
    dataUrl: '/Users/GetAll',
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