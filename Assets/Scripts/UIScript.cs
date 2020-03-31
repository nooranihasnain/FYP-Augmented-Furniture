using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.VR;
using UnityEngine.XR;

public class UIScript : MonoBehaviour
{
    public GameObject CompleteMenu;
    public GameObject ScanningFloorMenu;

    public bool IsOpenLeft = false;
    public bool IsOpenRight = false;
    public Animator LeftBarAnimator;
    public Animator RightBarAnimator;
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
    private ARCoreSession CurrSession;

    //MR Stuff
    public GameObject MRRender;
    public GameObject ARRender;
    public GameObject ARCanvas;
    public GameObject MRCanvas;
    void Start()
    {
        CurrSession = GameObject.Find("ARCore Device").GetComponent<ARCoreSession>();
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
                //Pause Plane detection
                CurrSession.SessionConfig.EnablePlaneFinding = false;
                CurrSession.OnEnable();
                CompleteMenu.SetActive(true);
                ScanningFloorMenu.SetActive(false);
            }
            else
            {
                PlaneGenerator.gameObject.SetActive(true);
                //Resume plane detection
                CurrSession.SessionConfig.EnablePlaneFinding = true;
                CurrSession.OnEnable();
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

    public void SwitchSelection(int Index)
    {
        FurnitureButtons[CurrentSelection].GetComponent<Image>().color = ButtonColor;
        CurrentSelection = Index;
        FurnitureButtons[Index].GetComponent<Image>().color = SelectionColor;
        DeactivateAll();
        FurnitureMenu[CurrentSelection].SetActive(true);
    }

    IEnumerator LoadDevice (string DeviceName, bool enable)
    {
        XRSettings.LoadDeviceByName(DeviceName);
        yield return null;
        XRSettings.enabled = enable;
    }

    public void DisableVR()
    {
        //StartCoroutine(LoadDevice("", false));
        ARRender.SetActive(true);
        MRRender.SetActive(false);
        ARCanvas.SetActive(true);
        MRCanvas.SetActive(false);
    }

    public void EnableVR()
    {
        //StartCoroutine(LoadDevice("cardboard", true));
        ARRender.SetActive(false);
        MRRender.SetActive(true);
        ARCanvas.SetActive(false);
        MRCanvas.SetActive(true);
    }
}
