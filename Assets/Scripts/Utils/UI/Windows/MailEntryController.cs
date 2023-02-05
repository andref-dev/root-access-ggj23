using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MailEntryController : MonoBehaviour
{

    public MailController Mail;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI From;
    public GameObject New;
    public MailData Data;
    public Image Icon;

    public int Index;

    public void Setup(MailData data, MailController mail, int index)
    {
        Data = data;
        Mail = mail;
        Index = index;
        Title.text = Data.Title;
        From.text = Data.FromUser;
        New.SetActive(!MailController.Instance.MailsData[Index].Opened);
        if (MailController.Instance.MailsData[Index].Completed)
            Icon.color = new Color32(134, 255, 110, 255);
    }

    public void OnPress()
    {
        Mail.SwitchModal(this);
        GameController.Instance.OpenedEmail(Index);
        New.SetActive(false);

        if (Index == 2 || Index == 5)
            Icon.color = new Color32(134, 255, 110, 255);
    }

    public void Onstart()
    {
        Mail.StartMission();
    }
}
