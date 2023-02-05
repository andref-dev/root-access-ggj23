using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission0Controller : MonoBehaviour, StageController
{

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
        MissionController.Instance.FinishedMission(true);
        Destroy(gameObject);
    }

    public MapsController Maps;

    // Start is called before the first frame update
    void Start()
    {
        Maps.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.Q)
            FinishedSection();
    }
}
