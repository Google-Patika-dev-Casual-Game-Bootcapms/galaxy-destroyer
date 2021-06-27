using Devkit.Base.Component;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EditorSceneBuilderComponent : MonoBehaviour, IComponent
{
    public ComponentContainer MyComponent;
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

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
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

    #region ObjectGenerator
    private GameObject InstantiateLevelCharacter(EGameObjectType type)
    {
        GameObject shape;
        switch (type)
        {
            case EGameObjectType.flyEnemyNPC:
                shape = Instantiate(flyEnemyNPCPrefab);
                break;
            case EGameObjectType.stableEnemyNPC:
                shape = Instantiate(stableEnemyNPCPrefab);
                break;
            case EGameObjectType.nonFlyEnemyNPC:
                shape = Instantiate(nonFlyEnemyNPCPrefab);
                break;
            case EGameObjectType.levelEndMonster:
                shape = Instantiate(levelEndMonsterPrefab);
                break;
            case EGameObjectType.friendNPC:
                shape = Instantiate(friendNPCPrefab);
                break;
            case EGameObjectType.box:
                shape = Instantiate(boxPrefab);
                break;
            case EGameObjectType.mars:
                shape = Instantiate(marsPrefab);
                break;
            case EGameObjectType.neptune:
                shape = Instantiate(neptunePrefab);
                break;
            case EGameObjectType.uranus:
                shape = Instantiate(uranusPrefab);
                break;
            case EGameObjectType.saturn:
                shape = Instantiate(saturnPrefab);
                break;
            case EGameObjectType.metalCheastBlue:
                shape = Instantiate(metalCheastBlue);
                break;
            case EGameObjectType.metalCheastRed:
                shape = Instantiate(metalCheastRed);
                break;
            case EGameObjectType.metalCheastGrey:
                shape = Instantiate(metalCheastGrey);
                break;
            case EGameObjectType.metalCheastYellow:
                shape = Instantiate(metalCheastYellow);
                break;
            case EGameObjectType.metalStand:
                shape = Instantiate(metalStand);
                break;
            case EGameObjectType.metalIskele:
                shape = Instantiate(metalIskele);
                break;
            case EGameObjectType.kazan:
                shape = Instantiate(kazan);
                break;
            case EGameObjectType.dagLow:
                shape = Instantiate(dagLow);
                break;
            case EGameObjectType.nukeDoor:
                shape = Instantiate(nukeDoor);
                break;
            case EGameObjectType.tower:
                shape = Instantiate(tower);
                break;
            case EGameObjectType.propPipes:
                shape = Instantiate(propPipes);
                break;
            case EGameObjectType.rock1:
                shape = Instantiate(rock1);
                break;
            case EGameObjectType.rock2:
                shape = Instantiate(rock2);
                break;
            case EGameObjectType.metalTower:
                shape = Instantiate(metalTower);
                break;
            case EGameObjectType.metalBridge2:
                shape = Instantiate(metalBridge2);
                break;
            case EGameObjectType.armBattery:
                shape = Instantiate(armBattery);
                break;
            case EGameObjectType.energyBumb:
                shape = Instantiate(energyBumb);
                break;
           case EGameObjectType.mountain002:
               shape = Instantiate(mountain002);
               break;
           case EGameObjectType.varil:
               shape = Instantiate(varil);
               break;
            default:
                shape = Instantiate(vinc);
                break;
        }
        return shape;
    }
    #endregion
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
    energyBumb,
    mountain002,
    varil
}
