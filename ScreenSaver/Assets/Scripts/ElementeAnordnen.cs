using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementeAnordnen : MonoBehaviour
{
    [SerializeField] RawImage fh_logo;
    void Start()
    {
        fh_logo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //fh_logo.transform.position = new Vector3((int)(5 * Screen.width / 6 - fh_logo.rectTransform.rect.width / 2), Screen.height - fh_logo.rectTransform.rect.height * fh_logo.transform.localScale.y, fh_logo.transform.position.z);
        //fh_logo.transform.position = new Vector3(Screen.width - fh_logo.rectTransform.rect.width, fh_logo.transform.position.y, fh_logo.transform.position.z);
    }
}
