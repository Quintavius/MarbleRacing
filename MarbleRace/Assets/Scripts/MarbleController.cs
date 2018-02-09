using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleController : MonoBehaviour {
    [Header("Player Settings")]
    public int playerId;
    public bool AIMarble;

    [Header("Movement Settings")]
    public float acceleration;
    public float maximumSpeed;
    public float slopeSpeed;
    public float waterDrag;

    //Shorthands
    [HideInInspector]
    public Rigidbody rb;
    Collider col;
    Camera cam;
    public GameObject stabilizer;
    MarbleSoundManager soundManager;
    MarbleRank marbleRank;

    //Variables
    float hmove;
    float vmove;
    string axisH;
    string axisV;
    [HideInInspector]
    public float mag;
    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool playDropSound;
    int colCount;
    float originalDrag;

    //Mobile specifics
    float xCalibrate;
    float yCalibrate;

	void Start () {
        //initialize
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        col = GetComponent<Collider>();
        soundManager = GetComponent<MarbleSoundManager>();
        originalDrag = rb.drag;
        marbleRank = GetComponent<MarbleRank>();
        colCount = 0;
        //Mobile
        CalibrateGyro();

        //Assign player ID
        if (playerId == 0) { axisH = "Horizontal_Player1"; axisV = "Vertical_Player1"; }
        else if (playerId == 1) { axisH = "Horizontal_Player2"; axisV = "Vertical_Player2"; }
        else if (playerId == 2) { axisH = "Horizontal_Player3"; axisV = "Vertical_Player3"; }
        else if (playerId == 3) { axisH = "Horizontal_Player4"; axisV = "Vertical_Player4"; }
    }
	
    void KeyboardInput(){
        //Balance out Keyboard input
        if (hmove == 1 && vmove == 1)
        {
            hmove = 0.5f;
            vmove = 0.5f;
        }
    }

    void MobileInput(){
        hmove = Mathf.Clamp((Input.acceleration.x - xCalibrate) * 5,-1f, 1f);
        vmove = Mathf.Clamp((Input.acceleration.y - yCalibrate) * 5, -1f, 1f);
    }

    void ControllerInput(){
        if (hmove != 0 || vmove != 0) {
            if (rb.velocity.magnitude < maximumSpeed * 1.5f)
            {
                //Get force direction
                Vector3 dir = new Vector3(hmove, 0, vmove);
                Vector3 steerDir = stabilizer.transform.TransformDirection(dir);
                //LET THE FORCE FLOW THROUGH YOU
                rb.AddForce(steerDir * acceleration * Time.deltaTime);
            }
        }
    }

	void Update () {
        hmove = Input.GetAxis(axisH);
        vmove = Input.GetAxis(axisV);
        if (!marbleRank.isFinished){
            KeyboardInput();
            //MobileInput();
            ControllerInput();
        }
	}

    private void FixedUpdate()
    {
        if (colCount == 0) { grounded = false; } else { grounded = true; }
    }

    void OnCollisionStay(Collision colInfo)
    {
        //Increase speed when rolling
        mag = rb.velocity.magnitude;
        if (rb.velocity.y < 0 && mag < maximumSpeed)
        {
            rb.AddForce(new Vector3(rb.velocity.x * slopeSpeed, rb.velocity.y * slopeSpeed, rb.velocity.z * slopeSpeed));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colCount++;

        if (collision.gameObject.tag == "Player")
        {
            //We've hit a player, do a click noise
            var impact = Mathf.Clamp(collision.impulse.sqrMagnitude + 0.1f,0,1.5f);
            soundManager.MarbleSound(impact);

            Rigidbody rbOther = collision.gameObject.GetComponent<Rigidbody>();
            rbOther.AddForceAtPosition(collision.impulse * 10, collision.contacts[0].point);
            rb.AddForceAtPosition(collision.impulse * 10, collision.contacts[0].point);
        }
        else
        {
            //are we touching multiple objects and did we hit hard enough to make a sound
            if (collision.impulse.magnitude > 0.01f && colCount > 1f)
            {
                var impact = collision.impulse.magnitude / 3;
                soundManager.WallSound(impact);
                
            }
            else
            {
                //Did we just fall?
                if (!grounded) { playDropSound = true; }
                if (playDropSound)
                {
                    var impact = collision.impulse.magnitude/3;
                    soundManager.DropSound(impact);
                    playDropSound = false;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        colCount--;
    }
    // Deal with being underwater, set drag
    private void OnTriggerEnter(Collider other){
        if (other.tag == "Water"){
            rb.drag = waterDrag;
            rb.angularDrag = waterDrag;
            rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.tag == "Water"){
            rb.drag = originalDrag;
            rb.angularDrag = originalDrag;
        }
    }
    public void CalibrateGyro() {
        xCalibrate = Input.acceleration.x;
        yCalibrate = Input.acceleration.y;
    }
}
