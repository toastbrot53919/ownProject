// UIManager.cs
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject characterIncreaseStatusUI;
    public GameObject characterUi;
    public GameObject tooltip;
    public GameObject skillTreeMenu;
    public GameObject SpellBookMenu;
    public GameObject InevntoryMenu;

    public GameObject questBookUi; 
    public GameObject questListQuickUi;
    private ToolTipUiController toolTipController;
    public PresentQuestUiController questUIPresenter;
    public event eventUi onPlayerHealthManaChange;
    public delegate void eventUi();

public void updateQuestBook(){
    questBookUi.GetComponent<QuestBookUIController>().UpdateQuestList();
}
public void OpenQuestBookMenu(){
    GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
    questBookUi.SetActive(true);
}
public void CloseQuestBookMenu(){
    GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
    questBookUi.SetActive(false);
}
public void showQuestListQuickUi(){
    GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
    questListQuickUi.SetActive(true);
}
public void hideQuestListQuickUi(){
    GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
    questListQuickUi.SetActive(false);
}

public void showQuestUiPresenter(Quest quest){
    GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
    questUIPresenter.showQuestInfo(quest,this);
    questUIPresenter.gameObject.SetActive(true);
}
public void hideQuestUiPresenter(){
    GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
    questUIPresenter.gameObject.SetActive(false);
}

    private void Update()
    {
        // Check for user input to pause/unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                PauseGame();
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Paused)
            {
                UnpauseGame();
            }
        }

        // Check for user input to open/close the CharacterStatusUI
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                OpenCharacterUi();
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.InMenu)
            {
                CloseCharacterUi();
            }
        }
        if(Input.GetKeyDown(KeyCode.V)){
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                OpenSkillTreeMenu();
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.InMenu)
            {
                CloseSkillTreeMenu();
            }
        }
        if(Input.GetKeyDown(KeyCode.B)){
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                OpenSkillTreeMenu();
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.InMenu)
            {
                CloseSkillTreeMenu();
            }
        }
    }
    public void Awake()
    {
        toolTipController = tooltip.GetComponent<ToolTipUiController>();
    }

    public void PauseGame()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Paused);
        Time.timeScale = 0f;
//        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        Time.timeScale = 1f;
       // pauseMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        mainMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        mainMenu.SetActive(false);
    }
    public void OpenCharacterStatusUI()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        characterIncreaseStatusUI.SetActive(true);
    }
    public void OpenCharacterUi()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        characterUi.SetActive(true);
    }
    public void CloseCharacterUi()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        characterUi.SetActive(false);
    }
    public void OpenSkillTreeMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        skillTreeMenu.SetActive(true);
    }
    public void CloseSkillTreeMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        CloseToolTip();
        skillTreeMenu.SetActive(false);
    }
    public void CloseCharacterStatusUI()
    {
        characterIncreaseStatusUI.SetActive(false);
    }
    public void OpenSpellBookMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        SpellBookMenu.SetActive(true);
    }
    public void CloseSpellBookMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        CloseToolTip();
        SpellBookMenu.SetActive(false);
    }
    public void OpenInventoryMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.InMenu);
        InevntoryMenu.SetActive(true);
    }
    public void CloseInventoryMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        CloseToolTip();
        InevntoryMenu.SetActive(false);
    }
    public void toggleInventoryMenu()
    {
        if(InevntoryMenu.activeSelf){
            CloseInventoryMenu();
        }
        else{
            OpenInventoryMenu();
        }
    }
    public void toggleSpellBookMenu()
    {
        if(SpellBookMenu.activeSelf){
            CloseSpellBookMenu();
        }
        else{
            OpenSpellBookMenu();
        }
    }
    public void toggleCharacterMenu()
    {
        if(characterUi.activeSelf){
            CloseCharacterUi();
        }
        else{
            OpenCharacterUi();
        }
    }
    public void toggleSkillTreeMenu()
    {
        if(skillTreeMenu.activeSelf){
            CloseSkillTreeMenu();
        }
        else{
            OpenSkillTreeMenu();
        }
    }
    public void toggleQuestBookMenu()
    {
        if(questBookUi.activeSelf){
            OpenQuestBookMenu();
        }
        else{
            CloseQuestBookMenu();
        }
    }
    public void OpenToolTip(SkillNode node, Vector3 Positon)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.gameObject.GetComponent<RectTransform>().position = Positon;
        toolTipController.UpdateUI(node);
    }
        public void OpenToolTip(Hotkey hotkey, Vector3 Positon)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.gameObject.GetComponent<RectTransform>().position = Positon;
        if(hotkey.ability != null){
            toolTipController.UpdateUI(hotkey.ability);
        }
        else if(hotkey.item != null){
            toolTipController.UpdateUI(hotkey.item);
        }
    }
    public void OpenToolTip(Ability ability, Vector3 Positon)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.gameObject.GetComponent<RectTransform>().position = Positon;
        toolTipController.UpdateUI(ability);
    }
    public void OpenToolTip(Item item, Vector3 Positon)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.gameObject.GetComponent<RectTransform>().position = Positon;
        toolTipController.UpdateUI(item);
    }
    public void CloseToolTip()
    {
        tooltip.gameObject.SetActive(false);
    }
    public void SaveGameButton()
    {
        GameManager.Instance.SaveGame();
    }
    public void LoadGameButton()
    {
        GameManager.Instance.LoadGame();
    }
}
