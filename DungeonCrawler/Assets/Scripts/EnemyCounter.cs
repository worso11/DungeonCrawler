using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private int enemyNum;

    public void AddEnemyNum(int n)
    {
        enemyNum += n;
        Debug.Log(enemyNum);

        if (enemyNum < 0)
        {
            enemyNum = 0;
        }
    }
    

    public int GetEnemyNum()
    {
        return enemyNum;
    }
}
