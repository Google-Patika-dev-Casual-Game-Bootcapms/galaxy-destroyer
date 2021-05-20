namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEngine;
    public class LoadComponent
    {
        
        public static T Load<T>(){
            string path = Directory.GetCurrentDirectory();
            path = path + "/Data/" + "accountData.txt";
            var data = ReadDataFromPath(path);
            return JsonUtility.FromJson<T>(data);
        }

        private static string ReadDataFromPath(string path)
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
                
            }
            return data;
        }
        
    }
}