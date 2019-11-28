using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePanels : MonoBehaviour
{
    [SerializeField] bool ja;
    [SerializeField] bool question;
    
    // Start is called before the first frame update
    void Start()
    {
        if(question){
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 3 * Screen.height / 7);
        }
        else if(ja){
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2* Screen.width / 5, Screen.height / 2);
            gameObject.transform.position = new Vector2(Screen.width - gameObject.GetComponent<RectTransform>().rect.width / 2, gameObject.GetComponent<RectTransform>().rect.height / 2);
        }
        else {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2 * Screen.width / 5, Screen.height / 2);
            gameObject.transform.position = new Vector2(gameObject.GetComponent<RectTransform>().rect.width / 2, gameObject.GetComponent<RectTransform>().rect.height / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        
    }
}
