using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Rooms : MonoBehaviour
{
    public GameObject[] L;
    public GameObject[] R;
    public GameObject[] T;
    public GameObject[] B;
    public GameObject[] enemies;
    public GameObject[] weapons;
    public GameObject portal;
    public GameObject levelNum;
    public Dictionary<Tuple<float,float>, Tuple<GameObject,int>> rooms;

    private EnemyCounter enemyCount;
    private GameObject player;
    private GameObject camera;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        enemyCount = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCounter>();
        rooms = new Dictionary<Tuple<float, float>, Tuple<GameObject,int>>();
        Invoke(nameof(PrepareRooms), 2f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelNum.level = 0;
            player.GetComponent<Movement>().ResetWeapon();
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(camera, SceneManager.GetActiveScene());
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(weapons[0], new Vector3(-4.5f, 1.5f, 0), Quaternion.identity);
            Instantiate(weapons[1], new Vector3(-3f, 1.5f, 0), Quaternion.identity);
            Instantiate(weapons[2], new Vector3(-1.5f, 1.5f, 0), Quaternion.identity);
        }
        
    }

    public void Reset()
    {
        Instantiate(levelNum, new Vector3(0.5f, -0.5f, -10f), Quaternion.identity);
        rooms = new Dictionary<Tuple<float, float>, Tuple<GameObject,int>>();
        player.transform.position = new Vector3(0.5f, -0.5f, 0f);
        camera.transform.position = new Vector3(0.5f, -0.5f, -10f);
        SceneManager.LoadScene("Game");
        Invoke(nameof(PrepareRooms), 2f);
    }

    public void GameOver()
    {
        player.GetComponent<Movement>().ResetWeapon();
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(camera, SceneManager.GetActiveScene());
        SceneManager.LoadScene("GameOver");
    }
    
    public void PrepareRooms()
    {
        AstarPath.active.Scan();

        Tuple<float, float> min = new Tuple<float, float>(0,0);
        Tuple<float, float> max = new Tuple<float, float>(0,0);
        float minVal = 0;
        float maxVal = 0;
        
        foreach (KeyValuePair<Tuple<float, float>, Tuple<GameObject,int>> room in rooms)
        {
            float val1 = room.Key.Item1 - Mathf.Abs(room.Key.Item2);
            float val2 = room.Key.Item1 + Mathf.Abs(room.Key.Item2);

            if (val1 < minVal)
            {
                minVal = val1;
                min = room.Key;
            }

            if (val2 > maxVal)
            {
                maxVal = val2;
                max = room.Key;
            }
        }

        if (max.Equals(min))
        {
            rooms[min] = new Tuple<GameObject, int>(rooms[min].Item1, 2);
        }
        else
        {
            int rand = 1;
        
            if (Mathf.Abs(minVal) >= maxVal)
            {
                rand = 2;
            }
        
            rooms[min] = new Tuple<GameObject, int>(rooms[min].Item1, rand);
            rooms[max] = new Tuple<GameObject, int>(rooms[max].Item1, 3-rand);   
        }

        foreach (KeyValuePair<Tuple<float, float>, Tuple<GameObject,int>> room in rooms)
        {
            Debug.Log(room.Key + ": " + room.Value.Item1.name + " (" + room.Value.Item2 + ")");
        }
    }

    public void Spawn(Vector3 pos, float x, float y)
    {
        float posX = pos.x + 15 * x;
        float posY = pos.y + 9 * y;
        if (Mathf.Approximately(posX,0.5f) && Mathf.Approximately(posY,-0.5f))
        {
            Debug.Log("Starting Room");
            return;
        }
        GameObject room = rooms[new Tuple<float, float>(posX, posY)].Item1;
        int type = rooms[new Tuple<float, float>(posX, posY)].Item2;

        if (type == 0)
        {
            SpawnEnemy(posX, posY, x, y);
            rooms[new Tuple<float, float>(posX, posY)] = new Tuple<GameObject, int>(room, 3);
        }

        if (type == 1)
        {
            SpawnWeapon(posX, posY, x, y);
        }
        
        if (type == 2)
        {
            Instantiate(portal, new Vector3(posX, posY, 0), Quaternion.identity);
        }
    }

    public void SpawnEnemy(float posX, float posY, float x, float y)
    {
        if (Mathf.Approximately(y, 0))
        {
            RollEnemy(posX, posY, 0, true);
            if (Mathf.Approximately(x, 1))
            {
                RollEnemy(posX, posY, 1, true);
            }
            else
            {
                RollEnemy(posX, posY, -1, true);
            }
        }
        else
        {
            RollEnemy(posX, posY, 0, false);
            if (Mathf.Approximately(y, 1))
            {
                RollEnemy(posX, posY, 1, false);
            }
            else
            {
                RollEnemy(posX, posY, -1, false);
            }
        }
    }

    public void RollEnemy(float posX, float posY, int num, bool vertical)
    {
        float moveX = 0f;
        float moveY = 0f;
        float startX = posX;
        float startY = posY;

        if (vertical)
        {
            startX += num * 5f;
            moveY = 2f;
        }
        else
        {
            startY += num * 2f;
            moveX = 5f;
        }
        
        
        for (int i = 0; i < 3; i++)
        {
            if (Random.value > Random.Range(1, 4)*0.25f)
            {
                int e = Random.Range(0, 3);
                Instantiate(enemies[e], new Vector3(startX + (i-1)*moveX, startY + (i-1)*moveY, 0), new Quaternion());
                enemyCount.AddEnemyNum(1);
            }
        }
    }

    public void SpawnWeapon(float posX, float posY, float x, float y)
    {
        int w = Random.Range(0, 3);
        Instantiate(weapons[w], new Vector3(posX, posY, 0), Quaternion.identity);
    }
}
