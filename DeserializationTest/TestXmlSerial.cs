using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Deserialization;
using System.IO;

namespace DeserializationTest
{
    [TestClass]
    public class TestXmlSerial
    {
        [TestMethod]
        public void TestXmlSerialize()
        {
            XmlSerial serial = new XmlSerial();
            serial.SerializeDatatoFile();
            Assert.IsTrue(File.Exists(serial.GetOutputPath()));
        }

        [TestMethod]
        public void TestXmlDeserialize()
        {
            XmlSerial serial = new XmlSerial();
            XmlModel model = serial.DeserializeDataFromFile();
            Assert.AreEqual(model.Name, serial.GetModel().Name);
            Assert.AreEqual(model.Id, serial.GetModel().Id);
        }
    }
}
