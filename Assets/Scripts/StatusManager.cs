using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelLoader;

public class StatusManager : MonoBehaviour
{
    public CharacterStatus playerStatus;
    public CharacterStatus enemyStatus;
    public bool isAttacked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.playerStatus.health > 0)
        {
            if (other.CompareTag("Enemy"))
            {
                if (!isAttacked)
                {
                    isAttacked = true;
                    Debug.Log("Collisions");
                    //setBattleData(other);
                    Debug.Log("Data Initialized");
                    LevelLoader.instance.LoadLevel("BattleArena");
                }
            }
        }
    }

    private void setBattleData(Collider2D other)
    {
        // Player Data
        playerStatus.position[0] = this.transform.position.x;
        playerStatus.position[1] = this.transform.position.y;

        // Vérifiez si le GameObject de collision possède un CharacterController
        CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            // Récupérez le CharacterStatus du personnage
            CharacterStatus status = characterController.characterStatus;

            // Copiez les données de status vers enemyStatus
            enemyStatus.charName = status.charName;
            enemyStatus.characterGameObject = status.characterGameObject.transform.GetChild(0).gameObject;
            enemyStatus.health = status.health;
            enemyStatus.maxHealth = status.maxHealth;
            enemyStatus.mana = status.mana;
            enemyStatus.maxMana = status.maxMana;
        }
    }


}
