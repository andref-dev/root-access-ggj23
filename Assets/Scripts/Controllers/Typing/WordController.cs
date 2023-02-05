using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordController : MonoBehaviour
{
    public string Word;
    public TextMeshProUGUI Label;

    public void Setup(string text)
    {
        Word = text;
        Label.text = Word;
    }

}
