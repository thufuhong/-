软件工程大作业：多元素风格游戏
=======

# 介绍

作为《软件工程(2017)》的课程作业，我们试图使用Unity 3D 引擎开发一个多风格融合的游戏。游戏以角色扮演和关卡冒险为主要玩法，融合了生存、弹幕、射击、养成等多种元素。

开发团队成员有：

- 付宏
- 叶豪
- 牟宏杰
- 武智源
- 慕思成

本仓库包含了Unity工程的场景文件以及所有的代码文件，为了完整参与到开发进程中来，需要额外下载用到的美术资源，我们欢迎有兴趣的读者联系我们。


# 更改历史

## V0.10(2017.11.19)

- 测试场景构建
- 人物构建：主角与敌人的移动控制和基本属性，敌人的仇恨机制
- 生存玩法加入：安全区会随时间缩小

## V0.11(2017.11.26)

- 重写场景生成：随机刷新障碍物与敌人，主动避开重叠位置

## V0.12(2017.11.28)

- 属性系统加入，包括等级、攻击防御、属性成长、金钱
- 加入第一个技能，按F发动，短暂的提升属性和移速，添加了简易UI
- Boss加入，融入弹幕元素，关卡获胜条件设置为击败Boss

## V0.13(2017.12.1)

- 加入Esc弹窗菜单
- 加入敌人的血条

## V0.14(2017.12.2)

- 完善了四个技能，加入了技能介绍UI，技能点设定和技能升级按钮
- Boss死后会召唤出一个商店，相关接口在shop.cs
- 游戏性数值修改；攻击键改为Alt；缩圈时间由常数修改为10+50/回合数
- 若干Bug修复和功能改进

