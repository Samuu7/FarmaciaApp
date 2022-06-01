using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmaciaApp.Entity
{
    class Farmacia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String id { get; set; }

        [BsonElement("nom")]
        public String Nom { get; set; }

        [BsonElement("carrer")]
        public String Carrer { get; set; }

        [BsonElement("telefon")]
        public String Telefon { get; set; }

        public Farmacia(string nom, string telefon, string carrer)
        {
            this.Nom = nom;
            this.Carrer = carrer;
            this.Telefon = telefon;

        }

        public Farmacia() { }


    }
}
