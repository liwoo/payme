using System;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    public class ExampleTest
    {
        [Fact]
        public void Test1()
        {
            true.Should().BeTrue();
        }
    }
}
