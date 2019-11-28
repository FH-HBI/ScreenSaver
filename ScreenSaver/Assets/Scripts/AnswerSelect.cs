using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnswerSelect : MonoBehaviour
{
    [SerializeField] AnimationBlockPosition position;
    [SerializeField] bool ja;
    public UnityEvent GivenAnswer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate(){
        if(position.y >= gameObject.transform.position.y){
            GivenAnswer.Invoke();
        }
    }
}
