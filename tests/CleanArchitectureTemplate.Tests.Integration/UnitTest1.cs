#if (nunit)
using NUnit.Framework;
#else
using Xunit;
#endif

namespace CleanArchitectureTemplate.Tests.Integration
{
    public class Tests
    {
        #if (nunit)
        [SetUp]
        public void Setup()
        {
        }
        #endif

        #if (nunit)
        [Test]
        #else
        [Fact]
        #endif
        public void Test1()
        {
            #if (nunit)
            Assert.Pass();
            #else
            Assert.True(true);
            #endif
        }
    }
}
