using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WIN,
    LOST
}

public class BattleSystemManager : MonoBehaviour
{
    public NPC npc;

    [SerializeField] private Canvas battleCanvas;

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
        battleCanvas.gameObject.SetActive(false);
        StartCoroutine(StartDialogueThenBattle());
    }

    IEnumerator StartDialogueThenBattle()
    {
        npc.StartDialogue();

        yield return new WaitUntil(() => !npc.IsDialogueActive());

        battleCanvas.gameObject.SetActive(true);

        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    IEnumerator BeginBattle()
    {
        Sprite enemySprite = enemyStatus.characterSprite;

        enemy = Instantiate(enemyStatus.characterGameObject, enemyBattlePosition);
        enemy.SetActive(true);
        SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        enemySpriteRenderer.sprite = enemySprite;

        Sprite playerSprite = playerStatus.characterSprite;
        player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerBattlePosition);
        player.SetActive(true);
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = playerSprite;

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
        yield return new WaitForSeconds(1);

        hasClicked = false;
    }

    public void OnAttackButtonPress()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
            UnityEngine.Debug.Log("you attack!");
            StartCoroutine(PlayerAttack());
            hasClicked = true;
        }
    }

    public void OnHealButtonPress()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
                StartCoroutine(UseHealthPotion());
                hasClicked = true;
        }
    }

    IEnumerator UseHealthPotion()
    {

        InventoryItem healthPotion = LevelLoader.instance.levelInventory.Find(i =>
            i.itemTemplate != null &&
            i.itemTemplate.ItemName == "Health Potion");

        if (healthPotion != null && healthPotion.quantity > 0)
        {
            healthPotion.quantity--;

            if (healthPotion.quantity == 0)
            {
                LevelLoader.instance.levelInventory.Remove(healthPotion);
            }
        UnityEngine.Debug.Log("heal");
        playerStatusHUD.SetHP(playerStatus, -50);
        yield return StatusHUD.statusBarCoroutine;

        }

        if (enemyStatus.health <= 0)
        {
            battleState = BattleState.WIN;
            UnityEngine.Debug.Log("You won!");
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(2);

        enemyStatusHUD.SetHP(enemyStatus, 50);
        yield return StatusHUD.statusBarCoroutine;

        if (enemyStatus.health <= 0)
        {
            battleState = BattleState.WIN;
            UnityEngine.Debug.Log("You won!");
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        UnityEngine.Debug.Log("ennemy attack");
        playerStatusHUD.SetHP(playerStatus, 10);
        yield return new WaitForSeconds(2);

        if (playerStatus.health <= 0)
        {
            battleState = BattleState.LOST;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            yield return StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator EndBattle()
    {
        if (battleState == BattleState.WIN)
        {
            yield return new WaitForSeconds(1);
            UnityEngine.Debug.Log("win");
            LevelLoader.instance.LoadLevel("SampleScene");
        }
        else if (battleState == BattleState.LOST)
        {
            yield return new WaitForSeconds(1);
            UnityEngine.Debug.Log("lost");
            LevelLoader.instance.LoadLevel("SampleScene");
        }
    }
}
