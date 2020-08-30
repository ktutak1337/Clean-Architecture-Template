#if (xunit)
using Xunit;
#else
using NUnit.Framework;
#endif

namespace CleanArchitectureTemplate.Tests.EndToEnd
{
    public class Tests
    {
        #if (!xunit)
        [SetUp]
        public void Setup()
        {
        }
        #endif

        #if (xunit)
        [Fact]
        #else
        [Test]
        #endif
        public void Test1()
        {
            #if (xunit)
            Assert.True(true);
            #else
            Assert.Pass();
            #endif
        }
    }
}
