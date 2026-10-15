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

    [SerializeField] private float topClamp=90.0f;

    [SerializeField] private float bottomClamp=-90.0f;

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

            cineMachineTargetPitch=ClampAngle(cineMachineTargetPitch,bottomClamp,topClamp);
            cameraTarget.localRotation=Quaternion.Euler(cineMachineTargetPitch,0.0f,0.0f);
            transform.Rotate(Vector3.up*rotationVelocity);
        }

        Vector3 MoveDirection=transform.TransformDirection(inputDir)*speed;
        MoveDirection.y=inputDir.y;
        controller.Move(MoveDirection*Time.deltaTime);        
                
    }
    private float ClampAngle(float lfAngle,float lfMin, float lfMax)
    {
        if(lfAngle< -360f) lfAngle+=360f;
        if(lfAngle> 360f) lfAngle-=360f;
        return Mathf.Clamp(lfAngle,lfMin,lfMax);
    }
}
