using Microsoft.AspNetCore.Mvc;
using System;
using Composition;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalProvider animal;

        public AnimalController(IAnimalProvider animal)
        {
            this.animal = animal;
        }

        
        [HttpGet("emitirSom/{animalNome}")]
        public ActionResult<string> Get(string animalNome)
        {
            Enum.TryParse(animalNome, out EAnimal eAnimal);

            return animal.EmitirSom(eAnimal); 
        }

        
    }
}
