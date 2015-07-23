using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    public Player playerScript;
    public bool canClimb;
    public float eixoY;

    private Rigidbody2D body2D;

    void Awake()
    {
        playerScript = GetComponent<Player>();
        body2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eixoY = Input.GetAxis("Vertical");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            canClimb = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            canClimb = false;
        }
    }

    void Escalar()
    {
        if ((canClimb) && (eixoY != 0))
        {
            body2D.gravityScale = 0;
        }
        else
        {
            //body2D.isKinematic = false;
        }
    }

}
