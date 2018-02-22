using Xunit;
using VioletIoc;

namespace VioletIoc.UnitTests
{
    public class OpenGenericsTest
    {
        [Fact]
        public void Works()
        {
            var container = new Container();
            container.Register(typeof(ISpeaker<>), typeof(Speaker<>));

            var catSpeaker = container.Resolve<ISpeaker<Cat>>();
            Assert.Equal(catSpeaker.Speak(), "I am a Cat");
        }

        private interface ISpeaker<TAnimal>
            where TAnimal : IAnimal
        {
            string Speak();
        }

        private class Speaker<TAnimal> : ISpeaker<TAnimal>
            where TAnimal : IAnimal
        {
            public string Speak() => $"I am a {typeof(TAnimal).Name}";
        }

        private interface IAnimal
        {
        }

        private class Cat : IAnimal
        {
        }
    }
}
