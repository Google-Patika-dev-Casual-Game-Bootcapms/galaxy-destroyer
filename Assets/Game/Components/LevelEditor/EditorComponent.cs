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
    [SerializeField] private GameObject flyEnemyNPC;
    [SerializeField] private GameObject stableEnemyNPC;
    [SerializeField] private GameObject nonFlyEnemyNPC;
    [SerializeField] private GameObject levelEndMonster;
    [SerializeField] private GameObject friendNPC;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject backupTerrain;
    [SerializeField] private GameObject marsTerrain;
    [SerializeField] private GameObject neptuneTerrain;
    [SerializeField] private GameObject uranusTerrain;
    [SerializeField] private GameObject saturnTerrain;
    [SerializeField] private GameObject metalCheastBlue;
    [SerializeField] private GameObject metalCheastRed;
    [SerializeField] private GameObject metalCheastGrey;
    [SerializeField] private GameObject metalCheastYellow;
    [SerializeField] private GameObject metalStand;
    [SerializeField] private GameObject metalIskele;
    [SerializeField] private GameObject kazan;
    [SerializeField] private GameObject marsMountain002;
    [SerializeField] private GameObject nukeDoor;
    [SerializeField] private GameObject tower; 
    [SerializeField] private GameObject propPipes;
    [SerializeField] private GameObject marsRock002;
    [SerializeField] private GameObject standartRock01;
    [SerializeField] private GameObject vinc;
    [SerializeField] private GameObject metalTower;
    [SerializeField] private GameObject metalBridge2;
    [SerializeField] private GameObject armBattery;
    [SerializeField] private GameObject energyBumb;
    [SerializeField] private GameObject marsMountain001;
    [SerializeField] private GameObject barrelRev;
    [SerializeField] private GameObject brokePlane;
    [SerializeField] private GameObject dangerWall;
    [SerializeField] private GameObject fanApplied;
    [SerializeField] private GameObject metalCenter;
    [SerializeField] private GameObject metalDirect;
    [SerializeField] private GameObject metalIskele2;
    [SerializeField] private GameObject neptuneIceBridge;
    [SerializeField] private GameObject neptuneIceCrystal;
    [SerializeField] private GameObject powerCenter;
    [SerializeField] private GameObject saturnRock1;
    [SerializeField] private GameObject saturnRock2;
    [SerializeField] private GameObject saturnMountain1;
    [SerializeField] private GameObject saturnMountain2;
    [SerializeField] private GameObject standartRock02;
    [SerializeField] private GameObject standartMountain01;
    [SerializeField] private GameObject standartMountain02;
    [SerializeField] private GameObject marsRock01;
    [SerializeField] private GameObject neptuneParallax;
    [SerializeField] private GameObject uranusParallax;
    [SerializeField] private GameObject saturnParallax;
    [SerializeField] private GameObject marsParallax;
    [SerializeField] private GameObject earthTerrain;
    [SerializeField] private GameObject earthTree;
    [SerializeField] private GameObject stackedRock;
    [SerializeField] private GameObject lightHouse;
    [SerializeField] private GameObject lightHouseWithRocks;
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

        levelData.Position = Camera.main.transform.position;
        levelData.Rotation = Camera.main.transform.eulerAngles;
        levelData.FieldofView = Camera.main.fieldOfView;

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
        Camera.main.transform.position = levelData.Position;
        Camera.main.transform.eulerAngles = levelData.Rotation;
        Camera.main.fieldOfView = levelData.FieldofView;

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
            EGameObjectType.marsTerrain => Instantiate(marsTerrain),
            EGameObjectType.neptuneTerrain => Instantiate(neptuneTerrain),
            EGameObjectType.uranusTerrain => Instantiate(uranusTerrain),
            EGameObjectType.saturnTerrain => Instantiate(saturnTerrain),
            EGameObjectType.earthTerrain => Instantiate(earthTerrain),
            EGameObjectType.backupTerrain => Instantiate(backupTerrain),
            EGameObjectType.marsParallax => Instantiate(marsParallax),
            EGameObjectType.neptuneParallax => Instantiate(neptuneParallax),
            EGameObjectType.uranusParallax => Instantiate(uranusParallax),
            EGameObjectType.saturnParallax => Instantiate(saturnParallax),
            EGameObjectType.flyEnemyNPC => Instantiate(flyEnemyNPC),
            EGameObjectType.stableEnemyNPC => Instantiate(stableEnemyNPC),
            EGameObjectType.nonFlyEnemyNPC => Instantiate(nonFlyEnemyNPC),
            EGameObjectType.levelEndMonster => Instantiate(levelEndMonster),
            EGameObjectType.friendNPC => Instantiate(friendNPC),
            EGameObjectType.box => Instantiate(boxPrefab),
            EGameObjectType.metalCheastBlue => Instantiate(metalCheastBlue),
            EGameObjectType.metalCheastRed => Instantiate(metalCheastRed),
            EGameObjectType.metalCheastGrey => Instantiate(metalCheastGrey),
            EGameObjectType.metalCheastYellow => Instantiate(metalCheastYellow),
            EGameObjectType.metalStand => Instantiate(metalStand),
            EGameObjectType.metalIskele => Instantiate(metalIskele),
            EGameObjectType.kazan => Instantiate(kazan),
            EGameObjectType.marsMountain002 => Instantiate(marsMountain002),
            EGameObjectType.nukeDoor => Instantiate(nukeDoor),
            EGameObjectType.tower => Instantiate(tower),
            EGameObjectType.propPipes => Instantiate(propPipes),
            EGameObjectType.marsRock002 => Instantiate(marsRock002),
            EGameObjectType.standartRock01 => Instantiate(standartRock01),
            EGameObjectType.metalTower => Instantiate(metalTower),
            EGameObjectType.metalBridge => Instantiate(metalBridge2),
            EGameObjectType.armBattery => Instantiate(armBattery),
            EGameObjectType.energyBumb => Instantiate(energyBumb),
            EGameObjectType.marsMountain001 => Instantiate(marsMountain001),
            EGameObjectType.barrelRev => Instantiate(barrelRev),
            EGameObjectType.brokePlane => Instantiate(brokePlane),
            EGameObjectType.dangerWall => Instantiate(dangerWall),
            EGameObjectType.fanApplied => Instantiate(fanApplied),
            EGameObjectType.metalCenter => Instantiate(metalCenter),
            EGameObjectType.metalDirect => Instantiate(metalDirect),
            EGameObjectType.metalIskele2 => Instantiate(metalIskele2),
            EGameObjectType.neptuneIceBridge => Instantiate(neptuneIceBridge),
            EGameObjectType.neptuneIceCrystal => Instantiate(neptuneIceCrystal),
            EGameObjectType.powerCenter => Instantiate(powerCenter),
            EGameObjectType.saturnRock1 => Instantiate(saturnRock1),
            EGameObjectType.saturnRock2 => Instantiate(saturnRock2),
            EGameObjectType.saturnMountain1 => Instantiate(saturnMountain1),
            EGameObjectType.saturnMountain2 => Instantiate(saturnMountain2),
            EGameObjectType.standartRock02 => Instantiate(standartRock02),
            EGameObjectType.standartMountain01 => Instantiate(standartMountain01),
            EGameObjectType.standartMountain02 => Instantiate(standartMountain02),
            EGameObjectType.earthTree => Instantiate(earthTree),
            EGameObjectType.stackedRock => Instantiate(stackedRock),
            EGameObjectType.marsRock01 => Instantiate(marsRock01),
            EGameObjectType.lightHouse => Instantiate(lightHouse),
            EGameObjectType.lightHouseWithRocks => Instantiate(lightHouseWithRocks),
            _ => Instantiate(vinc)
        };
        return shape;
    }
}
#endif