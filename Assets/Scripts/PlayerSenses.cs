using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSenses : MonoBehaviour
{
    public RawImage eyeImage;
    public bool InDanger;
    public float detectionRadius = 10f;
    public string enemyTag = "Enemy";
    public int DangerLevel;

    public GameObject Enemy;
    private EnemyScript foundEnemyScript;
    private Color normalColor;

    public Animation EyeUIShake; 

    void Start()
    {
        EyeUIShake = GetComponent<Animation>();
        DangerLevel = 0; 
        InDanger = false;
        if (eyeImage != null)
        {
            normalColor = eyeImage.color;
            eyeImage.enabled = false;
        }
    }

    void Update()
    {
        DetectEnemies();
        EyeUIShake.Play();
    }

    void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        bool foundEnemy = false;
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag(enemyTag))
            {
                Enemy = hit.gameObject;
                foundEnemyScript = hit.GetComponent<EnemyScript>();
                DangerLevel = foundEnemyScript.DangerLevel; 
                foundEnemy = true;

                if (DangerLevel > 1) 
                {
                    EyeUIShake.Play();
                }

                if (foundEnemyScript.DangerLevel > DangerLevel)
                {
                    DangerLevel = foundEnemyScript.DangerLevel;
                }
                break;
                
            }
        }

        if (foundEnemy)
        {
            Danger();
        }
        else
        {
            InDanger = false;
            if (eyeImage != null)
            {
                eyeImage.enabled = false;
                DangerLevel = 0; 
            }
        }
    }

    public void Danger()
    {
        InDanger = true;
        if (eyeImage != null)
        {
            
            eyeImage.enabled = true;
            eyeImage.color = GetColorFromDangerLevel(DangerLevel);
        }
    }

    Color GetColorFromDangerLevel(int level)
    {
        if (level <= 1) return normalColor;
        if (level >= 3) return Color.red;
        return Color.Lerp(normalColor, Color.red, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
