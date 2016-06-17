using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;


namespace mongodbCSharp
{
    public class ShopItem
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //insert into mongod
            //insertmongo();

            //query
            querymongo();

            //delete
           // deletemongo();

            //let the program do not exit
            Console.ReadLine();

        }

        static async void insertmongo()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("unity");
            var collection = database.GetCollection<BsonDocument>("vrshop");

            var document = new BsonDocument
                                        {
                                            { "name", "c#test" },
                                            { "price", "100" }
                                        };
            await collection.InsertOneAsync(document);

        }
        static async void querymongo()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("unity");
            var collection = database.GetCollection<BsonDocument>("vrshop");

            //count how many lines
            var count = await collection.CountAsync(new BsonDocument());
            Console.WriteLine(count);

            //query the first doc
            var document = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
            Console.WriteLine(document.ToString());

            //query all doc
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            await collection.Find(new BsonDocument()).ForEachAsync(d => Console.WriteLine(d));
        }

        static async void deletemongo()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("unity");
            var collection = database.GetCollection<BsonDocument>("vrshop");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "test");

            await collection.DeleteOneAsync(filter);
        }
    }
}
