using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Hit = 0,
    Explode
}

[System.Serializable]
public class AudioInfo
{
    public AudioType audioType;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioInfo> audios = new List<AudioInfo>();    

    private Dictionary<AudioType, AudioClip> dicAudios = new Dictionary<AudioType, AudioClip>();

    private void Awake()
    {
        instance = this;

        foreach(AudioInfo info in audios)
        {
            dicAudios.Add(info.audioType, info.audioClip);
        }
    }

    public void PlaySFX(AudioType audioType)
    {
        if(dicAudios.ContainsKey(audioType) == false)
        {
            return;
        }

        audioSource.PlayOneShot(dicAudios[audioType]);
    }
}
