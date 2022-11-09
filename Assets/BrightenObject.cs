using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightenObject : MonoBehaviour
{

    private Material m_brightMaterial = null;
    private Material m_darkMaterial = null;


    // Start is called before the first frame update
    void Start()
    {
        m_brightMaterial = transform.Find("BrightnessMaterials").GetComponent<Renderer>().materials[0];
        m_darkMaterial = transform.Find("BrightnessMaterials").GetComponent<Renderer>().materials[1];

        SetDark();
    }

    public void SetBright()
    {
        GetComponent<Renderer>().material = m_brightMaterial;
        Debug.Log("We set bright"); 
    }

    public void SetDark()
    {
        GetComponent<Renderer>().material = m_darkMaterial;
        Debug.Log("We set dark");
    }

}