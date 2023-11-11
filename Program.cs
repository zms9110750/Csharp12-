﻿#region 整数的字面量

//自动类型
var i1 = 100;//int
var i2 = 10000000000000000;//long
var i3 = 10000000000000000000;//ulong
var i4 = 100000000000000000000000000000000000000;//报错


//强制类型
var i5 = 100u;//uint
var i6 = 10000000000000000u;//ulong
var i7 = 100L;//long
var i8 = 10000uL;//ulong

//2进制和16进制
var i9 = 0b10000;
var i10 = 0x1a;
Console.WriteLine(i9);//16
Console.WriteLine(i10);//26

//分隔
var i11 = 1_000_000;
var i12 = 100_000;
var i13 = 1_0__0___0_____0______0;
#endregion

#region 浮点数的字面量

//强制类型
var f1 = 12f;
var f2 = 12.0f;
var d1 = 12d;
var d2 = 12.0;
var m1 = 12m;
var m2 = 12.0m;

//省略整数部分
var d3 = 0.0;
var d4 = .0;

//科学计数法
var d5 = 1e4;//10000
var d6 = 12e-2;//0.12
var d7 = 0e9;//0

//分隔
var d8 = 1e4_3;
var d9 = 1.3_4;
#endregion

#region 布尔的字面量
//布尔值的字面量只有两个，他们都是关键字
var b1 = true;
var b2 = false;
#endregion

#region 文本的字面量
//单引号的字符类型，双引号的字符串类型。
//字符类型只能且必须有一个字符。
var c1 = 'a';
var c2 = '好';
var s1 = "";
var s2 = "你好世界";

//转义
var c3 = '\'';//会输出'
var s3 = "\"\\";//会输出"\

var c4 = '\u5238';//输出卷
var s4 = "1\t1\n11\t1\n111\t1\n1111\t1";//水平制表符可以根据以及用的字符数量，自动决定生成多少个空格
Console.WriteLine(s4);

//原始字符串
Console.WriteLine("""C:\Program Files\dotnet\packs""");
Console.WriteLine("""""用户输入{""""}""""");//如果字符里面的连续引号过多，就增加原始字符串首尾引号数量，直到文本中的引号不会有歧义。

string s5 = """
	宽度=480px;
	高度=640px;
	""";
Console.WriteLine(s5);
#endregion