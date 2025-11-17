using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public HingeJoint2D rightArm;
    public HingeJoint2D leftArm;

    private bool hasSwung = false;
    public void Swing()
    {

        if (hasSwung) return;
        hasSwung = true;
        JointMotor2D rightMotor = rightArm.motor;
        rightMotor.motorSpeed = 700f;
        rightMotor.maxMotorTorque = 3000f;
        rightArm.motor = rightMotor;
        rightArm.useMotor = true;

        JointMotor2D leftMotor = leftArm.motor;
        leftMotor.motorSpeed = -700f;     
        leftMotor.maxMotorTorque = 3000f;
        leftArm.motor = leftMotor;
        leftArm.useMotor = true;

        StartCoroutine(StopSwing());
    }

    private IEnumerator StopSwing()
    {

        Rigidbody2D rightRb = rightArm.GetComponent<Rigidbody2D>();
        Rigidbody2D leftRb = leftArm.GetComponent<Rigidbody2D>();

        float lastRightVel = rightRb.angularVelocity;
        float lastLeftVel = leftRb.angularVelocity;

      
        while (true)
        {
            yield return null;

            if (lastRightVel > 0f && rightRb.angularVelocity <= 0f &&
                lastLeftVel < 0f && leftRb.angularVelocity >= 0f)
            {
            
                break;
            }

            lastRightVel = rightRb.angularVelocity;
            lastLeftVel = leftRb.angularVelocity;
        }

        rightArm.useMotor = false;
        leftArm.useMotor = false;

   
        rightRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        leftRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
