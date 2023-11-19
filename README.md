# 你好
这是一篇讲解c#的文章。
在新的语法出现后，对c#的学习顺序也应该更改，以更符合逻辑，形成知识体系。

本文描述的c#12配套于.Net8发行。在2023年11月发布正式版。
## 适合阅读本文的人群
仅限于个人学习者。因为.Net8是非常新的版本。如果是公司使用，或者学校考试，都不会给用上这个版本。
个人学习者无论只是想了解，还是想独立做一个项目出来。都不需要和别人的项目进行适配。

## 使用方式
在笔记文件的文件夹中，存放了md文件。在github网站，Obsidian等文本编辑程序中查看。
另一个文件夹里使用普通的txt文件，保存了对应章节里用于参考的代码。
将里面的内容复制到`Program.cs`文件中以查看效果。

# 计划目录
1. 下载编辑器
1. 创建项目
1. 注释和预处理命令 
1. 类型和基本类型
1. 变量
1. 数字类型
1. 字面量
1. 类型转换
1. 选择和循环
1. 复合运算
1. 作用域和跳转
1. 方法
1. 数组，元组。
1. 引用类型，引用变量
1. 范围和字符串裁剪
1. 特殊参数和可空值类型
1. 模式匹配
1. 一个记录
1. 访问权限
1. 命名空间
1. 类
1. 结构
1. 静态和扩展方法
1. 运算符
1. 继承，访问基类
1. 抽象，面向抽象编程
1. 接口
1. 枚举
1. 特性和异常
1. 同步
1. 泛型
1. 委托
1. 迭代器
1. 异步
1. 多线程


# CNDS
标题|小节|小节|小节|小节|小节|小节|小节
-|-|-|-|-|-|-|-|
基本语句|
[下载编辑器](https://blog.csdn.net/zms9110750/article/details/130451889)|IDE|c#和.Net|Framework&#xff0c;Core&#xff0c;Standard
[创建一个项目](https://blog.csdn.net/zms9110750/article/details/130452670)|创建一个项目|程序入口|提示|编写辅助
[代码格式](https://blog.csdn.net/zms9110750/article/details/130453475)|格式|注释|API|控制台类常用指令
[变量](https://blog.csdn.net/zms9110750/article/details/130453506)|变量|标识符|类型
[内置类型](https://blog.csdn.net/zms9110750/article/details/130460420)|内置类型|字面量|指定类型的变量声明|类型转换
流程控制|
[流程控制语句](https://blog.csdn.net/zms9110750/article/details/130469285)|随机数|选择|循环|跳转|作用域|流程预测
[运算符](https://blog.csdn.net/zms9110750/article/details/130478675)|一元运算符|二元运算符|特殊表达式|其他
[数组](https://blog.csdn.net/zms9110750/article/details/130498466)|数组|指针|默认值|截取数组
[方法](https://blog.csdn.net/zms9110750/article/details/130518530)|方法|元组|可空值类型
[模式匹配](https://blog.csdn.net/zms9110750/article/details/130538762)|模式匹配
面向对象|
[定义类](https://blog.csdn.net/zms9110750/article/details/130543225)|声明类|类成员|实例和静态|命名空间
[继承](https://blog.csdn.net/zms9110750/article/details/130574950)|继承|重写|object|多态
[接口](https://blog.csdn.net/zms9110750/article/details/130584905)|接口定义|实现接口|面向抽象
[结构](https://blog.csdn.net/zms9110750/article/details/130592502)|装箱|结构|只读结构|引用结构
[记录](https://blog.csdn.net/zms9110750/article/details/130595974)|记录
[枚举](https://blog.csdn.net/zms9110750/article/details/130603991)|枚举|使用枚举
[对象初始化器](https://blog.csdn.net/zms9110750/article/details/130617762)|对象初始化器|对象克隆器|集合初始化器|匿名类型|所需成员
高级特性|
[特性](https://blog.csdn.net/zms9110750/article/details/130619200)|特性|预定义特性|自定义特性
[异常](https://blog.csdn.net/zms9110750/article/details/130621882)|异常|抛出异常|捕获异常|finally块
[泛型](https://blog.csdn.net/zms9110750/article/details/130627472)|泛型方法|泛型类|泛型约束|协变逆变
[委托](https://blog.csdn.net/zms9110750/article/details/130649414)|委托|多播委托|匿名方法|事件
[迭代器](https://blog.csdn.net/zms9110750/article/details/130723432)|foreach循环|迭代器|Linq
[异步](https://blog.csdn.net/zms9110750/article/details/130747591)|异步|异步方法|手动拼接Task|取消|异步流
[多线程](https://blog.csdn.net/zms9110750/article/details/130767909)|并行|线程同步|并行Linq

# 要求
常量=>变量
扩展方法=>静态类=>静态，类
构造器=>类=>记录
结构=>类，记录
接口=>抽象=>继承
异步=>finally=>同步
多线程=>异步=>迭代器=>泛型，接口
委托=>泛型，方法
switch=>模式匹配=>数组,作用域