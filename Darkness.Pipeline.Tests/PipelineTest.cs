using System;
using Xunit;

namespace Darkness.Pipeline.Tests
{
    public class PipelineTest
    {
        [Fact]
        public void Test1()
        {
            var success = "10"
                .AsResult()
                .Try(Parse)
                .Apply(Floatify)
                .Value;
            
            
            Assert.Equal(10f, success);
        }

        private int Parse(string input) => int.Parse(input);
        private float Floatify(int input) => input;
        
    }
}