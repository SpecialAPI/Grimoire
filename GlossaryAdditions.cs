using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire
{
    internal static class GlossaryAdditions
    {
        internal static void Init()
        {
            Glossary.CreateAndAddCustom_KeywordToGlossary("Dry and Wet Damage", "Dry damage is direct damage that doesn't produce pigment.\nWet damage is indirect damage that produces pigment.");
            Glossary.CreateAndAddCustom_KeywordToGlossary("Reliable Damage", "Reliable damage always deals the same amount of damage, regardless of any passives, items, status effects or field effects. It will still \"trigger\" effects that would normally modify damage dealt, such as reducing frail and shield or dealing damage to other enemies if the target has divine protection.");
            Glossary.CreateAndAddCustom_KeywordToGlossary("Threaten", "\"Threatening\" an enemy or party member with damage means making them believe they are about to take that damage and then making them believe they took that damage, without actually dealing any damage. This will also trigger the \"on dealt damage\" events of the unit doing the threatening.");
        }
    }
}
