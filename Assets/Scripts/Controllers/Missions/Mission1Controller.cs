using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mission1Controller : MonoBehaviour, StageController
{

    public GameObject VictoyModal;
    public TextMeshProUGUI PasswordField;
    public string Password;

    public void ExitedMission()
    {
        MissionController.Instance.FinishedMission(false, true);
        Destroy(gameObject);
    }

    public void FailedSection()
    {
        MissionController.Instance.FinishedMission(false);
        Destroy(gameObject);
    }

    public void FinishedSection()
    {
        VictoyModal.SetActive(true);
        PasswordField.text = Password;
    }


    public void OnModalPress()
    {
        MissionController.Instance.FinishedMission(true);
        Destroy(gameObject);
    }

    public TypingController Typing;

    // Start is called before the first frame update
    void Start()
    {
        Typing.Setup(this);
        VictoyModal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.Q)
            FinishedSection();
    }
}
