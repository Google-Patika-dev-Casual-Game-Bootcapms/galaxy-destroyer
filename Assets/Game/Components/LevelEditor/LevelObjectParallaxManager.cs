using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelObjectParallaxManager : MonoBehaviour
{
    private readonly List<GameObjectType> allObjectList = new List<GameObjectType>();
    private LevelData levelData;
    private Transform mainCamera;
    public byte levelNumber;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        CreatLevel();
    }

    private void Update()
    {
        UpdateLevelObject(allObjectList);
    }

    private void CreatLevel()
    {
        var levelName = levelNumber switch
        {
            0 => "3DParallaxBackupLevel",
            1 => "3DParallaxMarsLevel",
            _ => "3DParallaxNeptuneLevel"
        };

        allObjectList.Clear();
        levelData = gameObject.GetComponent<EditorSceneBuilderComponent>().Pull3DParallaxData(levelName);
        CreatLevelObject();
    }

    private void CreatLevelObject()
    {
        for (var i = 0; i < (int) EGameObjectType.enumLastIndex; i++)
        {
            var objectCount = levelData?.LevelCharacters?.Count(x => x.Type == (EGameObjectType) i);
            if (objectCount == null || objectCount <= 0) continue;
            var creatObjectCount = (int) Math.Round((double) ((objectCount + 4) / 3));
            for (var j = 0; j < creatObjectCount; j++)
            {
                var levelObject = gameObject.GetComponent<EditorSceneBuilderComponent>()?
                    .InstantiateLevelCharacter((EGameObjectType) i)?.GetComponent<GameObjectType>();
                if (levelObject is null) continue;
                levelObject.gameObject.transform.position = new Vector3(50, 0, 50);
                allObjectList.Add(levelObject);
            }
        }
    }


    private void UpdateLevelObject(List<GameObjectType> objectList)
    {
        // TODO Make sure that setObjectPassive function is done
        SetObjectPassive(objectList);
        var pPos = mainCamera.position.z + 30f;

        var passiveListOnScene = objectList?.Where(x => !x.isActive).ToList();
        if (passiveListOnScene == null) return;

        foreach (var passiveObjectOnScene in passiveListOnScene)
        {
            var samePassiveObjectsData =
                levelData?.LevelCharacters?.Where(x => x.Type == passiveObjectOnScene.Type).ToList();
            if (samePassiveObjectsData == null) return;
            foreach (var samePassiveObjectData in samePassiveObjectsData)
            {
                var cPos = samePassiveObjectData.Position.z;
                if (cPos < pPos + 80 && cPos > pPos - 60)
                {
                    samePassiveObjectData.Type += 1000;
                    var transform1 = passiveObjectOnScene.transform;
                    transform1.localScale = samePassiveObjectData.Scale;
                    transform1.position = samePassiveObjectData.Position;
                    transform1.eulerAngles = samePassiveObjectData.Rotation;
                    passiveObjectOnScene.isActive = true;
                    break;
                }
            }
        }
    }

    private void SetObjectPassive(List<GameObjectType> objectList)
    {
        var pPos = mainCamera.position.z + 30f;
        var activeObjectList = objectList?.Where(x => x.isActive).ToList();
        if (activeObjectList == null || activeObjectList.Count == 0) return;

        foreach (var activeObject in activeObjectList)
        {
            var cPos = activeObject.transform.position;
            if (!(cPos.z < pPos + 80 && cPos.z > pPos - 60))
            {
                var sameCharacter =
                    levelData?.LevelCharacters?.First(x => x.Type == activeObject.Type + 1000 && x.Position == cPos);
                if (sameCharacter != null)
                {
                    sameCharacter.Type = activeObject.Type;
                    activeObject.isActive = false;
                }
            }
        }
    }
}