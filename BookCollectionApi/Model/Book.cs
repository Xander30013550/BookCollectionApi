using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace BookCollectionApi.Model
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Book")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("Author(s)")]
        public string Author { get; set; } = string.Empty;

        [BsonElement("Original language")]
        public string Language { get; set; } = string.Empty;

        [BsonElement("First published")]
        public int PublishDate { get; set; }

        [BsonElement("Approximate sales in millions")]
        public double Sales { get; set; }

        [BsonElement("Genre")]
        public string Genre { get; set; } = string.Empty;
    }
}