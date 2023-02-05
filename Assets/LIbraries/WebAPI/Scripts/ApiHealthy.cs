using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Result
{
    Waiting,
    Healthy,
    Unhealthy,
}

[System.Serializable]
public class HealthyResponse
{
    public bool healthy;
    public string status;
}

[System.Serializable]
public class CreateUserRequest
{
    public string name;

    public CreateUserRequest(string _name)
    {
        name = _name;
    }
}

[System.Serializable]
public class CreateUserResponse
{
    public string id;
    public string name;
}

[System.Serializable]
public class GetAllUsersResponse
{
    public CreateUserResponse[] users;
}

public class ApiHealthy : MonoBehaviour
{
    public Result Status;

    // Start is called before the first frame update
    void Start()
    {
        MakeHealthyRequest();
    }

    void MakeHealthyRequest()
    {
        Status = Result.Waiting;
        StartCoroutine(WebAPIBase.Get<HealthyResponse>("http://localhost:8080/health", OnSuccess, OnError));
    }

    void MakeCreateUserRequest()
    {
        var data = new CreateUserRequest("Cool user");
        StartCoroutine(WebAPIBase.Post<CreateUserRequest, CreateUserResponse>("http://localhost:8080/users", data, OnCreatedUser, OnError));
    }

    void MakeGetAllUsersRequest()
    {
        StartCoroutine(WebAPIBase.Get<GetAllUsersResponse>("http://localhost:8080/users", OnGetAllUsers, OnError));
    }

    public void OnSuccess(HealthyResponse response)
    {
        if (response.healthy)
            Status = Result.Healthy;
        else
            Status = Result.Unhealthy;
    }

    public void OnError(string error)
    {
        Debug.LogError(error);
        Status = Result.Unhealthy;
    }

    public void OnCreatedUser(CreateUserResponse response)
    {
        Debug.Log(response.id);
    }

    public void OnGetAllUsers(GetAllUsersResponse response)
    {
        foreach (var user in response.users)
        {
            Debug.Log(user.id);
        }
    }
}
