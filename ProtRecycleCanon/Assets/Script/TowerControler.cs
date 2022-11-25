using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joyS;
    [SerializeField] private GameObject localAtaq;
    [SerializeField] private GameObject mira;
    [SerializeField] private GameObject Tower;
    [SerializeField] private Transform Tiro;
    [SerializeField] private GameObject[] bullet;

   public List<int> muniType = new List<int>();
   public List<int> muniVal = new List<int>();

    [SerializeField] private float rotationSpeed = 0;
    void Start()
    {
        Tower.transform.LookAt(mira.transform.position);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(joyS.Horizontal, 0, 0);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            localAtaq.transform.rotation = Quaternion.RotateTowards(new Quaternion(0, Mathf.Clamp(localAtaq.transform.rotation.y, -15,15), 0, 1f), toRotation, rotationSpeed * Time.deltaTime);

            Tower.transform.LookAt(mira.transform.position);
        }
    }
    
    public void Ataque()
    {
        if (muniVal[0] != 0)
        {
            if (muniVal[0] == 3)
            {

                Debug.Log("Piu!");
                StartCoroutine(ataqueT(bullet[muniType[0]]));
                muniType.Remove(muniType[0]);
                muniVal.Remove(muniVal[0]);
            }
            else if (muniVal[0] == 5)
            {
                Debug.Log("Piu!");
                StartCoroutine(ataqueC(bullet[muniType[0]]));
                muniType.Remove(muniType[0]);
                muniVal.Remove(muniVal[0]);
            }
        }
        else Debug.Log("Sem munição");
       
    }

    IEnumerator ataqueT(GameObject bulletS)
    {
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
    }
    IEnumerator ataqueC(GameObject bulletS)
    {
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletS, Tiro.transform.position, Tiro.transform.rotation);
    }
}
