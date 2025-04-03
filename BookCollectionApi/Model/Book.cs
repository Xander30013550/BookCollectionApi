using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookCollectionApi.Model
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("bookID")]
        public int BookID { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("authors")]
        public string Author { get; set; } = string.Empty;

        [BsonElement("average_rating")]
        public double AverageRating { get; set; }

        [BsonElement("language_code")]
        public string Language { get; set; } = string.Empty;

        [BsonElement("ratings_count")]
        public int RatingCount { get; set; }

        [BsonElement("text_reviews_count")]
        public int TextReviewCount { get; set; }

        [BsonElement("publication_date")]
        public string PublishDate { get; set; } = string.Empty;

        [BsonElement("publisher")]
        public string Publisher { get; set; } = string.Empty;
    }
}