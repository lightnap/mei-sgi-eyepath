using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    [SerializeField] private Transform m_ChildTransform;

    [SerializeField] private float m_FullMoveTime = 2.0f; 
    private float m_CurrentMoveTime;

    private float m_MinHeight;
    [SerializeField] private float m_MaxHeight = 3.0f;
    private float m_CurrentHeight;

    private AudioSource m_Audio = null;

    // Start is called before the first frame update
    void Start()
    {
        m_MinHeight = m_ChildTransform.localPosition.y;
        m_CurrentHeight = m_MinHeight;
        m_CurrentMoveTime = m_FullMoveTime;
        m_Audio = GetComponent<AudioSource>();
    }


    public void OpenDoor()
    {
        if (m_Audio != null) 
        {
            m_Audio.Stop();
            m_Audio.Play();
        }
        StopAllCoroutines();
        //m_CurrentMoveTime = (m_MaxHeight - m_CurrentHeight) / (m_MaxHeight - m_MinHeight) * m_FullMoveTime;
        Move(m_CurrentHeight, m_MaxHeight);
    }

    public void CloseDoor() 
    {
        if (m_Audio != null)
        {
            m_Audio.Stop();
            m_Audio.Play();
        }
        StopAllCoroutines();
        //m_CurrentMoveTime = m_CurrentHeight / (m_MaxHeight - m_MinHeight) * m_FullMoveTime;
        Move(m_CurrentHeight, m_MinHeight);
    }

    void Move(float aCurrentHeight, float aGoalHeight) 
    {
        StartCoroutine(MoveRoutine(aCurrentHeight, aGoalHeight));
    }

    public IEnumerator MoveRoutine(float aCurrentHeight, float aGoalHeight) 
    {
        float timer = 0.0f;
        Vector3 newPosition = new Vector3(0.0f, 0.0f, 0.0f);


        while (timer <= m_FullMoveTime)
        {
            m_CurrentHeight = Mathf.Lerp(aCurrentHeight, aGoalHeight, timer / m_FullMoveTime);
            newPosition.y = m_CurrentHeight;
            m_ChildTransform.localPosition = newPosition;
            timer += Time.deltaTime;
            yield return null;
        }
        m_CurrentHeight = aGoalHeight;
        newPosition.y = m_CurrentHeight;
        m_ChildTransform.localPosition = newPosition;
        m_Audio.Stop();
    }
}
