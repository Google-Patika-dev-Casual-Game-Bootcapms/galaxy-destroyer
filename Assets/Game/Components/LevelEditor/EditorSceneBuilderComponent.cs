using Devkit.Base.Component;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EditorSceneBuilderComponent : MonoBehaviour, IComponent
{
    private ComponentContainer MyComponent;

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
    [SerializeField] private GameObject earthParallax;

    #endregion

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
    }

    public void BuildPlanet(int planetID)
    {
        //TODO: refactor
        switch (planetID)
        {
            case 0:
                BuildLevel("EarthParallax");
                break;
            case 1:
                BuildLevel("MarsParallax"); // TODO Add Saturn
                break;
            case 2:
                BuildLevel("NeptuneParallax");
                break;
            case 3:
                BuildLevel("MarsParallax");
                break;
            case 4:
                BuildLevel("UranusParallax");
                break;
            default:
                BuildLevel("EarthParallax");
                break;
        }
    }

    public void BuildLevel(string levelName)
    {
        var data = Resources.Load(levelName) as TextAsset;
        if (data is null)
        {
            Debug.LogWarning("File named " + levelName + " not found in Resources File");
            return;
        }

        var levelData = JsonUtility.FromJson<LevelData>(data.text);
        LoadScene(levelData);
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

    public void ClearScene()
    {
        var levelItems = FindObjectsOfType<GameObjectType>();
        foreach (var rect in levelItems)
            Destroy(rect.gameObject);
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
            EGameObjectType.earthParallax => Instantiate(earthParallax),
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

[Serializable]
public class LevelData
{
    public List<LevelCharacterData> LevelCharacters;
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
    marsTerrain,
    neptuneTerrain,
    uranusTerrain,
    saturnTerrain,
    metalCheastBlue,
    metalCheastRed,
    metalCheastGrey,
    metalCheastYellow,
    metalStand,
    metalIskele,
    metalTower,
    kazan,
    marsMountain002,
    nukeDoor,
    tower,
    propPipes,
    marsRock002,
    standartRock01,
    vinc,
    metalBridge,
    armBattery,
    energyBumb,
    marsMountain001,
    barrelRev,
    brokePlane,
    dangerWall,
    fanApplied,
    metalCenter,
    metalDirect,
    metalIskele2,
    neptuneIceBridge,
    neptuneIceCrystal,
    powerCenter,
    saturnRock1,
    saturnRock2,
    saturnMountain1,
    saturnMountain2,
    standartRock02,
    standartMountain01,
    standartMountain02,
    marsRock01,
    marsParallax,
    neptuneParallax,
    uranusParallax,
    saturnParallax,
    earthTerrain,
    earthTree,
    stackedRock,
    lightHouse,
    lightHouseWithRocks,
    backupTerrain,
    earthParallax
}