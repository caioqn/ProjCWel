using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asdf_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        // GET: api/<AnimalController>
        [HttpGet]
        public IEnumerable<string> Get([FromServices] MySqlConnection connection)
        {
            return connection.Query<string>("SELECT ani.ani_nome_usual FROM animais ani limit 100").ToArray();
        }

        // GET api/<AnimalController>/NomeAnimal    
        [HttpGet("{aniNome}")]
        public IEnumerable<string> Get(string aniNome, [FromServices] MySqlConnection connection)
        {
            return connection.Query<string>("SELECT ani.ani_nome_usual FROM animais ani WHERE ani.ani_nome like '%@aniNome%'", new { aniNome }).ToArray();
        }

        // POST api/<AnimalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AnimalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnimalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
