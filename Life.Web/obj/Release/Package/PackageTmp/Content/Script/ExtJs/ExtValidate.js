/**
* ���÷�ʽ
* { fieldLabel: '������', name: 'newPass', xtype: 'textfield', inputType: 'password', allowBlank: false,vtype:"Password",confirmTo:"pass" }, //pass��ʾ��Ҫ��֤���ı��������
*/
Ext.onReady(function(){
	Ext.apply(Ext.form.VTypes,{
		Password:function(val,field){               //valָ������ı���ֵ��fieldָ����ı�����������Ҫ���������˼  
			if(field.confirmTo){                    //confirmTo�������Զ�������ò�����һ��������������������idֵ  
				var pwd=Ext.getCmp(field.confirmTo);   //ȡ��confirmTo���Ǹ�id��ֵ  
				return (val==pwd.getValue());  
			}  
			return tr;  
		},
        PasswordText:"�������벻һ��"
	});

    Ext.apply(Ext.form.VTypes, {
        PwdFormat: function (val, field) {
            var reg = /^\w+$/; //����ֻ������ĸ�����ֺ��»������
            return reg.test(val);
        },
        PwdFormatText: "����ֻ������ĸ�����ֺ��»������"
    });

	Ext.apply(Ext.form.VTypes,{
		Age:function(val,field){
			var reg = /^\d+$/;//�����Ǵ���0������
			return reg.test(val);
		},
        AgeText:"����ֻ��������"
	});
});