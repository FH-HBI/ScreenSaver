﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FragenKatalog : ScriptableObject
{
    public Frage[] fragen = new Frage[5];
    public string[] antworten = new string[5];
    public int laenge = 5;
    public int pos = 0;
}