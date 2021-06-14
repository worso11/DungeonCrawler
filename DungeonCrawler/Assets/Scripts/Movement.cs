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
    public Transform shootingPoint;
    public GameObject[] enemies;
    public Texture2D cursor;
    
    private GameObject weaponPos;
    private bool canMove = true;
    private Camera cam;
    private float currentTime = 0.0f;
    private float weaponTime = 1f;
    private int iter = 1;
    private int weaponNum = 3;
    private int[] weaponArr;
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

        weaponArr = new int[] {1, 0, 0};
        weapon = weaponPos.transform.GetChild(iter).gameObject;
        weapon.GetComponent<Weapon>().PrepareWeapon(weaponArr[iter-1]);
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
            //Destroy(weapon);
            weaponPos.transform.GetChild(iter).gameObject.SetActive(false);
            iter = iter == weaponNum ? 1 : iter+1;
            
            while (weaponArr[iter-1] == 0)
            {
                iter = iter == weaponNum ? 1 : iter+1;
            }
            
            weapon = weaponPos.transform.GetChild(iter).gameObject;
            weaponPos.transform.GetChild(iter).gameObject.SetActive(true);
            weapon.GetComponent<Weapon>().PrepareWeapon(weaponArr[iter-1]);
            weaponTime = weapon.GetComponent<Weapon>().getFireRate();
            currentTime = Time.time + weaponTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(enemies[0], new Vector3(4.5f, -0.5f, 0), new Quaternion());
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(enemies[1], new Vector3(4.5f, -0.5f, 0), new Quaternion());
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(enemies[2], new Vector3(4.5f, -0.5f, 0), new Quaternion());
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
    

    public void SwitchMove()
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

    public void GetWeapon(int i)
    {
        weaponArr[i] += 1;
        Debug.Log("{" + weaponArr[0] + "," + weaponArr[1] + "," + weaponArr[2] + "}");
        if (i == iter-1)
        {
            weapon.GetComponent<Weapon>().PrepareWeapon(weaponArr[iter-1]);   
        }
    }

    public void ResetWeapon()
    {
        weaponArr = new int[] {1, 0, 0};
    }
}
