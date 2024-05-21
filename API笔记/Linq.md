# Linq

Linq是指在语言中包含的用于从数据集里查询数据的功能。
不过Linq也有方法版本的。

Linq方法是一系列扩展方法，定义在`Enumerable`类中，扩展于`IEnumerable`接口。
`IEnumerable`接口定义了如何从数据集中遍历其中的元素。

对于返回类型是`IEnumerable`的方法可以继续调用Linq方法。
并且`IEnumerable`类型的数据可以用`foreach`遍历。

Linq方法在截取和合并时不会产生越界。
截取的方法如果数量不足则会得到空序列。
合并多个序列以最短的序列长度为准。

## 主要API介绍
### 映射
#### Select

`Select`方法传入一个委托，以转化为一个新的序列。

```csharp
string[] strings = ["hello", "world", "haha"];
var en = strings.Select(s => s.Length);
foreach (var item in en)
{
	Console.WriteLine(item);//所有字符串的长度
}
```

#### SelectMany

这个方法传入一个委托。并且要求映射的值也是一个序列。
然后新序列是映射序列的元素的序列。

```csharp
int[][] ar = new int[2][];
ar[0] = [1, 2, 3];
ar[1] = [4, 5, 6];

var ap = ar.SelectMany(s => s);//int[]也是一个序列。会展开为int
foreach (var item in ap)
{
	Console.WriteLine(item);//item是int类型。
}
```

### 查找
#### Fitst，Last，Single

查找[**第一个/最后一个/唯一一个**]符合条件的元素。

```csharp
int[] ay = [6, 8, 3, 2];
int ae = ay.First(i => i % 2 == 1);
```

#### ElementAt

获取第x个元素。

#### MaxBy,MinBy

对可以排序的序列中找到最大/最小值。

### 聚合
#### Sun,Average,Max,Min

生成这个序列的[总和/平均值/最大值/最小值]

#### Aggregate

以自定义的方式累加这个序列。

```csharp
int[] ay = [6, 8, 3, 2, 7, 5];
int p = ay.Aggregate((a, b) => a + b);
```

### 生成集合

#### ToArray,ToList，ToHashSet

以这些元素创建一个新的[**数组/列表/哈希集**]。

#### ToDictionary,ToLookup

把这些元素按指定的键创建新的[**字典/表**]。

```csharp
string[] strings = ["hello", "world!", "haha"];
Dictionary<int, string>? dic=strings.ToDictionary(s=>s.Length);
```

字典必须保证映射的键是唯一的。
而表的键可以对应多个值。

### 筛选

#### Where

根据指定的条件过滤元素。

```csharp
int[] ay = [6, 8, 3, 2];
int[] ap = ay.Where(s => s > 4).ToArray();
```

#### OfType

筛选出具有指定类型的元素

```csharp
object[] ay = [6, 'a', false];
int[] i = ay.OfType<int>().ToArray();
```

### 验证

#### Any,All

判断整个序列里是否有[任一/全部]满足条件。
若没有任何元素时，[任一]始终不满足，[全部]始终满足。

```csharp
int[] ay = [6, 8, 3, 2];
bool by = ay.Any(i => i > 4);
```

#### Contains

判断是否包含指定元素。

#### Count

计算整个序列的数量。

```csharp
int[] ay = [6, 8, 3, 2];
int cou = ay.Count();
```

### 分组

#### GroupBy

按照指定的键划分为组

```csharp
int[] ay = [6, 8, 3, 2, 7, 5];
var gro = ay.GroupBy(x => x % 2);
foreach (var item in gro)
{
	Console.WriteLine("    " + item.Key + ":");//组有一个属性表示公共的键
	foreach (var item2 in item)//组是一个序列
	{
		Console.WriteLine(item2);
	}
	Console.WriteLine("======");
}
```

#### GroupJoin

将两个序列按有关联的键进行匹配。左侧为键，右侧为组。

```csharp
Person magnus = new Person("Magnus");
Person terry = new Person("Adams");
Person charlotte = new Person("Weiss");

Pet barley = new Pet("Barley", terry);
Pet boots = new Pet("Boots", terry);
Pet whiskers = new Pet("Whiskers", charlotte);
Pet daisy = new Pet("Daisy", magnus);

Person[] people = [magnus, terry, charlotte];
Pet[] pets = [barley, boots, whiskers, daisy];

var query = people.GroupJoin(pets, person => person, pet => pet.Owner, (person, petCollection) => (person.Name, petCollection.Select(pet => pet.Name)));

foreach (var obj in query)
{
	Console.Write(obj.Name + ":");
	Console.WriteLine("[" + string.Join(", ", obj.Item2) + "]");
} 
record Person(string Name);
record Pet(string Name, Person Owner);
``` 

### 联结

#### Join

