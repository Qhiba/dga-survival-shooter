using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Animator anim;
    private Rigidbody playerRigidbody;

    private Vector3 movement;

    private int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        //Mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor");

        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Mendapat nilai input horizontal (-1, 0, 1)
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Mendapatkan nilai input vertical (-1, 0, 1)
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);
    }

    //Method player dapat berjalan
    public void Move(float horizontal, float vertical)
    {
        //Set nilai x dan y
        movement.Set(horizontal, 0f, vertical);

        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;

        //Move to Position
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Buat rayacst untuk floorHit
        RaycastHit floorHit;

        //Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Mendapatkan vector dari posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float horizontal, float vertical)
    {
        bool isWalking = horizontal != 0f || vertical != 0f;
        anim.SetBool("isWalking", isWalking);
    }
}