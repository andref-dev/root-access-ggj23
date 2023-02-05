using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2Controller : MonoBehaviour, StageController
{

    public GridController Grid;
    public void FailedSection()
    {
        MissionController.Instance.FinishedMission(false, true);
        Destroy(gameObject);
    }

    public void FinishedSection()
    {
        MissionController.Instance.FinishedMission(true);
        Destroy(gameObject);
    }

    public void ExitedMission()
    {
        MissionController.Instance.FinishedMission(false);
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Grid.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
