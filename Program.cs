using System;

class Program
{
	static void Main(string[] args)
	{
		// 初始化敌人和你的位置
		Vector2 enemyPosition = new Vector2(500, 500);
		Vector2 yourPosition = Vector2.Zero;

		// 初始化加速度上限
		float accelerationLimit = 200;

		// 计算敌人位置与你的位置之间的角度
		float theta = CalculateAngleBetweenVectors(enemyPosition);

		// 将加速度分解为水平和垂直分量
		float ax, ay;
		DecomposeAccelerationIntoComponents(accelerationLimit, theta, out ax, out ay);

		// 在此处，你可以使用 ax 和 ay 对物体进行加速
		// 例如，如果你有一个名为 "yourObject" 的对象，你可以这样做：
		// yourObject.Acceleration = new Vector2(ax, ay);

		// 一些输出，以便我们可以看到计算出的加速度分量
		Console.WriteLine($"水平加速度: {ax}");
		Console.WriteLine($"垂直加速度: {ay}");
	}

	static float CalculateAngleBetweenVectors(Vector2 targetDirection)
	{
		return (float)Math.Atan2(targetDirection.Y, targetDirection.X);
	}

	static void DecomposeAccelerationIntoComponents(float accelerationLimit, float angle, out float ax, out float ay)
	{
		ax = accelerationLimit * MathF.Cos(angle);
		ay = accelerationLimit * MathF.Sin(angle);
	}
}

struct Vector2(float x, float y)
{
	public float X = x, Y = y;

	public static Vector2 Zero => new Vector2(0, 0);
}