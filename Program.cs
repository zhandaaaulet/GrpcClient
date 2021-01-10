using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcSE1Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(new HelloRequest { Name = "Balenbay" });
            //Console.WriteLine($"Greeteng: {reply.Message}");

            //Console.Write("Please, enter id of student: ");
            //int id = Convert.ToInt32(Console.ReadLine());
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Student.StudentClient(channel);
            //var reply = await client.GetStudentAsync(new StudentLookup { Id = id });
            //Console.WriteLine($"Response: {reply.User.Id} - {reply.User.Name} {reply.User.Surname}, {(reply.User.Gender ? "Male" : "Female")}, with GPA {reply.Gpa}");


            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Student.StudentClient(channel);
            using (var call = client.GetAllStudents(new AllStudentsLookup()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var reply = call.ResponseStream.Current;
                    Console.WriteLine($"Response: {reply.User.Id} - {reply.User.Name} {reply.User.Surname}, {(reply.User.Gender ? "Male" : "Female")}, with GPA {reply.Gpa}");
                }
            }
            
        }
    }
}
