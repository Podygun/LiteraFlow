using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteraFlowTest
{
    public class CacheTest : BaseTest
    {
        [Test]
        public void CreateKeyTest()
        {
            const string key = "key1_test";
            int value = 500;
            cache.Set(key, value);
            int valueFromCache = cache.GetOrNull<int>(key);  
            Assert.AreEqual(value, valueFromCache);
        }

        [Test]
        public void DeleteValueTest()
        {
            const string key = "key2_test";
            int value = 500;

            cache.Set(key, value);
            int valueFromCache = cache.GetOrNull<int>(key);
            Assert.AreEqual(value, valueFromCache);

            cache.Remove(key);
            int? valueFromCacheAfterRemove = cache.GetOrNull<int?>(key);
            Assert.IsNull(valueFromCacheAfterRemove);

        }



    }
}
