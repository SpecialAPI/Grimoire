using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire
{
    internal static class GrimoireProfile
    {
        private static bool _initialized;

        public static void TryInitializeProfile()
        {
            if(_initialized)
                return;

            var profile = ProfileManager.RegisterMod();
            profile.SetGuidAndPrefix(MODGUID, MODPREFIX);
            profile.LoadAssetBundle("grimoirebundle");

            _initialized = true;
        }
    }
}
