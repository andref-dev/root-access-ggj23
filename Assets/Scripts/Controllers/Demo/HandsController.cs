using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class SpritePositionResponse
{
    public int position_x;
    public int position_y;
    public int rotation_angles;
    public string name;

    public Vector3 GetPosition(float default_z = 0.0f)
    {
        return new Vector3(
            position_x / 1000.0f,
            position_y / 1000.0f,
            default_z
        );
    }

    public Vector3 GetRotation()
    {
        return new Vector3(
            0.0f,
            0.0f,
            rotation_angles / 1000.0f
        );
    }
}

[System.Serializable]
public class AllHandsResponse
{
    public SpritePositionResponse[] sprites;
}

public class HandsController : MonoBehaviour
{

    public static HandsController Instance;

    public Transform HandParent;
    public GameObject HandPrefab;

    public string BaseUrl;
    public string GetHandsUrl = "/sprite-positions";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetHands();
    }

    public void GetHands()
    {
        // StartCoroutine(GetHandsRequest());
        var url = BaseUrl + GetHandsUrl;
        StartCoroutine(WebAPIBase.Get<AllHandsResponse>(url, OnGetAllHands, OnError, "sprites"));
    }

    private void OnGetAllHands(AllHandsResponse hands)
    {
        int hand_count = HandParent.childCount;
        for (int i = hand_count - 1; i >= 0; i--)
        {
            GameObject.Destroy(HandParent.GetChild(i).gameObject);
        }

        foreach (var hand in hands.sprites)
        {
            var position = hand.GetPosition();
            var rotation = Quaternion.Euler(hand.GetRotation());
            Instantiate(HandPrefab, position, rotation, HandParent);
        }
    }

    private void OnError(string error)
    {
        Debug.LogError("Error getting hands: " + error);
    }
}
