using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public bool IsOpen = false;
    public Animator WindowAnimator;
    public GameObject OpenButton;
    public GameObject CloseButton;

    public GameObject[] FurnitureButtons;
    private GameObject SelectedChoice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWindow()
    {
        if(!IsOpen)
        {
            OpenButton.SetActive(false);
            CloseButton.SetActive(true);
            WindowAnimator.Play("Open");
            IsOpen = true;
        }
    }

    public void CloseWindow()
    {
        if(IsOpen)
        {
            OpenButton.SetActive(true);
            CloseButton.SetActive(false);
            WindowAnimator.Play("Close");
            IsOpen = false;
        }
    }
}
