using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed=5f;
    [SerializeField] private float jump=2f;
    [SerializeField] private float gravity=0.03f;

    [SerializeField] private float rotationSpeed=1.0f;

    [SerializeField] private Transform cameraTarget;

    [SerializeField] private float topClump=90.0f;

    [SerializeField] private float bottomClump=-90.0f;

    private float mouseX;
    private float mouseY;
    private float cineMachineTargetPitch;
    private float rotationVelocity;
    private CharacterController controller;

    private Vector3 inputDir;
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        controller=GetComponent<CharacterController>();
    }


    void Update()
    {
        float hor=Input.GetAxis("Horizontal");
        float ver=Input.GetAxis("Vertical");

        inputDir=new Vector3(hor,inputDir.y,ver);

        mouseX=Input.GetAxis("Mouse X");
        mouseY=Input.GetAxis("Mouse Y");

        if(mouseX!=0f ||mouseY!=0f)
        {
            cineMachineTargetPitch+=mouseY*rotationSpeed*-1;
            rotationVelocity=mouseX*rotationSpeed;
        }

        Vector3 MoveDirection=transform.TransformDirection(inputDir)*speed;
        MoveDirection.y=inputDir.y;
        controller.Move(MoveDirection*Time.deltaTime);        
                
    }
}
