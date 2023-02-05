using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StageController
{
    void FinishedSection();
    void FailedSection();

    void ExitedMission();
}

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;
    public int CurrentMission;

    public List<GameObject> MissionObjects = new List<GameObject>();

    public List<bool> MissionsCompleted = new List<bool>(7);

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

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartMission(int index)
    {
        if (index < 0 || index > MissionObjects.Count - 1)
        {
            Debug.Log("Trying to start a mission that doesn't exist: " + index);
            return;
        }

        WindowController.Instance.OnMissionStart();


        Instantiate(MissionObjects[index], Vector3.zero, Quaternion.identity);
        CurrentMission = index;
        GameController.Instance.MissionHearts = 3;
    }

    public void FinishedMission(bool success, bool exited = false)
    {
        if (!MissionsCompleted[CurrentMission])
            MissionsCompleted[CurrentMission] = success;
        if (success)
            GameController.Instance.CompletedEmail(CurrentMission);
        WindowController.Instance.EnableSideButtons();

        if (!success && !exited)
            WindowController.Instance.ShowMailFailure();
        else
            WindowController.Instance.ShowMail();
    }
}
