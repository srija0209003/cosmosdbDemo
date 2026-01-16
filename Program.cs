// See https://aka.ms/new-console-tems
using cosmosdbDemo;
using Microsoft.Azure.Cosmos;

//AccountEndpoint=https://cosmosdbsri.documents.azure.com:443/;AccountKey=TYqo7yNVBLD0MJhYbqJe5Jc3u1iB5TEfU76PRKEq3HqNAZJbltpHDsf1TTqYgIK1WDKu1dM36QTzACDb31K7yg==;
string cn = "";
CosmosClient client = new CosmosClient(cn);
Container container = client.GetContainer("TrainingDB", "Students");
var query = "SELECT * FROM c ";
QueryDefinition definition = new QueryDefinition(query);
FeedIterator<Student> resultSetIterator = container.GetItemQueryIterator<Student>(definition);

//await ShowAllRecords(resultSetIterator);
var studrecord=new Student
{
    id="3",
    studentid = "4",
    name = "Srinivas",
    course = "Azure",
    age = 28
};
PartitionKey pk = new PartitionKey(studrecord.course);
ItemResponse<Student> response = await container.CreateItemAsync<Student>(studrecord, pk);
Console.WriteLine("Record Inserted ");

static async Task ShowAllRecords(FeedIterator<Student> resultSetIterator)
{
    List<Student> students = new List<Student>();
    while (resultSetIterator.HasMoreResults)
    {
        FeedResponse<Student> response = await resultSetIterator.ReadNextAsync();
        foreach (Student s in response)
        {
            students.Add(s);
        }
    }

    Console.WriteLine("StudentID\tName\tCourse\tAge");
    foreach (Student s in students)
    {
        Console.WriteLine($"{s.studentid}\t{s.name}\t{s.course}\t{s.age}");
    }
}
