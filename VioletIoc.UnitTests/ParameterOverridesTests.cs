using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace VioletIoc.UnitTests
{
    public class ParameterOverridesTests
    {
        private readonly ITestOutputHelper _output;

        public ParameterOverridesTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OverridesWithType()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<Speak>();

            var friendly = root.Resolve<Speak>(
                new ParameterOverride<string>("Hello"),
                new ParameterOverride<int>(2));

            Assert.Equal(friendly.Say(), "Hello, Hello");
        }

        [Fact]
        public void OverridesWithName()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<Speak>();

            var friendly = root.Resolve<Speak>(
                new NamedParameterOverride("word", "Hello"),
                new NamedParameterOverride("times", 2));

            Assert.Equal(friendly.Say(), "Hello, Hello");
        }

        [Fact]
        public void OverridesWithAnonymousObject()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<Speak>();

            var friendly = root.Resolve<Speak>(new ObjectParametersOverride(new 
            { 
                word = "Hello",
                times = 2
            }));

            Assert.Equal(friendly.Say(), "Hello, Hello");
        }

        [Fact]
        public void OverridesWithObjectProperties()
        {
            var root = ContainerFactory.CreateRootContainer("root", _output.WriteLine);
            root.Register<Speak>();

            var friendly = root.Resolve<Speak>(new ObjectParametersOverride(new SpeakParams
            {
                word = "Hello",
                times = 2
            }));

            Assert.Equal(friendly.Say(), "Hello, Hello");
        }
    }

    internal class Speak
    {
        private readonly string _word;
        private readonly int _times;

        public Speak(string word, int times)
        {
            _word = word;
            _times = times;
        }

        public string Say()
        {
            return string.Join(", ", Enumerable.Range(1, _times).Select(i => _word));
        }
    }

    internal class SpeakParams
    {
        public string word { get; set; }
        public int times { get; set; }
    }
}
