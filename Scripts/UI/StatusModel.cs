using UnityEngine;

public class StatusModel
{
    public PlayerStatData Stats { get; private set; }
    public PlayerHPSystem HPSystem { get; private set; }
    public PlayerMPSystem MPSystem { get; private set; }

    public StatusModel(PlayerStatData stats, PlayerHPSystem hpSystem, PlayerMPSystem mpSystem)
    {
        Stats = stats;
        HPSystem = hpSystem;
        MPSystem = mpSystem;
    }

    public void UpdateStats(PlayerStatData newStats)
    {
        Stats = newStats;
    }

    public void UpdateHPSystem(PlayerHPSystem newHPSystem)
    {
        HPSystem = newHPSystem;
    }

    public void UpdateMPSystem(PlayerMPSystem newMPSystem)
    {
        MPSystem = newMPSystem;
    }
}
