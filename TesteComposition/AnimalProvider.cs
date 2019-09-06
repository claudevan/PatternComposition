using System;

namespace Composition
{
    public class AnimalProvider : IAnimalProvider
    {
        private readonly Func<EAnimal, IAnimal> animal;

        public AnimalProvider(Func<EAnimal, IAnimal> animal)
        {
            this.animal = animal;
        }

        public string EmitirSom(EAnimal eAnimal)
        {
            return animal(eAnimal).EmitirSom();
        }
    }
}
