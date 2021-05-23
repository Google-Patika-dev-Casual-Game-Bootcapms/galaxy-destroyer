namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class AudioComponent : MonoBehaviour, IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("Audio Component initialized!");
        }
        
        public static AudioComponent instance;

        [SerializeField]
        Sound[] sounds;

        void Awake()
        {
            if(instance != null)
            {
                 Debug.LogError("More than one AudioManager in the scene.");
            }

            else
            {
                 instance = this;
             }
        }

        void Start()
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
                 _go.transform.SetParent(this.transform);
                sounds[i].SetSource(_go.AddComponent<AudioSource>());
            }

        }

        public void playSound(string _name)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                if(sounds[i].name == _name)
                {
                    sounds[i].Play();
                    return;
                }
            }

            //no sound with _name
             Debug.LogWarning("AudioComponent: Sound not found in list" + _name);

        }
  
    }
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f , 1f)]
        public float volume = 0.7f;

        private AudioSource source;

        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
        }

        public void Play()
        {
            source.Play();
        }

    }
}


