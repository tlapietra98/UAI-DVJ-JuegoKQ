using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {

        // Muevo la camara en base a la posicion del jugador para que lo siga
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5f, Player.transform.position.z - 15f);


    }
}
