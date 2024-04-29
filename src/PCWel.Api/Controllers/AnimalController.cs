using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PCWel.Infra.DapperExtensions;
using PCWel.Infra.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asdf_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly MySqlConnection _context;

        public AnimalController([FromServices] MySqlConnection connection)
        {
            _context = connection;
        }

        // GET: api/<AnimalController>/GetLimited
        [HttpGet("GetLimited")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {
            const string SQL = @"SELECT ani.ani_nome,ani.ani_nome_usual,ani.ani_ativo FROM animais ani limit 10";

            var animais = await _context.QueryAsync(() => new
            {
                ani_nome = default(string),
                ani_nome_usual = default(string),
                ani_ativo = default(string),
            }, SQL);

            if (animais == null || !animais.Any()) return NotFound();

            return Ok(animais);
        }

        // GET api/<AnimalController>/ByName/NomeAnimal    
        [HttpGet("ByName/{aniNome}")]
        public async Task<ActionResult<IEnumerable<Animal>>> Get(string aniNome)
        {
            const string SQL = @"SELECT 
                                        ani.ani_pk,ani.ani_nome,ani.ani_nome_usual,ani.ani_dt_nasc, ani.ani_ativo
                                        FROM animais ani 
                                        WHERE ani.ani_nome like @encodedSearch";
            var encodedSearch = $"%{aniNome}%";

            var animais = await _context.QueryAsync<Animal>(SQL, new { encodedSearch });

            if (animais == null || !animais.Any()) return NotFound();

            return Ok(animais);
        }

        // GET api/<AnimalController>/ByDate/aniData1&aniData2
        [HttpGet("ByDate/{aniData1}&{aniData2}")]
        public ActionResult<IEnumerable<string?>> Get(DateTime aniData1, DateTime aniData2)
        {
            const string SQL = @"SELECT 
                                   ani_nome_usual
                                   FROM animais ani
                                   WHERE ani.ani_dt_nasc BETWEEN @aniData1 AND @aniData2";

            var animais = _context.Query<Animal>(SQL,new { aniData1, aniData2 });

            if (animais == null || !animais.Any()) return NotFound();

            return Ok(animais.Select(x => x.ani_nome_usual));
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
