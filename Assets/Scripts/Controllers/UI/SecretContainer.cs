using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SecretContainer : MonoBehaviour
{
    public TMP_InputField InputField;

    public RectTransform ModalBody;

    public List<int> Numbers = new List<int>();
    public List<TextMeshProUGUI> Labels = new List<TextMeshProUGUI>();


    public void SubmitPassword()
    {
        if (Numbers[0] == 2 &&
            Numbers[1] == 0 &&
            Numbers[2] == 1 &&
            Numbers[3] == 4)
        {
            SoundController.Instance.BmgSource.Stop();
            SoundController.Instance.ActualBmg = "";
            SceneManager.LoadScene("SceneEnd");
            return;
        }

        ModalBody.DOKill(false);
        ModalBody.DOShakeAnchorPos(0.3f, 20, 100);
    }

    public void UpValue(int index)
    {
        Numbers[index]++;
        if (Numbers[index] > 9)
            Numbers[index] = 0;
        UpdateLabels();
    }

    public void DownValue(int index)
    {
        Numbers[index]--;
        if (Numbers[index] < 0)
            Numbers[index] = 9;
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        for (var i = 0; i < 4; i++)
        {
            Labels[i].text = Numbers[i] + "";
        }
    }
}
