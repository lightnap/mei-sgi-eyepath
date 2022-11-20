using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEyepathScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fullFadeDuration = 2.0f;

    public Color fadeColor;
    private Renderer rend;
    
    private float mCurrentLerp = 0.0f; 
    private float mCurrentFadeDuration = 2.0f; 

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
        //Debug.Log("StartFadeIn"); 
        StopAllCoroutines(); 
        mCurrentFadeDuration = mCurrentLerp * fullFadeDuration;
        StartCoroutine(FadeInWithWait());
    }

    public void FadeOut() 
    {
        //Debug.Log("StartFadeOut");
        StopAllCoroutines(); 
        mCurrentFadeDuration = (1.0f - mCurrentLerp)  * fullFadeDuration;
        Fade(mCurrentLerp, 1.0f);
        
    }

    public IEnumerator FadeInWithWait() 
    {
        yield return new WaitForSeconds(mCurrentFadeDuration);
        Fade(mCurrentLerp, 0.0f);
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

        while (timer <= mCurrentFadeDuration) 
        {
            mCurrentLerp = Mathf.Lerp(aAlphhaIn, aAlphaOut, timer / mCurrentFadeDuration);
            newColor.a = mCurrentLerp;
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime; 
            yield return null;
        }
        mCurrentLerp = aAlphaOut;
        newColor.a = mCurrentLerp;
        rend.material.SetColor("_Color", newColor);
        if (aAlphaOut < 0.1f) 
        {
            rend.enabled = false;
        }
    }
}
