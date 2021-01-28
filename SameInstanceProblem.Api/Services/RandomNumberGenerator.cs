using System;

namespace SameInstanceProblem.Api
{
    public class RandomNumberGenerator : INumberGenerator
    {
        private readonly int _generatedNumber;

        public RandomNumberGenerator()
        {
            _generatedNumber = new Random().Next(0, 999999);
        }

        public int Generate() => _generatedNumber;
    }
}