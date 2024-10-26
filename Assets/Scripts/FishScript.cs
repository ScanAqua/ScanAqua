using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public GameObject mapManager;
    float speed = 0;
    public float rotationSpeed = 1f;
    public bool end = false;
    Vector3 destination;
    void Start()
    {
        GetComponent<Animator>().SetBool("isSwimming", true);
        StartCoroutine(ChangeDestination());
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            Vector3 direction = destination - transform.position; // 대상 벡터로의 방향
            if (direction != Vector3.zero) // 방향이 0이 아닐 때만 회전
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction); // 대상 방향을 바라보는 회전 계산
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            speed = Vector3.Distance(destination, transform.position) / 5 + 0.3f;
            GetComponent<Animator>().speed = speed;
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            rotationSpeed = 2f;
            Vector3 direction = new Vector3 (0,1,10) - transform.position; // 대상 벡터로의 방향
            if (direction != Vector3.zero) // 방향이 0이 아닐 때만 회전
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction); // 대상 방향을 바라보는 회전 계산
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            float dist = Vector3.Distance(new Vector3(0, 1, 10), transform.position);
            transform.Translate(0, 0, (40 - (dist<40  ?dist :40)) * Time.deltaTime);
            GetComponent<Animator>().speed = 10 - (dist < 40 ? dist : 40) / 4;
            //if (transform.position.y < 10) Destroy
        }
        

    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 값을 랜덤하게 재지정
            destination = new Vector3(Random.Range(-25, 25), Random.Range(-17, 17), Random.Range(30, 10));

            // 5초 기다림
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
