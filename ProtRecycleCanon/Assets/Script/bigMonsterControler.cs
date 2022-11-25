using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigMonsterControler : MonoBehaviour
{
    [SerializeField] private float velocity;
    private int life = 20;


    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.Translate(Vector3.forward * Time.deltaTime * velocity);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "tiro")
        {
            life--;    
            Destroy(other.gameObject);       
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "muralha")
        {
            velocity = 0;
            
            if (GameObject.FindGameObjectWithTag("Controller").gameObject.transform.GetComponent<GameController>().monsterTurn)
            {   //checar essa logica depois
                GameObject.FindGameObjectWithTag("Controller").gameObject.transform.GetComponent<GameController>().monsterTurn = false;

            }
        }
    }
}
