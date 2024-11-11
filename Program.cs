

 
foreach (var item in new Test())
{
	Console.WriteLine("在foreach循环中输出"+item);
}

class Test
{
	public IEnumerator<int> GetEnumerator()
	{
		for (int i = 0; i < 5; i++)
		{
			Console.WriteLine("在迭代器方法里输出" + i);
			yield return i;
		}
	}
}