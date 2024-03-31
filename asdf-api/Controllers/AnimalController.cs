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
        [HttpGet("ByName/{aniNome}")]
        public IEnumerable<string> Get(string aniNome, [FromServices] MySqlConnection connection)
        {
            var asdf = connection
                .Query<string>(@"SELECT 
                                        ani.ani_nome_usual 
                                        FROM animais ani 
                                        WHERE ani.ani_nome LIKE @%aniNome%",
                                        new { aniNome });
            return asdf.ToArray();
        }
        
        // GET api/<AnimalController>/aniData1&aniData2
        [HttpGet("ByDate/{aniData1}&{aniData2}")]
        public IEnumerable<string> Get(DateTime aniData1, DateTime aniData2, [FromServices] MySqlConnection connection)
        {
         
            var asdf = connection
                .Query<string>(@"SELECT 
                                        ani_nome_usual
                                        FROM animais ani
                                        WHERE ani.ani_dt_nasc BETWEEN  @aniData1 AND @aniData2 limit 100",
                                        new { aniData1,aniData2 });
            return asdf.ToArray();
        }


        // POST api/<AnimalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AnimalController>/5
        [HttpPut("{ id}")]
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
