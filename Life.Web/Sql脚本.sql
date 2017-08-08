/*  =======================================================================================
 *  时间：2013/12/29 12:34:44
 * ======================================================================================== */
/*创建数据库*/
use master
go
if exists(select * from sys.databases where name='Life')
	drop database Life
go
create database Life
go
use Life
go

/*Sql语句*/
/*  =======================================================================================
 *  Object Name		:	文章类型表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Art_Category
(            
        Cat_Id	varchar(64)	not null,
        Cat_Name	varchar(20),
        Cat_Remark	varchar(100),
        Cat_Order	int,
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Art_Category
       add constraint PK_Art_Category_Cat_Id primary key (Cat_Id);

EXEC sp_addextendedproperty 'MS_Description', '文章类型表', 'user', dbo, 'table', Art_Category, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '类型编号', 'user', dbo, 'table', Art_Category, 'column', Cat_Id;
EXEC sp_addextendedproperty 'MS_Description', '类型名称', 'user', dbo, 'table', Art_Category, 'column', Cat_Name;
EXEC sp_addextendedproperty 'MS_Description', '类型备注', 'user', dbo, 'table', Art_Category, 'column', Cat_Remark;
EXEC sp_addextendedproperty 'MS_Description', '排序', 'user', dbo, 'table', Art_Category, 'column', Cat_Order;
EXEC sp_addextendedproperty 'MS_Description', '创建人', 'user', dbo, 'table', Art_Category, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Art_Category, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改人', 'user', dbo, 'table', Art_Category, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'user', dbo, 'table', Art_Category, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	文章表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Article
(            
        Id	varchar(64)	not null,
        Title	varchar(100),
        Content	text,
        Cate_Id	varchar(64),
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Article
       add constraint PK_Article_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '文章表', 'user', dbo, 'table', Article, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '文章编号', 'user', dbo, 'table', Article, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '文章标题', 'user', dbo, 'table', Article, 'column', Title;
EXEC sp_addextendedproperty 'MS_Description', '文章内容', 'user', dbo, 'table', Article, 'column', Content;
EXEC sp_addextendedproperty 'MS_Description', '文章类型编号', 'user', dbo, 'table', Article, 'column', Cate_Id;
EXEC sp_addextendedproperty 'MS_Description', '创建人', 'user', dbo, 'table', Article, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Article, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改人', 'user', dbo, 'table', Article, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'user', dbo, 'table', Article, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	银行卡操作记录表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Bank_Card
(            
        Id	varchar(64)	not null,
        TIME	date,
        Price	float,
        Save_Type	numeric,
        Balance	float,
        Bank_Type	numeric,
        Note	varchar(200),
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Bank_Card
       add constraint PK_Bank_Card_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '银行卡操作记录表', 'user', dbo, 'table', Bank_Card, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Bank_Card, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '操作时间', 'user', dbo, 'table', Bank_Card, 'column', TIME;
EXEC sp_addextendedproperty 'MS_Description', '操作金额', 'user', dbo, 'table', Bank_Card, 'column', Price;
EXEC sp_addextendedproperty 'MS_Description', '操作类型 操作类型 表示存入还是取出，从字典表中获取', 'user', dbo, 'table', Bank_Card, 'column', Save_Type;
EXEC sp_addextendedproperty 'MS_Description', '余额', 'user', dbo, 'table', Bank_Card, 'column', Balance;
EXEC sp_addextendedproperty 'MS_Description', '银行卡名称 银行卡名称 表示操作的银行卡的名称，从字典表中获得', 'user', dbo, 'table', Bank_Card, 'column', Bank_Type;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Bank_Card, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '创建者', 'user', dbo, 'table', Bank_Card, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间 创建时间 系统当前默认时间', 'user', dbo, 'table', Bank_Card, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改者', 'user', dbo, 'table', Bank_Card, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间 修改时间 修改时候的系统默认时间', 'user', dbo, 'table', Bank_Card, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	银行卡日志记录表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Bank_Card_log
(            
        Id	varchar(64)	not null,
        Bank_Id	varchar(64),
        TIME	date,
        Price	float,
        Save_Type	varchar(20),
        Balance	float,
        Bank_Type	varchar(40),
        Note	varchar(200),
        Create_By	varchar(64),
        Create_Time	datetime,
        Ope_Type	varchar(10)
);

EXEC sp_addextendedproperty 'MS_Description', '银行卡日志记录表', 'user', dbo, 'table', Bank_Card_log, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Bank_Card_log, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '银行卡编号', 'user', dbo, 'table', Bank_Card_log, 'column', Bank_Id;
EXEC sp_addextendedproperty 'MS_Description', '操作时间', 'user', dbo, 'table', Bank_Card_log, 'column', TIME;
EXEC sp_addextendedproperty 'MS_Description', '操作金额', 'user', dbo, 'table', Bank_Card_log, 'column', Price;
EXEC sp_addextendedproperty 'MS_Description', '操作类型 存入/取出', 'user', dbo, 'table', Bank_Card_log, 'column', Save_Type;
EXEC sp_addextendedproperty 'MS_Description', '余额', 'user', dbo, 'table', Bank_Card_log, 'column', Balance;
EXEC sp_addextendedproperty 'MS_Description', '银行卡名称', 'user', dbo, 'table', Bank_Card_log, 'column', Bank_Type;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Bank_Card_log, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '创建人编号', 'user', dbo, 'table', Bank_Card_log, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Bank_Card_log, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '操作类型 修改/删除', 'user', dbo, 'table', Bank_Card_log, 'column', Ope_Type;
/*  =======================================================================================
 *  Object Name		:	字典表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Diction
(            
        Id	numeric	not null,
        Name	varchar(20),
        Note	varchar(200),
        Parent_Id	numeric,
        Order_Id	numeric,
        Create_By	varchar(20),
        Create_Time	datetime,
        Update_By	varchar(20),
        Update_Time	datetime
);
alter  table Diction
       add constraint PK_Diction_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '字典表', 'user', dbo, 'table', Diction, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号 编号 10位数的编号', 'user', dbo, 'table', Diction, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '字典名称', 'user', dbo, 'table', Diction, 'column', Name;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Diction, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '父级编号 父级编号 引用主键', 'user', dbo, 'table', Diction, 'column', Parent_Id;
EXEC sp_addextendedproperty 'MS_Description', '序号', 'user', dbo, 'table', Diction, 'column', Order_Id;
EXEC sp_addextendedproperty 'MS_Description', '创建者', 'user', dbo, 'table', Diction, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Diction, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改者', 'user', dbo, 'table', Diction, 'column', Update_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'user', dbo, 'table', Diction, 'column', Update_Time;
/*  =======================================================================================
 *  Object Name		:	收入记录表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Income
(            
        Id	varchar(64)	not null,
        TIME	date,
        Price	float,
        Note	varchar(200),
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Income
       add constraint PK_Income_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '收入记录表', 'user', dbo, 'table', Income, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Income, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '操作时间', 'user', dbo, 'table', Income, 'column', TIME;
EXEC sp_addextendedproperty 'MS_Description', '操作金额', 'user', dbo, 'table', Income, 'column', Price;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Income, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '创建者', 'user', dbo, 'table', Income, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间 创建时间 创建时间 系统当前默认时间', 'user', dbo, 'table', Income, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改者', 'user', dbo, 'table', Income, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间 修改时间 修改时间 修改时候的系统默认时间', 'user', dbo, 'table', Income, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	生活费操作管理
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Lifing_Cost
(            
        Id	varchar(64)	not null,
        TIME	date,
        Reason	varchar(200),
        Price	float,
        Cost_Type_Id	numeric,
        Notes	varchar(200),
        Img_Url	varchar(50),
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Lifing_Cost
       add constraint PK_Lifing_Cost_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '生活费操作管理', 'user', dbo, 'table', Lifing_Cost, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Lifing_Cost, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '消费时间', 'user', dbo, 'table', Lifing_Cost, 'column', TIME;
EXEC sp_addextendedproperty 'MS_Description', '消费名称', 'user', dbo, 'table', Lifing_Cost, 'column', Reason;
EXEC sp_addextendedproperty 'MS_Description', '消费金额', 'user', dbo, 'table', Lifing_Cost, 'column', Price;
EXEC sp_addextendedproperty 'MS_Description', '消费类型', 'user', dbo, 'table', Lifing_Cost, 'column', Cost_Type_Id;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Lifing_Cost, 'column', Notes;
EXEC sp_addextendedproperty 'MS_Description', '图片路径', 'user', dbo, 'table', Lifing_Cost, 'column', Img_Url;
EXEC sp_addextendedproperty 'MS_Description', '创建者', 'user', dbo, 'table', Lifing_Cost, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Lifing_Cost, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改者', 'user', dbo, 'table', Lifing_Cost, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'user', dbo, 'table', Lifing_Cost, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	生活费日志记录表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Lifing_Cost_log
(            
        Id	varchar(64)	not null,
        Cost_Id	varchar(64),
        TIME	date,
        Reason	varchar(200),
        Price	float,
        Notes	varchar(200),
        Cost_Type_Name	varchar(50),
        Create_By	varchar(64),
        Create_Time	datetime,
        Ope_Type	varchar(64)
);
alter  table Lifing_Cost_log
       add constraint PK_Lifing_Cost_log_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '生活费日志记录表', 'user', dbo, 'table', Lifing_Cost_log, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Lifing_Cost_log, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '消费编号', 'user', dbo, 'table', Lifing_Cost_log, 'column', Cost_Id;
EXEC sp_addextendedproperty 'MS_Description', '消费时间', 'user', dbo, 'table', Lifing_Cost_log, 'column', TIME;
EXEC sp_addextendedproperty 'MS_Description', '消费名称', 'user', dbo, 'table', Lifing_Cost_log, 'column', Reason;
EXEC sp_addextendedproperty 'MS_Description', '消费金额', 'user', dbo, 'table', Lifing_Cost_log, 'column', Price;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Lifing_Cost_log, 'column', Notes;
EXEC sp_addextendedproperty 'MS_Description', '消费类型名称', 'user', dbo, 'table', Lifing_Cost_log, 'column', Cost_Type_Name;
EXEC sp_addextendedproperty 'MS_Description', '操作人编号', 'user', dbo, 'table', Lifing_Cost_log, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '操作时间', 'user', dbo, 'table', Lifing_Cost_log, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '操作类型 修改/删除', 'user', dbo, 'table', Lifing_Cost_log, 'column', Ope_Type;
/*  =======================================================================================
 *  Object Name		:	模块表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Module
(            
        Module_ID	varchar(64)	not null,
        Module_Name	varchar(20),
        Module_URL	varchar(50),
        Icon_Url	varchar(200),
        Parent_Id	varchar(64),
        Order_Id	numeric,
        Notes	varchar(1000),
        STATUS	numeric
);
alter  table Module
       add constraint PK_Module_ID primary key (Module_ID);

EXEC sp_addextendedproperty 'MS_Description', '模块表', 'user', dbo, 'table', Module, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '模块编号', 'user', dbo, 'table', Module, 'column', Module_ID;
EXEC sp_addextendedproperty 'MS_Description', '模块名称', 'user', dbo, 'table', Module, 'column', Module_Name;
EXEC sp_addextendedproperty 'MS_Description', '模块路径', 'user', dbo, 'table', Module, 'column', Module_URL;
EXEC sp_addextendedproperty 'MS_Description', '图标路径', 'user', dbo, 'table', Module, 'column', Icon_Url;
EXEC sp_addextendedproperty 'MS_Description', '父级模块', 'user', dbo, 'table', Module, 'column', Parent_Id;
EXEC sp_addextendedproperty 'MS_Description', '序号', 'user', dbo, 'table', Module, 'column', Order_Id;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Module, 'column', Notes;
EXEC sp_addextendedproperty 'MS_Description', '状态 状态 1可用，0不可用', 'user', dbo, 'table', Module, 'column', STATUS;
/*  =======================================================================================
 *  Object Name		:	角色模块对应表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Role_To_Module
(            
        Id	varchar(64)	not null,
        Role_Id	varchar(64),
        Module_Id	varchar(64)
);
alter  table Role_To_Module
       add constraint PK_Role_To_Module_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '角色模块对应表', 'user', dbo, 'table', Role_To_Module, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '角色模块编号', 'user', dbo, 'table', Role_To_Module, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '角色编号', 'user', dbo, 'table', Role_To_Module, 'column', Role_Id;
EXEC sp_addextendedproperty 'MS_Description', '模块编号', 'user', dbo, 'table', Role_To_Module, 'column', Module_Id;
/*  =======================================================================================
 *  Object Name		:	角色表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Roles
(            
        Role_Id	varchar(64)	not null,
        Role_Name	varchar(20),
        Notes	varchar(100)
);
alter  table Roles
       add constraint PK_Roles_Role_Id primary key (Role_Id);

EXEC sp_addextendedproperty 'MS_Description', '角色表', 'user', dbo, 'table', Roles, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '角色编号', 'user', dbo, 'table', Roles, 'column', Role_Id;
EXEC sp_addextendedproperty 'MS_Description', '角色名称', 'user', dbo, 'table', Roles, 'column', Role_Name;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Roles, 'column', Notes;
/*  =======================================================================================
 *  Object Name		:	系统配置
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Sys_Config
(            
        Id	varchar(20)	not null,
        Name	varchar(50),
        Sys_Value	varchar(100),
        Remark	varchar(512),
        Group_No	varchar(50),
        Is_Visible	bit,
        Order_Id	int,
        Create_By	varchar(64),
        Create_Time	datetime,
        UpDate_By	varchar(64),
        UpDate_Time	datetime
);
alter  table Sys_Config
       add constraint PK_Sys_Config_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '系统配置', 'user', dbo, 'table', Sys_Config, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '唯一标识', 'user', dbo, 'table', Sys_Config, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '名称', 'user', dbo, 'table', Sys_Config, 'column', Name;
EXEC sp_addextendedproperty 'MS_Description', '值', 'user', dbo, 'table', Sys_Config, 'column', Sys_Value;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Sys_Config, 'column', Remark;
EXEC sp_addextendedproperty 'MS_Description', '分组标识', 'user', dbo, 'table', Sys_Config, 'column', Group_No;
EXEC sp_addextendedproperty 'MS_Description', '是否显示', 'user', dbo, 'table', Sys_Config, 'column', Is_Visible;
EXEC sp_addextendedproperty 'MS_Description', '排序序号', 'user', dbo, 'table', Sys_Config, 'column', Order_Id;
EXEC sp_addextendedproperty 'MS_Description', '创建人编号', 'user', dbo, 'table', Sys_Config, 'column', Create_By;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Sys_Config, 'column', Create_Time;
EXEC sp_addextendedproperty 'MS_Description', '修改人编号', 'user', dbo, 'table', Sys_Config, 'column', UpDate_By;
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'user', dbo, 'table', Sys_Config, 'column', UpDate_Time;
/*  =======================================================================================
 *  Object Name		:	临时信息存储表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Temp_Data
(            
        Id	varchar(64)	not null,
        Email	varchar(64),
        Expires	datetime,
        Create_Time	datetime
);
alter  table Temp_Data
       add constraint PK_TempData_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '临时信息存储表', 'user', dbo, 'table', Temp_Data, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Temp_Data, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '邮件地址', 'user', dbo, 'table', Temp_Data, 'column', Email;
EXEC sp_addextendedproperty 'MS_Description', '过期时间', 'user', dbo, 'table', Temp_Data, 'column', Expires;
EXEC sp_addextendedproperty 'MS_Description', '创建时间', 'user', dbo, 'table', Temp_Data, 'column', Create_Time;
/*  =======================================================================================
 *  Object Name		:	用户角色对应表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  User_To_Role
(            
        Id	varchar(64)	not null,
        User_Id	varchar(64),
        Role_Id	varchar(64)
);
alter  table User_To_Role
       add constraint PK_User_To_Role_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '用户角色对应表', 'user', dbo, 'table', User_To_Role, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '用户角色编号', 'user', dbo, 'table', User_To_Role, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '用户编号', 'user', dbo, 'table', User_To_Role, 'column', User_Id;
EXEC sp_addextendedproperty 'MS_Description', '角色编号', 'user', dbo, 'table', User_To_Role, 'column', Role_Id;
/*  =======================================================================================
 *  Object Name		:	用户表
 *  Storage Version	:	1.0.0.0
 *  Abstract		:	
 *  ---------------------------------------------------------------------------------------
 *	Output			:	
 * ======================================================================================== */
