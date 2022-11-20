using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingController : MonoBehaviour
{
    public Volume m_Volume;

    private bool m_HasVolumeActivated;

    public float fullFadeDuration = 2.0f;

    public Color fadeColor;
    public Renderer rend;

    private float mCurrentLerp = 0.0f; 
    private float mCurrentFadeDuration = 2.0f;

    void Start() 
    {
        mCurrentFadeDuration = fullFadeDuration;
    }


    public void FadeIn()
    {
        //Debug.Log("StartFadeIn");
        mCurrentFadeDuration = mCurrentLerp * fullFadeDuration;
        Fade(mCurrentLerp, 0.0f);
    }

    public void FadeOut()
    {
        //Debug.Log("StartFadeOut");
        mCurrentFadeDuration = (1.0f - mCurrentLerp) * fullFadeDuration;
        Fade(mCurrentLerp, 1.0f);
    }

    public void Fade(float aAlphaIn, float aAlphaOut)
    {
        StartCoroutine(FadeRoutine(aAlphaIn, aAlphaOut));
    }

    public IEnumerator FadeRoutine(float aAlphhaIn, float aAlphaOut)
    {
        float timer = 0.0f;
        Color newColor = fadeColor;
        mCurrentLerp = aAlphhaIn;
        newColor.a = mCurrentLerp;
        rend.material.SetColor("_Color", newColor);

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

    public IEnumerator FadeInAndOut() 
    {
        float timer = 0.0f;
        FadeOut();
        while (timer <= mCurrentFadeDuration) 
        {
            timer += Time.deltaTime;
            yield return null;
        }
        m_HasVolumeActivated = !m_HasVolumeActivated;
        m_Volume.weight = 1.0f - m_Volume.weight;
        FadeIn(); 
        yield return null;
    }

    public void ActivateEffect() 
    {
        StopAllCoroutines();
        if (m_HasVolumeActivated)
        {
            FadeIn();
        }
        else 
        {
            m_HasVolumeActivated = false;
            m_Volume.weight = 0.0f;
            StartCoroutine(FadeInAndOut());
        }
    }

    public void DeactivateEffect() 
    {
        StopAllCoroutines();
        if (!m_HasVolumeActivated)
        {
            FadeIn();
        }
        else 
        {
            m_HasVolumeActivated = true;
            m_Volume.weight = 1.0f;
            StartCoroutine(FadeInAndOut()); 
        }
    }

}
