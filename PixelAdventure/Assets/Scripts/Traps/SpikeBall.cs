using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [Header("Spike Ball")]
    [SerializeField] private HingeJoint2D chain;
    [SerializeField] private bool enabledLimits;
    [SerializeField] private bool right;
    [SerializeField][Range(0, 100)] private float speed;
    [SerializeField][Range(0, 90)] private float angle;


    private JointAngleLimits2D jointAngleLimits2D;
    private JointMotor2D motor2D;
    void Start()
    {
        motor2D.maxMotorTorque = 100;
    }
    void Update()
    {
        MovedLimits();
    }

    void MovedLimits()
    {
        chain.useLimits = enabledLimits;
        if (enabledLimits)
        {
            jointAngleLimits2D.max = angle;
            jointAngleLimits2D.min = -angle;

            chain.limits = jointAngleLimits2D;

            if (chain.limitState == JointLimitState2D.UpperLimit)
                right = true;
            else if (chain.limitState == JointLimitState2D.LowerLimit)
                right = false;
        }

        if (right) motor2D.motorSpeed = -speed;
        else motor2D.motorSpeed = speed;

        chain.motor = motor2D;
    }

}
