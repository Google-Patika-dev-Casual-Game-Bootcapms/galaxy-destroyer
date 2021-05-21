using UnityEngine;

public class CharacterGeneratorComponent : MonoBehaviour
{
    public static CharacterGeneratorComponent instance;

    public GameObject flyEnemyNPCPrefab;
    public GameObject stableEnemyNPCPrefab;
    public GameObject nonFlyEnemyNPCPrefab;
    public GameObject levelEndMonsterPrefab;
    public GameObject friendNPCPrefab;
    public GameObject boxPrefab;

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
