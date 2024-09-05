using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficeApplication.Shared
{
    /// <summary>
    /// Properties of Department specific to MongoDB
    /// </summary>
    public class DepartmentMongo
    {
        /// <summary>
        /// MongoDB-specific Id (can be ObjectId or any other type)
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Unique Id for each department (you can use the same as SQL or different)
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Name of each department
        /// </summary>
        [BsonElement("DepartmentName")]
        public string DepartmentName { get; set; }
    }
}
