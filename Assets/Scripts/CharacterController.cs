using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public CharacterStatus characterStatus;

    void Start()
    {
       
    }

 

    public void TakeDamage(float damage)
    {
        characterStatus.health -= damage;

        if (characterStatus.health <= 0)
        {
 
        }
    }
}
