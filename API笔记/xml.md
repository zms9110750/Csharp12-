# json

## 序列化

程序运行时，数据散落在内存各处。在程序内传递数据，可以传递内存地址。
但当我们希望关闭程序后（释放内存后）仍然能保持数据，又或是通过网络传递给外部，
那至少，首先要把所有数据收集起来，剔除掉内存地址的概念。

剔除内存地址，压缩和打包数据的过程就叫做序列化。

任何一种能还原为原有数据的方式就是有意义的序列化，无论是输出为文本还是图片。
其中，**xml**是一种文本格式，是常用的序列化方案。xml无法序列化具有循环引用的实例。

## xml格式

xml是一种具有很多复杂节点的格式，可以包含很多信息但数据量会很大。
更适合人查看，但在只需要程序处理时，使用json传递数据会更有效率。

以下xml为例，声明可以装在这份数据的类。

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Person>
    <name>John Doe</name>
    <age>30</age>
    <email>john.doe@example.com</email>
    <phoneNumbers>
        <phoneNumber type="home">212-555-1234</phoneNumber>
        <phoneNumber type="work">646-555-4567</phoneNumber>
    </phoneNumbers>
</Person>
```
```csharp
public class Person
{
	public string name { get; set; }
	public int age { get; set; }
	public string email { get; set; }
	public string[] phoneNumbers { get; set; }
}
```

## 序列化和反序列化

c#提供的序列化仅有从字符串到对象之间的序列化和反序列化。

并且API也只能和流交互，所以要使用内存流提取字符串。

```csharp
	/// <summary>将对象xml序列化为字符串</summary>
	public static string Serialize<T>(T value)
	{
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringWriter sw = new StringWriter();
		xml.Serialize(sw, value);
		return sw.ToString();
	}
	/// <summary>将对象序列化为xml节点</summary>
	public static XElement Serialize(object value)
	{
		XmlSerializer xml = new XmlSerializer(value.GetType());
		using MemoryStream ms = new MemoryStream();
		xml.Serialize(ms, value);
		ms.Position = 0;
		return XElement.Load(ms);
	}
	/// <summary>将xml字符串反序列化</summary>
	public static T? DeSerialize<T>(string value)
	{
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringReader sr = new StringReader(value);
		return (T?)xml.Deserialize(sr);
	}
```

## 特性控制序列化

参阅：(https://learn.microsoft.com/zh-cn/dotnet/standard/serialization/attributes-that-control-xml-serialization)


# XmlLinq

`System.Xml.Linq`命名空间里包含处理xml格式的类。

按照继承链有以下继承关系：

- `XObject`：基类
    - `XAttribute`：特性
    - `XNode`：节点
        - `XComment`：注释
        - `XContainer`：可以包含子元素的类
            - `XDocument`：文档
            - `XElement`：元素
        - `XDocumentType`：文档声明
        - `XProcessingInstruction`：指令
        - `XText`：文字
            - `XCData`：转义文字

## 解析

使用`XDocument`或`XElement`可以解析一段xml字符串

```csharp
var e=XElement.Parse(s);
var d = XDocument.Parse(s);
```

## 节点类型

### 文档

大部分情况下，只需要使用元素进行交互就可以了。

文档比元素多出来的只有xml声明部分，即`<?xml version="1.0" encoding="UTF-8"?>`。

### 元素

元素是一种成对，有序的标签。

- 嵌套内容的以`<xx>开头，以`</xx>`闭合。
- 不嵌套内容的是自闭合标签，以`<xx />`的形式。

元素可以在内部嵌套内容，或是在自己身上包含特性。

元素可以遍历内部节点。

```csharp
e.Element("name");//查找第一个指定名字的元素（同一元素下可以有多个同名元素）
e.Elements();//遍历所有子元素
e.Nodes();//遍历所有子节点
e.DescendantNodes();//遍历所有后代节点
```

### 特性

特性是元素上附加的键值对。以`xxx = "yyy"`的形式存在于元素的开始标签中。

特性可以从元素上查找

```csharp
e.Attribute("id");//获取指定名字的第一个特性
e.Attributes("");//获取所有特性
```

### 注释

注释是一种`<!--`开头，`-->`结尾的节点。

### 指令

指令是一种`<?`开头，`?>`结尾的节点。

指令有两个值，一个是目标，一个是数据。
目标是紧跟着开头后（不能有空格）的内容。
数据是目标后空格隔开的所有内容。

对于`<?a b c ?>`目标是`a`，数据是`b c `。

```csharp
foreach (var item in e.DescendantNodes())
{
    switch (item)
    {
        case XProcessingInstruction p:
            Console.WriteLine(p.Target);
            Console.WriteLine(p.Data);
            break;
    }
}
```

### 转义文字

CDATA部分由`<![CDATA[`开始，由`]]>`结束的文字节点。

xml因为`<`被解释为标签，不能认为是文本。在文本内使用需要改为`&gt;`。

而使用转义文字，可以不必考虑`<`的转义。

- `<message>if salary < 1000 then</message>`，非法
- `<message>if salary &lt; 1000 then</message>`，实体引用
- `<message><![CDATA[if salary < 1000 then]]></message>`，转义

## 添加和移除元素

`XElement`可以使用`Add`方法添加任何内容。
但只有`System.Xml.Linq`命名空间下的一些类型会解释为xml树中的东西。
其余的都会调用`ToString`作为文本节点。

```csharp
e.Add(new XElement("a"),new XAttribute("b",false));

e.Remove();//移除自己
e.RemoveNodes();//移除自己里面的节点
e.RemoveAttributes();//移除所有特性
e.SetAttributeValue("id",null);//修改一个特性的值，为null则删除
```

## xml命名

xml的元素和特性必须具有名字。通常在调用方法时，直接填入字符串。但实际上这些方法的参数类型是`XName`。

这个类型没有公开的构造器，通常只能由字符串隐式转换而来。

它包含两个属性：命名空间，本地名称。

```csharp
XName n = "{hello}world";

Console.WriteLine(n.NamespaceName);//hello
Console.WriteLine(n.LocalName);//world
```

### xml命名空间别名

当一个元素上具有`XNamespace.Xmlns`为命名空间的特性时，这个特性的名字会被当作值的命名空间别名。

```csharp
XAttribute a = new XAttribute(XNamespace.Xmlns +"p1","hello");
XElement e = new XElement("{hello}world",a);
e.Add(new XElement("{hello}root"));
Console.WriteLine(e);
```
```
<p1:world xmlns:p1="hello">
  <p1:root />
</p1:world>
```

## XPath
