/**
* 调用方式
* { fieldLabel: '新密码', name: 'newPass', xtype: 'textfield', inputType: 'password', allowBlank: false,vtype:"Password",confirmTo:"pass" }, //pass表示需要验证的文本框的名称
*/
Ext.onReady(function(){
	Ext.apply(Ext.form.VTypes,{
		Password:function(val,field){               //val指这里的文本框值，field指这个文本框组件，大家要明白这个意思  
			if(field.confirmTo){                    //confirmTo是我们自定义的配置参数，一般用来保存另外的组件的id值  
				var pwd=Ext.getCmp(field.confirmTo);   //取得confirmTo的那个id的值  
				return (val==pwd.getValue());  
			}  
			return tr;  
		},
        PasswordText:"两次密码不一致"
	});

    Ext.apply(Ext.form.VTypes, {
        PwdFormat: function (val, field) {
            var reg = /^\w+$/; //密码只能是字母，数字和下划线组成
            return reg.test(val);
        },
        PwdFormatText: "密码只能是字母，数字和下划线组成"
    });

	Ext.apply(Ext.form.VTypes,{
		Age:function(val,field){
			var reg = /^\d+$/;//年龄是大于0的数字
			return reg.test(val);
		},
        AgeText:"年龄只能是数字"
	});
});