using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FragenKatalog : ScriptableObject
{
    public Frage[] fragen = new Frage[12];
    public Fach[] faecher = new Fach[5];
    public int pos = 0;

    public bool[] antworten = new bool[12];

}