将两个序列按有关联的键进行匹配。

```csharp
int[] ay = [6, 8, 3, 2, 7, 5];
string[] sr = ["hello", "world", "haha", "red", "orange", "people"];
var gro = ay.Join(sr, a => a, s => s.Length, (a, b) => (a, b));
foreach (var item in gro)
{
	Console.WriteLine(item);
}
```

#### Zip

将两个/三个序列组合成元组的序列。

```csharp
int[] ay = [6, 8, 3, 2];
int[] aq = [3, 3, 6, 6];

foreach ((int, int) item in ay.Zip(aq))
{
	Console.WriteLine(item);
}
```

### 连接

#### Concat

连接两个序列。

#### Append

在末尾追加一个元素。

#### Prepend

在开头追加一个元素。

### 排序
#### OrderBy，OrderByDescending

将这个序列按指定的键进行[**排序/降序**]。

#### Reverse

将这个序列反转

### 截取

#### Take,TakeLast,TakeWhile

取开头的x个元素/取结尾的x个元素/取开头的元素直到不满足条件。

#### Skip,SkipLast,SkipWhile

忽略开头的x个元素/忽略结尾的x个元素/忽略开头的元素直到不满足条件。

#### Chunk

拆分为数组序列。每个数组里至多包含x个元素。

### 比较

#### SequenceEqual

比较两个序列里元素是否依次相同。

#### Union，Except，Intersect

获得两个序列的[**并集/差集/交集**]。

#### Distinct

对一个序列进行去重。

## 变种和重载

- Select，SelectMany，Where等方法的委托可以有第二个参数，表示这个元素的索引。

```csharp
int[] ay = [6, 4, 2];
foreach (var item in ay.Select((i, index) => i * index))
{
	Console.WriteLine(item);
}
```

- GroupBy 可以在分组同时映射所有元素。防止先映射元素无法分组，或分组后映射太困难。

```csharp
string[] sr = ["hello", "world", "haha", "red", "orange", "people"];
IEnumerable<IGrouping<int, char>>? sc=sr.GroupBy(s => s.Length, s => s[0]);
```

以下方法可以选填委托作为条件进行筛选。

- Any
- All
- Count
- Fitst
- Last
- Single

以下方法等效。但左侧若无法执行会报错。右侧无法执行会忽略或给出默认值。

- Fitst / FirstOrDefault
- Last / LastOrDefault
- Single / SingleOrDefault
- Cast / OfType

以下委托在不填委托时仅对数字生效。其他类型可以填写委托映射为数字。

- Max
- Min
- Sum
- Average

以下方法会在自身可以选填委托映射为键进行排序/比较。不填时依据本身排序/比较。

- MaxBy
- MinBy
- Order（映射需要改为调用`OrderBy`)
- OrderDescending（映射需要改为调用`OrderByDescending`）
- Distinct（映射需要改为调用`DistinctBy`）

## 性能说明

- 返回值为`IEnumerable`的方法返回的是一个记录操作的对象。创建时还没有执行操作。执行时会根据执行时序列的值操作。
    - `IEnumerable`的执行是按照你调用的顺序执行的。如果有截取和筛选操作，靠前可以优化性能。
- `First`,`Take`,`Any`,`All`这种方法仅需要从开头进行遍历。找到满意的值就会终止。
    - `IEnumerable`没有提供从后向前遍历的机制，所以`Last`,`Reverse`需要遍历整个序列。
	- `Single`也会查找整个序列以确保值是唯一符合条件的。
- Linq方法总是依靠遍历序列来完成操作。如果确认序列是数组，列表等可以直接从中间访问的类型，应该直接使用他们提供的访问方法。
    - 使用`for`循环依靠索引访问好过使用`Take`+`Skip`。
	- 集合一般知道自己储存了多少元素。直接访问`Count`属性或`Length`属性来获取长度，不要使用Linq的`Count`方法。
	- 仅判断是否至少包含多少个元素时，使用`Task`和不带条件的`Any`进行判断，而不是使用`Count`方法。

## 并行Linq

请参阅[这里](https://learn.microsoft.com/zh-cn/dotnet/standard/parallel-programming/introduction-to-plinq)。

对序列调用`AsParallel`方法后会获得`ParallelQuery`类。
`ParallelEnumerable`类提供了针对`ParallelQuery`的扩展方法。

- 并行Linq可以使用多线程进行查询，加快速度
    - 但发布任务至线程本身需要时间。如果任务量或数据量过小，可能不能回本。
- 并行Linq不能保证顺序，不能依赖和顺序有关的API。
    - `AsOrdered`方法可以保留顺序，但也会相应地降低性能。
	- 不再需要保留顺序时可以用`AsUnordered`方法。
- 对于由多个序列参与的方法，并行Linq的其他序列参数应该也是`ParallelQuery`。