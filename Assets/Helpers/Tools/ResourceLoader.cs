using System.Collections;
using System.Collections.Generic;
//using UnityEditor.iOS;
using UnityEngine;

namespace DevTools.ResourceLoader
{
    public class ResourceLoader<ResourceType> where ResourceType : Object
    {
        public ResourceLoader(string resourcePath)
        {
            this.resourcePath = resourcePath;
        }

        public ResourceType Resource
        {
            get
            {
                ResourceType resource;
                if ((resource = this.loadedResource) == null)
                {
                    resource = Resources.Load<ResourceType>(this.resourcePath);
                }
                this.loadedResource = resource;
                return this.loadedResource;
            }
        }

        public List<ResourceType> LstResources
        {
            get
            {
                if(loadedResources == null || loadedResources.Count == 0)
                {
                    loadedResources = new List<ResourceType>(Resources.LoadAll<ResourceType>(resourcePath));
                }
                return loadedResources;
            }
        }

        public ResourceType GetResource(string resourceName)
        {
            return LstResources.Find(x => x.name == resourceName);
        }

        private readonly string resourcePath;

        private ResourceType loadedResource;
        private List<ResourceType> loadedResources;
    }
}
