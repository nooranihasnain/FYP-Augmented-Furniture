using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class UIScriptSet : MonoBehaviour
{
    public GameObject CompleteMenu;
    public GameObject ScanningFloorMenu;

    public GameObject SnackBar;
    public Text SnackBarText;
    public DetectedPlaneGenerator PlaneGenerator;

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
        Screen.orientation = ScreenOrientation.Landscape;
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
            if (IsMainMenu)
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

    public void ProceedToMenu()
    {
        IsMainMenu = true;
    }

    public void RevertToScanner()
    {
        IsMainMenu = false;
    }

    IEnumerator LoadDevice(string DeviceName, bool enable)
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

    public void LoadScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
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
