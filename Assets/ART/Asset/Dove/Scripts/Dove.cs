using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dove : MonoBehaviour
{
    public GameObject Camera;
    private Animator dove;
    public float gravity = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        dove = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.y = gravity * Time.deltaTime;
        if (dove.GetCurrentAnimatorStateInfo(0).IsName("fly"))
        {
            dove.SetBool("takeoff", false);
            dove.SetBool("landing", false);
            dove.SetBool("glide", false);
        }
        if (dove.GetCurrentAnimatorStateInfo(0).IsName("flyleft"))
        {
            dove.SetBool("takeoff", false);
            dove.SetBool("landing", false);
            dove.SetBool("glide", false);
        }
        if (dove.GetCurrentAnimatorStateInfo(0).IsName("flyright"))
        {
            dove.SetBool("takeoff", false);
            dove.SetBool("landing", false);
            dove.SetBool("glide", false);
        }
        if (dove.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            dove.SetBool("eat", false);
            dove.SetBool("preen", false);
            dove.SetBool("scratching", false);
            dove.SetBool("landing", false);
            dove.SetBool("walk", false);
            dove.SetBool("walkleft", false);
            dove.SetBool("walkright", false);
            dove.SetBool("glide", false);
            dove.SetBool("takeoff", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dove.SetBool("takeoff", true);
            dove.SetBool("landing", true);
            dove.SetBool("idle", false);
            dove.SetBool("fly", false);
            dove.SetBool("flyleft", false);
            dove.SetBool("flyright", false);
            dove.SetBool("glide", false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            dove.SetBool("walk", true);
            dove.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            dove.SetBool("walk", false);
            dove.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dove.SetBool("flyleft", true);
            dove.SetBool("fly", false);
            dove.SetBool("walkleft", true);
            dove.SetBool("walk", false);
            dove.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            dove.SetBool("fly", true);
            dove.SetBool("flyleft", false);
            dove.SetBool("walk", true);
            dove.SetBool("walkleft", false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dove.SetBool("flyright", true);
            dove.SetBool("fly", false);
            dove.SetBool("walkright", true);
            dove.SetBool("walk", false);
            dove.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dove.SetBool("fly", true);
            dove.SetBool("flyright", false);
            dove.SetBool("walk", true);
            dove.SetBool("walkright", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dove.SetBool("eat", true);
            dove.SetBool("idle", false);
            dove.SetBool("walk", false);
            dove.SetBool("walkleft", false);
            dove.SetBool("walkright", false);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            dove.SetBool("preen", true);
            dove.SetBool("idle", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            dove.SetBool("scratching", true);
            dove.SetBool("idle", false);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            dove.SetBool("glide", true);
            dove.SetBool("fly", false);
            dove.SetBool("flyleft", false);
            dove.SetBool("flyright", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.GetComponent<DoveCamera>().enabled = !Camera.GetComponent<DoveCamera>().enabled;
        }
    }
}
