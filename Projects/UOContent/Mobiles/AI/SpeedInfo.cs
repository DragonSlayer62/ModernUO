using System;
using Server.Mobiles;

namespace Server;

public static class SpeedInfo
{
    public static double MinDelay { get; private set; }
    public static double MaxDelay { get; private set; }
    public static double MinMonsterDelay { get; private set; }
    public static double MaxMonsterDelay { get; private set; }

    // Determines the maximum dex for delay by dex
    public static int MaxDex { get; private set; }
    public static int MaxMonsterDex { get; private set; }

    public static void Configure()
    {
        // Default speed determined by dex (0 -> 190) for non-monster NPCs including pets
        MinDelay = ServerConfiguration.GetOrUpdateSetting("movement.delay.npcMinDelay", 0.1);
        MaxDelay = ServerConfiguration.GetOrUpdateSetting("movement.delay.npcMaxDelay", 0.5);
        MaxDex = ServerConfiguration.GetOrUpdateSetting("movement.delay.maxDex", 190);

        // Default speed determined by dex (0 -> 150) for monsters
        MinMonsterDelay = ServerConfiguration.GetOrUpdateSetting("movement.delay.monsterMinDelay", 0.4);
        MaxMonsterDelay = ServerConfiguration.GetOrUpdateSetting("movement.delay.monsterMaxDelay", 0.8);
        MaxMonsterDex = ServerConfiguration.GetOrUpdateSetting("movement.delay.monsterMaxDex", 150);
    }

    public static void GetSpeeds(BaseCreature bc, out double activeSpeed, out double passiveSpeed)
    {
        // Legacy is used if it is enabled, and the type is in the table
        if (LegacySpeedInfo.GetSpeeds(bc.GetType(), out activeSpeed, out passiveSpeed))
        {
            return;
        }

        var isMonster = bc.IsMonster;
        var maxDex = isMonster ? MaxMonsterDex : MaxDex;

        var dex = Math.Clamp(bc.Dex, 25, maxDex);

        double min = isMonster ? MinMonsterDelay : MinDelay;
        double max = isMonster ? MaxMonsterDelay : MaxDelay;

        activeSpeed = Math.Max(max - (max - min) * ((double)dex / maxDex), min);
        passiveSpeed = activeSpeed * 2;
    }
}
