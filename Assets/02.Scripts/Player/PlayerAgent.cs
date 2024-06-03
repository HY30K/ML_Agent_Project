using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAgent : Agent
{
    [SerializeField] private float YPos;
    public float strength = 15f;
    public float gravity = -9.8f;

    private Animator animator;
    private Vector3 jumpDir;
    private Renderer renderer;

    private Spawner spawner;


    public Material originMat;
    public Material SuccessMat, FailMat;
    public bool isGround = true;

    public override void Initialize()
    {
        animator = GetComponent<Animator>();
        spawner = transform.parent.Find("AiSpawner").GetComponent<Spawner>();
        renderer = transform.parent.Find("Checker").GetComponent<Renderer>();
    }

    public override void OnEpisodeBegin()
    {
        GameStart();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var Discrete = actions.DiscreteActions;

        if (Discrete[0] == 1)
        {
            if (isGround && !GameManager.Instance.IsAgentDie) // 수정된 부분
            {
                Jump();
                isGround = false;
            }
        }
        AddReward(0.01f);
    }

    private void FixedUpdate()
    {
        Gravity();

        transform.position += jumpDir * Time.deltaTime;

        if (transform.localPosition.y <= YPos + 0.1f)
            isGround = true;
    }

    private void GameStart()
    {
        transform.localPosition = new Vector3(-4, YPos, 0);
        jumpDir = Vector3.zero;

        foreach (GameObject obj in spawner.obstacles)
        {
            Destroy(obj);
        }
        spawner.obstacles = new List<GameObject>();
    }

    private void Gravity()
    {
        // 중력 적용
        if (!isGround) // 수정된 부분
            jumpDir.y += gravity * Time.deltaTime;
        else
        {
            jumpDir.y = 0;
            animator.SetBool("Jump", false);
        }
    }

    private void Jump()
    {
        SoundManager.PlaySound("DinoJump");
        jumpDir = Vector3.up * strength;
        animator.SetBool("Jump", true);
    }

    private void Die()
    {
        animator.SetBool("Die", true);
        GameManager.Instance.IsAgentDie = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-1f);
            StartCoroutine(ChangeColorCoroutine(FailMat));
            //Die();
            GameUIController.Instance.Score = 0;
            GameManager.Instance.Speed = 5;
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            Debug.Log("asdasdasd");
            AddReward(1);
            StartCoroutine(ChangeColorCoroutine(SuccessMat));
            GameUIController.Instance.Score++;
        }
    }


    private IEnumerator ChangeColorCoroutine(Material material)
    {
        renderer.material = material;
        yield return new WaitForSeconds(1f);
        renderer.material = originMat;
    }
}
