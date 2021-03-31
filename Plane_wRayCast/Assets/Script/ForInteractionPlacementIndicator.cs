using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ForInteractionPlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;
    private GameObject spawnedObject;

    [SerializeField] private GameObject PlaceablePrefab;
    [SerializeField] private Button PlaceModel;
    [SerializeField] private Button ShowIndicator;
    [SerializeField] Text ToggleIndicatorText;

    private bool placedModel = false;
    private bool showedIndicator = true;

    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    public Text placeIndicatorText
    {
        get { return ToggleIndicatorText; }
        set { ToggleIndicatorText = value; }
    }

    void Start()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);

        PlaceModel.onClick.AddListener(placeAModel);
        ShowIndicator.onClick.AddListener(showtheIndicator);
    }

    bool TryGetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }
        return false;
    }

    void placeAModel()
    {
        placedModel = true;
    }

    void showtheIndicator()
    {
        string IndicatorMessage = "";
        showedIndicator = !showedIndicator;
        if (showedIndicator)
        {
            IndicatorMessage = "Hide Indicator";
        }
        else
        {
            IndicatorMessage = "Show Indicator";
        }

        if (placeIndicatorText != null)
            placeIndicatorText.text = IndicatorMessage;
    }

    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            // visual.SetActive(true);
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (placedModel)
            {
                // if (spawnedObject == null)
                // {
                //     spawnedObject = Instantiate(PlaceablePrefab, transform.position, transform.rotation);
                // }
                // else
                // {
                //     spawnedObject.transform.position = transform.position;
                //     spawnedObject.transform.rotation = transform.rotation;
                // } 
                if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                {
                    spawnedObject = Instantiate(PlaceablePrefab, transform.position, transform.rotation);
                    m_NumberOfPlacedObjects++;
                }
                else
                {
                    if (m_CanReposition)
                    {
                        spawnedObject.transform.position = transform.position;
                        // spawnedObject.transform.rotation = transform.rotation;
                    } else {
                        visual.SetActive(false);
                    }
                }
                placedModel = false;
                // visual.SetActive(false);
            }

            // enable the visual if it's disabled
            // if(!visual.activeInHierarchy){
            if (showedIndicator == true)
            {
                visual.SetActive(true);
            }
            else
            {
                visual.SetActive(false);
            }
            // }
            // if (!visual.activeInHierarchy && showtheIndicator == true) {
            //     visual.SetActive(true);
            // }
        }
    }

    public void setPrefabType(GameObject prefabType)
    {
        PlaceablePrefab = prefabType;
    }
}
