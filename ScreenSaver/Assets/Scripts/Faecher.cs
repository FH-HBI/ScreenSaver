using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fach
{
    // Start is called before the first frame update

    public string bezeichnung;
    public string beschreibung;
    public int wert;

    public Fach(string pBezeichnung = "", string pBeschreibung = "", int pWert = 0)
    {
        wert = pWert;
        bezeichnung = pBezeichnung;
        beschreibung = pBeschreibung;
    }

}
