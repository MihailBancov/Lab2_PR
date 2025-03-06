using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>();
        private readonly HttpClient _httpClient;
        
        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = Orders.FirstOrDefault(o=> o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Order order)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://userservice/api/user/{order.UserId}");
                if(!response.IsSuccessStatusCode)
                {
                    return BadRequest("User not found");
                }
                Orders.Add(order);
                return CreatedAtAction(nameof(Get), new {id = order.Id}, order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Order updatedOrder)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                return NotFound();
            }

            order.UserId = updatedOrder.UserId;
            order.Product = updatedOrder.Product;
            order.Quantity = updatedOrder.Quantity;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            Orders.Remove(order);
            return NoContent();
        }
    }
}