using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO : Revisar porque me permite saltar infinitas veces en el aire

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public CapsuleCollider col;

    [Range(1, 20)]
    public float moveSpeed;

    [Range(1, 20)]
    public float jumpForce;

    private float fallGravity = 2.5f;

    private float shortJump = 2f;

    public LayerMask layerFloor;


    void Awake()
    {
        // Asigno el Rigidbody
        rb = GetComponent<Rigidbody>();
        // Asigno el CapsuleCollider
        col = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void FixedUpdate()
    {
        // MOVIMIENTO HORIZONTAL
        float horizontalMovement = Input.GetAxis("Horizontal");
        // Si estoy intentando moverme a la izquierda...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || horizontalMovement == -1)
        {

            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // Si estoy intentando moverme a la derecha...
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || horizontalMovement == 1)
        {

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // SALTO
        if (OnFloor() && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") ))
        {
            Debug.Log("El jugador esta en el piso intentando saltar...");

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

        // MEJORA AL SALTO, PARA QUE CAIGA MAS RAPIDO
        if (rb.velocity.y < 0) // Si mi velocidad vertical es menor a 0, osea que estoy cayendo
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallGravity - 1) * Time.deltaTime; // Aumento la velocidad con la que caigo
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && !Input.GetButton("Jump")) // Si dejo de presionar la tecla o boton de salto antes de peder mi velocidad vertical
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (shortJump - 1) * Time.deltaTime;
        }
        // ^esto significa que solo voy a poder tener velocidad vertical positiva si estoy saltando y que si suelto el boton de salto voy a empezar a caer



    }


    public bool OnFloor()
    {
        // Uso el collider para ver si estoy tocando algun piso
        Vector3 vector = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        return Physics.CheckCapsule(col.bounds.center, vector, col.radius * .9f, layerFloor);
    }




}
