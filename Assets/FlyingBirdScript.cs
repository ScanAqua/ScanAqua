using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBirdScript : MonoBehaviour
{
    float speed = 0;
    float speedY = 0;
    public float rotationSpeed = 0.5f;
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
        Vector3 direction = destination - transform.position; // ��� ���ͷ��� ����
        if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
        {
            targetRotation = Quaternion.LookRotation(direction); // ��� ������ �ٶ󺸴� ȸ�� ���
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        speed = Mathf.Lerp(speed,(Vector3.Distance(destination, transform.position) / 5 + 0.5f) * runSpeed, Time.deltaTime);
        speedY = (destination.y - transform.position.y) / 10 ;
        GetComponent<Animator>().speed = Mathf.Max(speedY, 0) + Quaternion.Angle(transform.rotation, targetRotation) / 180 + 1f;
        transform.Translate(0, speedY*Time.deltaTime, speed * Time.deltaTime + Time.deltaTime*3);

        if (speed > 7 && destination.y - transform.position.y < 5 && +Quaternion.Angle(transform.rotation, targetRotation) < 90)
        {
            GetComponent<Animator>().SetBool("gliding", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("gliding", false);
        }
    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 ���� �����ϰ� ������
            destination = new Vector3(Random.Range(-25, 25), Random.Range(20, 3), Random.Range(50, 20));
            

            // 5�� ��ٸ�
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
