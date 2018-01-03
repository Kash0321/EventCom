using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnetters.EventCom.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Messaging")]
    public class MessagingController : Controller
    {
        HubConnection HubConnection { get; set; } = new HubConnection("http://localhost:54762");

        //// GET: api/Messaging
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Messaging/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Messaging
        [HttpPost]
        public async Task Post([FromBody]string user, [FromBody]string message)
        {
            if (HubConnection.State == ConnectionState.Disconnected)
            {
                await HubConnection.Start();
            }

            IHubProxy messagingHubProxy = HubConnection.CreateHubProxy("MessagingHub");
            //stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
            await messagingHubProxy.Invoke("Send", user, message);
        }
        
        //// PUT: api/Messaging/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
