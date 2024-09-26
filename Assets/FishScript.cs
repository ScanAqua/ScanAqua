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
        Vector3 direction = destination - transform.position; // ��� ���ͷ��� ����
        if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // ��� ������ �ٶ󺸴� ȸ�� ���
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
            // Vector3 ���� �����ϰ� ������
            destination = new Vector3(Random.Range(-16, 16), Random.Range(-10, 10), Random.Range(-4, 4));

            // 5�� ��ٸ�
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
