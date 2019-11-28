using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] bool ja;
    [SerializeField] AnimationBlockPosition position;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(ja && player.transform.position.x > gameObject.transform.position.x - ((RectTransform)gameObject.transform).rect.width / 2){
            print("ja");
            position.x = gameObject.transform.position.x;
            print(player.transform.position.x + " " + player.transform.position.y);
            position.y = position.y + 4;
        }
        else if(!ja && player.transform.position.x < gameObject.transform.position.x + ((RectTransform)gameObject.transform).rect.width / 2){
            position.x = gameObject.transform.position.x;
            print("nein");
            print(player.transform.position.x + " " + player.transform.position.y);
            position.y = position.y + 4;
        }
        else if(position.y > -187){
            position.y = position.y - 1;
        }
    }
}
