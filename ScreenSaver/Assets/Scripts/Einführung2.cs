using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Einführung2 : MonoBehaviour
{
    [SerializeField] Text einleitung;
    [SerializeField] Image loadingBar;
    public int delay = 10;
    private string[] text;
    private int pos = 0;
    void Start()
    {
        text = new string[3];
        text[0] = "Wissenschaft entwicklet sich immer weiter. Wir haben uns mit dem Fortschritt beschäftigt, der die heutige Form von Sprach- und Gestenabhängiger Kommuni- kation durch Gehirnwellen gesteuerte Kommunikation ablösen wird.";
        text[1] = "Deshalb haben wir ein Programm entwickelt, dass mit Hilfe des Unicorn EEG ihre Gehirnwellen ausliest und erkennt ob Sie sich für „Ja“ oder „Nein“ entscheiden haben.";
        text[2] = "Am Ende des Spieles zeigen wir Ihnen Ihren optimalen Studiengang an unserem Fachbereich an!\nLesen Sie die Frage, konzentrierten Sie sich auf die richtige Antwort und ihre Gedanken übernehmen den Rest. Viel Spaß!";

        StartCoroutine(UpdateTextAndLoadBar());
    }

    // Update is called once per frame
    void Update()
    {
        if(pos >= text.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    IEnumerator UpdateTextAndLoadBar()
    {
        einleitung.text = text[pos];
        

        loadingBar.gameObject.SetActive(true);
        loadingBar.fillAmount = 0;
        

        float upperBound = 20f * delay;
        // warten
        for (int i = 1; i <= upperBound; i++)
        {
            yield return new WaitForSeconds(delay / upperBound);
            loadingBar.fillAmount = i / upperBound;
        }
        pos++;
        if (pos < text.Length)
        {
            StartCoroutine(UpdateTextAndLoadBar());
        }
        
    }
}
