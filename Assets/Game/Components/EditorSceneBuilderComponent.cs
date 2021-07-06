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
    
    public void BuildLevel(string levelNumber)
    {
        var data = Resources.Load(levelNumber) as TextAsset;
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
