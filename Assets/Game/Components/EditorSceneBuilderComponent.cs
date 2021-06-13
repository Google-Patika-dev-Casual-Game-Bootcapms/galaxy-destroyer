using Devkit.Base.Component;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EditorSceneBuilderComponent : MonoBehaviour, IComponent
{
    public ComponentContainer myComponent;

    [SerializeField] private GameObject flyEnemyNPCPrefab;
    [SerializeField] private GameObject stableEnemyNPCPrefab;
    [SerializeField] private GameObject nonFlyEnemyNPCPrefab;
    [SerializeField] private GameObject levelEndMonsterPrefab;
    [SerializeField] private GameObject friendNPCPrefab;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject marsPrefab;
    [SerializeField] private GameObject neptunePrefab;
    [SerializeField] private GameObject uranusPrefab;
    [SerializeField] private GameObject saturnPrefab;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    public void BuildScene(string levelName)
    {
        string path = Application.dataPath + "/Resources/" + levelName;
        var data = ReadDataFromText(path);
        var levelData = JsonUtility.FromJson<LevelData>(data);
        LoadScene(levelData);
    }

    private string ReadDataFromText(string path)
    {
        string data = null;
        try
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                data = reader.ReadToEnd();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
        return data;
    }

    private void LoadScene(LevelData levelData)
    {
        ClearScene();

        foreach (var levelItem in levelData.LevelCharacters)
        {
            var levelItemObject = InstantiateLevelCharacter(levelItem.Type);
            var levelItemObjectData = levelItemObject.GetComponent<GameObjectType>();
            levelItemObjectData.transform.localScale = levelItem.Scale;
            levelItemObjectData.transform.position = levelItem.Position;
            levelItemObjectData.transform.eulerAngles = levelItem.Rotation;            //TODO: Rotation value is wrong in json but it's true in world
        }
    }

    private void ClearScene()
    {
        var levelItems = GameObject.FindObjectsOfType<GameObjectType>();
        foreach (var rect in levelItems)
            DestroyImmediate(rect.gameObject);
    }
    private GameObject InstantiateLevelCharacter(EGameObjectType type)
    {
        GameObject shape;
        switch (type)
        {
            case EGameObjectType.flyEnemyNPC:
                shape = Instantiate(flyEnemyNPCPrefab) as GameObject;
                break;
            case EGameObjectType.stableEnemyNPC:
                shape = Instantiate(stableEnemyNPCPrefab) as GameObject;
                break;
            case EGameObjectType.nonFlyEnemyNPC:
                shape = Instantiate(nonFlyEnemyNPCPrefab) as GameObject;
                break;
            case EGameObjectType.levelEndMonster:
                shape = Instantiate(levelEndMonsterPrefab) as GameObject;
                break;
            case EGameObjectType.friendNPC:
                shape = Instantiate(friendNPCPrefab) as GameObject;
                break;
            case EGameObjectType.box:
                shape = Instantiate(boxPrefab) as GameObject;
                break;
            case EGameObjectType.mars:
                shape = Instantiate(marsPrefab) as GameObject;
                break;
            case EGameObjectType.neptune:
                shape = Instantiate(neptunePrefab) as GameObject;
                break;
            case EGameObjectType.uranus:
                shape = Instantiate(uranusPrefab) as GameObject;
                break;
            default:
                shape = Instantiate(saturnPrefab) as GameObject;
                break;
        }
        return shape;
    }
}

[Serializable]
public class LevelData
{
    public List<LevelCharacterData> LevelCharacters;

    public float CameraHeight;
    public float CameraWidth;

    public LevelData()
    {
        LevelCharacters = new List<LevelCharacterData>();
    }
}

[Serializable]
public class LevelCharacterData
{
    public EGameObjectType Type;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
}

public enum EGameObjectType
{
    flyEnemyNPC,
    stableEnemyNPC,
    nonFlyEnemyNPC,
    levelEndMonster,
    friendNPC,
    box,
    mars,
    neptune,
    uranus,
    saturn
}
