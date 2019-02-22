INSERT INTO `sys_admin_role`(`id`, `roleName`, `remark`, `crtUser`, `crtDate`) VALUES (1, '超级管理员', '所有权限', 'admin', '2018-06-09 16:14:18');
INSERT INTO `sys_admin_role`(`id`, `roleName`, `remark`, `crtUser`, `crtDate`) VALUES (2, '管理员', '大部分权限', 'admin', '2018-06-09 16:59:20');
INSERT INTO `sys_admin_role`(`id`, `roleName`, `remark`, `crtUser`, `crtDate`) VALUES (3, '普通用户', '测试', 'admin', '2018-07-12 00:45:17');

INSERT INTO `sys_user`(`id`, `status`, `userName`, `realName`, `password`, `lastIp`, `roleId`, `crtDate`, `lastLogDate`, `headImgUrl`, `sex`, `mobile`, `crtUser`) VALUES (1, 0, 'admin', '超级管理员', '1E10AB1B6A78A0DADC22E3C8F83AFFD3', '::1', 1, '2018-05-26 20:24:15', '2018-06-09 19:08:43', NULL, 0, NULL, 'admin');

INSERT INTO `sys_admin_menu`(`id`, `pid`, `tag`, `name`, `icon`, `lvl`, `childNum`, `lefts`, `rights`, `isShow`, `url`, `crtUser`, `crtDate`) VALUES (1, 0, 'root', '根目录', '', 0, 0, 1, 1, 1, '', 'admin', '2018-06-02 23:00:42');

