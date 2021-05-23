namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEngine;

    //public delegate void FirstTimeInitialization();

    public class LoadComponent
    {
        //public bool fileNotExist = false;
        //public event FirstTimeInitialization InitializeByDefault;

        public T Load<T>(string accountDataPath){

            Debug.Log("LOAD COMPONENT :  " + accountDataPath);
            string data = null;
            if(File.Exists(accountDataPath)){
                data = ReadDataFromPath(accountDataPath); 
                return JsonUtility.FromJson<T>(data);
            }else {
                Debug.Log("Else loop");
                /*if(InitializeByDefault != null){
                    InitializeByDefault();
                }
                InitializeByDefault += this.DebugFunc;*/
                //fileNotExist = true;
                return default(T);
            }
            
        }

       /*private void DebugFunc(){
            Debug.Log("Debug print");
        }*/

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