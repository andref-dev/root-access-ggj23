using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public List<GameObject> HiddenFiles = new List<GameObject>();

    public GameObject PasswordModal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSecret()
    {
        PasswordModal.SetActive(true);
    }

    public void HideSecret()
    {
        PasswordModal.SetActive(false);
    }

    public void OnShow()
    {
        foreach (var item in HiddenFiles)
        {
            item.SetActive(GameController.Instance.CanLixeira);
        }
        PasswordModal.SetActive(false);
    }
}
