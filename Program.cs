using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

string s = """
{
  "name": "John Doe",
  "age": 30,
  "email": "john.doe@example.com",
  "phoneNumbers": [
    "212-555-1234",
    "646-555-4567"
  ]
}
""";

Person Person = new Person();

var jo = JObject.Parse(s);
var jo2 = JObject.FromObject(Person);

var name = jo["name"];
var phone = jo["phoneNumbers"][1];


var per = jo.ToObject<Person>();
var phone2 = jo["phoneNumbers"].Values<string>();
var phone3 = (string)jo["phoneNumbers"][0];


jo["hello"] = new JObject(new JProperty("world", new JArray(1, 2, 3)));
Console.WriteLine(per.age);


Console.WriteLine(jo);
Console.WriteLine(jo2);


public class Person
{
	public string name { get; set; }
	public int age { get; set; }
	public string email { get; set; }
	public string[] phoneNumbers { get; set; }
}
