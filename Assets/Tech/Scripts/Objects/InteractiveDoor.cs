using SmallHedge.SoundManager;
using Tech.Scripts.Objects;
using UnityEngine;

public class InteractiveDoor : Interactive
{
    public override void DoSomething()
    {
        SoundManager.PlaySound(SoundType.DOOR);
    }
}
