using Tech.Scripts.Objects;

public class InteractiveSink : Interactive
{
    public override void DoSomething()
    {
        if (_playerInventory?.GetSelected() is not InteractiveMug mug)
            return;

        mug.Clear();
    }
}
