using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lane;
    private GameObject player;

    public Text score_Text;
    private int zombie_Kill_Count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("GenerateObstacles");
    }

    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(0f, 3f);
        yield return new WaitForSeconds(timer);
        CreateObstacles(player.transform.position.z + 100);
        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zPos)
    {
        if(Random.Range(0, 2) > 0)
        {
            int obstacleLane = Random.Range(0, lane.Length);
            AddObstacle(new Vector3(lane[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length));
        }
        else
        {
            int zombieLane = Random.Range(0, lane.Length);
            AddZombies(new Vector3(lane[zombieLane].transform.position.x, 0.15f, zPos));
        }
    }

    void AddObstacle(Vector3 position, int type)
    {
        Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
    }

    void AddZombies(Vector3 position)
    {
        int count = Random.Range(0, 3) + 1;
        for (int i = 0; i < count; i ++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * 1);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], position + shift * i, Quaternion.identity);
        }
    }

    public void IncreaseScore()
    {
        zombie_Kill_Count++;
        score_Text.text = zombie_Kill_Count.ToString();
    }
}
