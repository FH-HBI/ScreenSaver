using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if (Input.GetKey(KeyCode.A))
        {
            rectTransform.transform.position = new Vector2(rectTransform.position.x - 1, rectTransform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rectTransform.transform.position = new Vector2(rectTransform.position.x + 1, rectTransform.position.y);
        }
    }
}
