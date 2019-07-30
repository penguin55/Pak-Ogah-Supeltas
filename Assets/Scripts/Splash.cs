using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public void Play(string clipName)
    {
        AudioManager._audioManager.Play(clipName);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MAIN_MENU");
    }
}
