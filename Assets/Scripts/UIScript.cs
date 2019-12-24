using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.Examples.Common;

public class UIScript : MonoBehaviour
{
    public GameObject CompleteMenu;
    public GameObject ScanningFloorMenu;

    public bool IsOpenLeft = false;
    public bool IsOpenRight = false;
    public Animator LeftBarAnimator;
    public Animator RightBarAnimator;
    public GameObject OpenButtonLeft;
    public GameObject CloseButtonLeft;
    public GameObject OpenButtonRight;
    public GameObject CloseButtonRight;
    public GameObject SnackBar;
    public Text SnackBarText;
    public GameObject[] FurnitureButtons;
    public GameObject[] FurnitureMenu;
    private GameObject SelectedChoice;
    public DetectedPlaneGenerator PlaneGenerator;

    public int CurrentSelection = 0;
    public Color SelectionColor;
    public Color ButtonColor;

    private bool IsMainMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        SwitchSelection(CurrentSelection);
    }

    // Update is called once per frame
    void Update()
    {
        SetInitializationUI();
    }

    void SetInitializationUI()
    {
        if (Session.Status == SessionStatus.LostTracking)
        {
            SnackBar.SetActive(true);
            SnackBarText.text = "Initializing AR";
            CompleteMenu.SetActive(false);
            ScanningFloorMenu.SetActive(false);
        }
        else if (Session.Status == SessionStatus.Tracking)
        {
            SnackBar.SetActive(false);
            if(IsMainMenu)
            {
                PlaneGenerator.gameObject.SetActive(false);
                CompleteMenu.SetActive(true);
                ScanningFloorMenu.SetActive(false);
            }
            else
            {
                PlaneGenerator.gameObject.SetActive(true);
                CompleteMenu.SetActive(false);
                ScanningFloorMenu.SetActive(true);
            }
        }
    }

    void DeactivateAll()
    {
        for (int i = 0; i < FurnitureMenu.Length; i++)
        {
            FurnitureMenu[i].SetActive(false);
        }
    }

    public void ProceedToMenu()
    {
        IsMainMenu = true;
    }

    public void RevertToScanner()
    {
        IsMainMenu = false;
    }

    public void OpenRightWindow()
    {
        if (!IsOpenRight)
        {
            OpenButtonRight.SetActive(false);
            CloseButtonRight.SetActive(true);
            RightBarAnimator.Play("Open");
            IsOpenRight = true;
        }
    }

    public void CloseRightWindow()
    {
        if (IsOpenRight)
        {
            OpenButtonRight.SetActive(true);
            CloseButtonRight.SetActive(false);
            RightBarAnimator.Play("Close");
            IsOpenRight = false;
        }
    }

    public void OpenLeftWindow()
    {
        if(!IsOpenLeft)
        {
            OpenButtonLeft.SetActive(false);
            CloseButtonLeft.SetActive(true);
            LeftBarAnimator.Play("Open");
            IsOpenLeft = true;
        }
    }


    public void CloseLeftWindow()
    {
        if(IsOpenLeft)
        {
            OpenButtonLeft.SetActive(true);
            CloseButtonLeft.SetActive(false);
            LeftBarAnimator.Play("Close");
            IsOpenLeft = false;
        }
    }

    public void SwitchSelection(int Index)
    {
        FurnitureButtons[CurrentSelection].GetComponent<Image>().color = ButtonColor;
        CurrentSelection = Index;
        FurnitureButtons[Index].GetComponent<Image>().color = SelectionColor;
        DeactivateAll();
        FurnitureMenu[CurrentSelection].SetActive(true);
    }
}
