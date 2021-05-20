namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEditor;
    using UnityEngine;
    public class SaveComponent
    {
        
        public static void Save(AccountData accountData){

            string path = Directory.GetCurrentDirectory();
            path = path + "/Data/" + "accountData.txt";
            var data = JsonUtility.ToJson(accountData);

            using (FileStream fs = new FileStream(path, FileMode.Create))
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