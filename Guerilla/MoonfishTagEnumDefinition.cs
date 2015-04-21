using System.Collections.Generic;

namespace Moonfish.Guerilla
{
    public class MoonfishTagEnumDefinition
    {
        public List<string> Names { get; private set; }

        public MoonfishTagEnumDefinition( enum_definition definition )
        {
            Names = definition.Options;
        }
    }
}