create table  Users
(            
        Id	varchar(64)	not null,
        Login_Id	varchar(20),
        Login_Pwd	varchar(64),
        Name	varchar(20),
        Phone	varchar(20),
        Mail	varchar(50),
        Address	varchar(100),
        Age	numeric,
        Notes	varchar(1000)
);
alter  table Users
       add constraint PK_Users_Id primary key (Id);

EXEC sp_addextendedproperty 'MS_Description', '用户表', 'user', dbo, 'table', Users, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '编号', 'user', dbo, 'table', Users, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '登录名', 'user', dbo, 'table', Users, 'column', Login_Id;
EXEC sp_addextendedproperty 'MS_Description', '密码', 'user', dbo, 'table', Users, 'column', Login_Pwd;
EXEC sp_addextendedproperty 'MS_Description', '真实姓名', 'user', dbo, 'table', Users, 'column', Name;
EXEC sp_addextendedproperty 'MS_Description', '电话', 'user', dbo, 'table', Users, 'column', Phone;
EXEC sp_addextendedproperty 'MS_Description', '邮件', 'user', dbo, 'table', Users, 'column', Mail;
EXEC sp_addextendedproperty 'MS_Description', '地址', 'user', dbo, 'table', Users, 'column', Address;
EXEC sp_addextendedproperty 'MS_Description', '年龄', 'user', dbo, 'table', Users, 'column', Age;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'user', dbo, 'table', Users, 'column', Notes;

