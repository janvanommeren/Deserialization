using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deserialization
{
    public class XmlSerial
    {
        private XmlSerializer serializer;
        private XmlModel model;
        private String outputPath = "../../output/output.xml";

        public XmlSerial()
        {
            this.serializer = new XmlSerializer(typeof(XmlModel));
            this.model = new XmlModel(1, "Jean le Python");
        }

        public void SerializeDatatoFile()
        {
            using(FileStream fs = File.OpenWrite(outputPath))
            {
                serializer.Serialize(fs, model);
            }
        }

        public XmlModel DeserializeDataFromFile()
        {
            using (FileStream fs = File.OpenRead(outputPath))
            {
                return this.serializer.Deserialize(fs) as XmlModel; 
            } 
        }

        public String GetOutputPath()
        {
            return this.outputPath;
        }

        public XmlModel GetModel()
        {
            return this.model;
        }
    }

    public class XmlModel
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public XmlModel() { }

        public XmlModel(int id, String name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
