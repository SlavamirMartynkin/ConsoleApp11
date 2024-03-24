using System.Text.Json;
using System.Xml;

namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonStr = "{\"name\":\"Слава\",\"age\":38,\"city\":\"СПБ\"}";

            var json = JsonDocument.Parse(jsonStr);

            var xml = new XmlDocument();
            var xmlRoot = xml.CreateElement("root");
            xml.AppendChild(xmlRoot);

            ConvertJson2Xml(json.RootElement, xmlRoot);
            Console.WriteLine(xml.OuterXml);
        }

        static void ConvertJson2Xml(JsonElement jsonElement, XmlElement xmlParent)
        {
            foreach (var prop in jsonElement.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    var xmlElement = xmlParent.OwnerDocument.CreateElement(prop.Name);
                    xmlParent.AppendChild(xmlElement);
                    ConvertJson2Xml(prop.Value, xmlElement);
                }
                else
                {
                    var xmlElement = xmlParent.OwnerDocument.CreateElement(prop.Name);
                    xmlElement.InnerText = prop.Value.ToString();
                    xmlParent.AppendChild(xmlElement);
                }
            }
        }
    }
}
