using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class EditorComponent : MonoBehaviour
{
    public static EditorComponent instance;
    
    #region CharacterGenerator Field Declarations
    public GameObject flyEnemyNPCPrefab;
    public GameObject stableEnemyNPCPrefab;
    public GameObject nonFlyEnemyNPCPrefab;
    public GameObject levelEndMonsterPrefab;
    public GameObject friendNPCPrefab;
    public GameObject boxPrefab;
    // TODO: If the first assignment will not be made in Unity use >> flyEnemyNPCPrefab = Resources.Load<GameObject>("flyEnemyNPCPrefab");
    #endregion
    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad( this.gameObject );
    }
    public void SaveLevelDataAsJson(string levelName)
    {
        // get all charactertype object in the scene
        var itemsToSave = FindObjectsOfType<CharacterType>();

        string path = Application.dataPath + "/Resources/" + levelName + ".txt";
        
        // save all level data as JSON data
        var data = SerializeMapData(itemsToSave);

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(data);
            }
        }
        AssetDatabase.Refresh();
    }

    private string  SerializeMapData(CharacterType[] itemsToSave)
    {
        LevelData levelData = new LevelData();

        levelData.CameraHeight = 2f * Camera.main.orthographicSize;
        levelData.CameraWidth = levelData.CameraHeight * Camera.main.aspect;

        foreach (var item in itemsToSave)
        {
            LevelCharacterData levelItemData = new LevelCharacterData();
            levelItemData.Type = item.Type;
            levelItemData.Scale = item.transform.localScale;
            levelItemData.Position = item.transform.position;
            levelItemData.Rotation = item.transform.eulerAngles;
            levelData.LevelCharacters.Add(levelItemData);
            Debug.Log(item.gameObject.name + "GameObject is added to the list to save");
        }

        var data = JsonUtility.ToJson(levelData);

        return data;
    }


    public void LoadLevelDataFromJson(string fileName)
    {
        string path = Application.dataPath + "/Resources/" + fileName;
        var data = ReadDataFromText(path);
        var levelData = JsonUtility.FromJson<LevelData>(data);
        LoadScene(levelData);
    }

    private string ReadDataFromText(string path)
    {
        string data = null;
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    data = reader.ReadToEnd();
                }
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
        Debug.Log("Scene is Cleaned");
        // add the game object types you want to update
        foreach (var levelItem in levelData.LevelCharacters)
        {
            var levelItemObject = InstantiateLevelCharacter(levelItem.Type);
            var levelItemObjectData = levelItemObject.GetComponent<CharacterType>();
            levelItemObjectData.transform.localScale = levelItem.Scale;
            levelItemObjectData.transform.position = levelItem.Position;
            levelItemObjectData.transform.eulerAngles = levelItem.Rotation;
            Debug.Log(levelItemObject.name + " GameObject is created and updated in the scene");
        }
    }  
    
    private void ClearScene()
    {
        var levelItems = GameObject.FindObjectsOfType<CharacterType>();

        foreach (var rect in levelItems)
        {
            Debug.Log(rect.gameObject.name + " GameObject is deleted");
            DestroyImmediate(rect.gameObject);
        }
    }
    
    public List<string> GetLevelNames()
    {
        List<string> levelNames = new List<string>();

        string partialName = string.Empty;

        DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Application.dataPath + "/Resources");
        FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + partialName + "*.txt");

        foreach (FileSystemInfo foundFile in filesAndDirs)
        {
            string fullName = foundFile.Name;
            levelNames.Add(fullName);
        }
        return levelNames;
    }
    #region CharacterGenerator
    private GameObject InstantiateLevelCharacter(ECharacterType type)
    {
        GameObject shape;
        switch (type)
        {
            case ECharacterType.flyEnemyNPC:
                shape = Instantiate(flyEnemyNPCPrefab) as GameObject;
                break;
            case ECharacterType.stableEnemyNPC:
                shape = Instantiate(stableEnemyNPCPrefab) as GameObject;
                break;
            case ECharacterType.nonFlyEnemyNPC:
                shape = Instantiate(nonFlyEnemyNPCPrefab) as GameObject;
                break;
            case ECharacterType.levelEndMonster:
                shape = Instantiate(levelEndMonsterPrefab) as GameObject;
                break;
            case ECharacterType.friendNPC:
                shape = Instantiate(friendNPCPrefab) as GameObject;
                break;
            default:
                shape = Instantiate(boxPrefab) as GameObject;
                break;
        }
        return shape;
    }
    #endregion
    
}

#region EditorData
[Serializable]
public class LevelData
{
    public List<LevelCharacterData> LevelCharacters;

    public float CameraHeight;
    public float CameraWidth;
    
    //TODO:Add, on which planet will the scene begin, for example jupyter

    public LevelData()
    {
        LevelCharacters = new List<LevelCharacterData>();
    }
}

[Serializable]
public class LevelCharacterData
{
    //TODO: Add types of gameobjects data you have
    //TODO: gets the rotation wrong, needs to be fixed

    public ECharacterType Type;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
}

public enum ECharacterType
{
    //TODO: Add types of gameobjects you have

    flyEnemyNPC,
    stableEnemyNPC,
    nonFlyEnemyNPC,
    levelEndMonster,
    friendNPC,
    box
}
#endregion
