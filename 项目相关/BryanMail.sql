/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/12/8 20:24:32                           */
/*==============================================================*/


drop table if exists Log_Admin;

drop table if exists Log_Login;

drop table if exists Sys_AdminMenu;

drop table if exists Sys_AdminPermission;

drop table if exists Sys_AdminRole;

drop index Idx_code on Sys_Area;

drop table if exists Sys_Area;

drop table if exists Sys_Option;

drop table if exists Sys_UploadFile;

drop table if exists Sys_User;

/*==============================================================*/
/* Table: Log_Admin                                             */
/*==============================================================*/
create table Log_Admin
(
   Id                   int(11) not null auto_increment,
   TypeId               int(4) not null comment '��־����',
   CrtUserId            int(11) not null comment '������ID',
   CrtUserName          varchar(20) not null comment '�������˺�',
   OtherId              bigint(20) not null default 0 comment 'Ӱ�����ݵ�ID',
   Remark               varchar(255) character set utf8 comment '����',
   Url                  varchar(1000) character set utf8 comment 'url',
   Ip                   varchar(30) not null comment 'ip',
   CrtDate              datetime not null default CURRENT_TIMESTAMP,
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��¼��̨������־'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Log_Login                                             */
/*==============================================================*/
create table Log_Login
(
   Id                   int(11) not null auto_increment,
   UserName             varchar(255) not null,
   TypeId               int(4) not null default 1 comment '1����̨,5���û�',
   UserId               int(11) default NULL,
   CrtDate              datetime not null default CURRENT_TIMESTAMP,
   Ip                   varchar(32) character set utf8,
   Content              varchar(30) character set utf8,
   primary key (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 107
AVG_ROW_LENGTH = 157
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��¼��̨��¼��־'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminMenu                                         */
/*==============================================================*/
create table Sys_AdminMenu
(
   Id                   int(11) not null,
   Pid                  int(11) not null comment '���˵�ID',
   Tag                  varchar(32) character set utf8 comment 'ͼ��',
   MenuName             varchar(32) character set utf8 comment 'ҳ����',
   Icon                 varchar(48) character set utf8 comment 'icon��ʶ',
   Level                tinyint(4) not null default 1 comment '�ȼ�',
   ChildNum             int(11) not null default 0 comment '�Ӳ˵�����',
   Orders               int(11) not null comment '������',
   IsShow               int(4) not null default 1 comment '�Ƿ���ʾ��0:��ʾ,1:���أ�',
   Url                  varchar(200) character set utf8 comment 'ҳ������',
   CrtUser              varchar(32) character set utf8 comment '������',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '����ʱ��',
   BtnJson              varchar(500) comment 'ҳ���а�ťJson',
   unique key UK_sys_adminMenu_id (Id)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��̨ϵͳ�˵�'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminPermission                                   */
/*==============================================================*/
create table Sys_AdminPermission
(
   Id                   int(11) not null auto_increment,
   MenuId               int(11) not null comment '�˵�ID',
   RoleId               int(11) not null comment '��ɫID',
   CrtDate              datetime not null default CURRENT_TIMESTAMP,
   CrtUser              varchar(32) character set utf8,
   BtnJson              varchar(500) comment '�˵���ťJson',
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��̨ϵͳȨ��'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminRole                                         */
/*==============================================================*/
create table Sys_AdminRole
(
   Id                   int(11) not null auto_increment,
   RoleName             varchar(16) character set utf8 not null comment '��ɫ����',
   Remark               varchar(32) character set utf8 not null comment '��ɫ����',
   CrtUser              varchar(16) character set utf8,
   CrtDate              datetime default CURRENT_TIMESTAMP,
   primary key (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��̨�û�Ȩ����'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_Area                                              */
/*==============================================================*/
create table Sys_Area
(
   Id                   int(11) not null comment '��ַ����Id',
   Pid                  int(11) not null comment '��ַ��Id',
   Code                 int(11) not null comment '��ַ����',
   Name                 varchar(20) not null comment '��ַ��',
   NameMerger           varchar(30) not null comment '��ַʡ����',
   NameShort            varchar(10) not null comment '��ַ���',
   NameShortMerger      varchar(30) not null comment '��ַʡ�������',
   levels               int(11) not null comment '����',
   CityCode             varchar(20) comment '�绰����',
   ZipCode              varchar(0) comment '�ʱ�',
   PinYin               varchar(50) comment '��ַƴ��',
   JianPin              varchar(10) comment '��ַ��ƴ',
   Letter               varchar(5),
   Lng                  varchar(50) comment '����',
   Lat                  varchar(50) comment 'γ��',
   Remark               varchar(50),
   primary key (Id)
);

alter table Sys_Area comment 'ȫ��ʡ����';

/*==============================================================*/
/* Index: Idx_code                                              */
/*==============================================================*/
create unique index Idx_code on Sys_Area
(
   
);

/*==============================================================*/
/* Table: Sys_Option                                            */
/*==============================================================*/
create table Sys_Option
(
   Id                   int(11) not null auto_increment,
   GroupName            varchar(30) character set utf8 not null comment '��������',
   GroupKey             varchar(30) character set utf8 not null comment '�����ؼ���',
   EnumCode             int(11) not null default 0 comment '��������',
   EnumName             varchar(50) character set utf8 not null comment '��������������',
   EnumLabel            varchar(50) character set utf8 comment '����������������',
   Remark               varchar(500) character set utf8 comment '����������������',
   Levels               int(11) not null default 1 comment '����',
   Orders               varchar(255) not null default '0' comment '����',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '����ʱ��',
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = 'ϵͳ������������'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_UploadFile                                        */
/*==============================================================*/
create table Sys_UploadFile
(
   Id                   int(11) not null auto_increment,
   TypeId               int(4) not null default 0 comment '�ļ�����',
   UserId               int(11) not null comment '�ϴ���Id',
   FileName             nvarchar(50) default null comment '�ļ���',
   FilePath             nvarchar(200) default null comment '�ϴ�·��',
   FileSize             int(11) not null comment '�ϴ��ļ��Ĵ�С',
   FileType             nvarchar(50) not null comment '�ϴ��ļ����',
   ImgWidth             int(11) default null comment 'ͼƬ���',
   ImgHeight            int(11) default null comment 'ͼƬ�߶�',
   Ip                   varchar(20) not null comment '�ϴ�ʱip',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '�ϴ�ʱ��',
   primary key (Id),
   key AK_Key_1 (Id)
);

alter table Sys_UploadFile comment '�ļ��ϴ���¼';

/*==============================================================*/
/* Table: Sys_User                                              */
/*==============================================================*/
create table Sys_User
(
   Id                   int(11) unsigned not null auto_increment,
   Status               int(4) not null default 1 comment '�û�״̬(1:������5:ע��)',
   UserName             varchar(50) character set utf8 comment '�˺�',
   RealName             varchar(10) character set utf8 comment '�û���ʵ����',
   Password             varchar(255) character set utf8 comment '����',
   LastIp               varchar(20) character set utf8 comment '����¼IP',
   RoleId               int(4) not null default 1 comment '��ɫID',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '����ʱ��',
   LastLogDate          datetime default NULL comment '����¼ʱ��',
   HeadImgUrl           varchar(200) character set utf8 comment 'ͷ��',
   Sex                  int(4) unsigned default 0 comment '�Ա�',
   Mobile               varchar(11) character set utf8 comment '��ϵ����',
   CrtUser              varchar(50) character set utf8 comment '������',
   primary key (Id),
   unique key userName (UserName)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '��̨ϵͳ����Ա'
ROW_FORMAT = DYNAMIC;

