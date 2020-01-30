using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Antworten : MonoBehaviour
{
    [SerializeField] FragenKatalog fragenKatalog;
    [SerializeField] Text fragenText;
    [SerializeField] Text beschreibung;
    [SerializeField] Text ergebnisTitel;
    [SerializeField] SpellerAntwort spellerAntwort;
    [SerializeField] Image fortschritt;
    [SerializeField] Image statisch;
    [SerializeField] Image loadingBar;
    [SerializeField] Text loadingText;
    private bool coroutineActive = false;
    private bool spellerDelayActive = false;
    public int spellerDelay = 5;
    void Start()
    {
        fragenText.text = fragenKatalog.fragen[0].text;
        fragenKatalog.pos = 0;
        fragenText.gameObject.SetActive(true);
        beschreibung.gameObject.SetActive(false);
        ergebnisTitel.gameObject.SetActive(false);
        fortschritt.gameObject.SetActive(true);
        statisch.gameObject.SetActive(true);
        loadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        //StartCoroutine(UpdateLoadingBar());
    }


    void Update()
    {
        // Zum "Zurücknehmen" von Antworten
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            revert();
        }

        // Damit man in der "Pause" nicht antworten kann
        if (!coroutineActive)
        {
            // Zum Neustarten des Quiz
            if (fragenKatalog.pos == -1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.N)))
            {
                SceneManager.LoadScene(1);
            }
            // "Ja"
            else if (Input.GetKeyDown(KeyCode.J) || (spellerAntwort.neueAntwort == true && spellerAntwort.antwort == "J"))
            {
                answer(true);
            }
            // "Nein"
            else if (Input.GetKeyDown(KeyCode.N) || (spellerAntwort.neueAntwort == true && spellerAntwort.antwort == "N"))
            {
                answer(false);
            }
        }

    }



    void restart()
    {
        fragenKatalog.pos = 0;

        fragenKatalog.faecher[0] = new Fach("Informatik", "\n\nUnser Bachelorstudiengang Informatik vermittelt Ihnen einerseits solide Grundlagen in Informatik, Mathematik, Natur- und Wirtschafts- Wissenschaften und bietet Ihnen andererseits die Möglichkeit, in einem der attraktiven Forschungs- und Entwicklungsgebiete oder in einem mittelständischen Unternehmen mitzuarbeiten. Die Themen im Studiengang sind reichhaltig und so stehen neben Grundlagen in Programmierung, Algorithmen, Datenstrukturen, technischer und theoretischer Informatik vor allem Module in Rechnerarchitekturen und Betriebssystemen, Netzen und Datenbanken und Graphische Datenverarbeitung im Mittelpunkt.");
        fragenKatalog.faecher[1] = new Fach("Elektrotechnik", "\n\nOhne Elektrotechnik und Elektronik würde ein Großteil unseres heutigen Informations- und Kommunikationszeitalters stillstehen, denn nahezu in all unseren Lebensbereichen treffen wir auf die Errungenschaften der Elektrotechnik. Folglich ist auch das Berufsspektrum einer Ingenieurin oder eines Ingenieurs riesig, vielfältig, aber vor allem zukunftssicher.");
        fragenKatalog.faecher[2] = new Fach("MCD", "\n\nDer Studiengang Media and Communications for Digital Business (MCD) ist ein interdisziplinärer Studiengang, der sich vor allem mit der digitalen Wirtschaft befasst. Ohne digitale Medien ist mittlerweile kaum ein Berufszweig denkbar. Digitale Innovationen beeinflussen unser Handeln, unser Denken und sogar unsere körperliche Verfassung. Diese Veränderungen erkennen, verstehen und zukunftsfähig mitgestalten zu können, ist eine komplexe Herausforderung unserer Zeit. Der Studiengang MCD geht diese Fragestellungen aus drei verschiedenen Perspektiven an, die durch die Fächer Technik, Kommunikation und Management markiert sind.");
        fragenKatalog.faecher[3] = new Fach("Wirtschaftsinformatik", "\n\nDie Wirtschaftsinformatik beschäftigt sich mit Fragen an der Schnittstelle zwischen Informatik und BWL. In Unternehmen treffen die beiden Bereiche meistens in der IT-Abteilung aufeinander. Als Wirtschaftsinformatikerin oder -informatiker arbeitest Du dort zum Beispiel an Themen der Logistik, Produktion und Unternehmenskommunikation. Dazu benutzt Du IT-Systeme. Du organisierst also Abläufe und sorgst dafür, dass sie optimal durch Computer und Software unterstützt werden. Du solltest dazu ebenso gern technische wie betriebswirtschafltiche Aufgaben lösen und auf Deine Gesprächspartner eingehen können.");
        fragenKatalog.faecher[4] = new Fach("Smart Building Engineering", "\n\nSmarte Gebäude zu planen und zu bauen erfordert eine enge Zusammenarbeit von Bauwesen, Elektro -, Informations - und Energietechnik sowie der Technischen Gebäudeausrüstung.Fachplaner, Bauunternehmer, Zulieferer, Dienstleister, Verwaltung und Politik brauchen dringend qualifizierten Nachwuchs im Bereich der Gebäudetechnik. Der Smart Building Engineer ist wichtiger Bestandteil in interdisziplinären Planungsteams aus Architekten und Fachplanern.");



        fragenText.gameObject.SetActive(true);
        beschreibung.gameObject.SetActive(false);
        ergebnisTitel.gameObject.SetActive(false);
        fortschritt.gameObject.SetActive(true);
        statisch.gameObject.SetActive(true);

        fragenText.text = fragenKatalog.fragen[0].text;

        fortschritt.transform.localScale = new Vector3(0, fortschritt.transform.localScale.y, fortschritt.transform.localScale.z);
        float new_position = statisch.transform.position.x - (statisch.rectTransform.rect.width / 2);
        fortschritt.transform.position = new Vector3(new_position, statisch.transform.position.y, statisch.transform.position.z);
    }


    void revert()
    {
        unsort();
        if (fragenKatalog.pos < 0)
        {
            fragenKatalog.pos = 11;
        }
        else if (fragenKatalog.pos > 0)
        {
            fragenKatalog.pos--;
        }

        coroutineActive = false;
        for (int i = 0; i < fragenKatalog.faecher.Length; i++)
        {
            if (fragenKatalog.antworten[fragenKatalog.pos])
            {
                fragenKatalog.faecher[i].wert -= fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
            else
            {
                fragenKatalog.faecher[i].wert += fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
        }

        spellerAntwort.neueAntwort = false;

        updateProgressBar();
    }

    void answer(bool yes)
    {
        fragenKatalog.antworten[fragenKatalog.pos] = yes;
        for (int i = 0; i < fragenKatalog.faecher.Length; i++)
        {
            if (yes)
            {
                fragenKatalog.faecher[i].wert += fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
            else
            {
                fragenKatalog.faecher[i].wert -= fragenKatalog.fragen[fragenKatalog.pos].werte[i];
            }
        }
        fragenKatalog.pos++;
        spellerAntwort.neueAntwort = false;



        fragenText.alignment = TextAnchor.UpperCenter;

        // Ergebnis Anzeigen
        if (fragenKatalog.fragen.Length <= fragenKatalog.pos)
        {
            StartCoroutine(showResult());
        }
        else if (!spellerDelayActive)
        {
            StartCoroutine(UpdateLoadingBar());
        }

        updateProgressBar();

    }


    void updateProgressBar()
    {
        float new_scale = statisch.transform.localScale.x / 12 * (fragenKatalog.pos);
        if (fragenKatalog.pos == -1)
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


    void unsort()
    {
        Fach[] temp = new Fach[5];

        for (int i = 0; i < 5; i++)
        {
            if (fragenKatalog.faecher[i].bezeichnung == "Informatik")
            {
                temp[0] = fragenKatalog.faecher[i];
            }
            else if (fragenKatalog.faecher[i].bezeichnung == "Elektrotechnik")
            {
                temp[1] = fragenKatalog.faecher[i];
            }
            else if (fragenKatalog.faecher[i].bezeichnung == "MCD")
            {
                temp[2] = fragenKatalog.faecher[i];
            }
            else if (fragenKatalog.faecher[i].bezeichnung == "Wirtschaftsinformatik")
            {
                temp[3] = fragenKatalog.faecher[i];
            }
            else if (fragenKatalog.faecher[i].bezeichnung == "Smart Building Engineering")
            {
                temp[4] = fragenKatalog.faecher[i];
            }
        }

        fragenKatalog.faecher = temp;
    }

    // BubbleSort Babyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
    void sort()
    {
        for (int i = 0; i < fragenKatalog.faecher.Length; i++)
        {
            for (int j = i + 1; j < fragenKatalog.faecher.Length; j++)
            {
                if (fragenKatalog.faecher[i].wert < fragenKatalog.faecher[j].wert)
                {
                    Fach temp = fragenKatalog.faecher[i];
                    fragenKatalog.faecher[i] = fragenKatalog.faecher[j];
                    fragenKatalog.faecher[j] = temp;

                }
            }
        }
    }

    IEnumerator UpdateLoadingBar()
    {
        loadingBar.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        spellerDelayActive = true;
        loadingBar.fillAmount = 0;


        float upperBound = 20f * spellerDelay;
        // warten
        for (int i = 1; i <= upperBound; i++)
        {
            if (!coroutineActive)
            {
                yield return new WaitForSeconds(spellerDelay / upperBound);
                loadingBar.fillAmount = i / upperBound;
            }
            else
            {
                break;
            }
            
        }
        spellerDelayActive = false;

    }



    IEnumerator showResult()
    {
        loadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);
        // Ergebnisse anzeigen
        sort();

        fragenText.text = "";
        for (int i = 0; i < fragenKatalog.faecher.Length; i++)
        {
            if (fragenKatalog.faecher[i].wert >= -9 && fragenKatalog.faecher[i].wert <= -1)
            {
                fragenText.text += fragenKatalog.faecher[i].wert + "\t\t" +
                fragenKatalog.faecher[i].bezeichnung + "\n";
            }
            else if (fragenKatalog.faecher[i].wert >= 0 && fragenKatalog.faecher[i].wert <= 9)
            {
                fragenText.text += " " + fragenKatalog.faecher[i].wert + "\t\t" +
                fragenKatalog.faecher[i].bezeichnung + "\n";
            }
            else
            {
                fragenText.text += fragenKatalog.faecher[i].wert + "\t" +
                fragenKatalog.faecher[i].bezeichnung + "\n";
            }

        }

        fragenText.alignment = TextAnchor.UpperLeft;
        fragenKatalog.pos = -1;

        coroutineActive = true;

        loadingBar.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(false);
        spellerDelayActive = true;
        loadingBar.fillAmount = 0;


        float upperBound = 20f * spellerDelay;
        // warten
        for (int i = 1; i <= upperBound; i++)
        {
            if (coroutineActive)
            {
                yield return new WaitForSeconds(spellerDelay / upperBound);
                loadingBar.fillAmount = i / upperBound;
            }
            else
            {
                break;
            }
        }
        spellerDelayActive = false;


        loadingBar.gameObject.SetActive(false);

        if (coroutineActive)
        {
            // Beschreibungen anzeigen
            coroutineActive = false;

            fragenText.gameObject.SetActive(false);
            beschreibung.gameObject.SetActive(true);
            ergebnisTitel.gameObject.SetActive(true);
            fortschritt.gameObject.SetActive(false);
            statisch.gameObject.SetActive(false);
            beschreibung.text = "<b>" + fragenKatalog.faecher[0].bezeichnung + " (" + fragenKatalog.faecher[0].wert + "P)</b>" + fragenKatalog.faecher[0].beschreibung;

            if (fragenKatalog.faecher[0].wert - fragenKatalog.faecher[1].wert <= 1)
            {
                beschreibung.text += "\n\n" + "<b>" + fragenKatalog.faecher[1].bezeichnung + " (" + fragenKatalog.faecher[1].wert + "P)</b>" + fragenKatalog.faecher[1].beschreibung;
            }
        }

    }



}
