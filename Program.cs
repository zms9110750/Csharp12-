


MyStruct myStruct = new MyStruct("", 12);
myStruct.Age = 20;//外部只能访问允许访问的东西

struct MyStruct(string name, int age)
{
	string Name = name;
	public int Age = age;

	void Hello()
	{
		Console.WriteLine(Name);//类型内可以访问成员变量
	}

	struct Struct2()
	{
		void Hello(MyStruct my)
		{
			Console.WriteLine(my.Name);//嵌套的类型也可以访问到外部成员。
		}
	}
}