/*创建外键*/


alter  table Article
       add constraint FK_Article_Cate_Id foreign key (Cate_Id)
       references Art_Category(Cat_Id);

alter  table Bank_Card
       add constraint FK_BankCard_Bank_Type foreign key (Bank_Type)
       references Diction(Id);
alter  table Bank_Card
       add constraint FK_BankCard_Save_Type foreign key (Save_Type)
       references Diction(Id);




alter  table Lifing_Cost
       add constraint FK_Lifing_Cost_Cost_Type_Id foreign key (Cost_Type_Id)
       references Diction(Id);



alter  table Role_To_Module
       add constraint FK_Role_To_Module_Module_Id foreign key (Module_Id)
       references Module(Module_ID);
alter  table Role_To_Module
       add constraint FK_Role_To_Module_Role_Id foreign key (Role_Id)
       references Roles(Role_Id);




alter  table User_To_Role
       add constraint FK_User_To_Role_Role_Id foreign key (Role_Id)
       references Roles(Role_Id);
alter  table User_To_Role
       add constraint FK_User_To_Role_User_Id foreign key (User_Id)
       references Users(Id);

go


--说明：部分主外键关系需要手动增加联合删除功能
alter  table Role_To_Module
       drop constraint FK_Role_To_Module_Module_Id;
