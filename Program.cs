Console.WriteLine("准备调用异步方法");
HelloAsync();
Console.WriteLine("回到调用方法处了");


async Task HelloAsync()
{
	Console.WriteLine("异步方法开始了");
	await Task.Yield();
	Console.WriteLine("异步方法结束了");
}