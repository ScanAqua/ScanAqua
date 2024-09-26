using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    float speed = 0;
    public float rotationSpeed = 1f;
    Vector3 destination;
    void Start()
    {
        GetComponent<Animator>().SetBool("isSwimming", true);
        StartCoroutine(ChangeDestination());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = destination - transform.position; // 대상 벡터로의 방향
        if (direction != Vector3.zero) // 방향이 0이 아닐 때만 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // 대상 방향을 바라보는 회전 계산
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        speed = Vector3.Distance(destination, transform.position)/2;
        GetComponent<Animator>().speed = speed;
        transform.Translate(0, 0, speed * Time.deltaTime);

    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 값을 랜덤하게 재지정
            destination = new Vector3(Random.Range(-16, 16), Random.Range(-10, 10), Random.Range(-4, 4));

            // 5초 기다림
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
