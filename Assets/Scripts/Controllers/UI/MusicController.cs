using UnityEngine;

public class MusicController : MonoBehaviour
{
    public ImageData Data;

    public Transform MusicContainer;

    public GameObject MusicEntry;


    public void OnShow()
    {
        ClearMusic();
        foreach (var file in Data.Sounds)
        {
            var obj = Instantiate(MusicEntry, Vector3.zero, Quaternion.identity, MusicContainer);
            var musicCon = obj.GetComponent<MusicContainer>();
            if (musicCon == null)
            {
                Debug.LogError("Failed to get component 'MusicContainer' on provided prefab");
                return;
            }
            musicCon.Setup(file);
        }
    }

    public void OnHide()
    {
        ClearMusic();
    }

    private void ClearMusic()
    {
        var childCount = MusicContainer.childCount;
        for (var i = childCount - 1; i >= 0; i--)
        {
            Destroy(MusicContainer.GetChild(i).gameObject);
        }
    }
}
