using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHandsTransparent : MonoBehaviour
{
    private HandPresence leftHandPresence = null;
    private HandPresence rightHandPresence = null;

    // Start is called before the first frame update
    void Start()
    {
        TryFindHands();
    }

    void TryFindHands() 
    {
        Transform leftTransform = transform.Find("Rotation/LeftHandPresence(Clone)");
        if (leftTransform != null)
        {
            leftHandPresence = leftTransform.gameObject.GetComponent<HandPresence>();
        }
        Transform rightTransform = transform.Find("Rotation/RightHandPresence(Clone)");
        if (rightTransform != null)
        {
            rightHandPresence = rightTransform.gameObject.GetComponent<HandPresence>();
        }
    }


    void Update() 
    {
        // Finfing one of the two is enough. 
        if (leftHandPresence == null && rightHandPresence == null)
        {
            TryFindHands(); 
        }
    }

    public void SetHandsTransparent() 
    {
        if (leftHandPresence != null) 
        {
            leftHandPresence.SetMaterialTransparent();
        }
        if (rightHandPresence != null) 
        {
            rightHandPresence.SetMaterialTransparent();
        }

    }

    public void SetHandsSolid() 
    {
        if (leftHandPresence != null)
        {
            leftHandPresence.SetMaterialSolid();
        }
        if (rightHandPresence != null)
        {
            rightHandPresence.SetMaterialSolid();
        }
    }
}
