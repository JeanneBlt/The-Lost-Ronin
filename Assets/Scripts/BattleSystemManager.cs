using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemManager : MonoBehaviour
{

    private GameObject enemy;
    private GameObject player;

    public Transform enemyBattlePosition;
    public Transform playerBattlePosition;

    public CharacterStatus playerStatus;
    public CharacterStatus enemyStatus;

    public StatusHUD playerStatusHUD;
    public StatusHUD enemyStatusHUD;

    private BattleState battleState;

    private bool hasClicked = true;

    void Start()
    {
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        // spawn characters on the platforms
        enemy = Instantiate(enemyStatus.characterGameObject, enemyBattlePosition); enemy.SetActive(true);
        player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerBattlePosition); player.SetActive(true);

        // make the characters sprites invisible in the beginning
        enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        // set the characters stats in HUD displays
        playerStatusHUD.SetStatusHUD(playerStatus);
        enemyStatusHUD.SetStatusHUD(enemyStatus);

        yield return new WaitForSeconds(1);

        // fade in our characters sprites
        yield return StartCoroutine(FadeInOpponents());

        yield return new WaitForSeconds(2);

        // player turn!
        battleState = BattleState.PLAYERTURN;

        // let player select his action now!    
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator FadeInOpponents(int steps = 10)
    {
        float totalTransparencyPerStep = 1 / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            setSpriteOpacity(player, totalTransparencyPerStep);
            setSpriteOpacity(enemy, totalTransparencyPerStep);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void setSpriteOpacity(GameObject ob, float transPerStep)
    {
        Color currColor = ob.GetComponent<SpriteRenderer>().color;
        float alpha = currColor.a;
        alpha += transPerStep;
        ob.GetComponent<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, alpha);
    }

    IEnumerator PlayerTurn()
    {
        // probably display some message 
        // stating it's player's turn here
        yield return new WaitForSeconds(1);

        // release the blockade on clicking 
        // so that player can click on 'attack' button    
        hasClicked = false;
    }

    public void OnAttackButtonPress()
    {
        // don't allow player to click on 'attack'
        // button if it's not his turn!
        if (battleState != BattleState.PLAYERTURN)
            return;

        // allow only a single action per turn
        if (!hasClicked)
        {
            StartCoroutine(PlayerAttack());

            // block user from repeatedly 
            // pressing attack button  
            hasClicked = true;
        }
    }

    IEnumerator PlayerAttack()
    {
        // trigger the execution of attack animation
        // in 'BattlePresence' animator
        player.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(2);

        // decrease enemy health by a fixed
        // amount of 10. You probably want to have some
        // more complex logic here.
        enemyStatusHUD.SetHP(enemyStatus, 50);

        if (enemyStatus.health <= 0)
        {
            // if the enemy health drops to 0 
            // we won!
            battleState = BattleState.WIN;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            // if the enemy health is still
            // above 0 when the turn finishes
            // it's enemy's turn!
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        // as before, decrease playerhealth by a fixed
        // amount of 10. You probably want to have some
        // more complex logic here.
        playerStatusHUD.SetHP(playerStatus, 10);

        // play attack animation by triggering
        // it inside the enemy animator
        enemy.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(2);

        if (playerStatus.health <= 0)
        {
            // if the player health drops to 0 
            // we have lost the battle...
            battleState = BattleState.LOST;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            // if the player health is still
            // above 0 when the turn finishes
            // it's our turn again!
            battleState = BattleState.PLAYERTURN;
            yield return StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator EndBattle()
    {
        // check if we won
        if (battleState == BattleState.WIN)
        {
            // you may wish to display some kind
            // of message or play a victory fanfare
            // here
            yield return new WaitForSeconds(1);
            LevelLoader.instance.LoadLevel("TestLevel");
        }
        // otherwise check if we lost
        // You probably want to display some kind of
        // 'Game Over' screen to communicate to the 
        // player that the game is lost
        else if (battleState == BattleState.LOST)
        {
            // you may wish to display some kind
            // of message or play a sad tune here!
            yield return new WaitForSeconds(1);

            LevelLoader.instance.LoadLevel("TestLevel");
        }
    }

}

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST }
