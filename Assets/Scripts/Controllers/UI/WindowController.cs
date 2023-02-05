using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public static WindowController Instance;
    public MailController Mail;
    public TrashController Trash;
    public IniciarController Iniciar;
    public ImageController Image;
    public MusicController Music;
    public TutorialController Tutorial;

    public bool ShowingIniciar;

    public List<BoxCollider2D> SideBarButtons = new List<BoxCollider2D>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Iniciar.gameObject.SetActive(false);
        Mail.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void ShowIniciar()
    {

        Iniciar.gameObject.SetActive(true);
        Iniciar.OnShow();
    }

    public void HideIniciar()
    {
        Iniciar.gameObject.SetActive(false);
        Iniciar.OnHide();
    }

    public void OnMissionStart()
    {
        Mail.OnHide();
        Mail.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
        DisableSideButtons();
    }

    public void ShowMail()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(true);
        Trash.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
        Mail.OnShow();
    }

    public void ShowMailFailure()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(true);
        Trash.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
        Mail.OnShow(true);
    }

    public void HideMail()
    {
        if (ShowingIniciar)
            return;

        Mail.OnHide();
        Mail.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void ShowTrash()
    {
        if (ShowingIniciar)
            return;

        Mail.OnHide();
        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
        Trash.gameObject.SetActive(true);
        Trash.OnShow();
        Music.gameObject.SetActive(false);
    }

    public void HideTrash()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void ShowImage()
    {
        Iniciar.gameObject.SetActive(false);
        Iniciar.OnHide();
        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(true);
        Image.OnShow();
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void HideImage()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Image.OnHide();
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void ShowMusic()
    {
        Iniciar.gameObject.SetActive(false);
        Iniciar.OnHide();
        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(true);
        Tutorial.gameObject.SetActive(false);
        Music.OnShow();
    }

    public void HideMusic()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
        Music.OnHide();
    }

    public void ShowTutorial()
    {
        if (ShowingIniciar)
            return;

        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(true);
        Tutorial.OnShow();
    }

    public void HideTutorial()
    {
        Mail.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        Trash.gameObject.SetActive(false);
        Music.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(false);
    }

    public void DisableSideButtons()
    {
        foreach (var btn in SideBarButtons)
        {
            btn.enabled = false;
        }
    }

    public void EnableSideButtons()
    {
        foreach (var btn in SideBarButtons)
        {
            btn.enabled = true;
        }
    }
}
