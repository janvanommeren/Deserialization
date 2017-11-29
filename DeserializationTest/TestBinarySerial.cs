using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Deserialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeserializationTest
{
    [TestClass]
    public class TestBinarySerial
    {
        [TestMethod]
        public void TestBinarySerialize()
        {
            BinarySerial serializer = new BinarySerial();
            serializer.SerializeBinaryData();
            Assert.IsTrue(File.Exists(serializer.GetOutputPath()));
        }

        [TestMethod]
        public void TestBinaryDeserialize()
        {
            BinarySerial serializer = new BinarySerial();
            BinaryModel model = serializer.DeserializeBinaryData();
            Assert.AreEqual(model.Commmand, serializer.GetModel().Commmand);
        }

        [TestMethod]
        public void TestSerializeTempFileCollection()
        {
            if(!Directory.Exists("C:/testdir"))
            {
                Directory.CreateDirectory("C:/testdir");
            }
            String testFile = "C:/testdir/test.txt";
            if(!File.Exists(testFile))
            {
                File.Create(testFile);
            }

            String outputPath = "../../output/tempfilecollection.stm";
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = File.OpenWrite(outputPath))
            {
                System.CodeDom.Compiler.TempFileCollection tempFileCollection = 
                    new System.CodeDom.Compiler.TempFileCollection(@"C:\testdir", false);
                tempFileCollection.AddFile(@"C:\testdir\test.txt", false);
                formatter.Serialize(fs, tempFileCollection);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void TestDeserializeFileCollection()
        {
            String outputPath = "../../output/tempfilecollection.stm";
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = File.OpenRead(outputPath))
            {
                var data = (String)formatter.Deserialize(fs);
            }
            
        }

        //[TestMethod]
        //public void SerializeDos()
        //{
        //    String outputPath = "../../output/dos.stm";
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    using (FileStream fs = File.OpenWrite(outputPath))
        //    {
        //        formatter.Serialize(fs, new BinaryDosModel("join the dark side"));
        //    }
        //}

        //[TestMethod]
        //public void DeserializeDos()
        //{
        //    String outputPath = "../../output/dos.stm";
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    using (FileStream fs = File.OpenRead(outputPath))
        //    {
        //        IDisposable data = (IDisposable)formatter.Deserialize(fs);
        //        data.Dispose();
        //    }
        //}
    }
}
