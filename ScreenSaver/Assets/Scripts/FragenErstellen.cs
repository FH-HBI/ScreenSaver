using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragenErstellen : MonoBehaviour
{
    [SerializeField] SpellerAntwort spellerAntwort;
    [SerializeField] FragenKatalog fragenKatalog;
    [SerializeField] Image fortschritt;
    // Start is called before the first frame update
    void Start()
    {
        fragenKatalog.fragen[0] = new Frage("Hast Du ein gutes Verständnis von Mathe und Physik?", 5, 5, 0, 3, 3);
        fragenKatalog.fragen[1] = new Frage("Arbeitest du gerne in Teams?", 2, 2, 6, 4, 3);
        fragenKatalog.fragen[2] = new Frage("Hast Du Freude an kreativen/künstlerischen Arbeiten?", 1, 2, 6, 4, 3);
        fragenKatalog.fragen[3] = new Frage("Präsentierst/Kommunizierst Du gerne?", 0, 0, 5, 4, 3);
        fragenKatalog.fragen[4] = new Frage("Interessierst Du dich für das Programmieren?", 7, 3, 0, 4, 2);
        fragenKatalog.fragen[5] = new Frage("Möchtest du mehr über die Hintergründe von Marketing und BWL erfahren?", 0, 0, 5, 7, 0);
        fragenKatalog.fragen[6] = new Frage("Kannst du dir Dinge gut räumlich vorstellen und/oder interessierst du dich für Architektur?", 0, 0, 2, 0, 6);
        fragenKatalog.fragen[7] = new Frage("Hast du Spaß an Naturwissenschaften und interessierst du dich für ihre Besonderheiten?", 3, 4, 0, 0, 4);
        fragenKatalog.fragen[8] = new Frage("Interessierst du dich für Statistiken?", 0, 0, 2, 6, 0);
        fragenKatalog.fragen[9] = new Frage("Hast du schon davon geträumt einen Bau selbst zu planen?", 0, 0, 0, 0, 6);
        fragenKatalog.fragen[10] = new Frage("Würdest du gerne lernen eigene Elektrogeräte und -anlagen zu bauen?", 3, 8, 0, 0, 4);
        fragenKatalog.fragen[11] = new Frage("Möchtest du Content für mediengebundene Kummonikation professionell gestalten?", 0, 0, 6, 0, 0);


        // 
        fragenKatalog.faecher[0] = new Fach("Informatik", "\n\nUnser Bachelorstudiengang Informatik vermittelt Ihnen einerseits solide Grundlagen in Informatik, Mathematik, Natur- und Wirtschafts- Wissenschaften und bietet Ihnen andererseits die Möglichkeit, in einem der attraktiven Forschungs- und Entwicklungsgebiete oder in einem mittelständischen Unternehmen mitzuarbeiten. Die Themen im Studiengang sind reichhaltig und so stehen neben Grundlagen in Programmierung, Algorithmen, Datenstrukturen, technischer und theoretischer Informatik vor allem Module in Rechnerarchitekturen und Betriebssystemen, Netzen und Datenbanken und Graphische Datenverarbeitung im Mittelpunkt.");
        fragenKatalog.faecher[1] = new Fach("Elektrotechnik", "\n\nOhne Elektrotechnik und Elektronik würde ein Großteil unseres heutigen Informations- und Kommunikationszeitalters stillstehen, denn nahezu in all unseren Lebensbereichen treffen wir auf die Errungenschaften der Elektrotechnik. Folglich ist auch das Berufsspektrum einer Ingenieurin oder eines Ingenieurs riesig, vielfältig, aber vor allem zukunftssicher.");
        fragenKatalog.faecher[2] = new Fach("MCD", "\n\nDer Studiengang Media and Communications for Digital Business (MCD) ist ein interdisziplinärer Studiengang, der sich vor allem mit der digitalen Wirtschaft befasst. Ohne digitale Medien ist mittlerweile kaum ein Berufszweig denkbar. Digitale Innovationen beeinflussen unser Handeln, unser Denken und sogar unsere körperliche Verfassung. Diese Veränderungen erkennen, verstehen und zukunftsfähig mitgestalten zu können, ist eine komplexe Herausforderung unserer Zeit. Der Studiengang MCD geht diese Fragestellungen aus drei verschiedenen Perspektiven an, die durch die Fächer Technik, Kommunikation und Management markiert sind.");
        fragenKatalog.faecher[3] = new Fach("Wirtschaftsinformatik", "\n\nDie Wirtschaftsinformatik beschäftigt sich mit Fragen an der Schnittstelle zwischen Informatik und BWL. In Unternehmen treffen die beiden Bereiche meistens in der IT-Abteilung aufeinander. Als Wirtschaftsinformatikerin oder -informatiker arbeitest Du dort zum Beispiel an Themen der Logistik, Produktion und Unternehmenskommunikation. Dazu benutzt Du IT-Systeme. Du organisierst also Abläufe und sorgst dafür, dass sie optimal durch Computer und Software unterstützt werden. Du solltest dazu ebenso gern technische wie betriebswirtschafltiche Aufgaben lösen und auf Deine Gesprächspartner eingehen können.");
        fragenKatalog.faecher[4] = new Fach("Smart Building Engineering", "\n\nSmarte Gebäude zu planen und zu bauen erfordert eine enge Zusammenarbeit von Bauwesen, Elektro -, Informations - und Energietechnik sowie der Technischen Gebäudeausrüstung.Fachplaner, Bauunternehmer, Zulieferer, Dienstleister, Verwaltung und Politik brauchen dringend qualifizierten Nachwuchs im Bereich der Gebäudetechnik. Der Smart Building Engineer ist wichtiger Bestandteil in interdisziplinären Planungsteams aus Architekten und Fachplanern.");




        spellerAntwort.neueAntwort = false;
        spellerAntwort.antwort = "Keine Antwort";

        //fortschritt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        fortschritt.transform.localScale = new Vector3(0, fortschritt.transform.localScale.y, fortschritt.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
