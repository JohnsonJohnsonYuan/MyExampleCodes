﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDBIntIdGenerator.Tests.Stubs;
using NUnit.Framework;

namespace MongoDBIntIdGenerator.Tests
{
    [TestFixture]
    public class Int64IdGeneratorTest
    {
        private IMongoDatabase _db;

        [SetUp]
        public void TestFixtureSetUp()
        {
            _db = new MongoClient("mongodb://localhost/?safe=true")
                .GetDatabase("test");
            
            _db.DropCollection("IdInt64");
            _db.DropCollection("testEntities");
        }
        [Test]
        public void Saving_Item_Has_Id_Of_1()
        {
            var item = new StubInt64Entity { Name = "Testing" };

            _db.GetCollection<StubInt64Entity>("testEntities").InsertOne(item);

            Assert.AreEqual(1, item.Id);
        }

        [Test]
        public void When_Saving_2_Items_Second_Item_Has_Id_Of_2()
        {
            var item1 = new StubInt64Entity { Name = "Testing" };
            var item2 = new StubInt64Entity { Name = "Testing 2" };

            _db.GetCollection<StubInt64Entity>("testEntities").InsertOne(item1);
            _db.GetCollection<StubInt64Entity>("testEntities").InsertOne(item2);

            Assert.AreEqual(2, item2.Id);
        }

        [Test]
        public void Save_Batch_Of_Items()
        {
            var items = new List<StubInt64Entity>();

            for (int i = 0; i < 1000; i++)
                items.Add(new StubInt64Entity { Name = "Item " + i });

            _db.GetCollection<StubInt64Entity>("testEntities").InsertMany(items);

            for (var i = 1; i < 1001; i++)
                Assert.That(items.Select(x => x.Id).Contains(i));
        }
    }
}