alter  table Role_To_Module
       drop constraint FK_Role_To_Module_Role_Id;
alter  table User_To_Role
       drop constraint FK_User_To_Role_Role_Id;
alter  table User_To_Role
       drop constraint FK_User_To_Role_User_Id;
    
alter  table Role_To_Module
       add constraint FK_Role_To_Module_Module_Id foreign key (Module_Id)
       references Module(Module_ID) on delete cascade on update cascade;
alter  table Role_To_Module
       add constraint FK_Role_To_Module_Role_Id foreign key (Role_Id)
       references Roles(Role_Id) on delete cascade on update cascade;
alter  table User_To_Role
       add constraint FK_User_To_Role_Role_Id foreign key (Role_Id)
       references Roles(Role_Id) on delete cascade on update cascade;
alter  table User_To_Role
       add constraint FK_User_To_Role_User_Id foreign key (User_Id)
       references Users(Id) on delete cascade on update cascade;
GO

/**************************初始化数据***********************************/
--用户
INSERT [dbo].[Users] ([id], [login_Id], [login_Pwd], [name], [phone], [mail], [address], [age], [notes]) VALUES (N'-1', N'admin', N'21232F297A57A5A743894A0E4A801F', N'超级管理员', N'13628497041', N'', N'', CAST(0 AS Numeric(4, 0)), N'')
--角色
insert into Roles(Role_Id,Role_Name,notes) values('-1','超级管理员','超级管理员')
--用户角色对应表数据
insert into User_To_Role(Id,User_Id,Role_Id) values(NEWID(),'-1','-1')

