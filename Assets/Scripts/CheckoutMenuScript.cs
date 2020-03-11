using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.ObjectManipulation;
using UnityEngine.UI;

public class CheckoutMenuScript : MonoBehaviour
{
    public GameObject CheckoutItemPrefab;
    private FurnitureManager FM;
    public Transform ItemsViewport;
    public Text TotalPriceText;
    // Start is called before the first frame update
    void Start()
    {
        FM = GameObject.FindWithTag("FurnitureManager").GetComponent<FurnitureManager>();
        AddAllFurniture();
        CountTotal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        GetComponent<Animator>().Play("Exit");
        Destroy(this.gameObject, 1f);
    }

    public void CountTotal()
    {
        float TotalCost = 0f;
        for (int i = 0; i < FM.SpawnedFurnitures.Count; i++)
        {
            TotalCost += FM.SpawnedFurnitures[i].Price;
        }
        TotalPriceText.text = "Total Cost: " + TotalCost;
    }

    public void AddAllFurniture()
    {
        for (int i = 0; i < FM.SpawnedFurnitures.Count; i++)
        {
            GameObject Item = Instantiate(CheckoutItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Item.transform.GetChild(0).GetComponent<Image>().sprite = FM.SpawnedFurnitures[i].RenderImage;
            Item.transform.GetChild(1).GetComponent<Text>().text = FM.SpawnedFurnitures[i].FurnitureName;
            Item.transform.GetChild(2).GetComponent<Text>().text = FM.SpawnedFurnitures[i].Price + "$";
            Item.transform.SetParent(ItemsViewport, false);
        }
    }
}
