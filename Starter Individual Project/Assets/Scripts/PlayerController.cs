using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float timeCurrent = 0f;
    public float timeStart = 14f;

    Vector2 movement;

    public AudioSource musicSource1;
    public AudioSource musicSource2;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;

    public Text time;
    public Text mid;


    private Rigidbody2D rd2d;
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rd2d = GetComponent<Rigidbody2D>();
        mid.text = "Save Koala! WASD";
        timeCurrent = timeStart;
        musicSource2.clip = musicClipTwo;
        musicSource2.Play();
        musicSource1.clip = musicClipOne;
        musicSource1.Play();
        musicSource1.loop = true;
    }

    void FixedUpdate()
    {
        timeCurrent -= 1 * Time.deltaTime;
        time.text = timeCurrent.ToString("0");

        rd2d.MovePosition(rd2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (timeCurrent <= 12)
        {
            mid.text = "";
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            anim = GetComponent<Animator>();
        }

        if (timeCurrent <= 2)
        {
            mid.text = "Game Over! Game created by Brandon Rodriguez";
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
            Destroy(musicSource1);
            musicSource2.clip = musicClipTwo;
            musicSource2.Play();
        }

        if (timeCurrent <= 0)
        {
            Application.Quit();
        }
        

    }

    void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Back", true);           
        }
        else
        {
            anim.SetBool("Back", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Forward", true);
        }
        else
        {
            anim.SetBool("Forward", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Left", true);
        }
        else
        {
            anim.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Right", true);
        }
        else
        {
            anim.SetBool("Right", false);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {

        if (collision.collider.tag == "Koala")
        {
            mid.text = "You Win! Game created by Brandon Rodriguez";
            Destroy(sprite);
            Destroy(rd2d);
            Destroy(anim);
            Destroy(time);
            Destroy(musicSource1);
            Time.timeScale = 0;
            musicSource2.clip = musicClipTwo;
            musicSource2.Play();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Fire")
        {
            transform.position = new Vector2(-10f, 0f);
            musicSource2.clip = musicClipThree;
            musicSource2.Play();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
