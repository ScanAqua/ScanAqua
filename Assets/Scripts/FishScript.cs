using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishScript : MonoBehaviour
{
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
            Vector3 direction = destination - transform.position; // ��� ���ͷ��� ����
            if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction); // ��� ������ �ٶ󺸴� ȸ�� ���
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            speed = Vector3.Distance(destination, transform.position) / 5 + 0.3f;
            GetComponent<Animator>().speed = speed;
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = destination - transform.position; // ��� ���ͷ��� ����
            if (direction != Vector3.zero) // ������ 0�� �ƴ� ���� ȸ��
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction); // ��� ������ �ٶ󺸴� ȸ�� ���
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            transform.Translate(0, 0, 1 * Time.deltaTime);
        }
        

    }

    IEnumerator ChangeDestination()
    {
        while (true)
        {
            // Vector3 ���� �����ϰ� ������
            destination = new Vector3(Random.Range(-25, 25), Random.Range(-17, 17), Random.Range(30, 10));

            // 5�� ��ٸ�
            yield return new WaitForSeconds(Random.Range(1, 4));
        }
    }
}
