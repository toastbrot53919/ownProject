using UnityEngine;
using UnityEngine.UI;

public class OverlayUiController : MonoBehaviour
{
    private UIManager UIManager;
    [SerializeField] public Text characterNameText;
    [SerializeField] public Slider healthBar;
    [SerializeField] public Slider manaBar;
    [SerializeField] public Text levelText;
    private GameObject player;

    private HealthController HealthController;
    private ManaController ManaController;

    private void updateHealthBar()
    {
        healthBar.value = HealthController.currentHealth;
        healthBar.maxValue = HealthController.maxHealth;
    }
    
    private void updateManaBar()
    {
        manaBar.value = (ManaController.currentMana); 
        manaBar.maxValue = ManaController.maxMana;
    }
    private void updateHealthAndMana(){

        updateHealthBar();
        updateManaBar();
    }
    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>().gameObject;
        HealthController = player.GetComponent<HealthController>();
        ManaController = player.GetComponent<ManaController>();

        
    }
    public void Update()
    {
        updateHealthAndMana();
    }
}