using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioApiTeste.Fixture
{
    [CollectionDefinition("Mapper collection")]
    class MapperFixtureCollection : ICollectionFixture<MapperFixture>
    {
    }
}
