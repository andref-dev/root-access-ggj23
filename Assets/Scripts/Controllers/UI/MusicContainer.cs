using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MusicContainer : MonoBehaviour
{
    public SoundFile Data;
    public TextMeshProUGUI Label;

    public Image Icon;
    public Sprite CustomIcon;

    public void Setup(SoundFile data)
    {
        Data = data;
        Label.text = data.Name.ToUpper();
        if (data.CustomIcon != 0)
            Icon.sprite = CustomIcon;
    }

    public void OnPress()
    {
        SoundController.ChangeBmg(Data.Name, Data.Sound);
    }
}
