using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoorTrigger : MonoBehaviour
{
    [SerializeField] private MovingDoor m_Door;

    private int m_EyepathLayerIndex;
    private bool m_isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        m_EyepathLayerIndex = LayerMask.NameToLayer("Eyepath");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider other) 
   {
        if (other.gameObject.layer == m_EyepathLayerIndex && !m_isColliding) 
        {
            m_isColliding = true;
            m_Door.OpenDoor(); 

            //Debug.Log("Entered trigger");
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer == m_EyepathLayerIndex && m_isColliding)
        {
            m_isColliding = false;
            m_Door.CloseDoor();
            //Debug.Log("Exited trigger");
        }
    }
}
