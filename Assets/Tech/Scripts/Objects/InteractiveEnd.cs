using Tech.Scripts.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveEnd : Interactive
{
    [SerializeField] private string endSceneName = "End";
    public override void DoSomething()
    {
        SceneManager.LoadScene(endSceneName);
    }
}
