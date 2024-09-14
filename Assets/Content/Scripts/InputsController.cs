using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputsController : Singleton<InputsController>
{
    private PlayerInputs p;
    public Dictionary<string, InputAction> Inputs { get; private set; }
    
    [SerializeField] private Vector3 playerRotationDir;
    public Vector3 PlayerRotationDir { get { return playerRotationDir; } private set { playerRotationDir = value; } }

    [SerializeField] private float playerMoveForwardDir;
    public float PlayerMoveForwardDir { get { return playerMoveForwardDir; } private set { playerMoveForwardDir = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        p = new PlayerInputs();
        Inputs = new Dictionary<string, InputAction>();

        InputSystem.settings.maxEventBytesPerUpdate = 0;

        for (int i = 0; i < p.asset.actionMaps.Count; i++)
        {
            for (int j = 0; j < p.asset.actionMaps[i].actions.Count; j++)
            {
                Inputs[p.asset.actionMaps[i].actions[j].name] = p.asset.actionMaps[i].actions[j];
            }
        }

        //print(p.asset.actionMaps[0].actions[0].name);
    }

    private void OnEnable()
    {
        p.Enable();
    }

    private void OnDisable()
    {
        p.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotationDir = Inputs["RotationAxes"].ReadValue<Vector3>();
        PlayerMoveForwardDir = Inputs["ForwardDir"].ReadValue<float>();
    }
}
