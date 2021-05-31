using Devkit.Base.Component;
using System.IO;
using UnityEngine;

public class EditorSceneBuilderComponent : MonoBehaviour, IComponent
{
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

    public void SceneBuilder(string levelName)
    {
        string path = Application.dataPath + "/Resources/" + levelName;
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
