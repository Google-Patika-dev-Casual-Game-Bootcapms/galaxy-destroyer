namespace SpaceShooterProject.Component 
{
    using System.IO;
    using Devkit.Base.Component;
    using UnityEngine;

    //public delegate void FirstTimeInitialization();

    public class LoadComponent
    {
        public bool fileNotExist = false;
        //public event FirstTimeInitialization InitializeByDefault;

        public T Load<T>(){
            string path = Directory.GetCurrentDirectory();
            path = path + "/Data/" + "accountData.txt";
            string data = null;
            if(File.Exists(path)){
                data = ReadDataFromPath(path); 
                return JsonUtility.FromJson<T>(data);
            }else {
                Debug.Log("Else loop");
                /*if(InitializeByDefault != null){
                    InitializeByDefault();
                }
                InitializeByDefault += this.DebugFunc;*/
                fileNotExist = true;
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
            catch (System.Exception ex)
            {

            }
            return data;
        }
        
    }
}