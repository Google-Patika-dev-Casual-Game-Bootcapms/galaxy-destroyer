using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;

public class LevelEditorComponent : EditorWindow
{
    #region Level Editor Unity Window

    public static LevelEditorComponent window;
    public string NewLevelName = String.Empty;

    private List<string> _savedLevelNames = new List<string>();

    [MenuItem("Tools/LevelEditor")]
    public static void CreateWindow()
    {
        window = (LevelEditorComponent)EditorWindow.GetWindow(typeof(LevelEditorComponent)); //create a window
        window.title = "Level Editor";
    }

    void OnGUI()
    {
        if (window == null)
        {
            CreateWindow();
            _savedLevelNames = new List<string>();
        }

        NewLevelName = GUI.TextField(new Rect(10, 10, position.width, 20), NewLevelName, 25);

        if (GUI.Button(new Rect(10, 40, position.width, 20), "Save Level"))
        {
            SaveLevelDataAsJson(NewLevelName);
        }

        if (GUI.Button(new Rect(10, 70, position.width, 20), "Show Saved Levels"))
        {
            CreateLevelButtons();
        }

        GUILayout.BeginArea(new Rect(10, 110, position.width, position.height));

        for (int i = 0; i < _savedLevelNames.Count; i++)
        {
            if (GUILayout.Button(_savedLevelNames[i]))
            {
                LoadLevelDataFromJson(_savedLevelNames[i]);
            }
        }

        GUILayout.EndArea();
    }

    public void CreateLevelButtons()
    {
        _savedLevelNames = new List<string>();

        _savedLevelNames = GetLevelNames();

        if (GUI.Button(new Rect(10, 90, position.width, 20), "Level1"))
        {
            Debug.Log("Test is completed");
        }
    }
    #endregion

    private void SaveLevelDataAsJson(string levelName)
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

    private string SerializeMapData(CharacterType[] itemsToSave)
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

    public string ReadDataFromText(string path)
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
            var levelItemObject = CharacterGeneratorComponent.instance.InstantiateLevelCharacter(levelItem.Type);
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
            DestroyImmediate(rect.gameObject);
            Debug.Log(rect.gameObject.name + " GameObject is deleted");
        }
    }

    private List<string> GetLevelNames()
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
}

#endif

#region CharacterGeneratorComponent
public class CharacterGeneratorComponent : MonoBehaviour
{
    public static CharacterGeneratorComponent instance;

    public GameObject flyEnemyNPCPrefab;
    public GameObject stableEnemyNPCPrefab;
    public GameObject nonFlyEnemyNPCPrefab;
    public GameObject levelEndMonsterPrefab;
    public GameObject friendNPCPrefab;
    public GameObject boxPrefab;
    // TODO: If the first assignment will not be made in Unity use >> flyEnemyNPCPrefab = Resources.Load<GameObject>("flyEnemyNPCPrefab");

    private void Awake()
    {
        instance = this;
    }
    
    public GameObject InstantiateLevelCharacter(ECharacterType type)
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
}
#endregion

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
    //TODO: Add types of gameobjects data you have

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

