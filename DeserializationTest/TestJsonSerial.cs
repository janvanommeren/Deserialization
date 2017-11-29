using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Deserialization;
using System.IO;

namespace DeserializationTest
{
    [TestClass]
    public class TestJsonSerial
    {
        [TestMethod]
        public void TestJsonSerialization()
        {
            JsonSerial serializer = new JsonSerial();
            serializer.SerializeDataContract();
            Assert.IsTrue(File.Exists(serializer.OutputPath));
        }

        [TestMethod]
        public void TestJsonDeserialization()
        {
            JsonSerial serializer = new JsonSerial();
            DataContractModel model = serializer.DeserializeDataContract();
            Assert.AreEqual(model.Name, serializer.Model.Name);
        }

        [TestMethod]
        public void TestGenerateJsonTempFileColl()
        {
            
        }
    }
}
