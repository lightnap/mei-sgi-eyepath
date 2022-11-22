using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoorTriggerDouble : MonoBehaviour
{
    [SerializeField] private MovingDoor m_DoorStartsClosed;
    [SerializeField] private MovingDoor m_DoorStartsOpen;

    private int m_EyepathLayerIndex;
    private bool m_isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        m_EyepathLayerIndex = LayerMask.NameToLayer("Eyepath");
        m_DoorStartsOpen.OpenDoor(); 
    }

   void OnTriggerEnter(Collider other) 
   {
        if (other.gameObject.layer == m_EyepathLayerIndex && !m_isColliding) 
        {
            m_isColliding = true;
            m_DoorStartsClosed.OpenDoor();
            m_DoorStartsOpen.CloseDoor();
            //Debug.Log("Entered trigger");
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer == m_EyepathLayerIndex && m_isColliding)
        {
            m_isColliding = false;
            m_DoorStartsClosed.CloseDoor();
            m_DoorStartsOpen.OpenDoor();
            //Debug.Log("Exited trigger");
        }
    }
}
