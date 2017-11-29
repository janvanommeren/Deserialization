using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Deserialization
{
    public class BinarySerial
    {
        private BinaryFormatter formatter;
        private BinaryModel model;
        private String outputPath = "../../output/output.stm";

        public BinarySerial()
        {
            this.formatter = new BinaryFormatter();
            this.model = new BinaryModel("Jean le Python");
        }

        public void SerializeBinaryData()
        {
            using(FileStream fs = File.OpenWrite(this.outputPath))
            {
                this.formatter.Serialize(fs, this.model);
            }
        }

        public BinaryModel DeserializeBinaryData()
        {
            using(FileStream fs = File.OpenRead(this.outputPath))
            {
                return this.formatter.Deserialize(fs) as BinaryModel;
            }
        }

        public String GetOutputPath()
        {
            return this.outputPath;
        }

        public BinaryModel GetModel()
        {
            return this.model;
        }
    }

    [Serializable]
    public class BinaryModel : IDisposable
    {
        public String Commmand { get; set; }

        public BinaryModel() { }
        public BinaryModel(String name)
        {
            this.Commmand = name;
        }

        public void Dispose()
        {
            this.Commmand = "";
        }
    }

    [Serializable]
    public class BinaryDosModel
    {
        public String Commmand { get; set; }

        public BinaryDosModel() { }
        public BinaryDosModel(String name)
        {
            this.Commmand = name;
        }
    }
}
