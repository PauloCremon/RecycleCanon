using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterControler : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int monsterType;
    private int life = 3;

    [SerializeField] private GameObject[] restos;

    private void Start()
    {
        StartCoroutine(SpawnLixos());
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocity);
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").gameObject.transform.position) <= 2)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);
           
        }
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        if (life == 0)
        {
            Destroy(gameObject);
            Instantiate(restos[Random.Range(0, restos.Length)],transform.position, Quaternion.identity);
        }
    }

    IEnumerator SpawnLixos()
    {
        while (velocity>0)
        {
            Instantiate(restos[Random.Range(0, restos.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10,6));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "tiro")
        {
            if(monsterType == 0 && other.gameObject.transform.GetComponent<bullet>().type >= 1)
            {
                life--;
                Destroy(other.gameObject);
            }
            else if (monsterType == 1 && other.gameObject.transform.GetComponent<bullet>().type == 0)
            {
                life--;
                Destroy(other.gameObject);
            }
            else if (monsterType == 2 && other.gameObject.transform.GetComponent<bullet>().type == 0)
            {
                life--;
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "muralha")
        {
            velocity = 0;
           
        }
    }

}
