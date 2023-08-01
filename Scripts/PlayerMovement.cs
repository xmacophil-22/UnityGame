using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D body;
    private BoxCollider2D myCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask upperGroundLayer;
    [SerializeField] private float speed;
    public float horizontalInput = 0;
    private connection connection;

//////////////////////////////////////////////////////////////////////initialisiert Bewegung und baut verbindung mit python Bewegungserkennung auf
    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        connection = GameObject.Find("Connection").GetComponent<connection>();
    }
    private void OnEnable(){
        GameObject.Find("Connection").GetComponent<connection>().init();
    }

    private void OnDisable(){
        stop();
    }
    private void Start(){
        //horizontalInput = connection.horizontalIn;
        //Debug.Log(connection.horizontalIn);
    }

////////////////////////////////////////////////////////////////////Bewegung mit Tastaturinputs
    private void Update(){
        float test = Input.GetAxis("Horizontal");
        if(test != 0){
            horizontalInput = test;
        }
        transform.Translate(horizontalInput * speed * Time.deltaTime, 0, 0);
        if(horizontalInput < 0){
            transform.localScale = new Vector3(-1,1,1);
        }
        else{
            transform.localScale = Vector3.one;
        }
        //// MOVMENT ducken
        if(Input.GetKeyDown(KeyCode.L)){
            ducken();
        }


        //// MOVMENT jump
        if(Input.GetKey(KeyCode.Space) && isonGround()){
            jump();
        }

        //// provide falling through map
        if(transform.position.y < -5.5f){
            Physics2D.IgnoreLayerCollision(0,8,false);
            GameObject gameManager = GameObject.Find("GameManager");
            Standarts standarts = gameManager.GetComponent<Standarts>();
            gameManager.GetComponent<SpawnRoom>().createRooms(standarts.chooseTheme(standarts.getCurrentLayer() +1));
            
        }

        //// MOVMENT home
        if(Input.GetKeyDown(KeyCode.G)){
            home();
        }

        /////cheat

        if(Input.GetKeyDown(KeyCode.N)){
            cheat();
        }

        
    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////// MOVEMENTS

    public void stop(){
         
        body.velocity = new Vector2(0f, body.velocity.y);
        horizontalInput = 0;
        //body.angularVelocity = 0f;
    }
    public void ducken(){
        Physics2D.IgnoreLayerCollision(0,14);
        StartCoroutine(waitShort());
    }

    public void left(){
        /*transform.localScale = new Vector3(-1,1,1);
        //body.velocity = new Vector2(-1 * speed, body.velocity.y);
        if(body.velocity.x > -20.0f){
            //body.AddForce(new Vector2(-1 * speed, body.velocity.y), ForceMode2D.Impulse);
            body.velocity = new Vector2(body.velocity.x - 5f, body.velocity.y);
        }*/
        horizontalInput = -1;
        connection.horizontalIn = -1;
    }

    public void home(){
        SceneManager.LoadScene("Home");
    }

    public void right(){
        /*
        transform.localScale = Vector3.one;
        if(body.velocity.x < 20f){
            body.velocity = new Vector2(body.velocity.x + 5f, body.velocity.y);
            //body.AddForce(new Vector2(1 * speed, body.velocity.y), ForceMode2D.Impulse);
        }
        Debug.Log(body.velocity);
        //body.velocity = new Vector2(1 * speed, body.velocity.y);
        */
        horizontalInput = 1;
        connection.horizontalIn = 1;
    }

    public void cheat(){
        PlayerStats playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>(); 
            playerStats.changeAmountOfMaterial("wood", 20);
            playerStats.changeAmountOfMaterial("stone", 20);
            playerStats.changeAmountOfMaterial("whool", 20);
            playerStats.changeAmountOfMaterial("iron", 20);
            playerStats.changeAmountOfMaterial("coin", 20);
            playerStats.changeAmountOfMaterial("charcoal", 20);
    }

    public void jump(){
        body.velocity = new Vector2(body.velocity.x, 8*1.5f);
    }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////other

    ///////////////Zeit die das ducken anhält
    private IEnumerator waitShort(){
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreLayerCollision(0,14,false);
    }

    //////////////schützt vor double jumps
    private bool isonGround(){
        RaycastHit2D groundHit = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0, Vector2.down, 0.01f);
        RaycastHit2D groundHit2 = Physics2D.BoxCast(myCollider.bounds.center, myCollider.bounds.size, 0, Vector2.down, 0.01f);
        return groundHit.collider != null || groundHit2.collider != null;
    }
}
