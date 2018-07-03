using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float attackRadius = 5f;

    float currentHealthPoints = 100f;
    AICharacterControl aiCharacterControl = null;
    GameObject target = null;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public void Start()
    {
        aiCharacterControl = GetComponent<AICharacterControl>();
        target = GameObject.FindGameObjectWithTag("Player");
        //target = aiCharacterControl.target;
    }

    public void Update()
    {

        float distanceToPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius)
        {
            aiCharacterControl.SetTarget(target.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }
}
