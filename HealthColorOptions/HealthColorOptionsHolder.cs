using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.HealthColorOptions
{
    internal class HealthColorOptionsHolder(IUnit unit)
    {
        public readonly List<ManaColorSO> healthColors = [unit.HealthColor];
        public int currentHealthColor = 0;
    }
}
