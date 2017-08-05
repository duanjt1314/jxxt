/**
*   Sencha-Touch2.3
*   NListTree重新封装，方便使用，使用Ext.ux.NForm 需要遵守读取数据规范
*   xtype:nform
*   所属类别：【公共】
*   例子：
*   Ext.define("OA.SPOFixedAssets.SPOFixedAssetsEdit", {
*       extend: 'Ext.ux.NForm',
*       alias: "widget.spofixedassetsedit",
*       config: {
*           title: 'title',
*           subUrl: 'url',
*           items: [{
*               xtype: 'fieldset',
*               items: []
*           }],
*           listeners: {
*               success: function (form, result) {
*
*               },
*               Back: function (but,form) {
*
*               }
*           }
*       }
*   });
*/
Ext.define("Ext.ux.NForm", {
    extend: 'Ext.form.Panel',
    alias: "widget.nform",
    requires: ["Ext.form.Panel"],
    config: {
        fullscreen: true,
        signs: '', //状态
        subUrl: '' //提交地址
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
    },
    /**
    * Returns an object containing the value of each field in the form, keyed to the field's name.
    * For groups of checkbox fields with the same name, it will be arrays of values. For example:
    *
    *     {
    *         name: "Jacky Nguyen", // From a TextField
    *         favorites: [
    *             'pizza',
    *             'noodle',
    *             'cake'
    *         ]
    *     }
    *
    * @param {Boolean} [enabled] `true` to return only enabled fields.
    * @param {Boolean} [all] `true` to return all fields even if they don't have a
    * {@link Ext.field.Field#name name} configured.
    * @return {Object} Object mapping field name to its value.
    */
    getValues: function (enabled, all) {
        var fields = this.getFields(),
            values = {},
            isArray = Ext.isArray,
            field, value, addValue, bucket, name, ln, i;

        // Function which you give a field and a name, and it will add it into the values
        // object accordingly
        addValue = function (field, name) {
            if (!all && (!name || name === 'null') || field.isFile) {
                return;
            }

            if (field.isCheckbox) {
                value = field.getSubmitValue();
            } else if (field.$className == "Ext.field.DatePicker" || field.$className == "Ext.ux.Timefield") {
                value =Ext.Date.format(field.getValue(),field.getDateFormat());
            }else {
                value = field.getValue();
            }


            if (!(enabled && field.getDisabled())) {
                // RadioField is a special case where the value returned is the fields valUE
                // ONLY if it is checked
                if (field.isRadio) {
                    if (field.isChecked()) {
                        values[name] = value;
                    }
                } else {
                    // Check if the value already exists
                    bucket = values[name];
                    if (!Ext.isEmpty(bucket)) {
                        // if it does and it isn't an array, we need to make it into an array
                        // so we can push more
                        if (!isArray(bucket)) {
                            bucket = values[name] = [bucket];
                        }

                        // Check if it is an array
                        if (isArray(value)) {
                            // Concat it into the other values
                            bucket = values[name] = bucket.concat(value);
                        } else {
                            // If it isn't an array, just pushed more values
                            bucket.push(value);
                        }
                    } else {
                        values[name] = value;
                    }
                }
            }
        };

        // Loop through each of the fields, and add the values for those fields.
        for (name in fields) {
            if (fields.hasOwnProperty(name)) {
                field = fields[name];

                if (isArray(field)) {
                    ln = field.length;
                    for (i = 0; i < ln; i++) {
                        addValue(field[i], name);
                    }
                } else {
                    addValue(field, name);
                }
            }
        }

        return values;
    }
});