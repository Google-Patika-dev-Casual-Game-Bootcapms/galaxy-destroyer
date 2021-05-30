using Devkit.Base.Component;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

namespace SpaceShooterProject.Component
{
#if UNITY_EDITOR
    public class EditorComponent : EditorWindow, IComponent
    {
        private GameObject flyEnemyNPCPrefab;
        private GameObject stableEnemyNPCPrefab;
        private GameObject nonFlyEnemyNPCPrefab;
        private GameObject levelEndMonsterPrefab;
        private GameObject friendNPCPrefab;
        private GameObject boxPrefab;
        private GameObject marsPrefab;
        private GameObject neptunePrefab;
        private GameObject uranusPrefab;
        private GameObject saturnPrefab;

        private List<string> _savedLevelNames = new List<string>();
        private string NewLevelName = String.Empty;

        public ComponentContainer myComponent;

        public void Initialize(ComponentContainer componentContainer)
        {
            myComponent = componentContainer;

            flyEnemyNPCPrefab = Resources.Load<GameObject>("flyEnemyNPCPrefab");
            stableEnemyNPCPrefab = Resources.Load<GameObject>("stableEnemyNPCPrefab");
            nonFlyEnemyNPCPrefab = Resources.Load<GameObject>("nonFlyEnemyNPCPrefab");
            levelEndMonsterPrefab = Resources.Load<GameObject>("levelEndMonsterPrefab");
            friendNPCPrefab = Resources.Load<GameObject>("friendNPCPrefab");
            boxPrefab = Resources.Load<GameObject>("boxPrefab");
            marsPrefab = Resources.Load<GameObject>("marsPrefab");
            neptunePrefab = Resources.Load<GameObject>("neptunePrefab");
            uranusPrefab = Resources.Load<GameObject>("uranusPrefab");
            saturnPrefab = Resources.Load<GameObject>("saturnPrefab");
        }

        [MenuItem("Tools/LevelEditor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            EditorComponent window = (EditorComponent)EditorWindow.GetWindow(typeof(EditorComponent));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.TextArea("You can load level at runtime or after run time.");
            GUILayout.BeginArea(new Rect(15, 20, position.width, position.height));

            NewLevelName = GUI.TextField(new Rect(10, 10, position.width, 20), NewLevelName, 25);
            if (GUI.Button(new Rect(10, 40, position.width, 20), "Save Level"))
            {
                SaveLevelDataAsJson(NewLevelName);
            }

            if (GUI.Button(new Rect(10, 70, position.width, 20), "Show Saved Levels"))
            {
                _savedLevelNames = new List<string>();
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
            GUILayout.EndArea();
        }
        public void SaveLevelDataAsJson(string levelName)
        {
            var itemsToSave = FindObjectsOfType<GameObjectType>();
            string path = Application.dataPath + "/Resources/" + levelName + ".txt";
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
                levelItemData.Rotation = item.transform.eulerAngles;
                levelData.LevelCharacters.Add(levelItemData);
                Debug.Log(item.gameObject.name + "GameObject is added to the list to save");
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
                Debug.Log(levelItemObject.name + " GameObject is created and updated in the scene");
            }
        }

        private void ClearScene()
        {
            var levelItems = GameObject.FindObjectsOfType<GameObjectType>();

            foreach (var rect in levelItems)
            {
                Debug.Log(rect.gameObject.name + " GameObject is deleted");
                DestroyImmediate(rect.gameObject);
            }
        }

        public List<string> GetLevelNames()
        {
            List<string> levelNames = new List<string>();

            string partialName = string.Empty;

            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Application.dataPath + "/Resources");
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + partialName + "*.txt");

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
            shape = null;
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
}
#endif


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
    //TODO: gets the rotation wrong, needs to be fixed

    public EGameObjectType Type;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
}

public enum EGameObjectType
{
    //TODO: Add types of gameobjects you have

    flyEnemyNPC,
    stableEnemyNPC,
    nonFlyEnemyNPC,
    levelEndMonster,
    friendNPC,
    box,
    mars,
    neptune,
    uranus,
    saturn
}