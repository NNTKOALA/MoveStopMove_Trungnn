using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public enum AnimationType
    {
        Idle, Run
    }
    //[SerializeField] Animator Anim;
    [SerializeField] FloatingJoystick variableJoystick;

    public Rigidbody rb;

    //private AnimationType currentAnimType = AnimationType.Idle;
    private float yPos;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            /*if (Mathf.Abs(variableJoystick.Horizontal) > 0.1f && Mathf.Abs(variableJoystick.Vertical) > 0.1f)
            {
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical));
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 30f);
            }*/

            //ChangeAnim(AnimationType.Run);
            this.transform.position = new Vector3(this.transform.position.x + variableJoystick.Horizontal * moveSpeed * Time.deltaTime,
                yPos, this.transform.position.z + variableJoystick.Vertical * moveSpeed * Time.deltaTime);
        }
    }

    /*    public void ChangeAnim(AnimationType _type)
        {
            if (currentAnimType != _type)
            {
                currentAnimType = _type;
                switch (_type)
                {
                    case AnimationType.Idle:
                        Anim.SetTrigger("Idlling");
                        break;
                    case AnimationType.Run:
                        Anim.SetTrigger("Running");
                        break;
                }
            }
        }*/

    public void SetYPosition(float yPosition)
    {
        yPos = yPosition;
    }
}
