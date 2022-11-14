using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEyepathScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer rend; 

    // Start is called before the first frame update
    void Start()
    {
         
        rend = GetComponent<Renderer>();
 
        if (fadeOnStart) 
        {
            FadeIn();
        }
    }

    public void FadeIn() 
    {
        Debug.Log("StartFadeIn"); 
        Fade(1.0f, 0.0f);
    }

    public void FadeOut() 
    {
        Debug.Log("StartFadeOut");
        Fade(0.0f, 1.0f); 
    }

    public void Fade(float aAlphaIn, float aAlphaOut)
    {
        StartCoroutine(FadeRoutine(aAlphaIn, aAlphaOut)); 
    }

    public IEnumerator FadeRoutine(float aAlphhaIn, float aAlphaOut) 
    {
        float timer = 0.0f;
        Color newColor = fadeColor;
        rend.enabled = true;

        while (timer <= fadeDuration) 
        {
            newColor.a = Mathf.Lerp(aAlphhaIn, aAlphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime; 
            yield return null;
        }
        newColor.a = aAlphaOut;
        rend.material.SetColor("_Color", newColor);
        if (aAlphaOut < 0.1f) 
        {
            rend.enabled = false;
        }
    }
}
