using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBirdScript : MonoBehaviour
{
    float speed = 0;
    public float rotationSpeed = 1f;
    public float runSpeed = 1f;
    public float aniSpeed = 1f;
    public float y;
    Quaternion targetRotation;
    Vector3 destination;
    void Start()
    {
        StartCoroutine(ChangeDestination());
        y = transform.position.y;
        destination = new Vector3(Random.Range(-25, 25), y, Random.Range(35, 0));
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
        speed = (Vector3.Distance(destination, transform.position) / 10 + 1) * runSpeed;
        GetComponent<Animator>().speed = (speed + Quaternion.Angle(transform.rotation, targetRotation) / 45 * rotationSpeed) * aniSpeed;
        transform.Translate(0, 0, speed * Time.deltaTime);
        if(Vector3.Distance(destination, transform.position) < 2)
        {
            destination = new Vector3(Random.Range(-25, 25), y, Random.Range(35, 0));
        }
    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 값을 랜덤하게 재지정
            destination = new Vector3(Random.Range(-25, 25), y, Random.Range(35, 0));

            // 5초 기다림
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
}