--模块数据
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('0a88c27f-6929-4222-a57d-6a4a98bd8921','纯收入管理','/Income/Index','/Content/Images/icon16/workflow_list.gif','4f5bda73-760c-40d6-81d1-5903a4b78bed','5','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('1b8ff575-6656-4fe9-ad7f-1de651d68b0e','文章管理','','/Content/Images/icon16/Report.gif','0','3','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('25a56f6e-45c8-43ba-b167-11578bc46d4e','字典管理','/Diction/Index','/Content/Images/icon16/open.gif','adea464a-196b-4661-9dff-ea86ca474e46','4','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('2d31fd30-8a2a-4e33-af11-1fd840841804','银行卡管理','/BankCard/Index','/Content/Images/icon16/PageRefresh.gif','4f5bda73-760c-40d6-81d1-5903a4b78bed','3','Life.LifingCost.LifingCostLayout','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('33263bba-5442-427f-897f-07da11e1fea5','银行卡汇总','/BankCard/CollectionByMonth','/Content/Images/icon16/GanttChart.gif','4f5bda73-760c-40d6-81d1-5903a4b78bed','4','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('37bd81fe-703b-4c5f-8f0c-0a15ee22696e','角色模块管理','/RoleToModule/Index','/Content/Images/icon16/check.gif','adea464a-196b-4661-9dff-ea86ca474e46','5','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('384447eb-d38c-4668-b106-afbbb71c50e0','生活费汇总','/LifingCost/CollectionByMonth','/Content/Images/icon16/email_Logo.gif','4f5bda73-760c-40d6-81d1-5903a4b78bed','2','Life.LifingCost.LifingCostLayout','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('3a61ecbc-ce6c-4f10-a3f4-5ea2873d9ccf','生活费查询','/LifingCost/Search','/Content/Images/icon16/mytable.gif','4f5bda73-760c-40d6-81d1-5903a4b78bed','5','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('4f5bda73-760c-40d6-81d1-5903a4b78bed','生活管理','','/Content/Images/icon16/sortDir.gif','0','2','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('633814c0-1b20-4495-ab72-443c25556bd3','文章信息管理','/Article/Index','/Content/Images/icon16/customer.gif','1b8ff575-6656-4fe9-ad7f-1de651d68b0e','1','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('657cc10c-4f88-4653-841c-82582a84308a','文章类别管理','/ArtCategory/Index','/Content/Images/icon16/grzm.gif','1b8ff575-6656-4fe9-ad7f-1de651d68b0e','2','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('81445e5b-a11f-4f6c-8702-467d399e804a','生活费管理','/LifingCost/Index','/Content/Images/icon16/xmgl.png','4f5bda73-760c-40d6-81d1-5903a4b78bed','1','Life.LifingCost.LifingCostLayout','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('a6485558-2d58-48ad-93bd-bfa57e002c5c','用户管理','/Users/Index','/Content/Images/icon16/user.gif','adea464a-196b-4661-9dff-ea86ca474e46','3','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('a7368090-eead-4ae2-a9f1-305cbd53cfeb','角色管理','/Roles/Index','/Content/Images/icon16/xyj.gif','adea464a-196b-4661-9dff-ea86ca474e46','2','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('adea464a-196b-4661-9dff-ea86ca474e46','系统管理','','/Content/Images/icon16/ToExecl.gif','0','3','','1');
insert into dbo.Module(Module_ID,Module_Name,Module_URL,Icon_Url,Parent_Id,Order_Id,Notes,STATUS) values('b2698914-1571-4e7d-8b2b-035a10e3978c','模块管理','/Module/Index','/Content/Images/icon16/zygl.jpg','adea464a-196b-4661-9dff-ea86ca474e46','1','','1');

GO

--字典信息
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000100000','银行卡名称','银行卡名称','0','1','sys','2012/6/2 11:04:17','sys','2012/6/2 11:04:17');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000100001','中国建设银行','361.78','1000100000','2','sys','2012/6/2 11:04:17','sys','2012/6/2 11:04:17');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000100002','中国邮政储蓄银行','0.00','1000100000','3','sys','2012/6/2 11:04:17','sys','2012/6/2 11:04:17');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000100003','重庆银行','14314.93','1000100000','1','段江涛','2012/6/21 21:01:44','朱荣春','2013/8/12 20:53:16');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000100004','余额宝','7225.49','1000100000','4','段江涛','2013/12/8 0:00:00','段江涛','2013/12/8 22:12:27');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000200000','操作类型','操作类型','0','1','sys','2012/6/6 21:17:08','sys','2012/6/6 21:17:08');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000200001','存入','操作类型','1000200000','1','sys','2012/6/6 21:17:40','sys','2012/6/6 21:17:40');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000200002','取出','操作类型','1000200000','1','sys','2012/6/6 21:17:40','sys','2012/6/6 21:17:40');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300000','消费类型','消费类型','0','1','sys','2012/10/13 20:40:30','sys','2012/10/13 20:40:30');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300001','生活费','生活费','1000300000','1','超级管理员','2012/10/13 20:41:07','超级管理员','2012/10/13 20:41:07');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300002','车费','车费','1000300000','2','超级管理员','2012/10/13 20:41:26','超级管理员','2012/10/13 20:41:26');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300003','服装费','服装费','1000300000','3','超级管理员','2012/10/13 20:43:17','超级管理员','2012/10/13 20:43:17');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300004','医药费','','1000300000','4','段江涛','2012/10/24 21:06:28','段江涛','2012/10/24 21:06:28');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300005','生活用品','','1000300000','5','段江涛','2012/10/24 21:09:29','段江涛','2012/10/24 21:09:29');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300006','水果零食','','1000300000','6','朱荣春','2012/10/26 20:51:33','朱荣春','2012/10/26 20:51:33');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300007','礼节消费','','1000300000','7','段江涛','2012/10/28 10:11:25','段江涛','2012/10/28 10:11:25');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300008','出差消费','','1000300000','8','段江涛','2012/10/31 21:36:44','段江涛','2012/10/31 21:36:44');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300009','电话费网费','','1000300000','9','段江涛','2012/11/5 22:09:01','段江涛','2012/11/5 22:09:01');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300010','其他','','1000300000','9','段江涛','2012/11/11 9:08:59','段江涛','2012/11/11 9:08:59');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300011','房租','','1000300000','7','段江涛','2012/12/7 20:56:28','段江涛','2012/12/7 20:56:28');
insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('1000300012','水电费','','1000300000','5','段江涛','2012/12/22 20:40:26','段江涛','2012/12/22 20:40:26');

