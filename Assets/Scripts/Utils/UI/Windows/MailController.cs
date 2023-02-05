using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class LocalMail
{
    public int Index;
    public MailData Data;
    public bool Opened;
    public bool Completed;
}

public class MailController : MonoBehaviour
{
    public static MailController Instance;

    public Transform MailContainer;
    public GameObject MailEntryPrefab;

    public GameObject MailModal;
    public RectTransform MailModalTrans;
    public GameObject RewardModal;

    public Image RewardImage;

    public TextMeshProUGUI ModalTitle;
    public TextMeshProUGUI ModalFrom;
    public TextMeshProUGUI ModalBody;

    public GameObject Button;
    public int SelectedMission;

    public List<LocalMail> MailsData = new List<LocalMail>();

    public GameObject FailureModal;

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
        MailModal.SetActive(false);
        FailureModal.SetActive(false);
        ModalTitle.text = "";
        ModalFrom.text = "";
        ModalBody.text = "";
        SelectedMission = -1;
    }

    public void OnShow(bool failure = false)
    {
        ClearMail();
        for (var i = MailsData.Count - 1; i >= 0; i--)
        {
            var mail = MailsData[i];
            var obj = Instantiate(MailEntryPrefab, Vector3.zero, Quaternion.identity, MailContainer);
            var mailEntry = obj.GetComponent<MailEntryController>();
            if (mailEntry == null)
            {
                Debug.LogError("Mail Entry prefab has no 'MailEntryController' component");
                continue;
            }
            mailEntry.Setup(mail.Data, this, mail.Index);
        }
        if (failure)
            FailureModal.SetActive(true);
    }

    public void CloseFailure()
    {
        FailureModal.SetActive(false);
    }

    public void OnHide()
    {
        ClearMail();
        MailModal.SetActive(false);
        ModalTitle.text = "";
        ModalFrom.text = "";
        ModalBody.text = "";
        SelectedMission = -1;
    }

    private void ClearMail()
    {
        var childCount = MailContainer.childCount;
        for (var i = childCount - 1; i >= 0; i--)
        {
            Destroy(MailContainer.GetChild(i).gameObject);
        }
    }

    public void SwitchModal(MailEntryController entry)
    {
        var act = MailModal.activeSelf;
        if (act)
        {
            MailModal.SetActive(false);
            ModalTitle.text = "";
            ModalFrom.text = "";
            ModalBody.text = "";
            SelectedMission = -1;
            return;
        }

        var completed = MailsData[entry.Index].Completed;
        Button.SetActive(entry.Data.MissionIndex != -1 && !completed);

        var pos = MailModalTrans.anchoredPosition;

        completed = completed && entry.Data.RewardImage != null;
        RewardModal.SetActive(completed);
        if (completed)
        {
            RewardImage.sprite = entry.Data.RewardImage;
            pos.x = -68.4f;

        }
        else
        {
            pos.x = 0.0f;
        }
        MailModalTrans.anchoredPosition = pos;

        MailModal.SetActive(true);
        ModalTitle.text = entry.Data.Title;
        ModalFrom.text = entry.Data.FromUser + "<br><i><" + entry.Data.FromAddress + "></i>";
        ModalBody.text = entry.Data.Body.Replace("USER", GameController.Instance.UserName);
        SelectedMission = entry.Index;
    }

    public void StartMission()
    {
        MissionController.Instance.StartMission(SelectedMission);
    }
}
