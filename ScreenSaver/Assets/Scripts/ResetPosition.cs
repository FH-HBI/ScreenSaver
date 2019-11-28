using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] AnimationBlockPosition position;
    private GameObject player;
    private RectTransform rectTransform;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rectTransform = player.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset(){
        rectTransform.transform.position = new Vector2(Screen.width / 2, rectTransform.position.y);

        position.x = -1000;
    }
}
