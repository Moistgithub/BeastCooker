using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]

public class Dialogue
{
    public string npcName;
    //creates a text area with a minimum number of lines and a maximum
    [TextArea(3,10)]
    public string[] sentences;
}
