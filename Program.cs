// See https://aka.ms/new-console-tems
using cosmosdbDemo;
using Microsoft.Azure.Cosmos;

string cn = "AccountEndpoint=https://cosmosdbsri.documents.azure.com:443/;AccountKey=TYqo7yNVBLD0MJhYbqJe5Jc3u1iB5TEfU76PRKEq3HqNAZJbltpHDsf1TTqYgIK1WDKu1dM36QTzACDb31K7yg==;";
CosmosClient client = new CosmosClient(cn);
Container container = client.GetContainer("TrainingDB", "Students");
var query="SELECT * FROM c ";
QueryDefinition definition = new QueryDefinition(query);
FeedIterator<Student> resultSetIterator = container.GetItemQueryIterator<Student>(definition);

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
    Console.WriteLine($"{s.StudentID}\t{s.Name}\t{s.Course}\t{s.Age}");
}
