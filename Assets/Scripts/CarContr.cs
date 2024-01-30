using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CarContr : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }

    [SerializeField] private driveType drive;

    public float motorTorque;
    public float brakeTorque;
    //public float maxSpeed;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float radius = 6;
    //public float centreOfGravityOffset = -2f;
    public GameObject[] wheelMesh = new GameObject[4];
    public float steeringMax = 4;

    //public WheelController[] wheelControllers;
    public WheelCollider[] wheels = new WheelCollider[4];
    //public Rigidbody rigidBody;


   /* void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        wheelControllers = GetComponentsInChildren<WheelController>();
    }*/

    void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        steerVehicle();

        /*float _vInput = SimpleInput.GetAxis("Vertical");
        float _hInput = SimpleInput.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);

        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(_vInput) == Mathf.Sign(forwardSpeed);

        foreach (var _wheel in wheelControllers)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                if (_wheel.motorized)
                {
                    _wheel.WheelCollider.motorTorque = _vInput * currentMotorTorque;
                }
                _wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                _wheel.WheelCollider.brakeTorque = Mathf.Abs(_vInput) * brakeTorque;
                _wheel.WheelCollider.motorTorque = 0;
            }
        }*/
    }

    private void moveVehicle()
    {
        bool isAccelerating = Mathf.Sign(SimpleInput.GetAxis("Vertical")) == Mathf.Sign(motorTorque);

        if (isAccelerating)
        {
            if(drive == driveType.allWheelDrive)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = SimpleInput.GetAxis("Vertical") * (motorTorque / 4) * 7.0f;
                }
            }
            else if(drive == driveType.rearWheelDrive)
            {
                for(int i =2; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = SimpleInput.GetAxis("Vertical") * (motorTorque / 2) * 5.0f;
                }
            }
            else 
            {
                for(int i = 0; i < wheels.Length - 2; i++)
                {
                    wheels[i].motorTorque = SimpleInput.GetAxis("Vertical") * (motorTorque / 2) * 3.5f;
                }
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = Mathf.Abs(SimpleInput.GetAxis("Vertical")) * brakeTorque;
                wheels[i].motorTorque = 0;
            }
        }
    }
    private void steerVehicle()
    {
        if (SimpleInput.GetAxis("Horizontal") > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * SimpleInput.GetAxis("Horizontal");
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * SimpleInput.GetAxis("Horizontal");
        }
        else if (SimpleInput.GetAxis("Horizontal") < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * SimpleInput.GetAxis("Horizontal");
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * SimpleInput.GetAxis("Horizontal");
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }
    public void animateWheels()
    {
        Vector3 _wheelsPosition = Vector3.zero;
        Quaternion _wheelRotation = Quaternion.identity;

        for(int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetWorldPose(out _wheelsPosition, out _wheelRotation);
            wheelMesh[i].transform.position = _wheelsPosition;
            wheelMesh[i].transform.rotation = _wheelRotation;
        }
    }
}
