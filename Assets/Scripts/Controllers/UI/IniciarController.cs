using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IniciarController : MonoBehaviour
{

    public GameObject IniciarMenu;
    public GameObject CreditsModal;
    public TextMeshProUGUI UserLabel;

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
        CreditsModal.SetActive(false);
        IniciarMenu.SetActive(true);
        UserLabel.text = GameController.Instance.UserName;
    }

    public void OnHide()
    {
        CreditsModal.SetActive(false);
    }

    public void RestartGame()
    {
        // Logout user
        WindowController.Instance.HideIniciar();
        GameController.Instance.Logout();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        IniciarMenu.SetActive(false);
        CreditsModal.SetActive(true);
    }
}
