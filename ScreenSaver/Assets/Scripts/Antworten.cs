using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Antworten : MonoBehaviour
{
    [SerializeField] FragenKatalog fragenKatalog;
    [SerializeField] Text fragenText;
    [SerializeField] SpellerAntwort spellerAntwort;
    [SerializeField] Image fortschritt;
    [SerializeField] Image statisch;
    void Start()
    {
        fragenText.text = fragenKatalog.fragen[0].text;
        fragenKatalog.pos = 0;
    }


    void Update()
    {
        bool updated = false;

        // Zum Neustarten des Quiz
        if (fragenKatalog.pos == -1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.N)))
        {
            fragenKatalog.pos = 0;
            int[] nullArray = { 0, 0, 0, 0, 0 };
            fragenKatalog.faecher = nullArray;
            fragenKatalog.faecherbezeichnung[0] = "Informatik";
            fragenKatalog.faecherbezeichnung[1] = "Elektrotechnik";
            fragenKatalog.faecherbezeichnung[2] = "MCD";
            fragenKatalog.faecherbezeichnung[3] = "Wirtschaftsinformatik";
            fragenKatalog.faecherbezeichnung[4] = "Smart Building Engineering";
            updated = true;
        }
        // "Ja" als Antwort auf der Tastatur oder Speller
        else if (Input.GetKeyDown(KeyCode.J) || (spellerAntwort.neueAntwort == true && spellerAntwort.antwort == "J"))
        {
            for (int i = 0; i < fragenKatalog.faecher.Length; i++)
            {
                fragenKatalog.faecher[i] += fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
            fragenKatalog.pos++;
            updated = true;
            spellerAntwort.neueAntwort = false;
        }
        // "Nein"
        else if (Input.GetKeyDown(KeyCode.N) || (spellerAntwort.neueAntwort == true && spellerAntwort.antwort == "N"))
        {
            for (int i = 0; i < fragenKatalog.faecher.Length; i++)
            {
                fragenKatalog.faecher[i] -= fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
            fragenKatalog.pos++;
            updated = true;
            spellerAntwort.neueAntwort = false;
        }
        // Wenn eine Antwort abgegeben wurde
        if (updated)
        {
            if (fragenKatalog.fragen.Length <= fragenKatalog.pos)
            {
                printSort(fragenKatalog.faecher, fragenKatalog.faecherbezeichnung);
                /*
                fragenText.text =
                "Informatik:\t\t" + betrag(fragenKatalog.faecher[0]) + "\n" +
                "Elektrotechnik:\t" + betrag(fragenKatalog.faecher[1]) + "\n" +
                "MCD:\t" + betrag(fragenKatalog.faecher[2]) + "\n" +
                "Wirtschaftsinformatik:\t" + betrag(fragenKatalog.faecher[3]) + "\n" +
                "Smart Building Engineering:\t" + betrag(fragenKatalog.faecher[4]);
                */

                fragenText.alignment = TextAnchor.MiddleLeft;
                fragenKatalog.pos = -1;

            }

            fragenText.alignment = TextAnchor.MiddleCenter;


            // Das gleiche wie 1/12 * ... aber allgemeiner, falls wir den Balken noch größer Skalieren
            float new_scale = statisch.transform.localScale.x / 12 * (fragenKatalog.pos);
            if (fragenKatalog.pos == -1 || fragenKatalog.pos == 12)
            {
                new_scale = 1;
            }
            else
            {
                fragenText.text = fragenKatalog.fragen[fragenKatalog.pos].text;
            }

            fortschritt.transform.localScale = new Vector3(new_scale, fortschritt.transform.localScale.y, fortschritt.transform.localScale.z);
            float new_position = statisch.transform.position.x - (statisch.rectTransform.rect.width / 2) + (fortschritt.rectTransform.rect.width * new_scale / 2);
            fortschritt.transform.position = new Vector3(new_position, statisch.transform.position.y, statisch.transform.position.z);

        }
    }

    // BubbleSort Babyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
    void printSort(int[] faecher, string[] faecherbezeichnung)
    {
        for (int i = 0; i < faecher.Length; i++)
        {
            for (int j = i + 1; j < faecher.Length; j++)
            {
                if (faecher[i] < faecher[j])
                {
                    int temp = faecher[i];
                    faecher[i] = faecher[j];
                    faecher[j] = temp;

                    string tempBezeichnung = faecherbezeichnung[i];
                    faecherbezeichnung[i] = faecherbezeichnung[j];
                    faecherbezeichnung[j] = tempBezeichnung;
                }
            }
        }
        fragenText.text = "";
        for (int i = 0; i < faecher.Length; i++)
        {
            fragenText.text += fragenKatalog.faecher[i] + "\t" +
            fragenKatalog.faecherbezeichnung[i] + "\n";
        }
    }
}
