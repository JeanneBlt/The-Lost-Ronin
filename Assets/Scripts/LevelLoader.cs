using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Liste d'items qui sera une copie persistante de l'inventaire
    public List<InventoryItem> levelInventory = new List<InventoryItem>();

    #region Singleton
    public static LevelLoader instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Une instance existe déjà, détruisez celle-ci
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SyncInventoryWithCharacterInfos();
    }

    // Synchroniser l'inventaire de CharacterInfos avec celui de LevelLoader
    public void SyncInventoryWithCharacterInfos()
    {
        CharacterInfos characterInfos = FindObjectOfType<CharacterInfos>();

        if (characterInfos != null)
        {
            // Effacer la liste existante
            levelInventory.Clear();

            // Copier chaque item de l'inventaire de CharacterInfos
            foreach (InventoryItem item in characterInfos.GetInventory())
            {
                InventoryItem copiedItem = new InventoryItem(item.itemTemplate, item.quantity);
                levelInventory.Add(copiedItem);
            }
        }
        else
        {
            Debug.LogWarning("CharacterInfos not found! Unable to sync inventory.");
        }
    }

    // Fonction pour mettre à jour l'inventaire de CharacterInfos depuis LevelLoader
    private void UpdateCharacterInfosFromLevelLoader()
    {
        CharacterInfos characterInfos = FindObjectOfType<CharacterInfos>();

        if (characterInfos != null)
        {
            // Effacer l'inventaire existant
            characterInfos.GetInventory().Clear();

            // Copier les items de LevelLoader vers CharacterInfos
            foreach (InventoryItem item in levelInventory)
            {
                characterInfos.GetInventory().Add(new InventoryItem(item.itemTemplate, item.quantity));
            }
        }
        else
        {
            Debug.LogWarning("CharacterInfos not found! Unable to update CharacterInfos inventory.");
        }
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadNamedLevel(levelName));
    }

    IEnumerator LoadNamedLevel(string levelName)
    {
        transition.SetTrigger("FadeIn");
        // Start transition animation
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);

        // Synchroniser l'inventaire après le chargement de la nouvelle scène
        yield return new WaitForSeconds(transitionTime);  // Attendre un instant pour s'assurer que tout est chargé
        UpdateCharacterInfosFromLevelLoader();  // Mettre à jour l'inventaire de CharacterInfos
    }
}
