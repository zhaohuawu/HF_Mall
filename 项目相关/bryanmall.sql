/*
Navicat MySQL Data Transfer

Source Server         : tt
Source Server Version : 50624
Source Host           : 193.112.41.35:3306
Source Database       : bryanmail

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2019-02-21 18:02:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for gd_goods
-- ----------------------------
DROP TABLE IF EXISTS `gd_goods`;
CREATE TABLE `gd_goods` (
  `Id` varchar(32) NOT NULL COMMENT '商品主键（G+门店简称+商品类目简称+创建日期+时分秒+当天该门店增加的商品数量）',
  `Status` int(4) NOT NULL DEFAULT '1' COMMENT '商品状态：1暂存，2删除，3审核驳回，5审核中，10通过审核',
  `CategoryId` int(11) NOT NULL DEFAULT '0' COMMENT '类目Id（gd_goodscategory主键）',
  `SellerId` int(11) unsigned NOT NULL DEFAULT '1' COMMENT '卖家Id',
  `Title` varchar(100) NOT NULL COMMENT '商品标题',
  `Price` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '商品单价',
  `PriceMarket` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '市场价格（原价）',
  `PriceFreight` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '运费',
  `PriceFreightAdditional` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '偏远地区附加运费',
  `Stock` int(11) NOT NULL DEFAULT '0' COMMENT '商品库存',
  `SpecificationsNumber` int(11) NOT NULL DEFAULT '1' COMMENT '规格数量',
  `RemoteRegion` varchar(200) DEFAULT NULL COMMENT '偏远地区',
  `ImgSmall` varchar(255) NOT NULL COMMENT '图片缩略图',
  `Remark` varchar(255) DEFAULT NULL COMMENT '商品描述',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品信息';

-- ----------------------------
-- Records of gd_goods
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodsactivity
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsactivity`;
CREATE TABLE `gd_goodsactivity` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GoodsId` varchar(32) NOT NULL COMMENT '商品ID',
  `TypeId` int(4) NOT NULL DEFAULT '1' COMMENT '活动类型：1拼团，5秒杀',
  `ActivityId` int(11) NOT NULL DEFAULT '0' COMMENT '活动ID',
  `Price` decimal(10,4) NOT NULL DEFAULT '0.0000' COMMENT '活动价格',
  `Stock` int(11) NOT NULL DEFAULT '0' COMMENT '活动商品库存',
  `GroupMin` int(11) NOT NULL DEFAULT '0' COMMENT '拼团活动成团人数',
  `StartTime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '活动开始时间',
  `EndTime` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '活动结束时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品参加的活动';

-- ----------------------------
-- Records of gd_goodsactivity
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodscategory
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodscategory`;
CREATE TABLE `gd_goodscategory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pid` int(11) NOT NULL COMMENT '类目父Id',
  `Name` varchar(10) NOT NULL COMMENT '类目名称',
  `Level` int(4) NOT NULL COMMENT '类目级别',
  `Orders` int(11) NOT NULL COMMENT '排序',
  `Abbreviation` varchar(10) NOT NULL COMMENT '类目缩写',
  `IsShow` varchar(4) NOT NULL DEFAULT '1' COMMENT '是否商城显示',
  `ImgUrl` varchar(500) NOT NULL COMMENT '图片路径',
  `Remark` varchar(100) DEFAULT NULL COMMENT '备注',
  `CrtDate` datetime NOT NULL COMMENT '创建时间',
  `ModifyDate` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品类目';

-- ----------------------------
-- Records of gd_goodscategory
-- ----------------------------

-- ----------------------------
-- Table structure for gd_goodsimg
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsimg`;
CREATE TABLE `gd_goodsimg` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='商品图片';

-- ----------------------------
-- Records of gd_goodsimg
-- ----------------------------

-- ----------------------------
-- Table structure for log_admin
-- ----------------------------
DROP TABLE IF EXISTS `log_admin`;
CREATE TABLE `log_admin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TypeId` int(4) NOT NULL COMMENT '日志类型',
  `CrtUserId` int(11) NOT NULL COMMENT '创建人ID',
  `CrtUserName` varchar(20) NOT NULL COMMENT '创建人账号',
  `OtherId` bigint(20) NOT NULL DEFAULT '0' COMMENT '影响数据的ID',
  `Remark` varchar(255) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Url` varchar(1000) CHARACTER SET utf8 DEFAULT NULL COMMENT 'url',
  `Ip` varchar(30) NOT NULL COMMENT 'ip',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='记录后台操作日志';

-- ----------------------------
-- Records of log_admin
-- ----------------------------
INSERT INTO `log_admin` VALUES ('1', '10', '1', 'admin', '1', '账号：ceshi3注册成功', 'https://localhost:5001/api/Role/SysUser/AddSysUser', '::1', '2019-01-09 12:15:03');
INSERT INTO `log_admin` VALUES ('2', '10', '1', 'admin', '1', '账号：ceshi1注册成功', 'http://193.112.41.35:81/api/Role/SysUser/AddSysUser', '193.112.41.35', '2019-01-31 17:58:38');
INSERT INTO `log_admin` VALUES ('3', '0', '1', 'admin', '2', null, 'https://', '::1', '2019-02-19 22:20:46');
INSERT INTO `log_admin` VALUES ('4', '0', '1', 'admin', '2', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-19 22:26:58');
INSERT INTO `log_admin` VALUES ('5', '0', '1', 'admin', '3', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-19 22:27:52');
INSERT INTO `log_admin` VALUES ('6', '0', '1', 'admin', '4', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-19 22:28:45');
INSERT INTO `log_admin` VALUES ('7', '0', '1', 'admin', '5', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-19 22:28:57');
INSERT INTO `log_admin` VALUES ('8', '0', '1', 'admin', '1', null, 'https://', '::1', '2019-02-20 17:11:17');
INSERT INTO `log_admin` VALUES ('9', '0', '1', 'admin', '1', null, 'https://', '::1', '2019-02-20 17:21:05');
INSERT INTO `log_admin` VALUES ('10', '0', '1', 'admin', '1', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-20 17:51:05');
INSERT INTO `log_admin` VALUES ('11', '0', '1', 'admin', '1', null, 'https://localhost:5001/api/Role/SysRole/AddMenu', '::1', '2019-02-20 17:54:35');

-- ----------------------------
-- Table structure for log_login
-- ----------------------------
DROP TABLE IF EXISTS `log_login`;
CREATE TABLE `log_login` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `TypeId` int(4) NOT NULL DEFAULT '1' COMMENT '1：后台,5：用户',
  `UserId` int(11) DEFAULT NULL,
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Ip` varchar(32) CHARACTER SET utf8 DEFAULT NULL,
  `Content` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 AVG_ROW_LENGTH=157 ROW_FORMAT=DYNAMIC COMMENT='记录后台登录日志';

-- ----------------------------
-- Records of log_login
-- ----------------------------

-- ----------------------------
-- Table structure for sys_adminmenu
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminmenu`;
CREATE TABLE `sys_adminmenu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pid` int(11) NOT NULL COMMENT '父菜单ID',
  `Tag` varchar(32) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `MenuName` varchar(32) CHARACTER SET utf8 DEFAULT NULL COMMENT '页面名',
  `Icon` varchar(48) CHARACTER SET utf8 DEFAULT NULL COMMENT 'icon标识',
  `Level` int(4) NOT NULL DEFAULT '1' COMMENT '等级',
  `ChildNum` int(11) NOT NULL DEFAULT '0' COMMENT '子菜单数量',
  `Orders` int(11) NOT NULL COMMENT '左移量',
  `IsShow` int(4) NOT NULL DEFAULT '1' COMMENT '是否显示（0:显示,1:隐藏）',
  `Url` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '页面链接',
  `CrtUser` varchar(32) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `BtnJson` varchar(500) DEFAULT NULL COMMENT '页面中按钮Json[{"code":"btn","type":"button/uri","name":"添加","description":"按钮描述","isForbidden":0,,"url":"http://localhost/index"}]',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UK_sys_adminMenu_id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 AVG_ROW_LENGTH=16384 ROW_FORMAT=DYNAMIC COMMENT='后台系统菜单';

-- ----------------------------
-- Records of sys_adminmenu
-- ----------------------------
INSERT INTO `sys_adminmenu` VALUES ('1', '0', 'root', '根目录', '', '0', '0', '1', '1', '', 'admin', '2018-06-02 23:00:42', '');
INSERT INTO `sys_adminmenu` VALUES ('2', '1', 'sys', '系统配置', 'icon-001', '1', '0', '1', '1', 'http://www.baidu.com', 'admin', '2018-12-09 21:38:50', '');
INSERT INTO `sys_adminmenu` VALUES ('3', '2', 'sys:user', '用户管理', 'icon-1', '2', '0', '1', '1', 'http://www.baidu.com', 'admin', '2018-12-14 10:04:52', '');
INSERT INTO `sys_adminmenu` VALUES ('4', '2', 'sys:role', '角色管理', 'icon-1', '2', '0', '2', '1', 'http://www.baidu.com', 'admin', '2018-12-14 10:06:07', '');
INSERT INTO `sys_adminmenu` VALUES ('5', '2', 'sys:permission', '权限管理', 'icon-1', '2', '0', '3', '1', 'http://www.baidu.com', 'admin', '2018-12-14 10:07:55', '');
INSERT INTO `sys_adminmenu` VALUES ('6', '1', 'gd', '商品管理', 'product', '1', '0', '2', '1', '/pms/product', 'admin', '2019-02-20 17:11:16', null);
INSERT INTO `sys_adminmenu` VALUES ('7', '1', 'order', '订单管理', 'order', '1', '0', '3', '1', '/oms/order', 'admin', '2019-02-20 17:21:05', null);
INSERT INTO `sys_adminmenu` VALUES ('8', '6', 'gd:goodlist', '商品列表', 'product-list', '2', '0', '1', '1', '/pms/product/index', 'admin', '2019-02-20 17:51:05', null);
INSERT INTO `sys_adminmenu` VALUES ('9', '6', 'gd:addgood', '添加商品', 'product-add', '2', '0', '2', '1', '/pms/product/add', 'admin', '2019-02-20 17:54:35', null);

-- ----------------------------
-- Table structure for sys_adminmenubtn
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminmenubtn`;
CREATE TABLE `sys_adminmenubtn` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuId` int(11) NOT NULL COMMENT '菜单Id',
  `Code` varchar(20) NOT NULL COMMENT '按钮或资源编码',
  `Type` varchar(10) NOT NULL COMMENT '按钮或资源类型',
  `Name` varchar(20) NOT NULL COMMENT '按钮或资源名称',
  `Description` varchar(100) DEFAULT NULL COMMENT '描述',
  `Url` varchar(500) DEFAULT NULL COMMENT 'url或其他',
  `IsForbidden` int(2) NOT NULL DEFAULT '0' COMMENT '是否禁用，0:启用，1:禁用',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='页面按钮或资源';

-- ----------------------------
-- Records of sys_adminmenubtn
-- ----------------------------
INSERT INTO `sys_adminmenubtn` VALUES ('1', '3', 'adduser', 'button', '新增用户', null, null, '0');

-- ----------------------------
-- Table structure for sys_adminpermission
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminpermission`;
CREATE TABLE `sys_adminpermission` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuId` int(11) NOT NULL COMMENT '菜单ID',
  `RoleId` int(11) NOT NULL COMMENT '角色ID',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserId` int(11) DEFAULT NULL COMMENT '创建用户ID',
  `BtnJson` varchar(500) DEFAULT NULL COMMENT '菜单按钮Json',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='后台系统权限';

-- ----------------------------
-- Records of sys_adminpermission
-- ----------------------------
INSERT INTO `sys_adminpermission` VALUES ('1', '2', '1', '2018-12-21 17:21:54', '1', 'string');
INSERT INTO `sys_adminpermission` VALUES ('2', '3', '1', '2018-12-21 17:21:54', '1', '');

-- ----------------------------
-- Table structure for sys_adminrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminrole`;
CREATE TABLE `sys_adminrole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(16) CHARACTER SET utf8 NOT NULL COMMENT '角色名称',
  `Remark` varchar(32) CHARACTER SET utf8 NOT NULL COMMENT '角色描述',
  `UserId` int(11) NOT NULL COMMENT '创建用户ID',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsForbidden` int(4) NOT NULL DEFAULT '0' COMMENT '是否禁用，0：正常，1：禁用',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 AVG_ROW_LENGTH=8192 ROW_FORMAT=DYNAMIC COMMENT='后台用户权限组';

-- ----------------------------
-- Records of sys_adminrole
-- ----------------------------
INSERT INTO `sys_adminrole` VALUES ('1', '超级管理员', '总管理员', '1', '2018-12-21 17:12:13', '0');
INSERT INTO `sys_adminrole` VALUES ('2', '运营', '运营人员相关权限', '1', '2018-12-21 17:14:26', '1');

-- ----------------------------
-- Table structure for sys_area
-- ----------------------------
DROP TABLE IF EXISTS `sys_area`;
CREATE TABLE `sys_area` (
  `Id` int(11) NOT NULL COMMENT '地址主键Id',
  `Pid` int(11) NOT NULL COMMENT '地址父Id',
  `Code` int(11) NOT NULL COMMENT '地址编码',
  `Name` varchar(20) NOT NULL COMMENT '地址名',
  `NameMerger` varchar(30) NOT NULL COMMENT '地址省市区',
  `NameShort` varchar(10) NOT NULL COMMENT '地址简称',
  `NameShortMerger` varchar(30) NOT NULL COMMENT '地址省市区简称',
  `Levels` int(11) NOT NULL COMMENT '级别',
  `CityCode` varchar(20) DEFAULT NULL COMMENT '电话编码',
  `ZipCode` varchar(0) DEFAULT NULL COMMENT '邮编',
  `PinYin` varchar(50) DEFAULT NULL COMMENT '地址拼音',
  `JianPin` varchar(10) DEFAULT NULL COMMENT '地址简拼',
  `Letter` varchar(5) DEFAULT NULL,
  `Lng` varchar(50) DEFAULT NULL COMMENT '经度',
  `Lat` varchar(50) DEFAULT NULL COMMENT '纬度',
  `Remark` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='全国省市区';

-- ----------------------------
-- Records of sys_area
-- ----------------------------

-- ----------------------------
-- Table structure for sys_option
-- ----------------------------
DROP TABLE IF EXISTS `sys_option`;
CREATE TABLE `sys_option` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(30) CHARACTER SET utf8 NOT NULL COMMENT '参数名称',
  `GroupKey` varchar(30) CHARACTER SET utf8 NOT NULL COMMENT '参数关键词',
  `EnumCode` int(11) NOT NULL DEFAULT '0' COMMENT '参数编码',
  `EnumName` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '参数编码类型名',
  `EnumLabel` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '参数编码类型描述',
  `Remark` varchar(500) CHARACTER SET utf8 DEFAULT NULL COMMENT '参数编码类型描述',
  `Levels` int(11) NOT NULL DEFAULT '1' COMMENT '级别',
  `Orders` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='系统操作参数管理';

-- ----------------------------
-- Records of sys_option
-- ----------------------------

-- ----------------------------
-- Table structure for sys_uploadfile
-- ----------------------------
DROP TABLE IF EXISTS `sys_uploadfile`;
CREATE TABLE `sys_uploadfile` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TypeId` int(4) NOT NULL DEFAULT '0' COMMENT '文件类型',
  `UserId` int(11) NOT NULL COMMENT '上传者Id',
  `FileName` varchar(50) DEFAULT NULL COMMENT '文件名',
  `FilePath` varchar(200) DEFAULT NULL COMMENT '上传路径',
  `FileSize` int(11) NOT NULL COMMENT '上传文件的大小',
  `FileType` varchar(50) NOT NULL COMMENT '上传文件类别',
  `ImgWidth` int(11) DEFAULT NULL COMMENT '图片宽度',
  `ImgHeight` int(11) DEFAULT NULL COMMENT '图片高度',
  `Ip` varchar(20) NOT NULL COMMENT '上传时ip',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '上传时间',
  PRIMARY KEY (`Id`),
  KEY `AK_Key_1` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件上传记录';

-- ----------------------------
-- Records of sys_uploadfile
-- ----------------------------

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Status` int(4) NOT NULL DEFAULT '1' COMMENT '用户状态(1:正常，5:注销)',
  `UserName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '账号',
  `RealName` varchar(10) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户真实姓名',
  `Password` varchar(255) CHARACTER SET utf8 DEFAULT NULL COMMENT '密码',
  `LastIp` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '最后登录IP',
  `RoleId` int(4) NOT NULL DEFAULT '1' COMMENT '角色ID',
  `CrtDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `LastLogDate` datetime DEFAULT NULL COMMENT '最后登录时间',
  `HeadImgUrl` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '头像',
  `Sex` int(4) unsigned DEFAULT '0' COMMENT '性别',
  `Mobile` varchar(11) CHARACTER SET utf8 DEFAULT NULL COMMENT '联系号码',
  `CrtUser` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `userName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 AVG_ROW_LENGTH=8192 ROW_FORMAT=DYNAMIC COMMENT='后台系统管理员';

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1', '1', 'admin', '超级管理员', '1E10AB1B6A78A0DADC22E3C8F83AFFD3', '::1', '1', '2018-05-26 20:24:15', '2018-06-09 19:08:43', null, '0', null, 'admin');
INSERT INTO `sys_user` VALUES ('2', '5', 'ceshi3', 'string', '1E10AB1B6A78A0DADC22E3C8F83AFFD3', '::1', '1', '2019-01-09 12:15:01', '2019-01-09 12:15:01', null, '0', null, 'admin');
INSERT INTO `sys_user` VALUES ('3', '1', 'bryan', 'bryan', '1E10AB1B6A78A0DADC22E3C8F83AFFD3', '193.112.41.35', '1', '2019-01-31 17:58:35', '2019-01-31 17:58:35', null, '0', null, 'admin');

-- ----------------------------
-- Table structure for sys_userrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_userrole`;
CREATE TABLE `sys_userrole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL COMMENT '用户ID',
  `RoleId` int(11) NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='账号角色配置';

-- ----------------------------
-- Records of sys_userrole
-- ----------------------------
INSERT INTO `sys_userrole` VALUES ('1', '1', '1');
SET FOREIGN_KEY_CHECKS=1;
