﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Extensions;

namespace Utilities.Tests.Extensions
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private readonly IDictionary<String, String> _dictionary1 = new Dictionary<String, String>()
        {
            { "1" , "One" },
            { "2" , "Two" },
            { "3" , "Three" },
            { "4" , "Four" },
            { "5" , "Five" },
            { "6" , "Six" },
            { "7" , "Seven" },
            { "8" , "Eight" },
            { "9" , "Nine" },
            { "10" , "Ten" }
        };
        private readonly String _keyValueString1 = "1=One;2=Two;3=Three;4=Four;5=Five;6=Six;7=Seven;8=Eight;9=Nine;10=Ten";
        private readonly String _keyValueString2 = "1=ONE,2=TWO,3=THREE,4=FOUR,5=FIVE,6=SIX,7=SEVEN,8=EIGHT,9=NINE,TEN=TEN";

        [TestMethod]
        public void ConvertDictionaryToKeyValueString()
        {
            Func<String, String> normalizeKeyFunc = s => s.Replace("10", "TEN");
            Func<String, String> normalizeValueFunc = s => s.ToUpper();

            Assert.AreEqual(_keyValueString1, _dictionary1.ToKeyValueString());
            Assert.AreEqual(_keyValueString2, _dictionary1.ToKeyValueString(",", "=", normalizeKeyFunc, normalizeValueFunc));
        }

        [TestMethod]
        public void ConvertKeyValueStringToDictionary()
        {
            Func<String, String> normalizeKeyFunc = s => s.Replace("TEN", "10");
            Func<String, String> normalizeValueFunc = s => new CultureInfo("en").TextInfo.ToTitleCase(s.ToLower());

            CollectionAssert.AreEqual((ICollection)_dictionary1, (ICollection)_keyValueString1.ToDictionary());
            CollectionAssert.AreEqual((ICollection)_dictionary1, (ICollection)_keyValueString2.ToDictionary(",", "=", normalizeKeyFunc, normalizeValueFunc));
        }

        [TestMethod]
        public void GetOrDefault()
        {
            var source = new Dictionary<string, string>
            {
                { "1", "A" },
                { "2", "B" },
                { "3", "C" },
                { "4", "D" },
                { "5", "E" }
            };

            Assert.AreEqual("A", source.GetOrDefault("1"));
            Assert.AreEqual("B", source.GetOrDefault("2"));
            Assert.AreEqual(null, source.GetOrDefault("6"));
        }

        [TestMethod]
        public void GetOrAdd()
        {
            var source = new Dictionary<string, string>
            {
                { "1", "A" },
                { "2", "B" },
                { "3", "C" },
                { "4", "D" },
                { "5", "E" }
            };

            Assert.AreEqual("A", source.GetOrAdd("1", _ => "F"));
            Assert.AreEqual("B", source.GetOrAdd("2", _ => "J"));
            Assert.AreEqual("H", source.GetOrAdd("6", _ => "H"));
        }
    }
}
