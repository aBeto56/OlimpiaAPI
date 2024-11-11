using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpiaAPI.Models;

namespace OlimpiaAPI.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayer)
        {
            var player = new Player
            {
                Id = Convert.ToString(Guid.NewGuid()),
                Name = createPlayer.Name,
                Age = createPlayer.Age,
                Weight = createPlayer.Weight,
                Height = createPlayer.Height,
                CreatedTime = DateTime.Now,
            };
            if (player != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }
            return BadRequest();
        }
        [HttpGet]

        public ActionResult<Player> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Players.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Player> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(x => x.Id == Convert.ToString(id));
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Player> Put(UpdatePlayerDto updatePlayerDto, string id)
        {
            using (var context = new OlimpiaContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == id);

                if (existingPlayer != null)
                {
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Age = updatePlayerDto.Age;
                    existingPlayer.Height = updatePlayerDto.Height;
                    existingPlayer.Weight = updatePlayerDto.Weight;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();
                    return Ok(existingPlayer);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == Convert.ToString(id));
                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres törlés" });
                }
                return NotFound();
            }
        }
        [HttpGet("playerData/{id}")]
        public ActionResult<Player> Get(Guid id)
        {
            using (var contex = new OlimpiaContext())
            {
                var player = contex.Players.Include(x => x.Data).FirstOrDefault(player => player.Id == Convert.ToString(id));
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }
        [HttpGet]
        public ActionResult<LowPlayerData> GetLowPlayerData(Guid id)
        {
            using ( var context= new OlimpiaContext())
            {
                var player = context.Players.Include(x => x.Data).FirstOrDefault(player => player.Id == Convert.ToString(id));
                var LowData = new LowPlayerData
                {
                    Name =player.Name,
                    Country = player.Data
                }
                }
            }

        }
    }
}
