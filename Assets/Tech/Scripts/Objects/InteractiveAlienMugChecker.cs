using Tech.Scripts.Objects;
using UnityEngine;

public class InteractiveAlienMugChecker : Interactive
{
    [SerializeField]
        private GameObject alien;
    public override void DoSomething()
    {
        if (_playerInventory?.GetSelected() is not InteractiveMug mug)
            return;

        if (mug.IsDone)
        {
            alien.GetComponent<Animator>().SetBool("Dance", true);
        }
    }
}
