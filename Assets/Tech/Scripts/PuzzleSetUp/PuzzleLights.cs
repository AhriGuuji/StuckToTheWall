using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleLights : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private GameObject roomLights;
        [SerializeField] private GameObject alarmLight;
        protected override void DoPuzzleReward()
        {
            alarmLight.SetActive(false);
            roomLights.SetActive(true);
        }

        public override void CHEAT()
        {
            DoPuzzleReward();
        }
    }
}