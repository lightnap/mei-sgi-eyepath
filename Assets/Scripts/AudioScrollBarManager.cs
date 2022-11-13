using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class AudioScrollBarManager : MonoBehaviour
{

    private float mCurrentScrollValue = 0.0f;

    [SerializeField] private AudioSource[] mAllAudioSources = null;
    [SerializeField] private TextMeshProUGUI mVolumeUIText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnScrollBarValueChanged(float aValue) 
    {
        if (aValue != mCurrentScrollValue)
        {
            mCurrentScrollValue = aValue;

            int CurrentScrollValueInt = (int)Mathf.Round(mCurrentScrollValue * 100.0f);

            // Update text
            mVolumeUIText.text = CurrentScrollValueInt.ToString();

            // Update list of volumes
            for (int i = 0; i < mAllAudioSources.Length; i++)
            {
                mAllAudioSources[i].volume = mCurrentScrollValue;
            }
        }
    }
}
