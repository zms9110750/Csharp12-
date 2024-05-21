# 任务

## Task

### 创建一个已经完成的任务

以下静态方法/属性可以创建一个已经完成的任务，并设置输出为值/异常/取消。

- Task.FromException<T>()
- Task.FromException()
- Task.FromCanceled<T>()
- Task.FromCanceled()
- Task.FromResult<T>()
- Task.CompletedTask

### 创建延续任务

`ContinueWith`方法可以创建一个任务。当依赖的任务完成时才启动。
创建延续任务的委托是依赖任务的整个`Task`而不是仅一个完成的值。
因为任务可能取消或异常。这些信息都储存在`Task`里。

```csharp
Task task1 = Task.Delay(1000);

Task<int> task2 = task1.ContinueWith(t =>
{
	Console.WriteLine(t.Status);
});
```

### 创建合并任务

静态方法`WhenAll`和`WhenAny`可以创建多个任务组合的任务。

其中，`WhenAll`方法假设所有任务顺利完成，因此泛型是一个数组。
如果任何组成的任务出现错误或取消，那么整个任务是错误或取消。

`WhenAny`则只返回最快完成的任务，并且是整个任务。因此泛型就是带`Task`的。

```csharp
Task<int>? task1 = Task.Delay(1000).ContinueWith(t => 6);
Task<int>? task2 = Task.Delay(500).ContinueWith(t => 3);
Task<int[]>? task3 = Task.WhenAll(task1, task2);
Task<Task<int>>? task4 = Task.WhenAny(task1, task2);
```

### Task的状态

`Task`的状态可以访问`Status`属性查看。这是一个枚举，有以下值：

- `Created`:已经创建
- `WaitingForActivation`:激活但等待进入计划队列
- `WaitingToRun`:在计划队列中等待执行
- `Running`:运行中
- `WaitingForChildrenToComplete`:等待附加任务完成
- `RanToCompletion`:顺利完成
- `Canceled`:取消
- `Faulted`:异常

还有有一些属性可以快速判断是否处于上述某种状态：

- `IsCompleted`：已经完成。（不要求顺利完成）
- `IsCompletedSuccessfully `：成功完成
- `IsFaulted`：失败
- `IsCanceled `：取消

在任务完成时可以访问下列属性：

- `Result`：成功完成的结果值
- `Exception`：导致失败的异常。

取消也是一种异常。是否是因取消而结束只是判断异常是否属于取消导致的异常。

### Task的阻塞

在任务没有完成时，调用`Result`获取结果，或调用方法`Wait`，或
调用静态方法`WaitAll`，`WaitAny`会阻塞当前线程。

阻塞发生就可能导致死锁。

### 超时

虽然`Wait`会导致阻塞，但`WaitAsync`会返回一个新的任务。并且可以加个时间表示超时。
如果时间内没完成，那么会得到一个超时的异常。

```csharp
var t1 = Task.Delay(3000);
var t2 = t1.WaitAsync(TimeSpan.FromSeconds(1));
await t2;
Console.WriteLine(t2.Status);
```

## TaskCompletionSource

`TaskCompletionSource`是一个任务生成器。可以在你想要的时候让任务完成。
把`Task`属性给外部访问让外部等待。

```csharp
TaskCompletionSource<int> ts = new TaskCompletionSource<int>();
Task<int>? task = ts.Task; 
ts.SetException(new Exception());
ts.SetCanceled();
ts.SetResult(1);
```

## CancellationToken

大部分异步任务都可以接受一个`CancellationToken`的可选参数用于取消。
**取消令牌**需要创建**取消令牌源**来获得。

接收者拿到取消令牌后可以从`Register`方法注册一个取消时执行的委托。
在循环时可以以**是否已经取消**作为条件。

布置好后可以调用**取消令牌源**上的**取消**方法。

```csharp
CancellationTokenSource source = new CancellationTokenSource();
_ = Task.Delay(3000).ContinueWith(t =>
{
	source.Cancel();
});
await Hello(source.Token);

async Task Hello(CancellationToken token = default)
{
	var registration = token.Register(() => Console.WriteLine("从外部取消了"));//注册取消时的回调
	int i = 0;
	while (!token.IsCancellationRequested)//现在有没有取消
	{
		Console.WriteLine(i++);
		await Task.Yield();
	}
	registration.Unregister();//任务完成，不再需要观察令牌了
	token.ThrowIfCancellationRequested();//如果取消，引发异常
}
```

取消令牌做成结构是为了可以用`default`做默认值时也可以访问内容。
取消令牌保存着创建自己的令牌源。从`default`而来的令牌这项值为`null`。
取消令牌的`CanBeCanceled`属性用于判断自己的源是不是`null`（是不是从`default`来的）.

取消令牌源的构造器中可以添加一个时间，一定时间后取消。
取消方法也可以加一个时间表示一定时间后取消。
取消令牌源有一个静态方法，可以观察一个或多个取消令牌。当任一令牌取消时，自己也取消。

```csharp
CancellationTokenSource source = new CancellationTokenSource(TimeSpan.FromSeconds(3));
var source2=CancellationTokenSource.CreateLinkedTokenSource(source.Token);
source2.CancelAfter(TimeSpan.FromSeconds(1));
```
