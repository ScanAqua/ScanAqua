using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    float speed = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Animator>().speed += Time.deltaTime * 0.5f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GetComponent<Animator>().speed -= Time.deltaTime * 0.5f;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (GetComponent<Animator>().GetBool("isSwimming")) GetComponent<Animator>().SetBool("isSwimming", false);
            else GetComponent<Animator>().SetBool("isSwimming", true);

            GetComponent<Animator>().speed = 1;
        }
    }
}
