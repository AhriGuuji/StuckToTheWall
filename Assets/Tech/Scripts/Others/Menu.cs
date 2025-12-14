using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tech.Scripts.Others
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private string gameScene = "GameScene";
        [SerializeField] private Canvas exitMenu;

        private void Awake()
        {
            ExitExitMenu();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {OpenExitMenu();}
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameScene);
        }

        public void OpenExitMenu()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            exitMenu.enabled = true;
        }
        
        public void ExitExitMenu()
        {
            exitMenu.enabled = false;
            if (SceneManager.GetActiveScene().name == gameScene)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
