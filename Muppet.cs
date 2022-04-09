using Redis.OM.Modeling;


namespace TestRedisOm
{
    [Document (StorageType = StorageType.Json)]
    internal class Muppet
    {
        [Indexed]
        public string FirstName { get; set; }
        [Indexed]
        public string LastName { get; set; }
        [Searchable]
        public string PersonalStatement { get; set; }
        [Indexed (Aggregatable = true)]
        public int Age { get; set; }
        //public GeoLoc HomeLocation { get; set; }

    }
}

