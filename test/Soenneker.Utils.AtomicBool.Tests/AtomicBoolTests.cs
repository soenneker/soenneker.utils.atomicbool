using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Utils.AtomicBool.Tests;

[Collection("Collection")]
public sealed class AtomicBoolTests : FixturedUnitTest
{
    public AtomicBoolTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
