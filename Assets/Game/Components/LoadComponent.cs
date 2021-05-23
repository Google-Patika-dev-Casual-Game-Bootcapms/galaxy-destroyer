namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEngine;

    public class LoadComponent
    {
        public T Load<T>(string accountDataPath){

            //Debug.Log("LOAD COMPONENT :  " + accountDataPath);
            string data;
            data = ReadDataFromPath(accountDataPath);
            return JsonUtility.FromJson<T>(data);
            
        }

        private string ReadDataFromPath(string path)
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
            catch (System.Exception ex )
            {
                Debug.Log("Generic Exception Handler:" + ex );
            }
            return data;
        }
        
    }
}