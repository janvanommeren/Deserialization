using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Runtime.Serialization;

namespace DeserializationTest
{
    [TestClass]
    public class FindDotNetStuff
    {
        static bool HasAttribute(MemberInfo mi, Type attrType)
        {
            return mi.GetCustomAttributes(attrType, false).Length > 0;
        }

        [TestMethod]
        public void FindSerializableTypes()
        {
            Assembly asm = null;
            //get all project assemblies, loop over them and print stuff
            foreach (Type t in asm.GetTypes())
            {
                if (!t.IsAbstract && !t.IsEnum && t.IsSerializable)
                {
                    if (typeof(ISerializable).IsAssignableFrom(t))
                    {
                        Console.WriteLine("ISerializable {0}", t.FullName);
                    }
                    if (typeof(IObjectReference).IsAssignableFrom(t))
                    {
                        Console.WriteLine("IObjectReference {0}", t.FullName);
                    }
                    if (typeof(IDeserializationCallback).IsAssignableFrom(t))
                    {
                        Console.WriteLine("IDeserializationCallback {0}", t.FullName);
                    }
                    foreach (MethodInfo m in t.GetMethods(BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (HasAttribute(m, typeof(OnDeserializingAttribute)))
                        {
                            Console.WriteLine("OnDeserializing {0}", t.FullName);
                        }
                        if (HasAttribute(m, typeof(OnDeserializedAttribute)))
                        {
                            Console.WriteLine("OnDeserialized {0}", t.FullName);
                        }
                        if (m.Name == "Finalize" && m.DeclaringType != typeof(object))
                        {
                            Console.WriteLine("Finalizable {0}", t.FullName);
                        }
                    }
                }
            }
        }
    }
}
