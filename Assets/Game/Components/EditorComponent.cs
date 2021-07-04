using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR
public class EditorComponent : EditorWindow
{
    private List<string> _savedLevelNames = new List<string>();
    private string NewLevelName = String.Empty;
    
    #region Variables
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
    [SerializeField] private GameObject energyBumb;
    [SerializeField] private GameObject mountain002;
    [SerializeField] private GameObject varil;
    #endregion
    
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
           
        foreach (var t in _savedLevelNames)
        {
            if (GUILayout.Button(t))
            {
                LoadLevelDataFromJson(t);
            }
        }

        GUILayout.EndArea();
    }

    private void SaveLevelDataAsJson(string levelName)
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

    private void LoadLevelDataFromJson(string fileName)
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
            levelItemObject.transform.localScale = levelItem.Scale;
            levelItemObject.transform.position = levelItem.Position;
            levelItemObject.transform.eulerAngles = levelItem.Rotation;
        }
    }

    private void ClearScene()
    {
        var levelItems = FindObjectsOfType<GameObjectType>();
        foreach (var rect in levelItems)
            DestroyImmediate(rect.gameObject);
    }

    private List<string> GetLevelNames()
    {
        string partialName = string.Empty;

        DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Application.dataPath + "/Resources");
        FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + partialName + "*.json");

        return filesAndDirs.Select(foundFile => foundFile.Name).ToList();
    }

    private GameObject InstantiateLevelCharacter(EGameObjectType type)
    {
        var shape = type switch
        {
            EGameObjectType.flyEnemyNPC => Instantiate(flyEnemyNPCPrefab),
            EGameObjectType.stableEnemyNPC => Instantiate(stableEnemyNPCPrefab),
            EGameObjectType.nonFlyEnemyNPC => Instantiate(nonFlyEnemyNPCPrefab),
            EGameObjectType.levelEndMonster => Instantiate(levelEndMonsterPrefab),
            EGameObjectType.friendNPC => Instantiate(friendNPCPrefab),
            EGameObjectType.box => Instantiate(boxPrefab),
            EGameObjectType.mars => Instantiate(marsPrefab),
            EGameObjectType.neptune => Instantiate(neptunePrefab),
            EGameObjectType.uranus => Instantiate(uranusPrefab),
            EGameObjectType.saturn => Instantiate(saturnPrefab),
            EGameObjectType.metalCheastBlue => Instantiate(metalCheastBlue),
            EGameObjectType.metalCheastRed => Instantiate(metalCheastRed),
            EGameObjectType.metalCheastGrey => Instantiate(metalCheastGrey),
            EGameObjectType.metalCheastYellow => Instantiate(metalCheastYellow),
            EGameObjectType.metalStand => Instantiate(metalStand),
            EGameObjectType.metalIskele => Instantiate(metalIskele),
            EGameObjectType.kazan => Instantiate(kazan),
            EGameObjectType.dagLow => Instantiate(dagLow),
            EGameObjectType.nukeDoor => Instantiate(nukeDoor),
            EGameObjectType.tower => Instantiate(tower),
            EGameObjectType.propPipes => Instantiate(propPipes),
            EGameObjectType.rock1 => Instantiate(rock1),
            EGameObjectType.rock2 => Instantiate(rock2),
            EGameObjectType.metalTower => Instantiate(metalTower),
            EGameObjectType.metalBridge2 => Instantiate(metalBridge2),
            EGameObjectType.armBattery => Instantiate(armBattery),
            EGameObjectType.energyBumb => Instantiate(energyBumb),
            EGameObjectType.mountain002 => Instantiate(mountain002),
            EGameObjectType.varil => Instantiate(varil),
            _ => Instantiate(vinc)
        };
        return shape;
    }
}
#endif