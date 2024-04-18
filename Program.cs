
 

Console.WriteLine("准备调用异步方法");
var taskHello = Tool.HelloAsync();
Console.WriteLine("回到调用方法处了");
await taskHello;
/*
准备调用异步方法
异步方法开始了
191
回到调用方法处了
250
171
0
异步方法结束了
*/

static class Tool
{
	public static async Task HelloAsync()
	{
		Console.WriteLine("异步方法开始了");
		await foreach (var item in 0xabfabf.Iteration())
		{
			Console.WriteLine(item);
		}
		Console.WriteLine("异步方法结束了");
	}

	public static async IAsyncEnumerable<byte> Iteration(this int Int32)
	{
		for (int i = 0; i < 4; i++)
		{
			yield return (byte)(Int32 >> (i * 8));
			await Task.Delay(300);
		}
	}
}
