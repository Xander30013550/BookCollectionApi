using BookCollectionApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace BookCollectionApi.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _books;

    public BookService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _books = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetBooks([FromQuery] BookQueryParamaters queryParameters)
    {
        var filtered = new List<FilterDefinition<Book>>();
        var builder = Builders<Book>.Filter;

        // Build filters
        if (!string.IsNullOrEmpty(queryParameters.author))
        {
            filtered.Add(builder.Regex(c => c.Author, new BsonRegularExpression(queryParameters.author, "i")));
        }
        if (!string.IsNullOrEmpty(queryParameters.title))
        {
            filtered.Add(builder.Regex(c => c.Title, new BsonRegularExpression(queryParameters.title, "i")));
        }
        if (!string.IsNullOrEmpty(queryParameters.language))
        {
            filtered.Add(builder.Regex(c => c.Language, new BsonRegularExpression(queryParameters.language, "i")));
        }
        if (!string.IsNullOrEmpty(queryParameters.genre))
        {
            filtered.Add(builder.Regex(c => c.Genre, new BsonRegularExpression(queryParameters.genre, "i")));
        }

        var combinedFilter = filtered.Count > 0 ? builder.And(filtered) : builder.Empty;

        var sortBuilder = Builders<Book>.Sort;
        var sortDefinition = queryParameters.SortOrder.ToLower() == "desc"
            ? sortBuilder.Descending(queryParameters.SortBy)
            : sortBuilder.Ascending(queryParameters.SortBy);

        var result = await _books.Find(combinedFilter)
                                 .Sort(sortDefinition)
                                 .Skip(queryParameters.Size * (queryParameters.Page - 1))
                                 .Limit(queryParameters.Size)
                                 .ToListAsync();

        return result;
    }



    public async Task<Book?> GetABook(string id) =>
        await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateBook(Book newBook)
    {
        if (newBook.Id == null || newBook.Id =="string" )
        {
            var objectId = ObjectId.GenerateNewId();
            string objectIdString = objectId.ToString();
            newBook.Id = objectIdString;
        }
            await _books.InsertOneAsync(newBook);
    }


    public async Task UpdateBook(string id, Book updatedBook) =>
        await _books.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveBook(string id) =>
        await _books.DeleteOneAsync(x => x.Id == id);

}
