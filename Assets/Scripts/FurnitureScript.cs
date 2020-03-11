using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureScript : MonoBehaviour
{
    public Text PriceText;
    public float Price = 100;
    public Sprite RenderImage;
    public string FurnitureName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PriceText.text = Price + "$";
    }
}
