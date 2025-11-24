using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleLights : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private GameObject roomLights;
        [SerializeField] private Light alarmLight;
        protected override void DoPuzzleReward()
        {
            alarmLight.enabled = false;
            roomLights.SetActive(true);
        }
    }
}