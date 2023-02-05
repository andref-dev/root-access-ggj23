using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebAPIBase
{
    public static IEnumerator Get<T>(string url, System.Action<T> success, System.Action<string> error, string top_level_name = "")
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                error(request.error);
                yield break;
            }

            var response_json = request.downloadHandler.text;
            if (top_level_name != "")
            {
                response_json = string.Concat("{\"", top_level_name, "\":", response_json, "}");
            }
            success(JsonUtility.FromJson<T>(response_json));
        }
    }

    public static IEnumerator Post<T, U>(string url, T data, System.Action<U> success, System.Action<string> error)
    {
        var json_data = JsonUtility.ToJson(data);
        using (var request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json_data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                error(request.error);
                yield break;
            }

            success(JsonUtility.FromJson<U>(request.downloadHandler.text));
        }
    }
}
