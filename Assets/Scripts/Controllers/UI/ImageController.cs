using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public ImageData Data;

    public Transform ImageContainer;

    public GameObject ImageEntry;

    public GameObject ImageModal;
    public TextMeshProUGUI ModalLabel;
    public Image ModalImage;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShow()
    {
        ImageModal.SetActive(false);
        ClearImage();
        foreach (var file in Data.Images)
        {
            var obj = Instantiate(ImageEntry, Vector3.zero, Quaternion.identity, ImageContainer);
            var imageCon = obj.GetComponent<ImageContainer>();
            if (imageCon == null)
            {
                Debug.LogError("Failed to get component 'ImageContainer' on provided prefab");
                return;
            }
            imageCon.Setup(file, this);
        }
    }

    public void OnHide()
    {
        ClearImage();
    }

    private void ClearImage()
    {
        var childCount = ImageContainer.childCount;
        for (var i = childCount - 1; i >= 0; i--)
        {
            Destroy(ImageContainer.GetChild(i).gameObject);
        }
    }

    public void OpenModal(ImageFile data)
    {
        ImageModal.SetActive(true);
        ModalImage.sprite = data.Image;
        ModalLabel.text = data.Name.ToUpper();
    }

    public void CloseModal()
    {
        ImageModal.SetActive(false);
    }
}
