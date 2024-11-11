using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpiaAPI.Models;

namespace OlimpiaAPI.Controllers
{
    [Route("Data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Data> Post(CreateDatDto createDatDto)
        {
            var data = new Data()
            {
                Id = Guid.NewGuid(),
                Country = createDatDto.Country,
                County = createDatDto.County,
                Description = createDatDto.Description,
                PlayerId = createDatDto.PlayerId,
            };
            if (data != null)
            {
                using (var context= new OlimpiaContext())
                {
                    context.Datas.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);
                }
            }
            return BadRequest();
        }
        [HttpGet]

        public ActionResult<Data> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Datas.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Data> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var data = context.Datas.FirstOrDefault(x=> x.Id== id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        
        public ActionResult<Data> Put(UpdateDatDto updateDatDto, Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var existingData = context.Datas.FirstOrDefault(data => data.Id == id);
                if (existingData != null)
                {
                    existingData.Country = updateDatDto.Country;
                    existingData.County=updateDatDto.County;
                    existingData.Description = updateDatDto.Description;

                    context.Datas.Update(existingData);
                    context.SaveChanges();

                    return Ok(existingData);
                }
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var data = context.Datas.FirstOrDefault(data => data.Id == id);
                if (data != null)
                {
                    context.Datas.Remove(data);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres törlés" });
                }
                return NotFound();
            }
        }
    }
}
