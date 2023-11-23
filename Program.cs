﻿using System.Reflection;

var t = typeof(Hello);//获取类型
var p = t.GetProperties((BindingFlags)(-1));//获取所有属性

var my1 = (MyAttribute?)t.GetCustomAttribute(typeof(MyAttribute));//获取特性
var my2 = (MyAttribute?)p[0].GetCustomAttribute(typeof(MyAttribute));//如果没有则返回null

Console.WriteLine(my1?.NamedInt);
Console.WriteLine(my2?.NamedInt);

 
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class MyAttribute(string positionalString = "") : Attribute//声明特性
{
	public string PositionalString { get; } = positionalString;

	public int NamedInt { get; set; }
}
[@MyAttribute, My]//省略后缀和完整后缀
class Hello
{
	[My]
	[Obsolete]//可以分开多个中括号写，也可以用逗号隔开
	public int Age;

	[My("hello", NamedInt = 12)]//特性的构造器
	public int Name { get; set; }

	[return: My]//给返回值添加特性
	[My]//给方法添加特性
	public int Add([My] int a, [My] int b)//给参数添加特性
	{
		return a + b;
	}
}