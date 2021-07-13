using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainParallaxManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainList;
    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
            TerrainMovement();
    }

    private void TerrainMovement()
    {
        var pPosZ = mainCamera.position.z + 30f;
        var tPosZ = terrainList[1].transform.position.z;

        if (pPosZ > tPosZ + 60f)
        {
            terrainList[0].transform.position = new Vector3(0, 0, tPosZ + 240f);
            terrainList.Add(terrainList[0]);
            terrainList.RemoveAt(0);
        }
        else if (pPosZ < tPosZ - 100f)
        {
            terrainList[2].transform.position = new Vector3(0, 0, tPosZ - 240f);
            terrainList.Insert(0, terrainList[2]);
            terrainList.RemoveAt(terrainList.Count - 1);
        }
    }
}