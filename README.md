# Time tree for unity(C#)

# 介绍
树状图执行行为框架，父节点执行完毕后，执行所有子节点（由上至下，直至所有节点结束），用于实现一些常见的特效流程控制
此工程为Unity Package的Git包，可通过Unity的PackageManagerWindow导入到需要使用的项目中。

# Unity导入步骤
- 在Unity中点击菜单栏的Window->Package Manager打开PackageManager面板
- 点击Add package from git url
- 复制git工程的地址，粘贴到输入栏
- 点击Add

# 框架图
![Frame](/Images/Frame.png)

# 应用场景
![Sample](/Images/Sample.png)

# 使用方法
- 1：实现ATreeNode的各种派生类。
- 2：创建TimeTree对象
- 3：将ATreeNode的派生类对象链接到TimeTree对象的EntryNode或上一个Node
- 4：执行TimTree对象的Start函数即可。
- 项目中有简单示例，导入Unity运行即可。

# API
### ATreeNode
| Function |   Params  | Return | ReamMe                                                                                                              |
|------------|:---------:|:--------:|---------------------------------------------------------------------------------------------------------------------|
| OnEnter    |       |  | Called when enter this node                                                                                         |
| OnUpdate   |       |  | Called when node is running                                                                                         |
| OnExit     |       |  | Called when exit the node                                                                                           |
| AddChild   | ATreeNode | ATreeNode | You can call this function to link child node, function return the child node |
| StopSelf   |       |  | Called when the time tree is stop(timetree is break, not finished), by default it call OnExit to process stop event |

### TimeTree
| Fun's Name |    Params    | Return    | ReamMe                                      |
|------------|:------------:|-----------|---------------------------------------------|
| Ctor       | Action<bool> |           | Create a time tree with finished callback   |
| Start      |              |           | Start the time tree                         |
| Update     |     float    |           | Update total node whith delta time          |
| Stop       |              |           | Stop time tree if time tree is not finished |
| GetEntry   |              | ATreeNode | Return entry node                           |