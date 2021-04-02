using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    private List<GameObject> placePrefabList = new List<GameObject>();

    [SerializeField] private int maxPrefabSpawnCount = 0;
    private int placePrefabCount;

    [SerializeField]
    private GameObject placeablePrefab;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        // if (!TryGetTouchPosition(out Vector2 touchPosition))
        // {
        //     return;
        // }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

                // if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                if (hits.Count > 0)
                {
                    // var hitPos = s_Hits[0].pose;
                    var hitPos = hits[0].pose;
            // if (spawnedObject == null)
            // {
            //     // spawnedObject = Instantiate(placeablePrefab, hitPos.position, hitPos.rotation);
            //     // placePrefabList.Add(spawnedObject);
            //     // placePrefabCount++;
            //     SpawnPrefab(hitPos);
            // }
            // else
            // {
            //     // spawnedObject.transform.position = hitPos.position;
            //     // spawnedObject.transform.rotation = hitPos.rotation;
            //     if(placePrefabCount < maxPrefabSpawnCount) {
            //         SpawnPrefab(hitPos);
            //     }
            // }
            if(placePrefabCount < maxPrefabSpawnCount) {
                    SpawnPrefab(hitPos);
            }
        }
    }
        }
    }

    public void setPrefabType(GameObject prefabType)
    {
        placeablePrefab = prefabType;
    }

    private void SpawnPrefab(Pose hitPose){
        spawnedObject = Instantiate(placeablePrefab, hitPose.position, hitPose.rotation);
        placePrefabList.Add(spawnedObject);
        placePrefabCount++;
    }
}
