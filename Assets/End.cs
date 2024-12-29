using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{

    public GameObject SFX_1;
    public GameObject SFX_2;
    public GameObject SFX_3;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SFX_1.SetActive(true);
            SFX_2.SetActive(true);
            SFX_3.SetActive(true);
        }
    }
}
