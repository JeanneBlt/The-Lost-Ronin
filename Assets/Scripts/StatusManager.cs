using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using LevelLoader;

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
            if (other.tag == "Enemy")
            {
                if (!isAttacked)
                {
                    isAttacked = true;
                    setBattleData(other);
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

        // Enemy Data
        CharacterStatus status = other.gameObject.GetComponent<CharacterStatus>();
        enemyStatus.charName = status.charName;
        enemyStatus.characterGameObject = status.characterGameObject.transform.GetChild(0).gameObject;
        enemyStatus.health = status.health;
        enemyStatus.maxHealth = status.maxHealth;
        enemyStatus.mana = status.mana;
        enemyStatus.maxMana = status.maxMana;
    }
}
