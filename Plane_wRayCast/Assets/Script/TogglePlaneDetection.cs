using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class TogglePlaneDetection : MonoBehaviour
{
    [SerializeField] Text m_TogglePlaneDetectionText;

    ARPlaneManager m_ARPlaneManager;

    private bool planeIsActive = true;

    public Text togglePlaneDetectionText
    {
            get { return m_TogglePlaneDetectionText; }
            set { m_TogglePlaneDetectionText = value; }
    }

    void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
    }
    
    public void TogglePlane()
    {
        m_ARPlaneManager.enabled = !m_ARPlaneManager.enabled;
        planeIsActive = !planeIsActive;

        string planeDetectionMessage = "";
        if (planeIsActive)
        {
            planeDetectionMessage = "Hide Plane";
            SetAllPlanesActive(true);
        }
        else
        {
            planeDetectionMessage = "Display Plane";
            SetAllPlanesActive(false);
        }

        if (togglePlaneDetectionText != null)
            togglePlaneDetectionText.text = planeDetectionMessage;
    }

    void SetAllPlanesActive(bool value)
    {
        foreach (var plane in m_ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }
}
