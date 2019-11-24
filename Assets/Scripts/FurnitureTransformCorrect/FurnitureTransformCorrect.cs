using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureTransformCorrect : MonoBehaviour
{
    private GameObject m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindWithTag("MainCamera");
        if(m_Camera.transform.rotation.eulerAngles.x >= 0f && m_Camera.transform.rotation.eulerAngles.x <= 90f)
        {
            print("Small");
            Quaternion NewAngle = Quaternion.Euler((m_Camera.transform.rotation.eulerAngles.x - 90f) * (-1f) , transform.rotation.eulerAngles.y, 0f);
            transform.rotation = NewAngle;
        }
        else if (m_Camera.transform.rotation.eulerAngles.x >= 270f && m_Camera.transform.rotation.eulerAngles.x <= 360f)
        {
            print("Large");
            Quaternion NewAngle = Quaternion.Euler(m_Camera.transform.rotation.eulerAngles.x - 90f, transform.rotation.eulerAngles.y, 0f);
            transform.rotation = NewAngle;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Quaternion NewAngle = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, NewAngle, Time.deltaTime * 2f);
        transform.rotation = NewAngle;*/
    }
}
