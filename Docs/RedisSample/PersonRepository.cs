// namespace Utilities_aspnet.Docs.RedisSample;
//
// public interface IPersonRepository {
//     void Create(PersonEntity dto);
//     PersonEntity? GetById(string id);
//     IEnumerable<PersonEntity> Get();
// }
//
// public class PersonRepositoryRedis : IPersonRepository {
//     private readonly IDatabase _db;
//
//     public PersonRepositoryRedis(IConnectionMultiplexer redis) {
//         _db = redis.GetDatabase();
//     }
//
//     public void Create(PersonEntity platform) {
//         if (platform == null) throw new ArgumentException("", nameof(platform));
//
//         string serialPlatform = JsonSerializer.Serialize(platform);
//         _db.HashSet("hashPlatform", new[] {new HashEntry(platform.Id, serialPlatform)});
//     }
//
//     public IEnumerable<PersonEntity> Get() {
//         HashEntry[]? data = _db.HashGetAll("hashPlatform");
//         IEnumerable<PersonEntity> obj = Array.ConvertAll(data, val => JsonSerializer.Deserialize<PersonEntity>(val.Value)).ToList()!;
//         return obj;
//     }
//
//     public PersonEntity? GetById(string id) {
//         RedisValue i = _db.HashGet("hashPlatform", id);
//         return JsonSerializer.Deserialize<PersonEntity>(i);
//     }
// }

