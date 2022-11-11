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
        m_brightMaterial = Resources.Load("Materials/SemiTransparent", typeof(Material)) as Material;
        m_darkMaterial = new Material (GetComponent<Renderer>().materials[0]);

        SetDark();
    }

    public void SetDark()
    {
        Material[] materialsVector = {m_darkMaterial};
        GetComponent<Renderer>().materials = materialsVector; 
    }

    public void SetBright()
    {
        Material[] materialsVector = {m_darkMaterial, m_brightMaterial};
        GetComponent<Renderer>().materials = materialsVector; 
    }
}