using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject fadeOut;

    public void FadeIn()
    {
        StartCoroutine(delay(fadeIn));
    }
    
    public void FadeOut()
    {
        StartCoroutine(delay(fadeOut));
    }
    
    public void FadeOut(string _name)
    {
        StartCoroutine(delayChange(fadeOut, _name));
    }

    IEnumerator delay(GameObject fade)
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fade.SetActive(false);
    }
    
    IEnumerator delayChange(GameObject fade, string _name)
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(_name);
        //fade.SetActive(false);
    }
}
