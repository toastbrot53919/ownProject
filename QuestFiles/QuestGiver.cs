using UnityEngine;

public class QuestGiver : MonoBehaviour, IInteractable
{
    [SerializeField] private Quest quest;
 
    [SerializeField] private GameObject interactionIndicator;

    private bool playerInRange = false;
    private QuestSystem playerQuestSystem;
    private GameObject Interacts;
    private UIManager uiManager;
    public void Start(){
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact(Interacts.transform);
        }
    }
    public void Interact(Transform interactFrom)
    {
        if(uiManager.questUIPresenter.gameObject.activeInHierarchy ){
            uiManager.hideQuestUiPresenter();
        }
        else{
            uiManager.showQuestUiPresenter(quest);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Interacts = other.gameObject;
            interactionIndicator.SetActive(true);
            playerQuestSystem = other.GetComponent<QuestSystem>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIndicator.SetActive(false);
           
        }
    }


}
