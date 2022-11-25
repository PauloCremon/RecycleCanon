using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [SerializeField] FixedJoystick joyS;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject municao;

    private Rigidbody rig;
    private bool podepegar = true;
    private int containerType = -1;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movementDirection = new Vector3(joyS.Horizontal, 0, joyS.Vertical) * speed; ;
        movementDirection.y = rig.velocity.y;
        rig.velocity = movementDirection;
        
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "municao")
        {
            if(podepegar)municao = other.gameObject;
        }
        if(other.gameObject.transform.tag == "plastico")
        {
            containerType = 2;
        }
        if(other.gameObject.transform.tag == "metal")
        {
            containerType = 1;
        }
        if(other.gameObject.transform.tag == "organico")
        {
            containerType = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag == "municao")
        {
            if (podepegar) municao = null;
        }
        if (other.gameObject.transform.tag == "plastico" || other.gameObject.transform.tag == "metal" || other.gameObject.transform.tag == "organico")
        {
            containerType = -1;
        }
        

    }

    public void Pegar()
    {
        if (municao != null && podepegar == true)
        {
            podepegar = false;
            municao.transform.SetParent(transform.GetChild(0).gameObject.transform);
            municao.transform.GetComponent<Rigidbody>().isKinematic = true;
            municao.transform.localPosition=Vector3.zero;
            municao.transform.GetComponent<BoxCollider>().enabled=false;
            
        }
        else if(municao!=null && podepegar == false && containerType ==-1)
        {
            podepegar = true;
            municao.transform.SetParent(null);
            municao.transform.GetComponent<Rigidbody>().isKinematic = false;
            municao.transform.GetComponent<BoxCollider>().enabled = true;
        }
        else if(municao != null && podepegar == false && municao.gameObject.transform.GetComponent<lixo>().type == containerType)
        {
            podepegar = true;
            Debug.Log("deu certo!");
            GameObject canhao = GameObject.FindGameObjectWithTag("canhao");
            canhao.gameObject.transform.GetComponent<TowerControler>().muniType.Add(municao.gameObject.transform.GetComponent<lixo>().type);
            canhao.gameObject.transform.GetComponent<TowerControler>().muniVal.Add(municao.gameObject.transform.GetComponent<lixo>().valMunicao);
            Destroy(municao.gameObject);
            municao = null;
        }
    }

}
