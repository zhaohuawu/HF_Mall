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
   TypeId               int(4) not null comment '日志类型',
   CrtUserId            int(11) not null comment '创建人ID',
   CrtUserName          varchar(20) not null comment '创建人账号',
   OtherId              bigint(20) not null default 0 comment '影响数据的ID',
   Remark               varchar(255) character set utf8 comment '描述',
   Url                  varchar(1000) character set utf8 comment 'url',
   Ip                   varchar(30) not null comment 'ip',
   CrtDate              datetime not null default CURRENT_TIMESTAMP,
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '记录后台操作日志'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Log_Login                                             */
/*==============================================================*/
create table Log_Login
(
   Id                   int(11) not null auto_increment,
   UserName             varchar(255) not null,
   TypeId               int(4) not null default 1 comment '1：后台,5：用户',
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
COMMENT = '记录后台登录日志'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminMenu                                         */
/*==============================================================*/
create table Sys_AdminMenu
(
   Id                   int(11) not null,
   Pid                  int(11) not null comment '父菜单ID',
   Tag                  varchar(32) character set utf8 comment '图标',
   MenuName             varchar(32) character set utf8 comment '页面名',
   Icon                 varchar(48) character set utf8 comment 'icon标识',
   Level                tinyint(4) not null default 1 comment '等级',
   ChildNum             int(11) not null default 0 comment '子菜单数量',
   Orders               int(11) not null comment '左移量',
   IsShow               int(4) not null default 1 comment '是否显示（0:显示,1:隐藏）',
   Url                  varchar(200) character set utf8 comment '页面链接',
   CrtUser              varchar(32) character set utf8 comment '创建人',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '创建时间',
   BtnJson              varchar(500) comment '页面中按钮Json',
   unique key UK_sys_adminMenu_id (Id)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '后台系统菜单'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminPermission                                   */
/*==============================================================*/
create table Sys_AdminPermission
(
   Id                   int(11) not null auto_increment,
   MenuId               int(11) not null comment '菜单ID',
   RoleId               int(11) not null comment '角色ID',
   CrtDate              datetime not null default CURRENT_TIMESTAMP,
   CrtUser              varchar(32) character set utf8,
   BtnJson              varchar(500) comment '菜单按钮Json',
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '后台系统权限'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_AdminRole                                         */
/*==============================================================*/
create table Sys_AdminRole
(
   Id                   int(11) not null auto_increment,
   RoleName             varchar(16) character set utf8 not null comment '角色名称',
   Remark               varchar(32) character set utf8 not null comment '角色描述',
   CrtUser              varchar(16) character set utf8,
   CrtDate              datetime default CURRENT_TIMESTAMP,
   primary key (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '后台用户权限组'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_Area                                              */
/*==============================================================*/
create table Sys_Area
(
   Id                   int(11) not null comment '地址主键Id',
   Pid                  int(11) not null comment '地址父Id',
   Code                 int(11) not null comment '地址编码',
   Name                 varchar(20) not null comment '地址名',
   NameMerger           varchar(30) not null comment '地址省市区',
   NameShort            varchar(10) not null comment '地址简称',
   NameShortMerger      varchar(30) not null comment '地址省市区简称',
   levels               int(11) not null comment '级别',
   CityCode             varchar(20) comment '电话编码',
   ZipCode              varchar(0) comment '邮编',
   PinYin               varchar(50) comment '地址拼音',
   JianPin              varchar(10) comment '地址简拼',
   Letter               varchar(5),
   Lng                  varchar(50) comment '经度',
   Lat                  varchar(50) comment '纬度',
   Remark               varchar(50),
   primary key (Id)
);

alter table Sys_Area comment '全国省市区';

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
   GroupName            varchar(30) character set utf8 not null comment '参数名称',
   GroupKey             varchar(30) character set utf8 not null comment '参数关键词',
   EnumCode             int(11) not null default 0 comment '参数编码',
   EnumName             varchar(50) character set utf8 not null comment '参数编码类型名',
   EnumLabel            varchar(50) character set utf8 comment '参数编码类型描述',
   Remark               varchar(500) character set utf8 comment '参数编码类型描述',
   Levels               int(11) not null default 1 comment '级别',
   Orders               varchar(255) not null default '0' comment '排序',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '创建时间',
   primary key (Id)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '系统操作参数管理'
ROW_FORMAT = DYNAMIC;

/*==============================================================*/
/* Table: Sys_UploadFile                                        */
/*==============================================================*/
create table Sys_UploadFile
(
   Id                   int(11) not null auto_increment,
   TypeId               int(4) not null default 0 comment '文件类型',
   UserId               int(11) not null comment '上传者Id',
   FileName             nvarchar(50) default null comment '文件名',
   FilePath             nvarchar(200) default null comment '上传路径',
   FileSize             int(11) not null comment '上传文件的大小',
   FileType             nvarchar(50) not null comment '上传文件类别',
   ImgWidth             int(11) default null comment '图片宽度',
   ImgHeight            int(11) default null comment '图片高度',
   Ip                   varchar(20) not null comment '上传时ip',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '上传时间',
   primary key (Id),
   key AK_Key_1 (Id)
);

alter table Sys_UploadFile comment '文件上传记录';

/*==============================================================*/
/* Table: Sys_User                                              */
/*==============================================================*/
create table Sys_User
(
   Id                   int(11) unsigned not null auto_increment,
   Status               int(4) not null default 1 comment '用户状态(1:正常，5:注销)',
   UserName             varchar(50) character set utf8 comment '账号',
   RealName             varchar(10) character set utf8 comment '用户真实姓名',
   Password             varchar(255) character set utf8 comment '密码',
   LastIp               varchar(20) character set utf8 comment '最后登录IP',
   RoleId               int(4) not null default 1 comment '角色ID',
   CrtDate              datetime not null default CURRENT_TIMESTAMP comment '创建时间',
   LastLogDate          datetime default NULL comment '最后登录时间',
   HeadImgUrl           varchar(200) character set utf8 comment '头像',
   Sex                  int(4) unsigned default 0 comment '性别',
   Mobile               varchar(11) character set utf8 comment '联系号码',
   CrtUser              varchar(50) character set utf8 comment '创建人',
   primary key (Id),
   unique key userName (UserName)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = '后台系统管理员'
ROW_FORMAT = DYNAMIC;

