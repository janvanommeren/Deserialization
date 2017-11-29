using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Not enough args[].");
                return;
            }
            if (args[0].Equals("serialize"))
            {
                if (args[1].Equals("xml"))
                {
                    new XmlSerial().SerializeDatatoFile();
                }
                else if (args[1].Equals("binary"))
                {
                    new BinarySerial().SerializeBinaryData();
                }
                else if (args[1].Equals("json"))
                {
                    new JsonSerial().SerializeDataContract();
                }
            }
            else
            {
                if (args[1].Equals("xml"))
                {
                    XmlModel model = new XmlSerial().DeserializeDataFromFile();
                    Console.WriteLine(model.ToString());
                }
                else if (args[1].Equals("binary"))
                {
                    BinaryModel model = new BinarySerial().DeserializeBinaryData();
                    Console.WriteLine(model.ToString());
                }
                else if (args[1].Equals("json"))
                {
                    DataContractModel model = new JsonSerial().DeserializeDataContract();
                    Console.WriteLine(model.Name);
                }
            }
        }
    }
}
