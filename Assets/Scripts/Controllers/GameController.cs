using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public TextMeshProUGUI WantedDebugLabel;
    public TextMeshProUGUI HeartDebugLabel;

    public bool Started;

    public bool CanLixeira;

    public string UserName;

    public List<MailData> Mails = new List<MailData>();

    public GameObject NewMail;

    private int _wantedLevel;
    public int WantedLevel
    {
        set
        {
            _wantedLevel = value;
            WantedDebugLabel.text = _wantedLevel + "";
        }
        get
        {
            return _wantedLevel;
        }
    }

    private int _missionHearts;
    public int MissionHearts
    {
        set
        {
            _missionHearts = value;
            HeartDebugLabel.text = _missionHearts + "";
        }
        get
        {
            return _missionHearts;
        }
    }

    public TMP_InputField UserInput;
    public GameObject LoginWindow;

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
        WantedLevel = 0;
        MissionHearts = 0;
        //Started = true;
        WindowController.Instance.DisableSideButtons();
        WindowController.Instance.ShowingIniciar = true;
        Started = true;
        LoginWindow.SetActive(true);

        MailController.Instance.MailsData.Add(new LocalMail
        {
            Index = 0,
            Data = Mails[0],
            Opened = false,
            Completed = false
        });
        MailController.Instance.MailsData.Add(new LocalMail
        {
            Index = 1,
            Data = Mails[1],
            Opened = false,
            Completed = false
        });
        NewMail.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.ResetGame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OpenedEmail(int index)
    {
        MailController.Instance.MailsData[index].Opened = true;
        if (index == 2 || index == 5)
        {
            MailController.Instance.MailsData[index].Completed = true;
            CompletedEmail(index);
        }
        if (index == 5)
            CanLixeira = true;

        NewMail.SetActive(false);
        foreach (var mail in MailController.Instance.MailsData)
        {
            if (!mail.Opened)
                NewMail.SetActive(true);
        }
    }

    public void CompletedEmail(int index)
    {
        MailController.Instance.MailsData[index].Completed = true;

        if (MailController.Instance.MailsData.Count == 2)
        {
            if (!MailController.Instance.MailsData[0].Completed ||
                !MailController.Instance.MailsData[1].Completed
            )
                return;

            MailController.Instance.MailsData.Add(new LocalMail
            {
                Index = 2,
                Data = Mails[2],
                Opened = false,
                Completed = false
            });
            MailController.Instance.MailsData.Add(new LocalMail
            {
                Index = 3,
                Data = Mails[3],
                Opened = false,
                Completed = false
            });
            NewMail.SetActive(true);
        }

        if (MailController.Instance.MailsData.Count == 4)
        {
            if (!MailController.Instance.MailsData[3].Completed)
                return;

            MailController.Instance.MailsData.Add(new LocalMail
            {
                Index = 4,
                Data = Mails[4],
                Opened = false,
                Completed = false
            });
            NewMail.SetActive(true);
        }

        if (MailController.Instance.MailsData.Count == 5)
        {
            if (!MailController.Instance.MailsData[4].Completed || CanLixeira)
                return;

            MailController.Instance.MailsData.Add(new LocalMail
            {
                Index = 5,
                Data = Mails[5],
                Opened = false,
                Completed = false
            });
            NewMail.SetActive(true);
        }

    }

    public void SetUser()
    {
        if (UserInput.text == "")
        {
            var trans = UserInput.transform as RectTransform;
            trans.DOKill(false);
            trans.DOShakeAnchorPos(0.3f, 1, 100);
            return;
        }

        UserName = UserInput.text;
        WindowController.Instance.EnableSideButtons();
        WindowController.Instance.ShowingIniciar = false;
        Started = true;
        Destroy(LoginWindow);
    }
}
