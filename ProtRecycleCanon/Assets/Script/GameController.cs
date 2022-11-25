using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Monster
{
    public GameObject Prefab;
    [Range(0f,100f)]public float chance = 100f;

    [HideInInspector] public double _weight;
}
public class GameController : MonoBehaviour
{
  

    private bool playerTurn = true;
    [HideInInspector]public bool monsterTurn = false;

    [SerializeField] private GameObject[] municoes;
    
    [SerializeField] private Monster[] monster;
    [SerializeField] private GameObject bigMonster;
    [SerializeField] private Transform[] monsterSpawnPosition;
    private double accumulatedWeihgts;
    private System.Random rand = new System.Random();
    [SerializeField] private int[] waveMonsters;
    private int waveMoment = 0;


    private void Awake()
    {
        CalculateWeights();
    }
    void Start()
    {
        
        waveMoment = 0;
        for(int i = 0; i < 11; i++)
        {
            SpawnRandomLixo(new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(4f,15f)));
        }
       
    }

    private void Update()
    {
        

        if (playerTurn==false && monsterTurn == true)
        {
            playerTurn = true;
            Debug.Log("deu certo?");
            StartCoroutine(SpawnRandomMonster());
         
        }
        


    }
    private void SpawnRandomLixo (Vector3 position)
    {
        GameObject prefab = municoes[Random.Range(0, municoes.Length)];

        Instantiate(prefab, position, Quaternion.identity, transform);
    }
    IEnumerator SpawnRandomMonster()
    {
        
        for(int i =0; i < waveMonsters[waveMoment]; i++)
        {
            Monster randomMonster = monster[GetRandomEnemyIndex()];
            Transform position = monsterSpawnPosition[Random.Range(0, monsterSpawnPosition.Length)];
            Instantiate(randomMonster.Prefab, position.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(1f);
        }
        waveMoment++;

        yield return new WaitForSeconds(5f);
        Instantiate(bigMonster,monsterSpawnPosition[1].position, Quaternion.identity);
    }

    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeihgts;

        for(int i = 0; i < monster.Length; i++)
        {
            if(monster[i]._weight >= r)
            {
                return i;
            }
            
        }
        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeihgts = 0f;
        foreach(var monsters in monster)
        {
            accumulatedWeihgts += monsters.chance;
            monsters._weight = accumulatedWeihgts;
        }
    }

    public void EndTurnPlayer()
    {
        if(playerTurn==true && monsterTurn == false)
        {
            playerTurn = false;
            monsterTurn = true;
        }
       
    }
}
