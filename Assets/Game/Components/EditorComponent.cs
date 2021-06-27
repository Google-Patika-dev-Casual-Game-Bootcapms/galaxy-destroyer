using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;


#if UNITY_EDITOR
public class EditorComponent : EditorWindow
{
    private List<string> _savedLevelNames = new List<string>();
    private string NewLevelName = String.Empty;

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
    
     
    [SerializeField] private GameObject metalCheastBlue;
    [SerializeField] private GameObject metalCheastRed;
    [SerializeField] private GameObject metalCheastGrey;
    [SerializeField] private GameObject metalCheastYellow;
    [SerializeField] private GameObject metalStand;
    [SerializeField] private GameObject metalIskele;
    [SerializeField] private GameObject kazan;
    [SerializeField] private GameObject dagLow;
    [SerializeField] private GameObject nukeDoor;
    [SerializeField] private GameObject tower; 
    [SerializeField] private GameObject propPipes;
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock2;
    [SerializeField] private GameObject vinc;
    [SerializeField] private GameObject metalTower;
    [SerializeField] private GameObject metalBridge2;
    [SerializeField] private GameObject armBattery;
    [SerializeField] private GameObject EnergyBumb;
    [SerializeField] private GameObject mountain002;
    [SerializeField] private GameObject varil;
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
            levelItemData.Rotation = item.transform.eulerAngles;         //TODO :Rotation value is wrong in json but it's true in world
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
            levelItemObjectData.transform.eulerAngles = levelItem.Rotation;
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
            case EGameObjectType.saturn:
                shape = Instantiate(saturnPrefab) as GameObject;
                break;
            case EGameObjectType.metalCheastBlue:
                shape = Instantiate(metalCheastBlue) as GameObject;
                break;
            case EGameObjectType.metalCheastRed:
                shape = Instantiate(metalCheastRed) as GameObject;
                break;
            case EGameObjectType.metalCheastGrey:
                shape = Instantiate(metalCheastGrey) as GameObject;
                break;
            case EGameObjectType.metalCheastYellow:
                shape = Instantiate(metalCheastYellow) as GameObject;
                break;
            case EGameObjectType.metalStand:
                shape = Instantiate(metalStand) as GameObject;
                break;
            case EGameObjectType.metalIskele:
                shape = Instantiate(metalIskele) as GameObject;
                break;
            case EGameObjectType.kazan:
                shape = Instantiate(kazan) as GameObject;
                break;
            case EGameObjectType.dagLow:
                shape = Instantiate(dagLow) as GameObject;
                break;
            case EGameObjectType.nukeDoor:
                shape = Instantiate(nukeDoor) as GameObject;
                break;
            case EGameObjectType.tower:
                shape = Instantiate(tower) as GameObject;
                break;
            case EGameObjectType.propPipes:
                shape = Instantiate(propPipes) as GameObject;
                break;
            case EGameObjectType.rock1:
                shape = Instantiate(rock1) as GameObject;
                break;
            case EGameObjectType.rock2:
                shape = Instantiate(rock2) as GameObject;
                break;
            case EGameObjectType.metalTower:
                shape = Instantiate(metalTower) as GameObject;
                break;
            case EGameObjectType.metalBridge2:
                shape = Instantiate(metalBridge2) as GameObject;
                break;
            case EGameObjectType.armBattery:
                shape = Instantiate(armBattery) as GameObject;
                break;
            case EGameObjectType.EnergyBumb:
                shape = Instantiate(EnergyBumb) as GameObject;
                break;
           case EGameObjectType.mountain002:
               shape = Instantiate(mountain002) as GameObject;
               break;
           case EGameObjectType.varil:
               shape = Instantiate(varil) as GameObject;
               break;
            default:
                shape = Instantiate(vinc) as GameObject;
                break;
        }
        return shape;
    }
}
#endif