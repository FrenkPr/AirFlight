using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelsController : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> airplaneWheelColliders;

    [SerializeField] private float brakeForce;
    [SerializeField] private Slider currentBrake;
    
    [SerializeField] private float steerAngleForce;
    [SerializeField] private Slider currentSteerAngle;
    
    [SerializeField] private AirPlaneController airplaneController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //airplaneWheelColliders[0].steerAngle = steerAngleForce * Input.GetAxis("AirplaneYaw");
        airplaneWheelColliders[0].steerAngle = steerAngleForce * InputsController.Instance.PlayerRotationDir.y;
        airplaneWheelColliders[0].motorTorque = airplaneController.AppliedPower;

        foreach (WheelCollider wheel in airplaneWheelColliders)
        {
            //wheel.brakeTorque = brakeForce * Input.GetAxis("Brake");
            wheel.brakeTorque = brakeForce * InputsController.Instance.Inputs["BrakeValue"].ReadValue<float>();
        }
    }
}
