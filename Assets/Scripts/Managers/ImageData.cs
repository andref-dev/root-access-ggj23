using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageFile
{
    public string Name;
    public Sprite Image;
    public int CustomIcon;
}

[System.Serializable]
public class SoundFile
{
    public string Name;
    public AudioClip Sound;

    public int CustomIcon;
}

[System.Serializable]
public class ImageData : ScriptableObject
{
    public List<ImageFile> Images = new List<ImageFile>();
    public List<SoundFile> Sounds = new List<SoundFile>();
}
