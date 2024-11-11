# json

## 序列化

程序运行时，数据散落在内存各处。在程序内传递数据，可以传递内存地址。
但当我们希望关闭程序后（释放内存后）仍然能保持数据，又或是通过网络传递给外部，
那至少，首先要把所有数据收集起来，剔除掉内存地址的概念。

剔除内存地址，压缩和打包数据的过程就叫做序列化。

任何一种能还原为原有数据的方式就是有意义的序列化，无论是输出为文本还是图片。
其中，**json**是一种文本格式，是常用的序列化方案。json无法序列化具有循环引用的实例。

## json格式

json格式是键值对的形式，键值对以逗号隔开。其中键需要用双引号。值可以是以下类型之一

- 数字（整数或浮点数）
- 字符串（在双引号中）
- 逻辑值（true 或 false）
- 数组（在中括号中）
- 对象（在大括号中）
- null

以下json为例，声明可以装在这份数据的类。

```json
{
  "name": "John Doe",
  "age": 30,
  "email": "john.doe@example.com",
  "phoneNumbers": [
    "212-555-1234",
    "646-555-4567"
  ]
}
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

# Newtonsoft.Json

安装扩展包`Newtonsoft.Json`可以用于交互json。它比c#自己处理json的API提供了更多的功能。

## 序列化和反序列化

`JObject`的静态方法可以直接从一个实例序列化为`JObject`对象。
将这个`JObject`直接`ToString`就是合法的json字符串。

`JObject`的实例方法可以反序列化为c#对象。

```csharp
var jo2 = JObject.FromObject(new Person());//从实例序列化

var per = jo.ToObject<Person>();//对象可以用反序列化
var phone2 = jo["phoneNumbers"].Values<string>();//数组可以解析为可迭代类型
var phone3 = (string)jo["phoneNumbers"][0];//数字，字符串，bool，时间等基础数据可以直接强转
```

### 节点类型

- `JToken`：这个包里json节点类型的基类。
- `JValue`：数字，字符串，bool等基础数据。可以直接强转为对应类型。不能嵌套内容。
- `JProperty`：只能存在于`JObject`中。包含一个名字和一个`JToken`。
- `JObject`：一个json对象，以`{}`表示的内容。只能存在`JProperty`。
- `JArray`：一个json数组，以`[]`表示的内容。可以包含除了`JProperty`以外的`JToken`。

## 解析

`JObject`和`JArray`可以从文字解析。

```csharp
string s = """
{
  "name": "John Doe",
  "age": 30,
  "email": "john.doe@example.com",
  "phoneNumbers": [
    "212-555-1234",
    "646-555-4567"
  ]
}
""";

var jo=JObject.Parse(s);//从文字解析
```

## 访问内容

对于对象，通过索引和字符串访问。对于数组，通过索引和数字进行访问。

```csharp
var name = jo["name"];
var phone = jo["phoneNumbers"][1];
```

如果没有对应的节点，会得到`null`。

## 添加节点

索引也可以直接赋值来添加/覆写节点。

```csharp
jo["hello"] = new JObject(new JProperty("world", new JArray(1, 2, 3)));
```

## 特性控制序列化

Newtonsoft.Json 提供了一系列的特性（Attributes），使用这些特性可以控制对象的序列化和反序列化行为。以下是一些常见的特性及其用法：


1. `[JsonIgnore]`用于标记属性或字段，使其在序列化过程中被忽略。
   ```csharp
   public class MyClass
   {
       [JsonIgnore]
       public string IgnoreThisProperty { get; set; }
   }
   ```

2. `[JsonProperty]`用于自定义属性或字段的序列化名称。
   ```csharp
   public class MyClass
   {
       [JsonProperty("customName")]
       public string MyProperty { get; set; }
   }
   ```

3. `[JsonConverter]`用于指定一个自定义的转换器（Converter）来处理特定类型的序列化和反序列化。
   ```csharp
   public class MyCustomConverter : JsonConverter<MyClass>
   {
       // 实现 Read 和 Write 方法来进行自定义序列化和反序列化
   }

   public class MyClass
   {
       [JsonConverter(typeof(MyCustomConverter))]
       public string UseCustomConverterForThisProperty { get; set; }
   }
   ```

4. `[JsonExtensionData]`用于包含在序列化和反序列化过程中未匹配到其他属性的数据。
   ```csharp
   public class MyClass
   {
       [JsonExtensionData]
       public Dictionary<string, object> ExtensionData { get; set; }
   }
   ```
