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
        
        // public static AudioComponent instance;

        [SerializeField]
        Sound[] sounds;

        public void PlaySound(string _name)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                
                if(sounds[i].name == _name)
                {
                    sounds[i].SetSource(gameObject.AddComponent<AudioSource>());
                    sounds[i].Play();
                    
                    return;
                }
            }

            //no sound with _name
             Debug.LogWarning("AudioComponent: Sound not found in list" + _name);

        }

        public void PlayMusic(string music)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                if(sounds[i].name == music)
                {
                    sounds[i].SetSource(gameObject.AddComponent<AudioSource>());
                    sounds[i].Play();
                    sounds[i].source.loop = true;

                    return;
                }
            }
        }
  
    }
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f , 1f)]
        public float volume = 0.7f;

        public AudioSource source;

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