GO



create view V_Art_Category
as
select a.*,b.Name Create_Name,c.Name Update_Name from Art_Category a
inner join Users b on a.Create_By=b.Id
inner join Users c on a.UpDate_By=c.Id
GO



--查询银行卡信息
CREATE VIEW V_BANK_CARD
AS
SELECT A.*,B.NAME BANK_TYPE_NAME,C.NAME SAVE_NAME,D.NAME CREATE_NAME,E.NAME UPDATE_NAME FROM BANK_CARD A 
INNER JOIN DICTION B ON A.BANK_TYPE=B.ID
INNER JOIN DICTION C ON A.SAVE_TYPE=C.ID
INNER JOIN USERS D ON A.CREATE_BY=D.ID
INNER JOIN USERS E ON A.UPDATE_BY=E.ID





GO
create view V_Income
as
select a.*,b.Name Create_Name,c.Name Update_Name from Income a inner join Users b on a.Create_By=b.Id
left join Users c on a.UpDate_By=c.Id

GO
CREATE VIEW V_LIFING_COST
AS
SELECT A.*,B.NAME COST_TYPE_NAME,C.NAME CREATE_NAME,D.NAME UPDATE_NAME 
FROM LIFING_COST A 
INNER JOIN DICTION B ON A.COST_TYPE_ID=B.ID
INNER JOIN USERS C ON A.CREATE_BY=C.ID 
INNER JOIN USERS D ON A.UPDATE_BY=D.ID



