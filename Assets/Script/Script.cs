using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script : MonoBehaviour
{
    public float velocity;
    public Rigidbody2D body;
    public float JumpForce;
    public Animator Animacao;
    private float DEAD;
    public Collider2D colIni;
    public Collider2D colMORTO;
    private bool onFloor;
    private bool canJump = true;
    private bool highPlace;
    private bool keyLeftOneDown;
    private bool keyLeftTwoDown;
    private bool keyRightOneDown;
    private bool keyRightTwoDown;
    
    // Start is called before the first frame update
   
    void Start()
    {
        Application.targetFrameRate = 60;
        colMORTO.enabled = false;
    }

    // Update is called once per frame
   
    void Update()
    {
         if (Input.GetButtonDown("Jump") && canJump)
         {
            Invoke("Jump", 0.3f);
            canJump = false;
            highPlace = false;
            
            if (transform.position.y > 0) 
            {
                highPlace = true;
            }
         }

        
        Move();
        Animacao.SetBool("Jumping", !onFloor || !canJump);

       
    }
    private void Move()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * velocity;
        keyLeftOneDown = Input.GetKey(KeyCode.A);
        keyLeftTwoDown = Input.GetKey(KeyCode.LeftArrow);
        keyRightOneDown = Input.GetKey(KeyCode.D);
        keyRightTwoDown = Input.GetKey(KeyCode.RightArrow);

        if(keyLeftOneDown || keyLeftTwoDown)
        {
            body.velocity = new Vector2(-6, body.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        
        if(keyRightOneDown || keyRightTwoDown)
        {
            body.velocity = new Vector2(6, body.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if(body.velocity.x > 5 || body.velocity.x < -5)
        {

            Animacao.SetBool("Walking",true);
        } else 
        {
            Animacao.SetBool("Walking", false);
        }

        
    }

    private void Jump()
    { 
        
        onFloor = false;
        
        
        if (highPlace)
        {
            body.AddForce(new Vector2(0f, JumpForce / 1.6f), ForceMode2D.Impulse);
        }
        else
        {
            body.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
            
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
       
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("Está no Chão");
            canJump = true;
            onFloor = true;
            //Animacao.SetBool("Jumping", false);
        }
    }

     /*void OnCollisionExit2D(Collision2D collision)
     {
        
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("Saiu do Chão");
            onFloor = false;
        }
     }*/

    //void MoveObject ()

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log ("Bateu no bixo");
        //Animacao.Play("DEAD");
        Animacao.SetBool("DEAD", true);
        
        colIni.enabled = false;
        colMORTO.enabled = true;
        Invoke("SceneExit", 1);
        

    }
    void SceneExit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
 }
