using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMoveAndRotateIdle : MonoBehaviour
{

    [SerializeField] private float mRotationSpeed = 1.0f;
    [SerializeField] private float mBobbleSpeed = 1.0f;
    [SerializeField] private float mFloatDistance = 0.1f;


    private float mRotation;
    private float mHeight; 


    // Start is called before the first frame update
    void Start()
    {
        mRotation = transform.rotation.y;
        mHeight = 0.0f; 
    }

    // Update is called once per frame
    void Update()
    {

        mRotation += Time.deltaTime * mRotationSpeed;
        mHeight += Time.deltaTime * mBobbleSpeed; 

        if (mRotation >= 360.0f) 
        {
            mRotation -= 360.0f; 
        }

        if (mHeight >= 360.0f) 
        {
            mHeight -= 360.0f; 
        }

        transform.localPosition = new Vector3 (transform.localPosition.x,  mFloatDistance * Mathf.Sin(Mathf.Deg2Rad * mHeight), transform.localPosition.z);
        transform.eulerAngles = new Vector3 (transform.rotation.x, mRotation, transform.rotation.z); 
    }
}
