using Microsoft.AspNetCore.Mvc;
using PflegeboxAPI.DataAccess;
using System.Linq;

namespace PflegeboxAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PflegeboxAntragController : ControllerBase
    {
        private readonly DataModelContext _context;
        public PflegeboxAntragController(DataModelContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Get all PflegeboxAntraege
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PflegeboxAntrag>))]
        public IActionResult GetAll() => Ok(_context.PflegeboxAntraege);
        /// <summary>
        /// Get a single PflegeboxAntrag with id from query-params
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PflegeboxAntrag))]
        public IActionResult Get(int id)
        {
            var pAntrag = _context.PflegeboxAntraege.FirstOrDefault(p => p.Id == id);
            if (pAntrag is null) return NotFound($"No content found with Id={id}");
            else return Ok(pAntrag);
        }
        /// <summary>
        /// Create new PflegeboxAntrag, supplying the object directly
        /// </summary>
        /// <param name="pAntrag"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PflegeboxAntrag pAntrag)
        {
            try
            {
                PflegeboxAntrag newAntrag = new PflegeboxAntrag()
                {
                    Id = pAntrag.Id,
                    BoxArt = pAntrag.BoxArt,
                    EmpfaengerAdresse = pAntrag.EmpfaengerAdresse,
                    LieferAdresse = pAntrag.LieferAdresse,
                    IstPrivatVersichert = pAntrag.IstPrivatVersichert,
                    Krankenkasse = pAntrag.Krankenkasse,
                    VersichertenNummer = pAntrag.VersichertenNummer
                };
                _context.Add(newAntrag);
                await _context.SaveChangesAsync();
                return Ok(newAntrag);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid Data-Model: {ex.Message}");
            }

        }
        /// <summary>
        /// Change a PflegeboxAntrag already in database, supplying the id of the object and
        /// the changed object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pAntrag"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PflegeboxAntrag pAntrag)
        {
            var antragToChange = _context.PflegeboxAntraege.FirstOrDefault(p =>
                p.Id == id);
            if (antragToChange == null) return NotFound($"No content found with Id ={ id}");
            try
            {
                antragToChange.VersichertenNummer = pAntrag.VersichertenNummer;
                antragToChange.LieferAdresse = pAntrag.LieferAdresse;
                antragToChange.EmpfaengerAdresse = pAntrag.EmpfaengerAdresse;
                antragToChange.IstPrivatVersichert = pAntrag.IstPrivatVersichert;
                antragToChange.Krankenkasse = pAntrag.Krankenkasse;
                antragToChange.BoxArt = pAntrag.BoxArt;
                await _context.SaveChangesAsync();
                return Ok(antragToChange);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid PflegeboxAntrag-Object: " + ex.Message);
            }

        }
        /// <summary>
        /// Deletes the PflegeboxAntrag from the database with the supplied id from query-parameters
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var antragToDelete = _context.PflegeboxAntraege.FirstOrDefault(p =>
                    p.Id == id);
                if (antragToDelete == null) return NotFound($"No content found with Id={id}");
                _context.Remove(antragToDelete);
                await _context.SaveChangesAsync();
                return Ok(antragToDelete);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
