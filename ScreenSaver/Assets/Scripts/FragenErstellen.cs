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
        
        int[] nullArray = { 0, 0, 0, 0 ,0};
        fragenKatalog.faecher = nullArray;
        fragenKatalog.faecherbezeichnung[0] = "Informatik";
        fragenKatalog.faecherbezeichnung[1] = "Elektrotechnik";
        fragenKatalog.faecherbezeichnung[2] = "MCD";
        fragenKatalog.faecherbezeichnung[3] = "Wirtschaftsinformatik";
        fragenKatalog.faecherbezeichnung[4] = "Smart Building Engineering";

        spellerAntwort.neueAntwort = false;
        spellerAntwort.antwort = "Keine Antwort";

        //fortschritt.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        fortschritt.transform.localScale = new Vector3(0, fortschritt.transform.localScale.y, fortschritt.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart(){
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
        
        int[] nullArray = { 0, 0, 0, 0 ,0};
        fragenKatalog.faecher = nullArray;
        fragenKatalog.faecherbezeichnung[0] = "Informatik";
        fragenKatalog.faecherbezeichnung[1] = "Elektrotechnik";
        fragenKatalog.faecherbezeichnung[2] = "MCD";
        fragenKatalog.faecherbezeichnung[3] = "Wirtschaftsinformatik";
        fragenKatalog.faecherbezeichnung[4] = "Smart Building Engineering";

        spellerAntwort.neueAntwort = false;
        spellerAntwort.antwort = "Keine Antwort";
    }
}
