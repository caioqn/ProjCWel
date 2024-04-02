using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using asdf_api.DapperExtensions;
using asdf_api.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asdf_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        // GET: api/<AnimalController>/GetLimited
        [HttpGet("GetLimited")]
        public IEnumerable<dynamic> Get([FromServices] MySqlConnection connection)
        {
            var asdf = connection.Query(() => new
            {
                ani_nome = default(string),
                ani_nome_usual = default(string),
                ani_ativo = default(string),
            }, "SELECT ani.ani_nome,ani.ani_nome_usual,ani.ani_ativo FROM animais ani limit 10").ToArray();
            return asdf;
        }

        // GET api/<AnimalController>/ByName/NomeAnimal    
        [HttpGet("ByName/{aniNome}")]
        public IEnumerable<Animal> Get(string aniNome, [FromServices] MySqlConnection connection)
        {
            var encodedSearch = $"%{aniNome}%";

            return connection
                .Query<Animal>(@"SELECT 
                                        ani.ani_pk,ani.ani_nome,ani.ani_nome_usual,ani.ani_dt_nasc, ani.ani_ativo
                                        FROM animais ani 
                                        WHERE ani.ani_nome like @encodedSearch",
                                        new { encodedSearch }).ToArray();
        }

        // GET api/<AnimalController>/ByDate/aniData1&aniData2
        [HttpGet("ByDate/{aniData1}&{aniData2}")]
        public IEnumerable<string?> Get(DateTime aniData1, DateTime aniData2, [FromServices] MySqlConnection connection)
        {
            var asdf = connection
                .Query<Animal>(@"SELECT 
                                   ani_nome_usual
                                   FROM animais ani
                                   WHERE ani.ani_dt_nasc BETWEEN @aniData1 AND @aniData2",
                                        new { aniData1, aniData2 });
            return asdf.Select(x => x.ani_nome_usual).ToArray();
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
