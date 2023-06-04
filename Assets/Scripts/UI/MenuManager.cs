using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        public void PlayGameButtonIsPressed(string playerPrefsPath)
        {
            PlayerPrefs.SetString("PPGameMode", playerPrefsPath);
            SceneManager.LoadScene("GameScene");
        }

        public void ExitButtonPressed()
        {
            Application.Quit();
        }
    }
}
