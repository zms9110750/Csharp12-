var t1 = Task.Delay(3000);
var t2 = t1.WaitAsync(TimeSpan.FromSeconds(1));
await t2;
Console.WriteLine(t2.Status);

