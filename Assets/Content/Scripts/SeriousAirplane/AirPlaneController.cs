using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AirPlaneController : MonoBehaviour
{
    [Header("Airplane rigidBody variables")]
    [SerializeField] private Transform com;
    [SerializeField] private Rigidbody rb;

    [Header("Airplane details")]
    [SerializeField] private float maxPower;
    [SerializeField] private Slider currentPowerPercentage;
    public float AppliedPower { get; private set; }
    private float liftPower;

    [SerializeField] private float moveSpeedKmH;

    public bool V_Tol;

    [SerializeField] private float torqueForce;

    [SerializeField] private AnimationCurve currentLiftPower;

    // Start is called before the first frame update
    void Start()
    {
        rb.centerOfMass = com.localPosition;
        moveSpeedKmH = 0;
    }

    public void ToggleV_Tol()
    {
        bool toggleV_Tol = InputsController.Instance.Inputs["ToggleV_TolBtnPressed"].triggered;

        if (toggleV_Tol)
        {
            V_Tol = !V_Tol;
        }
    }

    public float GetCurrentLiftPower(float currentMoveSpeed)
    {
        float currentPower = maxPower * currentLiftPower.Evaluate(currentMoveSpeed) * Time.deltaTime;

        return currentPower;
    }

    public void QuitGame()
    {
        if (InputsController.Instance.Inputs["QuitGameBtnPressed"].triggered)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#else
            Application.Quit();
#endif
        }
    }

    public void Rotate()
    {
        //Vector3 dir = new Vector3(Input.GetAxis("AirplanePitch"), Input.GetAxis("AirplaneYaw"), Input.GetAxis("AirplaneRoll"));
        Vector3 dir = InputsController.Instance.PlayerRotationDir;

        rb.AddRelativeTorque(torqueForce * dir * Time.deltaTime, ForceMode.Force);
    }

    public void Move()
    {
        //AppliedPower = V_Tol ? 0 : maxPower * Input.GetAxis("AirplaneVertical") * Time.deltaTime;
        //liftPower = V_Tol ? maxPower * Input.GetAxis("AirplaneVertical") * Time.deltaTime : GetCurrentLiftPower(moveSpeedKmH);

        AppliedPower = V_Tol ? 0 : maxPower * InputsController.Instance.PlayerMoveForwardDir * Time.deltaTime;
        liftPower = V_Tol ? maxPower * InputsController.Instance.PlayerMoveForwardDir * Time.deltaTime : GetCurrentLiftPower(moveSpeedKmH);

        rb.AddRelativeForce(new Vector3(0, liftPower, AppliedPower), ForceMode.Force);

        moveSpeedKmH = rb.velocity.magnitude * 3.6f;

        //print(Input.GetAxis("AirplaneVertical"));

        UI_Mngr.Instance.TextSprites["airplaneCurrentMoveSpeed"].text = moveSpeedKmH.ToString("0") + " Km/h";
    }

    // Update is called once per frame
    void Update()
    {
        //player inputs
        QuitGame();
        
        ToggleV_Tol();

        Rotate();
        Move();
    }
}
