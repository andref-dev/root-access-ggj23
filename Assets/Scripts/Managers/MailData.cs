using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MailData : ScriptableObject
{
    public string FromAddress;
    public string FromUser;
    public string Title;
    public string Body;
    public int MissionIndex;
    public Sprite RewardImage;

}