GO


CREATE proc [dbo].[proc_calcAllBalance]
as
select * into newTab from Bank_Card order by TIME,Create_Time
alter table bank_card disable trigger tri_calcBalance

DECLARE @id NVARCHAR(64);
DECLARE @time date;
DECLARE @save_type NVARCHAR(64);
DECLARE @bank_type NVARCHAR(64);
declare @price money;

DECLARE MYCUR CURSOR FOR
(select id,time,save_type,bank_type,price from newTab)--声明游标

--清空余额
update Diction set Note='0' where  Parent_Id='1000100000'

OPEN MYCUR--打开游标
FETCH NEXT FROM MYCUR INTO @id,@time,@save_type,@bank_type,@price
WHILE @@FETCH_STATUS = 0--当SQLCODE值为0时表明一切正常,100表示已经取到了结果集的末尾
BEGIN
	--1.获得余额
	declare @calcPrice money;
	select @calcPrice=CONVERT(money,Note) from Diction where id=@bank_type
	
	--2.修改余额
	if(@save_type='1000200002')	
		begin
			set @calcPrice=@calcPrice-@price
			update Diction set Note=@calcPrice where id=@bank_type
			update Bank_Card set Balance=@calcPrice where id=@id
		end
	else
		begin
			set @calcPrice=@calcPrice+@price
			update Diction set Note=@calcPrice where id=@bank_type
			update Bank_Card set Balance=@calcPrice where id=@id
		end
	FETCH NEXT FROM MYCUR INTO @id,@time,@save_type,@bank_type,@price
