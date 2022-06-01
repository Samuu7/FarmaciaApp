using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoExample.Entity
{
    public class Producte
    {
        /// <summary>
        /// La ID de MongoDB ha de ser autogenerada i del tipus ObjectId
        /// Si es vol que el Id sigui del tipus "autonumeric", s'ha de gestionar per codi:
        /// Exemple: https://web.archive.org/web/20151009224806/http://docs.mongodb.org/manual/tutorial/create-an-auto-incrementing-field/
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String id { get; set; }

        [BsonElement("name")]
        public String Name { get; set; }

        [BsonElement("descripcio")]
        public String Descripcio { get; set; }

        [BsonElement("data")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Data { get; set; }

        [BsonElement("representantfarmacia")]
        public String Representant { get; set; }

        [BsonElement("stock")]
        public int Stock { get; set; }

        public Producte(string name, string descripcio, DateTime data, string representant, int stock)
        {
            this.Name = name;
            this.Descripcio = descripcio;
            this.Data = data;
            this.Representant = representant;
            this.Stock = stock;
        }

        public Producte() {

        }

    }
}

