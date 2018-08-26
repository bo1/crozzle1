using System;
using CrozzleApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        // Validator.IsBoolean
        [TestMethod]
        public void TestValidatorIsBoolean1()
        {
            Boolean value;
            Boolean result = Validator.IsBoolean("TRUE", out value);
            Assert.IsTrue(result);
            Assert.IsTrue(value);
        }

        [TestMethod]
        public void TestValidatorIsBoolean2()
        {
            Boolean value;
            Boolean result = Validator.IsBoolean("FALSE", out value);
            Assert.IsTrue(result);
            Assert.IsFalse(value);
        }

        [TestMethod]
        public void TestValidatorIsBoolean3()
        {
            Boolean value;
            Boolean result = Validator.IsBoolean("ERROR", out value);
            Assert.IsFalse(result);
        }

        // Validator.IsInt32
        [TestMethod]
        public void TestValidatorIsInt321()
        {
            int value;
            Boolean result = Validator.IsInt32("100", out value);
            Assert.IsTrue(result);
            Assert.AreEqual(value, 100);
        }

        [TestMethod]
        public void TestValidatorIsInt322()
        {
            int value;
            Boolean result = Validator.IsInt32("-99999", out value);
            Assert.IsTrue(result);
            Assert.AreEqual(value, -99999);
        }

        [TestMethod]
        public void TestValidatorIsInt323()
        {
            int value;
            Boolean result = Validator.IsInt32("ERROR", out value);
            Assert.IsFalse(result);
        }

        // Validator.IsHexColourCode
        [TestMethod]
        public void TestValidatorIsHexColourCode1()
        {
            int value;
            Boolean result = Validator.IsHexColourCode("#00A3F9");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestValidatorIsHexColourCode2()
        {
            int value;
            Boolean result = Validator.IsHexColourCode("#FFFFFF");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestValidatorIsHexColourCode3()
        {
            int value;
            Boolean result = Validator.IsHexColourCode("ABCDEFG");
            Assert.IsFalse(result);
        }

        // KeyValue.TryParse
        [TestMethod]
        public void TestKeyValueTryParse1()
        {
            KeyValue kv;
            Boolean result = KeyValue.TryParse("A=2", "A", out kv);
            Assert.IsTrue(result);
            Assert.AreEqual(kv.Key, "A");
            Assert.AreEqual(kv.Value, "2");
        }

        [TestMethod]
        public void TestKeyValueTryParse2()
        {
            KeyValue kv;
            Boolean result = KeyValue.TryParse("Secret=Password", "Secret", out kv);
            Assert.IsTrue(result);
            Assert.AreEqual(kv.Key, "Secret");
            Assert.AreEqual(kv.Value, "Password");
        }

        [TestMethod]
        public void TestKeyValueTryParse3()
        {
            KeyValue kv;
            Boolean result = KeyValue.TryParse("Key:Value", "Key", out kv);
            Assert.IsFalse(result);
        }

        // #5 Crozzle.ToStringHTML

        // #6 CrozzleMap.GroupCount

    }
}
