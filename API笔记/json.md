# json

程序运行时，数据散落在内存各处。在程序内传递数据，可以传递内存地址。
但当我们希望关闭程序后（释放内存后）仍然能保持数据，又或是通过网络传递给外部，
那至少，首先要把所有数据收集起来，剔除掉内存地址的概念。

剔除内存地址，压缩和打包数据的过程就叫做序列化。

任何一种能还原为原有数据的方式就是有意义的序列化，无论是输出为文本还是图片。
其中，**json**是一种文本格式，是常用的序列化方案。json无法序列化具有循环引用的实例。

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

## JObject

`JObject`类，需要安装扩展包`Newtonsoft.Json`，API更简便。

### 解析

`JObject`可以从文字解析，或是从一个实例直接反序列化。

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
var jo=JObject.Parse(s);

Person Person = new Person();
var jo2 = JObject.FromObject(Person);
```

### 访问内容

对于对象，通过索引和字符串访问。对于数组，通过索引和数字进行访问。

```csharp
var name = jo["name"];
var phone = jo["phoneNumbers"][1];
```

如果没有对应的节点，会得到`null`。
可以通过索引和赋值来添加/覆写节点。

### 反序列化

```csharp
var per = jo.ToObject<Person>();//对象可以用反序列化
var phone2 = jo["phoneNumbers"].Values<string>();//数组可以解析为可迭代类型
var phone3 = (string)jo["phoneNumbers"][0];//数字，字符串，bool，时间等基础数据可以直接强转
```

### 新建对象/数组

在覆写或添加节点时，有时候也需要一个Json的对象或数组。
可以直接通过构造器创建。

```csharp
jo["hello"] = new JObject(new JProperty("world", new JArray(1, 2, 3)));
```

- `JObject`：一个json对象，以`{}`表示的内容。
- `JArray`：一个json数组，以`[]`表示的内容。
- `JProperty`：一个json属性，json对象里的成员都是这东西。分为成员名字和成员的值。
- `JValue`：数字，字符串，bool，时间等基础数据。可以直接强转为对应类型。
- `JToken`：上面的基类，需要执行类型判断。

### 特性控制序列化


### 序列化设置

## JsonObject

`JsonObject`，来自`System.Text.Json.Nodes`命名空间，效率更高但要求更严格。

### 解析

### 序列化

### 内容访问

### 反序列化

### 新建对象/数组

### 特性控制序列化


### 序列化设置

