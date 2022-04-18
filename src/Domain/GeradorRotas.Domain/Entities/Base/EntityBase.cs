using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GeradorRotas.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        //[BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
