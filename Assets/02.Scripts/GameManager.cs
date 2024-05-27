using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsAgentDie = false;
    public bool IsPlayerDie = false;
    public bool GameStart = false;

    public float Speed = 5;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsPlayerDie)
            SceneManager.LoadScene("AgentWin");
        else if (IsAgentDie)
            SceneManager.LoadScene("PlayerWin");
    }
}
