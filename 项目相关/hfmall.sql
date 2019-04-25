/*
 Navicat Premium Data Transfer

 Source Server         : linux
 Source Server Type    : MySQL
 Source Server Version : 50725
 Source Host           : 114.116.182.73:3306
 Source Schema         : hfmall

 Target Server Type    : MySQL
 Target Server Version : 50725
 File Encoding         : 65001

 Date: 21/04/2019 16:48:45
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for gd_goods
-- ----------------------------
DROP TABLE IF EXISTS `gd_goods`;
CREATE TABLE `gd_goods`  (
  `Id` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品主键（G+门店简称+商品类目简称+创建日期+时分秒+当天该门店增加的商品数量）',
  `Status` int(4) NOT NULL DEFAULT 1 COMMENT '商品状态：1暂存，2删除，3审核驳回，5审核中，10通过审核',
  `CategoryId` int(11) NOT NULL DEFAULT 0 COMMENT '类目Id（gd_goodscategory主键）',
  `SellerId` int(11) UNSIGNED NOT NULL DEFAULT 1 COMMENT '卖家Id',
  `Title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品标题',
  `BrandId` int(11) NOT NULL DEFAULT 0 COMMENT '品牌Id',
  `Price` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '商品单价',
  `PriceMarket` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '市场价格（原价）',
  `PriceFreight` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '运费',
  `PriceFreightAdditional` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '偏远地区附加运费',
  `Stock` int(11) NOT NULL DEFAULT 0 COMMENT '商品库存',
  `SpecsNumber` int(11) NOT NULL DEFAULT 1 COMMENT '规格数量',
  `SpecsJson` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '规格类型Json，如：[{Key：\"颜色\",\"Value\":[\"黑色\",\"红色\"]},{Key:\"内存\",\"Value\":[\"16G\",\"32G\"]}}]',
  `RemoteRegion` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '偏远地区',
  `ImgSmall` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片缩略图',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品描述',
  `CrtDate` datetime(0) NOT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `ModifyDate` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改日期',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for gd_goodsactivity
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsactivity`;
CREATE TABLE `gd_goodsactivity`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GoodsId` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品ID',
  `TypeId` int(4) NOT NULL DEFAULT 1 COMMENT '活动类型：1拼团，5秒杀',
  `ActivityId` int(11) NOT NULL DEFAULT 0 COMMENT '活动ID',
  `Price` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '活动价格',
  `Stock` int(11) NOT NULL DEFAULT 0 COMMENT '活动商品库存',
  `GroupMin` int(11) NOT NULL DEFAULT 0 COMMENT '拼团活动成团人数',
  `StartTime` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '活动开始时间',
  `EndTime` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '活动结束时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品参加的活动' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for gd_goodscategory
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodscategory`;
CREATE TABLE `gd_goodscategory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pid` int(11) NOT NULL COMMENT '类目父Id',
  `Name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '类目名称',
  `Level` int(4) NOT NULL COMMENT '类目级别',
  `Orders` int(11) NOT NULL COMMENT '排序',
  `Abbreviation` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '类目缩写',
  `IsShow` varchar(4) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '1' COMMENT '是否商城显示',
  `ImgUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片路径',
  `Remark` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CrtDate` datetime(0) NOT NULL COMMENT '创建时间',
  `ModifyDate` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品类目' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for gd_goodsimg
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsimg`;
CREATE TABLE `gd_goodsimg`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GoodsId` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品Id',
  `TypeId` int(4) NOT NULL DEFAULT 0 COMMENT '图片类型',
  `UploadFileId` int(11) NOT NULL DEFAULT 0 COMMENT '上传文件ID（sys_uploadfile主键）',
  `Orders` int(4) NOT NULL DEFAULT 0 COMMENT '排序',
  `ImgUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CrtDate` datetime(0) NOT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `IsDelete` int(1) NOT NULL DEFAULT 0 COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品图片' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for gd_goodsinfo
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsinfo`;
CREATE TABLE `gd_goodsinfo`  (
  `GoodsId` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `GoodsDetails` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品详情',
  `GoodsParamsJson` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品参数，如：{“品牌”:\"新百伦\",“尺码”:\"38/39/40/41\"}',
  `CrtDate` datetime(0) NOT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `ModifyDate` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改日期',
  PRIMARY KEY (`GoodsId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品详细信息' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for gd_goodsspecs
-- ----------------------------
DROP TABLE IF EXISTS `gd_goodsspecs`;
CREATE TABLE `gd_goodsspecs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GoodsId` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品Id',
  `SpecsJosn` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '商品规格json数据，如：{\"颜色\":\"金色\",\"内存\":\"32G\"}',
  `Stock` int(11) NOT NULL DEFAULT 0 COMMENT '规格库存',
  `SkuImgUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '规格缩略图',
  `CrtDate` datetime(0) NOT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `ModifyDate` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '修改日期',
  `Price` decimal(10, 4) NOT NULL DEFAULT 0.0000 COMMENT '规格价格',
  `IsDelete` int(2) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '商品规格' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for log_admin
-- ----------------------------
DROP TABLE IF EXISTS `log_admin`;
CREATE TABLE `log_admin`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TypeId` int(4) NOT NULL COMMENT '日志类型',
  `CrtUserId` int(11) NOT NULL COMMENT '创建人ID',
  `CrtUserName` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人账号',
  `OtherId` bigint(20) NOT NULL DEFAULT 0 COMMENT '影响数据的ID',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Url` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'url',
  `Ip` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'ip',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '记录后台操作日志' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for log_login
-- ----------------------------
DROP TABLE IF EXISTS `log_login`;
CREATE TABLE `log_login`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TypeId` int(4) NOT NULL DEFAULT 1 COMMENT '1：后台,5：用户',
  `UserId` int(11) NULL DEFAULT NULL,
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Ip` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Content` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '记录后台登录日志' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_adminmenu
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminmenu`;
CREATE TABLE `sys_adminmenu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pid` int(11) NOT NULL COMMENT '父菜单ID',
  `Tag` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `MenuName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '页面名',
  `Icon` varchar(48) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'icon标识',
  `Level` int(4) NOT NULL DEFAULT 1 COMMENT '等级',
  `ChildNum` int(11) NOT NULL DEFAULT 0 COMMENT '子菜单数量',
  `Orders` int(11) NOT NULL COMMENT '左移量',
  `IsShow` int(4) NOT NULL DEFAULT 1 COMMENT '是否显示（0:显示,1:隐藏）',
  `Url` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '页面链接',
  `CrtUser` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `BtnJson` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '页面中按钮Json[{\"code\":\"btn\",\"type\":\"button/uri\",\"name\":\"添加\",\"description\":\"按钮描述\",\"isForbidden\":0,,\"url\":\"http://localhost/index\"}]',
  `Status` int(2) NULL DEFAULT 1 COMMENT '状态：1：正常，5：禁用',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `UK_sys_adminMenu_id`(`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台系统菜单' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_adminmenubtn
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminmenubtn`;
CREATE TABLE `sys_adminmenubtn`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MenuId` int(11) NOT NULL COMMENT '菜单Id',
  `Code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '按钮或资源编码',
  `Type` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '按钮或资源类型',
  `Name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '按钮或资源名称',
  `Description` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Url` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'url或其他',
  `IsForbidden` int(2) NOT NULL DEFAULT 0 COMMENT '是否禁用，0:启用，1:禁用',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '页面按钮或资源' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_adminpermission
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminpermission`;
CREATE TABLE `sys_adminpermission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '菜单类型，如：url菜单类目，btn菜单中的按钮',
  `MenuId` int(11) NOT NULL COMMENT '菜单ID',
  `RoleId` int(11) NOT NULL COMMENT '角色ID',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserId` int(11) NOT NULL DEFAULT 0 COMMENT '创建用户ID',
  `BtnJson` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '菜单按钮Json',
  `CheckStatus` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '选中状态（checkd：选中，half：半选中）',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 101 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台系统权限' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_adminrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_adminrole`;
CREATE TABLE `sys_adminrole`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色名称',
  `Remark` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色描述',
  `UserId` int(11) NOT NULL COMMENT '创建用户ID',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IsForbidden` int(4) NOT NULL DEFAULT 0 COMMENT '是否禁用，1：正常，5：禁用',
  `ModifyDate` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台用户权限组' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_area
-- ----------------------------
DROP TABLE IF EXISTS `sys_area`;
CREATE TABLE `sys_area`  (
  `Id` int(11) NOT NULL COMMENT '地址主键Id',
  `Pid` int(11) NOT NULL COMMENT '地址父Id',
  `Code` int(11) NOT NULL COMMENT '地址编码',
  `Name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '地址名',
  `NameMerger` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '地址省市区',
  `NameShort` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '地址简称',
  `NameShortMerger` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '地址省市区简称',
  `Levels` int(11) NOT NULL COMMENT '级别',
  `CityCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话编码',
  `ZipCode` varchar(0) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮编',
  `PinYin` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址拼音',
  `JianPin` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址简拼',
  `Letter` varchar(5) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Lng` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '经度',
  `Lat` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '纬度',
  `Remark` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '全国省市区' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_option
-- ----------------------------
DROP TABLE IF EXISTS `sys_option`;
CREATE TABLE `sys_option`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '参数名称',
  `GroupKey` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '参数关键词',
  `EnumCode` int(11) NOT NULL DEFAULT 0 COMMENT '参数编码',
  `EnumName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '参数编码类型名',
  `EnumLabel` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '参数编码类型描述',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '参数编码类型描述',
  `Levels` int(11) NOT NULL DEFAULT 1 COMMENT '级别',
  `Orders` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `IsHide` int(2) NOT NULL DEFAULT 0 COMMENT '是否隐藏',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统操作参数管理' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_uploadfile
-- ----------------------------
DROP TABLE IF EXISTS `sys_uploadfile`;
CREATE TABLE `sys_uploadfile`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TypeId` int(4) NOT NULL DEFAULT 0 COMMENT '文件类型',
  `UserId` int(11) NOT NULL COMMENT '上传者Id',
  `FileName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件名',
  `FilePath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上传路径',
  `FileSize` int(11) NOT NULL COMMENT '上传文件的大小',
  `FileType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '上传文件类别',
  `ImgWidth` int(11) NULL DEFAULT NULL COMMENT '图片宽度',
  `ImgHeight` int(11) NULL DEFAULT NULL COMMENT '图片高度',
  `Ip` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '上传时ip',
  `Status` int(4) NOT NULL DEFAULT 1 COMMENT '图片状态：1未用，3可删除，15已删除，20使用中',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '上传时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `idx_FileName`(`FileName`) USING BTREE,
  INDEX `AK_Key_1`(`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '文件上传记录' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `Status` int(4) NOT NULL DEFAULT 1 COMMENT '用户状态(1:正常，5:注销)',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账号',
  `RealName` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户真实姓名',
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `RoleId` int(4) NOT NULL DEFAULT 1 COMMENT '角色ID',
  `HeadImgUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `Sex` int(4) UNSIGNED NOT NULL DEFAULT 0 COMMENT '性别',
  `Mobile` varchar(11) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系号码',
  `Email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `CrtUser` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '0' COMMENT '创建人',
  `LastIp` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后登录IP',
  `LastLogDate` datetime(0) NULL DEFAULT NULL COMMENT '最后登录时间',
  `CrtDate` datetime(0) NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `ModifyDate` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `userName`(`UserName`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台系统管理员' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for sys_userrole
-- ----------------------------
DROP TABLE IF EXISTS `sys_userrole`;
CREATE TABLE `sys_userrole`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL COMMENT '用户ID',
  `RoleId` int(11) NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '账号角色配置' ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
