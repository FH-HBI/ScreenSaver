using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragenErstellen : MonoBehaviour
{
    [SerializeField] FragenKatalog fragenKatalog;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < fragenKatalog.laenge; i++){
            fragenKatalog.fragen[i] = new Frage("Frage " + (i + 1), null, null, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
