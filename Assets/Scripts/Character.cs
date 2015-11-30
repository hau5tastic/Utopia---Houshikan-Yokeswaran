using UnityEngine;
using System.Collections;

// prevents accidental deletion of componenent
[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour {

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float rotateSpeed = 5.0f;
    public float gravity = 9.81f;

    Vector3 moveDirection = Vector3.zero;

    public int type = 0;

    CharacterController cc;

    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    public float fireSpeed = 50f;


    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        if(!cc)
        {
            Debug.Log("Character Controller doesn't exist");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //SimpleMove()
	    if(type == 1)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);

            float curSpeed = speed * Input.GetAxis("Vertical");

            cc.SimpleMove(forward * curSpeed);
        }
        //Move()
        else if(type == 0)
        {
            if(cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

                moveDirection = transform.TransformDirection(moveDirection);

                moveDirection *= speed; 
                
                if(Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }             
            }

            moveDirection.y -= gravity * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);
        }

        // Key Press
        if(Input.GetButtonDown("Fire1"))
        {
            if (projectilePrefab)
            {
                Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Rigidbody;
                temp.AddForce(transform.forward * fireSpeed, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("No Prefab Found");
            }
        }

	} // Update
}
