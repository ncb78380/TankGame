using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFXPrefab;
    private float speed = 1;
    private Rigidbody myBody;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        speed = Random.Range(1, 5);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }

        if(transform.position.y < -3f)
        {
            gameObject.SetActive(false);
        }
    }

    void Die()
    {
        isAlive = false;
        myBody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);
            Invoke("DeactivateGameObject", 3f);

            GameplayController.instance.IncreaseScore();
            Die();
        }
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
