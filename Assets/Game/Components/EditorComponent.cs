using Devkit.Base.Component;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;


#if UNITY_EDITOR
public class EditorComponent : EditorWindow, IComponent
{
    private List<string> _savedLevelNames = new List<string>();
    private string NewLevelName = String.Empty;

    public ComponentContainer myComponent;

    public GameObject flyEnemyNPCPrefab;
    public GameObject stableEnemyNPCPrefab;
    public GameObject nonFlyEnemyNPCPrefab;
    public GameObject levelEndMonsterPrefab;
    public GameObject friendNPCPrefab;
    public GameObject boxPrefab;
    public GameObject marsPrefab;
    public GameObject neptunePrefab;
    public GameObject uranusPrefab;
    public GameObject saturnPrefab;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    [MenuItem("Tools/LevelEditor")]
    private static void Init()
    {
        EditorComponent window = (EditorComponent)EditorWindow.GetWindow(typeof(EditorComponent));
        window.Show();
    }

    private void OnGUI()
    {
        NewLevelName = GUI.TextField(new Rect(10, 10, position.width, 20), NewLevelName, 25);

        if (GUI.Button(new Rect(10, 40, position.width, 20), "Save Level"))
        {
            SaveLevelDataAsJson(NewLevelName);
        }

        if (GUI.Button(new Rect(10, 70, position.width, 20), "Show Saved Levels"))
        {           
            _savedLevelNames = GetLevelNames();
        }

        GUILayout.BeginArea(new Rect(10, 150, position.width, position.height));
           
        for (int i = 0; i < _savedLevelNames.Count; i++)
        {
            if (GUILayout.Button(_savedLevelNames[i]))
            {
                LoadLevelDataFromJson(_savedLevelNames[i]);
            }
        }

        GUILayout.EndArea();
    }
    public void SaveLevelDataAsJson(string levelName)
    {
        var itemsToSave = FindObjectsOfType<GameObjectType>();
        string path = Application.dataPath + "/Resources/" + levelName + ".json";
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

    private string SerializeMapData(GameObjectType[] itemsToSave)
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
            //TODO :Rotation value is wrong in json but it's true in world
            levelItemData.Rotation = item.transform.localRotation;
            levelData.LevelCharacters.Add(levelItemData);
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

        foreach (var levelItem in levelData.LevelCharacters)
        {
            var levelItemObject = InstantiateLevelCharacter(levelItem.Type);
            var levelItemObjectData = levelItemObject.GetComponent<GameObjectType>();
            levelItemObjectData.transform.localScale = levelItem.Scale;
            levelItemObjectData.transform.position = levelItem.Position;
            levelItemObjectData.transform.localRotation = levelItem.Rotation;
        }
    }

    private void ClearScene()
    {
        var levelItems = GameObject.FindObjectsOfType<GameObjectType>();
        foreach (var rect in levelItems)
            DestroyImmediate(rect.gameObject);
    }

    public List<string> GetLevelNames()
    {
        List<string> levelNames = new List<string>();

        string partialName = string.Empty;

        DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Application.dataPath + "/Resources");
        FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + partialName + "*.json");

        foreach (FileSystemInfo foundFile in filesAndDirs)
        {
            string fullName = foundFile.Name;
            levelNames.Add(fullName);
        }
        return levelNames;
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
#endif