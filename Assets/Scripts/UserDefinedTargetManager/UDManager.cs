using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class UDManager : MonoBehaviour, IUserDefinedTargetEventHandler
{
    UserDefinedTargetBuildingBehaviour UDTBuilderBehavior;
    ObjectTracker objTracker;
    DataSet dataSet;
    ImageTargetBuilder.FrameQuality UDTFrameQuality;
    ImageTargetBehaviour TargetImageBehav;

    //Testing Starting
    public Text FQText;
    public GameObject m_Camera;
    //Testing End

    void Start()
    {
        UDTBuilderBehavior = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if(UDTBuilderBehavior)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            UDTBuilderBehavior.RegisterEventHandler(this);

        }
    }

    // Update is called once per frame
    void Update()
    {
        FQText.text = m_Camera.transform.rotation.eulerAngles.x.ToString();
    }
    //Whenever camera moves, it calculates frame quality
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        UDTFrameQuality = frameQuality;
    }

    public void OnInitialized()
    {
        objTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if(objTracker != null)
        {
            dataSet = objTracker.CreateDataSet();
            objTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        objTracker.DeactivateDataSet(dataSet);
        ImageTargetBehaviour TargetImageCopy = (ImageTargetBehaviour)Instantiate(TargetImageBehav);
        dataSet.CreateTrackable(trackableSource, TargetImageCopy.gameObject);
        objTracker.ActivateDataSet(dataSet);
        UDTBuilderBehavior.StartScanning();
    }

    public void BuildTarget(ImageTargetBehaviour TIBehavior)
    {
        /*if(UDTFrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            UDTBuilderBehavior.BuildNewTarget(TIBehavior.name, TIBehavior.GetSize().x);
            TargetImageBehav = TIBehavior;
        }*/
        UDTBuilderBehavior.BuildNewTarget(TIBehavior.name, TIBehavior.GetSize().x);
        TargetImageBehav = TIBehavior;
    }

}
