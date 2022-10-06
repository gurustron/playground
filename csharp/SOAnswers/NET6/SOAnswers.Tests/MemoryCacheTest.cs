using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace SOAnswers.Tests;

public class MemoryCacheTest
{
    public class MemoryCacheManagement
    {
        private MemoryCache Cache { get; set; }
    
        public MemoryCacheManagement()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 5
            });
        }
    
        public void SetValue<T>(string key, T value)
        {
            Cache.Set(key, value, new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetSlidingExpiration(TimeSpan.FromMinutes(50))
            );
        }
    }    
    
    [Test]
    public void Test1()
    {
        var cache = new MemoryCacheManagement();

        User[]? users = new User[]
        {
            new User { Id = 1, Name = "User1", Age = 20 },
            new User { Id = 2, Name = "User2", Age = 21 },
            new User { Id = 3, Name = "User3", Age = 22 },
            new User { Id = 4, Name = "User4", Age = 21 },
            new User { Id = 5, Name = "User5", Age = 22 },
            new User { Id = 6, Name = "User6", Age = 21 },
            new User { Id = 1, Name = "User1", Age = 20 },
            new User { Id = 7, Name = "User7", Age = 22 },
            new User { Id = 2, Name = "User2", Age = 21 },
            new User { Id = 3, Name = "User3", Age = 22 },
            new User { Id = 2, Name = "User2", Age = 21 },
            new User { Id = 3, Name = "User3", Age = 22 }
        };

        foreach (var t in users)
        {
            cache.SetValue(t.Id.ToString(), t);
        }

        var x = cache;
    }
    
    [DebuggerDisplay("{Id} - {Name} - {Age}")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
