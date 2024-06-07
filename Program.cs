
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

string s = """
<?xml version="1.0" encoding="UTF-8"?>
<person>
    <name>John Doe</name>
    <age>30</age>
    <email>john.doe@example.com</email>
    <phoneNumbers>
        <phoneNumber type="home">212-555-1234</phoneNumber>
        <phoneNumber type="work">646-555-4567</phoneNumber>
    </phoneNumbers>
</person>
""";

var p=P.DeSerialize<person>(s);






public class person
{
	public string name { get; set; }
	public int age { get; set; }
	public string email { get; set; }
	public string[] phoneNumbers { get; set; }
}
class P
{
	public static string Serialize<T>(T value)
	{
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringWriter sw = new StringWriter();
		xml.Serialize(sw, value);
		return sw.ToString();
	}
	/// <summary>将对象序列化为xml节点</summary>
	public static XElement Serialize(object value)
	{
		XmlSerializer xml = new XmlSerializer(value.GetType());
		using MemoryStream ms = new MemoryStream();
		xml.Serialize(ms, value);
		ms.Position = 0;
		return XElement.Load(ms);
	}
	/// <summary>将xml字符串反序列化</summary>
	public static T? DeSerialize<T>(string value)
	{
		XmlSerializer xml = new XmlSerializer(typeof(T));
		StringReader sr = new StringReader(value);
		return (T?)xml.Deserialize(sr);
	}
}