using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using PCWel.Infra.DapperExtensions;
using PCWel.Infra.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace asdf_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComMorteController : ControllerBase
    {

        private readonly MySqlConnection _context;

        public ComMorteController([FromServices] MySqlConnection connection)
        {
            _context = connection;
        }

        // GET api/<ComMorteController>/GetLimited
        [HttpGet("GetLimited")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get()
        {
            const string SQL = @"SELECT cma.com_morte_ani_pk,
                            cma.com_morte_ani_nome,
                            cma.com_morte_ani_dt_morte 
                            FROM com_morte_ani cma limit 10";

            var ComMorte = await _context.QueryAsync(() => new
            {
                com_morte_ani_pk = default(int),
                com_morte_ani_nome = default(string),
                com_morte_ani_dt_morte = default(DateTime)
            }, SQL);

            if (ComMorte == null || !ComMorte.Any()) return NotFound();

            return Ok(ComMorte);
        }

        // GET api/<ComMorteController>/ByDate/aniData1&aniData2    
        [HttpGet("ByDate/{aniData1}&{aniData2}")]
        public ActionResult<IEnumerable<string?>> Get(DateTime aniData1, DateTime aniData2)
        {
            const string SQL = @"SELECT 
                                   cma.com_morte_ani_nome
                                   FROM com_morte_ani cma
                                   WHERE cma.com_morte_ani_dt_morte  BETWEEN @aniData1 AND @aniData2";
                                      
            var ComMorte = _context.Query<ComMorte>(SQL, new { aniData1, aniData2 });

            if (ComMorte == null || !ComMorte.Any()) return NotFound();

            return Ok(ComMorte);
        }

        // GET api/<ComMorteController>/ByName/ComMorteAniNome
        [HttpGet("ByName/{comMorteAniNome}")]
        public ActionResult<IEnumerable<string?>> Get(string comMorteAniNome)
        {
            var encodedSearch = $"%{comMorteAniNome}%";

            const string SQL = @"SELECT 
                               cma.com_morte_ani_pk,
                               cma.com_morte_ani_fk,
                               cma.com_morte_criador_fk,
                               cma.com_morte_ani_nome, 
                               cma.com_morte_tipo_fk,
                               cma.com_morte_ani_causa_morte,
                               cma.com_morte_protocolo,
                               cma.com_morte_ani_dt_morte,
                               cma.com_morte_ani_dt_com,
                               cma.com_morte_ani_ts
                               FROM com_morte_ani cma
                               WHERE cma.com_morte_ani_nome like @encodedSearch";
            var ComMorte = _context.Query<ComMorte>(SQL, new { encodedSearch });
           
            if (ComMorte == null || !ComMorte.Any()) return NotFound();

            return Ok(ComMorte);

        }

        // POST api/<ComMorteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ComMorteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ComMorteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
