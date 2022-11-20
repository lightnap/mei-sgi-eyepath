using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEypathAttachDistance : MonoBehaviour
{

    public Transform handTransform;
    public Transform objectTrasform;

    private bool m_UpdateTranform = false;

    [SerializeField] private float zOffset = 0.1f; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_UpdateTranform && handTransform != null && objectTrasform != null) 
        {
            float distance = Vector3.Distance(objectTrasform.position, handTransform.position);
            transform.localPosition = new Vector3(0.0f, 0.0f, distance - zOffset);  

        }
    }

    public void SetObjectTrasform(Transform aTransform) 
    {
        m_UpdateTranform = true; 
        objectTrasform = aTransform;
    }

    public void ResetObjectTransform() 
    {
        m_UpdateTranform = false; 
        objectTrasform = null;
    }
}
