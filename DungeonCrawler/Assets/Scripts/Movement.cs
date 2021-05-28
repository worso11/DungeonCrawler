using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class Movement : MonoBehaviour
{

    public float moveHorizontal;
    public float moveVertical;
    public GameObject[] guns;
    public Transform shootingPoint;
    public GameObject[] enemies;
    public Texture2D cursor;
    
    private GameObject weaponPos;
    private bool canMove = true;
    private Camera cam;
    private float currentTime = 0.0f;
    private float weaponTime = 1f;
    private int iter = 0;
    private GameObject weapon;
    private AudioSource audio;
    private float speed = 3.0f;
    private Rigidbody2D player;
    private Vector2 move;
    private Vector3 position;
    private Vector2 mousePos;
    private Vector3 lastPosition;
    private bool rightHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        Cursor.SetCursor(cursor, new Vector2(cursor.width/2, cursor.height/2), CursorMode.Auto);
        player = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        lastPosition = player.transform.position;
        weaponPos = GameObject.Find("Weapon");
        
        weapon = Instantiate(guns[iter], new Vector3(weaponPos.transform.position.x, 
            weaponPos.transform.position.y, -1), new Quaternion());
        weapon.transform.parent = cam.transform;
        weapon.layer = LayerMask.NameToLayer("TransparentFX");
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        move = new Vector2(moveHorizontal, moveVertical);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //cam.transform.position = new Vector3(player.position.x, player.position.y, -10);

        if (player.transform.position != lastPosition && !audio.isPlaying)
        {
            audio.volume = Random.Range(0.1f, 0.3f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentTime < Time.time)
        {
            Destroy(weapon);
            iter = iter == 1 ? 0 : 1;
            weapon = Instantiate(guns[iter], new Vector3(weaponPos.transform.position.x, 
                weaponPos.transform.position.y, -1), new Quaternion());
            weapon.transform.parent = cam.transform;
            weapon.layer = LayerMask.NameToLayer("TransparentFX");
            weaponTime = weapon.GetComponent<Weapon>().getFireRate();
            currentTime = Time.time + weaponTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(enemies[0], new Vector3(5f, Random.Range(-5, 7)*0.5f, 0), new Quaternion());
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(enemies[1], new Vector3(5f, Random.Range(-5, 7)*0.5f, 0), new Quaternion());
        }

        if (Input.GetKey(KeyCode.Space) && currentTime < Time.time)
        {
            Shoot();
        }

        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        lastPosition = player.transform.position;
    }

    private void Shoot()
    {
        weapon.GetComponent<Weapon>().Shoot(shootingPoint);
    }
    

    public void switchMove()
    {
        canMove = !canMove;
    }
    
    private void FixedUpdate()
    {
        if (canMove)
        {
            position = player.position + move * (speed * Time.fixedDeltaTime);
            player.MovePosition(position);   
        }

        Vector2 shootingDir = mousePos - player.position;
        float shootingAngle = Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg;
        shootingPoint.eulerAngles = new Vector3(0f,0f,shootingAngle);
    }
}
