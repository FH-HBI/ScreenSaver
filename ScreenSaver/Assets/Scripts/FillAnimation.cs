using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAnimation : MonoBehaviour
{
    [SerializeField] AnimationBlockPosition position;

    void start(){
        //gameObject.GetComponent<RectTransform>().sizeDelta = panel.GetComponent<RectTransform>().sizeDelta;
        //position.y = panel.transform.position.y - ((RectTransform)panel.transform).rect.height / 2;
        //position.x = panel.transform.position.x - ((RectTransform)panel.transform).rect.width / 2;
        gameObject.transform.position = new Vector2(0, 0);
    }

    void Update(){

    }

    void FixedUpdate(){
        
        if(position.y != gameObject.transform.position.y){
            gameObject.transform.position = new Vector2(position.x, position.y);
        }
        
    }
}

