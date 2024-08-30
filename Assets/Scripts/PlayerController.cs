using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 speed;
    public float x_Speed = 8f, z_Speed = 15f;
    private float rotationSpeed = 10f;
    private float maxAngle = 10f;

    private AudioSource soundManager;
    public AudioClip engine_On_Sound;
    public AudioClip engine_Off_Sound;

    private Rigidbody myBody;

    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;

    // Start is called before the first frame update
    void Awake()
    {
        soundManager = this.GetComponent<AudioSource>();
        soundManager.clip = engine_On_Sound;
        soundManager.volume = 0.5f;
        soundManager.Play();

        myBody = this.GetComponent<Rigidbody>();
        speed = new Vector3(0f, 0f, z_Speed);
        Invoke("TurnOffAcceleration", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTank();
        ControlMovementWithKeyboard();
        ChangeRotation();
        ShootingControl();
    }

    void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    void ControlMovementWithKeyboard()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            speed = new Vector3(-x_Speed / 2f, 0f, speed.z);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            speed = new Vector3(0f, 0f, speed.z);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            speed = new Vector3(x_Speed / 2f, 0f, speed.z);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            speed = new Vector3(0f, 0f, speed.z);
        }
    }

    void ChangeRotation()
    {
        if(speed.x > 0)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    void TurnOffAcceleration()
    {
        soundManager.clip = engine_Off_Sound;
        soundManager.volume = 0.3f;
        soundManager.Play();
    }

    public void ShootingControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
            bullet.GetComponent<BulletScript>().Move(3000f);
            shootFX.Play();
        }
    }

}
