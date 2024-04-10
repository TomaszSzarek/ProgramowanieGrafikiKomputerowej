using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthSystem healthSystemPrefab;
    public static HealthSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            //instance = Instantiate(healthSystemPrefab);
            //DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}