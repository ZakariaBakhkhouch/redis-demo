using Microsoft.AspNetCore.Connections;
using RedisDemoApp.Dtos;
using RedisDemoApp.Models;
using StackExchange.Redis;
using System;
using System.Text.Json;

namespace RedisDemoApp.Repository
{
    public class PlatfomRepository : IPlatfomRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public PlatfomRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task AddPlatform(Platform platform)
        {
            if(platform is null)
            {
                throw new ArgumentException(nameof(platform));
            }

            var db = _connectionMultiplexer.GetDatabase();

            var serializePlatform = JsonSerializer.Serialize(platform);

            //await db.StringSetAsync(platform.Id.ToString(), serializePlatform);

            await db.HashSetAsync($"hashplatform", new HashEntry[] { new HashEntry(platform.Id.ToString(), serializePlatform) });

        }
       
        public async Task<List<Platform>> GetAllPlatforms()
        {
            var db = _connectionMultiplexer.GetDatabase();

            var completeSet = await db.HashGetAllAsync("hashplatform");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                
                if (obj is not null && obj.Count > 0)
                {
                    return obj;
                }
            }

            return null;

        }

        public async Task<Platform> GetPlatform(Guid id)
        {
            var db = _connectionMultiplexer.GetDatabase();

            //var plat = await db.StringGetAsync(id.ToString());

            var plat = await db.HashGetAsync("hashplatform", id.ToString());

            var result = JsonSerializer.Deserialize<Platform>(plat);

            if (result is not null)
            {
                return result;
            }

            return null;
        }

        public async Task<bool> DeletePlatform(Guid id)
        {
            var db = _connectionMultiplexer.GetDatabase();

            var result = await db.HashDeleteAsync("hashplatform", id.ToString());

            return result;
        }
        
        
        //public async Task<bool> UpdatePlatform(Platform platform, Guid id)
        //{
        //    var db = _connectionMultiplexer.GetDatabase();

        //    var plat = await db.HashGetAsync("hashplatform", id.ToString());

        //    var serializePlatform = JsonSerializer.Serialize(plat);

        //    await db.HashSetAsync("hashplatform", plat);

        //    return result;
        //}

    }
}
