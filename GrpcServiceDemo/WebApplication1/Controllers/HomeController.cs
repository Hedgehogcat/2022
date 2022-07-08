using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using GrpcService1;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<string> Get()
        {
            string url = "https://http://192.168.13.129:8090";
            using (var channel = GrpcChannel.ForAddress(url))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync( new HelloRequest { Name = "GreeterClient" });

                Console.WriteLine($"结果:message:{reply.Message}");
                 return reply.Message;
            }
        }
        [HttpGet]
        public async Task<string> Get123()
        {
            try { 
            //使用http
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            string url = "http://192.168.13.129:8090";
            using (var channel = GrpcChannel.ForAddress(url))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

                Console.WriteLine($"结果:message:{reply.Message}");
                return reply.Message;
            }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        public async Task<string> Get456()
        {
            try
            {
                //使用http
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                string url = "http://192.168.13.129:8090";
                using (var channel = GrpcChannel.ForAddress(url))
                {
                    var client = new Greeter.GreeterClient(channel);
                    var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

                    Console.WriteLine($"结果:message:{reply.Message}");
                    return reply.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
