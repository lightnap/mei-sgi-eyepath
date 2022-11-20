using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEypathAttachDIstance : MonoBehaviour
{

    enum eSides 
    {
        Left,
        Right
    }

    [SerializeField]
    private eSides m_CurrentSide = eSides.Left; 

    // Tranforms. 
    public Transform handTransformLeft;
    public Transform handTransformRight;
    public Transform objectTrasform;

    private bool m_UpdateTranform = false;
    private Vector3 m_HandPosition = new Vector3(0.0f, 0.0f, 0.0f); 

    [SerializeField] private float zOffset = 0.1f; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_UpdateTranform && handTransformLeft != null && handTransformRight != null && objectTrasform != null) 
        {
            if (m_CurrentSide == eSides.Left) 
            {
                m_HandPosition = handTransformLeft.position; 
            }
            if (m_CurrentSide == eSides.Right)
            {
                m_HandPosition = handTransformRight.position;
            }

            float distance = Vector3.Distance(objectTrasform.position, m_HandPosition);
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
