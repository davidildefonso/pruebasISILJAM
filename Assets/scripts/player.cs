using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;    
    private BoxCollider2D boxCollider2d;
    Vector3 characterScale;
    float characterScaleX;   
    float lockPos = 0;

    private float speed2 = 1;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;

    public Transform hermano;
    public Transform mama;
    public Transform abuelo;
    public Transform hermana;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;  

        target = new Vector2(1,1);
        position = gameObject.transform.position;

    }


    
    public LayerMask groundLayers;

    bool isGrounded(){
        Vector2 position=transform.position;
        Vector2 direction = Vector2.down;
        float distance=1.0f;
        
        RaycastHit2D hit = Physics2D.Raycast(position,direction,distance,groundLayers);
        if(hit.collider !=null){
            return true;
        }
        return false;
    }

    
    void Update()
    {
        
        transform.rotation = Quaternion.Euler (lockPos, lockPos, lockPos);

        float h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftControl))
        {
            
           gameObject.transform.position = new Vector2 (transform.position.x + (h*speed*2),transform.position.y); 
        }else{
             gameObject.transform.position = new Vector2 (transform.position.x + (h*speed),transform.position.y);
        }
       
        if (Input.GetAxis("Horizontal") < 0) {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;

        if (Input.GetKeyDown(KeyCode.Space))
        {   
            if(!isGrounded())
            {
                return;
            }else
            {
                rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            }
           
            
        } 

        //float step = speed2 * Time.deltaTime;


    }


    public void OnCollisionEnter2D(Collision2D col)
     {
       //  float step = speed2 * Time.deltaTime;
         if(col.transform.tag == "hermano")
         {
             Debug.Log("rescatado!"); 
             Destroy(col.gameObject);
             Transform  myHermano = Instantiate(hermano, new Vector3( gameObject.transform.position.x-1f, gameObject.transform.position.y+0.5f, 0), Quaternion.identity) as Transform ;
            myHermano.parent=transform;
           
          //  if(col.gameObject.tag=="hermano"){
                
            //}
                          
         }else if(col.transform.tag == "mama"){
             Destroy(col.gameObject);
             Transform  myMama = Instantiate(mama, new Vector3( gameObject.transform.position.x-0.5f, gameObject.transform.position.y+0.5f, 0), Quaternion.identity) as Transform ;
            myMama.parent=transform;
         }
         else if(col.transform.tag == "abuelo"){
             Destroy(col.gameObject);
             Transform  myAbuelo = Instantiate(abuelo, new Vector3( gameObject.transform.position.x-1.5f, gameObject.transform.position.y+0.5f, 0), Quaternion.identity) as Transform ;
            myAbuelo.parent=transform;
         }
         else if(col.transform.tag == "hermana"){
             Destroy(col.gameObject);
             Transform  myHermana = Instantiate(hermana, new Vector3( gameObject.transform.position.x-2f, gameObject.transform.position.y+0.5f, 0), Quaternion.identity) as Transform ;
            myHermana.parent=transform;
         }
     }










}
