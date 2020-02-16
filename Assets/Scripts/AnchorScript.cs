using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    public GameObject ChildObj { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ChildObj.gameObject)
        {
            transform.position = ChildObj.transform.position;
        }
    }
}
