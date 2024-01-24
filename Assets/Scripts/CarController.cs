using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CarController : MonoBehaviour
{
    [Header("Wheels collider")]
    public WheelCollider front_Left_Wheel;
    public WheelCollider front_Right_Wheel;
    public WheelCollider rear_Left_Wheel;
    public WheelCollider rear_Right_Wheel;

    [Header("Wheels Transform")]
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform backRightTransform;
    public Transform backLeftTransform;

    [Header("Car Engine")]
    public float accelerationForce = 300f;
    public float breakingForce = 3000f;
    public float presentBreakForce = 0f;
    public float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float wheelsTorque = 15f;
    public float presentTurnAngle = 0f;


    private void Update()
    {
        MoveCar();
        CarSteering();
        ApplyBreaks();
    }

    private void MoveCar()
    {
        front_Left_Wheel.motorTorque = presentAcceleration;
        front_Right_Wheel.motorTorque = presentAcceleration;
        rear_Left_Wheel.motorTorque = presentAcceleration;
        rear_Right_Wheel.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
    }

    private void CarSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        front_Left_Wheel.steerAngle = presentTurnAngle;
        front_Right_Wheel.steerAngle = presentTurnAngle;

        SteeringWheels(front_Left_Wheel, frontLeftTransform);
        SteeringWheels(front_Right_Wheel, frontRightTransform);
        SteeringWheels(rear_Left_Wheel, backLeftTransform);
        SteeringWheels(rear_Right_Wheel, backRightTransform);
    }

    private void SteeringWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 _position;
        Quaternion _rotation;

        wheelCollider.GetWorldPose(out _position, out _rotation);

        wheelTransform.position = _position;
        wheelTransform.rotation = _rotation;
    }

    private void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreakForce = breakingForce;
        }
        else
        {
            presentBreakForce = 0f;
        }
        rear_Left_Wheel.brakeTorque = presentBreakForce;
        rear_Right_Wheel.brakeTorque = presentBreakForce;
    }

}
