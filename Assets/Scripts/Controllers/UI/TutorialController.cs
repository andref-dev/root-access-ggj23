using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public Image TutorialImage;

    public TextMeshProUGUI PageLabel;
    public Sprite Tuto1;
    public Sprite Tuto2;
    public int Page;

    // Start is called before the first frame update
    void Start()
    {
        Page = 1;
        TutorialImage.sprite = Tuto1;
    }

    public void OnShow()
    {
        Page = 1;
        TutorialImage.sprite = Tuto1;
        PageLabel.text = Page + "/2";
    }

    public void ChangeImage()
    {
        var page = Page == 1 ? 2 : 1;
        Page = page;
        TutorialImage.sprite = Page == 1 ? Tuto1 : Tuto2;
        PageLabel.text = Page + "/2";
    }
}
