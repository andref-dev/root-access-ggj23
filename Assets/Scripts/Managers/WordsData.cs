using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordsData : ScriptableObject
{
    public List<string> ThreeLetters = new List<string>();
    public List<string> FourLetters = new List<string>();
    public List<string> SixLetters = new List<string>();
    public List<string> TenLetters = new List<string>();

    public string FourWords;

    [ContextMenu("Parse 10")]
    public void ParseFour()
    {
        var words = FourWords.Split(",");
        foreach (var word in words)
        {
            TenLetters.Add(word);
        }
    }
}
