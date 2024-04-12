using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newBoatMovement : MonoBehaviour
{

    // Variables
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float NewMaxSpeed = 10f;
    public float Drag = 0.1f;

    // Previous variables

    public float speed;
    public float maxSpeed;
    public float acceleration = 0f;
    public float torque;
    public float angle;
    public float sv_normalized;
    public float maxTorque = 5f;
    public float torqueChangeSpeed = 2f;
    Rigidbody rb;
    public InputActionReference triggerAction;
    public InputActionReference gripAction;
    public Transform rightController;
    public Transform vectorGuide;
    public Transform parentBoat;
    private GameManager gm;
    bool triggerDown = false;
    bool gripDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GetComponent<GameManager>();
    }

    void Update()
    {
        buttonStates();

        // Get the y-angle from the right controller
        angle = rightController.transform.localEulerAngles.y;

        // Convert values to lie between -180 to 180
        if(angle > 180)
        {
            angle -= 360;
        }

        // Clamp value to be between -90 and 90 
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Change the vector if 
        if(triggerDown)
        {
            vectorGuide.localRotation = Quaternion.Euler(0, angle, 0);
        }

        // Normalize the support to lie between -1 to 1
        sv_normalized = vectorGuide.localEulerAngles.y;
        sv_normalized = sv_normalized > 180 ? sv_normalized - 360 : sv_normalized;
        sv_normalized = sv_normalized / 90;

        if(gripDown)
        {
            if(speed < maxSpeed)
            {
            acceleration += 10*Time.deltaTime;
            speed = speed+acceleration;
            }
        }
        else{
            acceleration -= 15*Time.deltaTime; 
            speed -= 1400*Time.deltaTime;
        }
    
        if(acceleration < 0)
        {
            acceleration = 0;
        }
               if(speed < 0)
        {
            speed = 0;
        }
    }

    void FixedUpdate()
    {
        //default direction
        var forceDirection = transform.forward;
        float steer = 0;

        steer = sv_normalized;

        //Debug.Log($"Force added to rotation: {steer * transform.right * SteerPower}");


        //Rotational Force
        rb.AddForceAtPosition(steer * transform.right * SteerPower, Motor.position);

        //compute vectors
        var forward = Vector3.Scale(new Vector3(1,0,1), transform.forward);

        rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Force);

        /* //forward/backward power
        if (gripDown)
            ApplyForceToReachVelocity(rb, forward * MaxSpeed, Power);
        if (!gripDown)
            ApplyForceToReachVelocity(rb, forward * -MaxSpeed, Power); */
    }

    public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
        {
            if (force == 0 || velocity.magnitude == 0)
                return;

            velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

            //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
            force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

            //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
            if (rigidbody.velocity.magnitude == 0)
            {
                rigidbody.AddForce(velocity * force, mode);
            }
            else
            {
                var velocityProjectedToTarget = velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude;
                rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
            }
        }

        void LateUpdate()
    {
        // Preventing unneccessary position or rotation changes

        // Get the current rotation
        Quaternion currentRotation = transform.rotation;

        // Reset the rotation to zero
        transform.rotation = Quaternion.identity;

        // Preserve the y-axis rotation
        float newYRotation = currentRotation.eulerAngles.y;
        
        this.transform.position = new Vector3(this.transform.position.x, 0.38f, this.transform.position.z);
        transform.Rotate(0, newYRotation, 0);
        
    }

    void buttonStates()
    {
        // wow look how fancy we are we're using terniary operators oooo
        triggerDown = triggerAction.action.ReadValue<float>() > 0 ? true : false;
        gripDown = gripAction.action.ReadValue<float>() > 0 ? true : false;
    }
    void OnEnable()
    {
        triggerAction.action.Enable();
        triggerAction.action.performed += OnTriggerStarted;

        gripAction.action.Enable();
        gripAction.action.performed += OnGripStarted;
    }

    void OnDisable()
    {
        triggerAction.action.Disable();
        triggerAction.action.performed -= OnTriggerStarted;

        gripAction.action.Disable();
        gripAction.action.performed -= OnGripStarted;
    }

    void OnTriggerStarted(InputAction.CallbackContext context)
    {
        if (context.action == triggerAction.action)
        {
            //Debug.Log("Right hand trigger pressed!");
        }
    }

    void OnGripStarted(InputAction.CallbackContext context)
    {
        if (context.action == gripAction.action)
        {
            //Debug.Log("Right hand grip pressed!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("buoy"))
        //{
            gm.crashCounter += 1;
            Debug.Log($"Ouch! Crash Counter is now {gm.crashCounter}");
        //}
    }
}
