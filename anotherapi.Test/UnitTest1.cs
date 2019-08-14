using anotherapi.Controllers;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace anotherapi.Test
{
    public class anotherapiTest
    {
        [Fact]
        public void Check_For_Hi()
        {
            ValuesController controller = new ValuesController();
            var actual = controller.Get("hi");
            var expected = "Hello";

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Check_For_Hello()
        {
            ValuesController controller = new ValuesController();
            var actual = controller.Get("hello");
            var expected = "Hi";

            Assert.Equal(expected, actual);
        }

        
    }
}
