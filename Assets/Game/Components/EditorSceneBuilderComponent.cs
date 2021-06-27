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

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }
    
    public void BuildLevel(int levelNumber)
    {
        var data = Resources.Load(levelNumber.ToString()) as TextAsset;
        if (data is null)
        {
            Debug.LogWarning( "File named "+levelNumber+" not found in Resources File");
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
    saturn,
    metalCheastBlue,
    metalCheastRed,
    metalCheastGrey,
    metalCheastYellow,
    metalStand,
    metalIskele,
    metalTower,
    kazan,
    dagLow,
    nukeDoor,
    tower,
    propPipes,
    rock1,
    rock2,
    vinc,
    metalBridge2,
    armBattery,
    EnergyBumb,
    mountain002,
    varil
}
