using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FadeManager fading;
    private void Start()
    {
        fading.FadeIn();
    }


    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SelectLevel(int level)
    {
        
        LevelManagement.levelCurrent = level - 1;
        fading.FadeOut(("Level "+level).ToString());
    }

    public void SelectSound()
    {
        AudioManager._audioManager.Play("Click");
    }
}
