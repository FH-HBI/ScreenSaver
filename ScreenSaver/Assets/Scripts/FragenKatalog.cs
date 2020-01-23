using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FragenKatalog : ScriptableObject
{
    public Frage[] fragen = new Frage[12];
    public int[] faecher = new int[5];
    public string[] faecherbezeichnung = new string[5];
    public int pos = 0;
}
