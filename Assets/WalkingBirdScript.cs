using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBirdScript : MonoBehaviour
{
    float speed = 0;
    public float rotationSpeed = 1f;
    public float runSpeed = 1f;
    Quaternion targetRotation;
    Vector3 destination;
    void Start()
    {
        StartCoroutine(ChangeDestination());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = destination - transform.position; // 대상 벡터로의 방향
        if (direction != Vector3.zero) // 방향이 0이 아닐 때만 회전
        {
            targetRotation = Quaternion.LookRotation(direction); // 대상 방향을 바라보는 회전 계산
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        speed = (Vector3.Distance(destination, transform.position) / 10 + Quaternion.Angle(transform.rotation, targetRotation)/90) * runSpeed;
        GetComponent<Animator>().speed = speed/runSpeed;
        transform.Translate(0, 0, speed * Time.deltaTime);

    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 값을 랜덤하게 재지정
            destination = new Vector3(Random.Range(-25, 25), -5, Random.Range(30, 10));

            // 5초 기다림
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
