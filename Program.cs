﻿using System;

try
{
	Student st = new Student();
	st.Score = -3;
}
catch (ScoreException e)//捕获异常
{
	Console.WriteLine(e.Message);
}
catch when (Random.Shared.Next(6) > 3)//次要判断
{

}
catch
{
	throw;//把捕获到的异常直接抛出
}
finally
{
	Console.WriteLine("无论有没有异常，都会执行");
}
using var ms = new MemoryStream();
//在脱离作用域时，自动调用他身上的释放方法。
//ms.Dispose();


class ScoreException : Exception//自定义异常类
{
	public ScoreException() : base() { }
	public ScoreException(string message) : base(message) { }
	public ScoreException(string message, Exception inner) : base(message, inner) { }
}
class Student
{
	int score;
	public int Score
	{
		get => score;
		set
		{
			if (value < 0)
				throw new ScoreException("分数不能是负数");//抛出异常
			score = value;
		}
	}
}
