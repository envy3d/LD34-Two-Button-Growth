using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    public string turnInputButton = "Jump";

    public float maxVelocity = 10;
    public float maxForce = 25;
    public float maxAngularVelocity = 180;
    public AnimationCurve turningAngleCurve = AnimationCurve.Linear(0, 0, 2, 50);

    private Rigidbody rb;
    private ITurningSelection turningSelection;
    private float turnStartTime = 0;
    private float currTurnAngle = 0;
    private int currTurnDirection = 1;
    private Vector3 wheelForce = Vector3.zero;

	void Start()
    {
        turningSelection = new TurningFlipFlopSelection();
        rb = GetComponent<Rigidbody>();
	}
	
	
	void Update()
    {
        // Turning control
	    if (Input.GetButton(turnInputButton))
        {
            // If the button was pressed this frame, reset turn variables and change turn direction.
            if (Input.GetButtonDown(turnInputButton))
            {
                turnStartTime = Time.time;
                currTurnDirection = turningSelection.GetNewTurningDirection(currTurnDirection);
            }
            // If the button is being held, advance turn variables.
            else
            {
                currTurnAngle = currTurnDirection * turningAngleCurve.Evaluate(Time.time - turnStartTime);
            }
        }
        // If the vehicle should no longer be turning, stop turning.
        else
        {
            currTurnAngle = 0;
        }

        //rb.AddTorque(currTurnAngle);
        //rb.AddRelativeTorque(Vector3.back * currTurnAngle);

        // Force vector creation
        Debug.DrawRay(transform.position, rb.velocity, Color.green);
        Debug.DrawRay(transform.position, transform.localToWorldMatrix * wheelForce, Color.cyan);
        Debug.DrawRay(transform.position, transform.localToWorldMatrix * new Vector3(-rb.angularVelocity.z, 0, 0), Color.yellow);
        Debug.DrawRay(transform.position, transform.localToWorldMatrix * new Vector3(currTurnAngle, 0, 0), Color.blue);

        wheelForce = Vector3.up * maxForce;

    }

    void FixedUpdate()
    {
        // Limit velocity
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        rb.velocity = Vector3.ClampMagnitude(Vector3.Project(rb.velocity, Vector3.up), maxVelocity) +
                      Vector3.Project(rb.velocity, Vector3.right);
        //rb.angularVelocity = new Vector3(0, 0, Mathf.Clamp(rb.angularVelocity.z,
        //                                                   -maxAngularVelocity * Mathf.Deg2Rad,
        //                                                   maxAngularVelocity * Mathf.Deg2Rad));

        rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(currTurnAngle * Time.fixedDeltaTime, Vector3.back));

        //Debug.Log(rb.angularVelocity.z * Mathf.Rad2Deg);
        //Debug.Log(currTurnAngle);

        // Force vector application
        rb.AddRelativeForce(wheelForce);
       // rb.AddRelativeTorque(Vector3.back * currTurnAngle);
    }
}
