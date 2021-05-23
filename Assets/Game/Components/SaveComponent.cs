namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEditor;
    using UnityEngine;
    public class SaveComponent
    {
        
        public static void Save<T>(T dataObject , string accountDataPath){
            
            Debug.Log("SAVE COMPONENT : " + accountDataPath);          
            var data = JsonUtility.ToJson(dataObject);

            using (FileStream fs = new FileStream(accountDataPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {

                    writer.Write(data);
                }

            }

        AssetDatabase.Refresh();
            
        }

        
    }
}