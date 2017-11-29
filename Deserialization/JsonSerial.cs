using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Deserialization
{
    public class JsonSerial
    {
        public DataContractModel Model { get; }
        private DataContractJsonSerializer Serializer;
        public String OutputPath { get; }

        public JsonSerial()
        {
            this.Model = new DataContractModel("Jean le Python");
            this.Serializer = new DataContractJsonSerializer(typeof(DataContractModel));
            this.OutputPath = "../../output/datacontract.json";
        }

        public void SerializeDataContract()
        {
            using (FileStream fs = File.OpenWrite(this.OutputPath))
            {
                this.Serializer.WriteObject(fs, this.Model);
            }
        }

        public DataContractModel DeserializeDataContract()
        {
            using (FileStream fs = File.OpenRead(this.OutputPath))
            {
                return this.Serializer.ReadObject(fs) as DataContractModel;
            }
        }
    }

    [DataContract]
    public class DataContractModel
    {
        [DataMember]
        public String Name { get; set; }

        public DataContractModel() { }
        public DataContractModel(String name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
