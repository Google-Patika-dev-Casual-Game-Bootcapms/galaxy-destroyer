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
        
        [SerializeField]
        Sound[] sounds;

        public void PlaySound(string soundName)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                
                if(sounds[i].name == soundName)
                {
                    sounds[i].SetSource(gameObject.AddComponent<AudioSource>());
                    sounds[i].Play();
                    
                    return;
                }
            }

            //no sound with _name
             Debug.LogWarning("AudioComponent: Sound not found in list" + soundName);

        }

        public void PlayMusic(string musicName)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
                if(sounds[i].name == musicName)
                {
                    sounds[i].SetSource(gameObject.AddComponent<AudioSource>());
                    sounds[i].Play();
                    sounds[i].source.loop = true;

                    return;
                }
            }
        }

        public void AudioControl(float audioVolume)
        {
            for(int i=0 ; i<sounds.Length ; i++)
            {
               if(audioVolume == 0f)
               {
                  sounds[i].Stop();
               }

               else
               {
                   sounds[i].source.volume = audioVolume ;
               }
               
            }
        }
  
    }
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        public AudioSource source;

        public void SetSource(AudioSource audioSource)
        {
            source = audioSource;
            source.clip = clip;
            source.volume = 0.7f;
        }

        public void Play()
        {
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
        }


    }
}

