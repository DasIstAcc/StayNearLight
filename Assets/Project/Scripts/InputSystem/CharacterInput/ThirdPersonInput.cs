using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonInput : MonoBehaviour
{
    #region Variables

    [Header("Controller Input")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public KeyCode jumpInput = KeyCode.Space;
    public KeyCode strafeInput = KeyCode.Tab;
    public KeyCode sprintInput = KeyCode.LeftShift;
    public KeyCode dashInput = KeyCode.V;
    public KeyCode mainAtkInput = KeyCode.Mouse0;
    public KeyCode secondaryAtkInput = KeyCode.Mouse1;

    [Header("Camera Input")]
    public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";

    [HideInInspector] public ThirdPersonController cc;
    [HideInInspector] public ThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;

    [HideInInspector] public bool isActive = false;

    #endregion

    public PlayerUnit owner { get; private set; }

    protected virtual void Start()
    {
        owner = gameObject.GetComponent<PlayerUnit>();
        InitilizeController();
        InitializeTpCamera();
    }

    protected virtual void FixedUpdate()
    {
        cc.UpdateMotor();               // updates the ThirdPersonMotor methods
        cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
        cc.ControlRotationType();       // handle the controller rotation type
    }

    protected virtual void Update()
    {
        if (!isActive) cc.input = new Vector3(0, 0, 0);
        if (isActive && owner.IsAlive()) InputHandle();                  // update the input methods
        if (cameraMain)
        {
            cc.UpdateMoveDirection(cameraMain.transform);
        }
        cc.UpdateAnimator();            // updates the Animator Parameters
    }

    public virtual void OnAnimatorMove()
    {
        cc.ControlAnimatorRootMotion(); // handle root motion animations 
    }

    #region Basic Locomotion Inputs

    protected virtual void InitilizeController()
    {
        cc = GetComponent<ThirdPersonController>();

        if (cc != null)
            cc.Init();
    }

    protected virtual void InitializeTpCamera()
    {
        if (tpCamera == null)
        {
            tpCamera = FindObjectOfType<ThirdPersonCamera>();
            if (tpCamera == null)
                return;
            if (tpCamera)
            {
                tpCamera.SetMainTarget(this.transform);
                tpCamera.Init();
                cc.camera_look = tpCamera.transform;
            }
        }
    }

    protected virtual void InputHandle()
    {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        JumpInput();
        DashInput();
        MainAttackInput();
    }

    public virtual void MoveInput()
    {
        cc.input.x = Input.GetAxis(horizontalInput);
        cc.input.z = Input.GetAxis(verticallInput);
    }

    protected virtual void CameraInput()
    {
        if (!cameraMain)
        {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            else
            {
                cameraMain = Camera.main;
                cc.rotateTarget = cameraMain.transform;
            }
        }

        if (tpCamera == null)
            return;

        var Y = Input.GetAxis(rotateCameraYInput);
        var X = Input.GetAxis(rotateCameraXInput);

        tpCamera.RotateCamera(X, Y);
    }

    protected virtual void StrafeInput()
    {
        if (Input.GetKeyDown(strafeInput))
            cc.Strafe();
    }

    protected virtual void SprintInput()
    {
        if (Input.GetKeyDown(sprintInput))
            cc.Sprint(true);
        else if (Input.GetKeyUp(sprintInput))
            cc.Sprint(false);
    }

    /// <summary>
    /// Conditions to trigger the Jump animation & behavior
    /// </summary>
    /// <returns></returns>
    protected virtual bool JumpConditions()
    {
        return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
    }

    /// <summary>
    /// Input to trigger the Jump 
    /// </summary>
    protected virtual void JumpInput()
    {
        if (Input.GetKeyDown(jumpInput) && JumpConditions())
            cc.Jump();
    }

    protected virtual bool DashConditions()
    {
        return cc.currDashCooldown <= 0;
    }

    protected virtual void DashInput()
    {
        if (Input.GetKeyDown(dashInput) && DashConditions())
            cc.Dash();
    }

    protected virtual void MainAttackInput()
    {
        if (Input.GetKeyDown(mainAtkInput))
            owner.PerformAbility(new AbilityTarget(), owner.SelectedMainAbl);
    }

    protected virtual void SecondaryAttackInput()
    {
        if (Input.GetKeyDown(secondaryAtkInput))
            owner.PerformAbility(new AbilityTarget(), owner.SelectedSecondaryAbl);
    }

    #endregion
}