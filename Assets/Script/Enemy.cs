using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    
    
    [SerializeField] int KillScore = 20;
    [SerializeField] int hitPoints = 4;

    private GameObject parentGameObject;
    private Scoreboard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<Scoreboard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if(hitPoints <= 0)
        {
            KillEnemy();
        }
        
    }
    private void ProcessHit()
    {
        GameObject fx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        hitPoints -= 1;
        
    }
    private void KillEnemy()
    {
        scoreBoard.ModifyScore(KillScore);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }


}
