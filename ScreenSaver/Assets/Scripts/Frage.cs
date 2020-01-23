using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frage
{
    // Start is called before the first frame update
    
    public string text;
    public int[] werte = new int[5];

    public Frage(string pText = "", int wertInf = 0, int wertETec = 0, int wertMCD = 0, int wertWI = 0, int wertSmart = 0){
        text = pText;
        int[] pWerte = {wertInf, wertETec, wertMCD, wertWI, wertSmart};
        werte = pWerte;
    }
    
}
