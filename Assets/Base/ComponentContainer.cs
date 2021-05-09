namespace Devkit.Base.Component 
{
    using System.Collections.Generic;

    public class ComponentContainer
    {
        private Dictionary<string, object> components;

        public ComponentContainer() 
        {
            components = new Dictionary<string, object>();
        }

        public void AddComponent(string componentKey, object component) 
        {
            components.Add(componentKey, component);
        }

        public object GetComponent(string componentKey) 
        {
            if (!components.ContainsKey(componentKey)) 
            {
                return null;
            }

            return components[componentKey];
        }
    }

}

