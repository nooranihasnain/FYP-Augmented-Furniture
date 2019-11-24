using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeGizmos;

public class TransformGizmoMenuScript : MonoBehaviour
{
    public TransformGizmo TGScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTransform(string Type)
    {
        switch(Type)
        {
                case "Move":
                {
                    TGScript.transformType = TransformType.Move;
                    break;
                }
                case "Rotate":
                {
                    TGScript.transformType = TransformType.Rotate;
                    break;
                }
        }
    }
}
