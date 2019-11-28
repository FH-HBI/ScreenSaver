using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frage
{
    // Start is called before the first frame update
    public string text;
    public Frage nein;
    public Frage ja;
    public int index;

    public Frage(string pText = "", Frage pNein = null, Frage pJa = null, int pIndex = 0){
        text = pText;
        nein = pNein;
        ja = pJa;
        index = pIndex;
    }
}
