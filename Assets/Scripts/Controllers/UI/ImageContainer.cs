using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImageContainer : MonoBehaviour
{

    public ImageFile Data;

    public TextMeshProUGUI Label;

    public ImageController Image;

    public Image Icon;

    public Sprite CustomIconTriangle;
    public Sprite CustomIconHashtag;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(ImageFile data, ImageController imageCotroller)
    {
        Image = imageCotroller;
        Data = data;
        Label.text = Data.Name.ToUpper();

        if (Data.CustomIcon == 0)
            return;

        if (Data.CustomIcon == 1)
            Icon.sprite = CustomIconTriangle;
        if (Data.CustomIcon == 2)
            Icon.sprite = CustomIconHashtag;
    }

    public void OpenModal()
    {
        Image.OpenModal(Data);
    }
}
