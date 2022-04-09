// See https://aka.ms/new-console-template for more information
using Redis.OM;
using Redis.OM.Modeling;
using TestRedisOm;

var provider = new RedisConnectionProvider("redis://localhost:6379");
var connection = provider.Connection;
/**
//This Connection was succeeded
var response = connection.Execute("PING").ToString();
Console.WriteLine(response);
*/


var amine = new Muppet
{
    FirstName = "Amine",
    LastName = "KLABI",
    //HomeLocation = new GeoLoc(-118.332665, 34.057098),
    Age = 37,
    PersonalStatement = "I'm here to start"
};

var ali = new Muppet
{
    FirstName = "Ali",
    //HomeLocation = new GeoLoc(-120.47552, 38.184258),
    Age = 35,
    PersonalStatement = "I'm here to be"
};

var ahmed = new Muppet
{
    FirstName = "Ahmed",
    LastName = "AYEDI",
    //HomeLocation = new GeoLoc(-111.474452, 32.186558),
    Age = 36,
    PersonalStatement = "I'm here to be Scientist "
};

var bunsen = new Muppet
{
    FirstName = "Bunsen",
    LastName = "Honeydem",
    //HomeLocation = new GeoLoc(-111.474452, 32.186558),
    Age = 41,
    PersonalStatement = "I'm a Scientist and Inventor"
};

//var nobu = new GeoLoc(-118.377512, 34.332518);

var amineId = connection.Set(amine);
var ahmedId = connection.Set(ahmed);
var aliId = connection.Set(ali);
var bunsenId = connection.Set(bunsen);

var alsoAmine = await connection.GetAsync<Muppet>(amineId);

Console.WriteLine($"{alsoAmine.FirstName} {alsoAmine.LastName}");

// create an index for an object
connection.CreateIndex(typeof(Muppet));
var mappets = provider.RedisCollection<Muppet>();

Console.WriteLine("======================= Muppets Name Amine =================");
await foreach(var mappet in mappets.Where(x=> x.FirstName == "Amine"))
{
    Console.WriteLine($"{mappet.FirstName} {mappet.LastName}");
}
Console.WriteLine("======================= Muppets Name Amine Or Ahmed =================");
await foreach (var mappet in mappets.Where(x => x.FirstName == "Amine" || x.FirstName == "Ahmed"))
{
    Console.WriteLine($"{mappet.FirstName} {mappet.LastName}");
}
Console.WriteLine("======================= Muppets Scientist =================");
// this is related to searchable in Muppet class
await foreach (var mappet in mappets.Where(x => x.PersonalStatement == "Scientist"))
{
    Console.WriteLine($"{mappet.FirstName} {mappet.LastName}");
}

Console.WriteLine("======================= Muppets older Than 35 =================");

await foreach (var mappet in mappets.Where(x => x.Age > 35))
{
    Console.WriteLine($"{mappet.FirstName} {mappet.LastName}");
}

Console.WriteLine("======================= Muppets with aggregation [ ages in 3 years ] =================");

var muppetsAggregation = provider.AggregationSet<Muppet>();

await foreach (var result in muppetsAggregation.Apply(x=> x.RecordShell.Age + 3 , "AgeInThreeYears"))
{
    Console.WriteLine(result["AgeInThreeYears"].ToString());
}








// create some indexes
connection.Execute("FLUSHDB");