END
CLOSE MYCUR--关闭游标
DEALLOCATE MYCUR--删除游标资源

drop table newTab
alter table bank_card enable trigger tri_calcBalance

GO


/*分页存储过程*/
CREATE PROC [DBO].[PROC_SELECTPAGE]
(
@PRIKEY VARCHAR(64),--主键
@FIELDS VARCHAR(1024),--要查询的列
@ORDERBY VARCHAR(64),--按哪一列排序
@ORDERBYTYPE VARCHAR(64),--排序类型，ASC或者DESC
@TABLE VARCHAR(1024),--查询哪一张表
@SQLWHERE VARCHAR(64)='',--查询条件
@PAGESIZE INT,--每页显示几条数据
@PAGEINDEX INT, --当前是第几页
@COUNT INT OUTPUT--总共多少条数据
)
AS
DECLARE @SQL NVARCHAR(MAX),--存储SQL语句
  @SQL1 NVARCHAR(MAX),
  @CON INT
SET @SQL='SELECT TOP '+CONVERT(VARCHAR(10),@PAGESIZE)+' '+@FIELDS+' FROM '+@TABLE+' '+@SQLWHERE+' AND '+@PRIKEY+' NOT IN
(SELECT TOP ' +CONVERT(VARCHAR(10),@PAGESIZE*(@PAGEINDEX-1))+' '+@PRIKEY+' FROM '+@TABLE+' '+@SQLWHERE+' ORDER BY '+@ORDERBY+' '+@ORDERBYTYPE+' )
 ORDER BY '+@ORDERBY+' '+@ORDERBYTYPE
 EXEC(@SQL)
 
SET @SQL='SELECT @COUNT=COUNT(*) FROM '+@TABLE+' '+@SQLWHERE
EXEC SP_EXECUTESQL @SQL,
N'@COUNT INT OUTPUT',
@COUNT OUTPUT


GO

--创建计算余额的触发器
CREATE trigger [dbo].[tri_calcBalance]
--增加数据后自动计算余额
on [dbo].[Bank_Card]
for insert,delete
as
begin
	--定义变量
	declare @balance money,
	@bank_Type varchar(40),
	@id varchar(64),
	@save_Type varchar(20),
	@price money	
	
	if not exists(select 1 from deleted)
		begin
			print('add')
			--获取新增的数据
			select @Bank_Type=Bank_Type,@id=id,@save_Type=save_Type,@price=price from inserted;
			--获取以前的余额
			select @balance=isnull(CONVERT(money,Note),0) from Diction where id=@bank_Type
			--修改余额			
			if(@save_Type='1000200001')
			  begin
				update Bank_Card set balance=@balance+@price where id=@id;
				update Diction set Note=@balance+@price where id=@bank_Type
			  end
			else
			  begin				
				update Bank_Card set balance=@balance-@price where id=@id;
				update Diction set Note=@balance-@price where id=@bank_Type
			  end
		end
	else
		begin			
			--获取删除的数据
			select @Bank_Type=Bank_Type,@id=id,@save_Type=save_Type,@price=price from deleted;
			--获取以前的余额
			select @balance=isnull(CONVERT(money,Note),0) from Diction where id=@bank_Type
			--修改余额
			if(@save_Type='1000200001')--新增
			  begin				
				update Diction set Note=@balance-@price where id=@bank_Type
			  end
			else
			  begin
				update Diction set Note=@balance+@price where id=@bank_Type
			  end
		end
end


/*获得字典表数据
public string GetDiction()
{
    string result="";
    SqlConnection conn=new SqlConnection(this.DataBase.ConnectionString);
    SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.Diction",conn);
    DataSet set = new DataSet();
    adapter.Fill(set);
    DataTable dt= set.Tables[0];
    for(int i=0;i<dt.Rows.Count;i++)
    {
        result+=string.Format("insert into dbo.Diction(Id,Name,Note,Parent_Id,Order_Id,Create_By,Create_Time,Update_By,Update_Time) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');\n",dt.Rows[i]["Id"],dt.Rows[i]["Name"],dt.Rows[i]["Note"],dt.Rows[i]["Parent_Id"],dt.Rows[i]["Order_Id"],dt.Rows[i]["Create_By"],dt.Rows[i]["Create_Time"],dt.Rows[i]["Update_By"],dt.Rows[i]["Update_Time"]);
    }
    return result;
}
*/
