using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float chaseDistance = 8f;
    public float attackDistance = 2f;
    public float moveSpeed = 3f;

    public int damage = 10;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            AttackPlayer(); 
        }
        else if (distance <= chaseDistance)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        transform.position = transform.position + direction.normalized * moveSpeed * Time.deltaTime;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

   void AttackPlayer()
{
    PlayerStats stats = player.GetComponent<PlayerStats>();

    if (stats != null && stats.IsDead())
    {
        return;
    }

    if (Time.time >= lastAttackTime + attackCooldown)
    {
        if (stats != null)
        {
            stats.TakeDamage(damage);
        }

        Debug.Log(gameObject.name + " hit player");

        lastAttackTime = Time.time;
    }

    }
}