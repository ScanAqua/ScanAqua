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
        Vector3 direction = destination - transform.position; // ��� ���ͷ��� ����
        if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
        {
            targetRotation = Quaternion.LookRotation(direction); // ��� ������ �ٶ󺸴� ȸ�� ���
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
            // Vector3 ���� �����ϰ� ������
            destination = new Vector3(Random.Range(-25, 25), -5, Random.Range(30, 10));

            // 5�� ��ٸ�
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
