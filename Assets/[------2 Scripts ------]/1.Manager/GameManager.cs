using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Search;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        normalMode,
        SelectingHand,
        SelectingBin,
    }

    [field:SerializeField] public GameState currentGameState {get; private set;}
    public UnityEvent<GameState> OnGameStateChange {get; private set;} = new ();
    [field:SerializeField] public SelectionCondition currentSelectionCondition {get; private set;}
    
    public static GameManager Instance { get; private set; }
    #region SubManager Refference
        [Header("Sub Manager")]
        [SerializeField] private CookingManager cookingManager;
        public CookingManager CookingManager => cookingManager;
        [SerializeField] private DeckManager deckManager;
        public DeckManager DeckManager => deckManager;
        [SerializeField] private HandsManager handsManager;
        public HandsManager HandsManager => handsManager;
        [SerializeField] private PointerManager pointerManager;
        public PointerManager PointerManager => pointerManager;
        [SerializeField] private QuestManager questManager;
        public QuestManager QuestManager => questManager;
        [SerializeField] private SceneManager sceneManager;
        public SceneManager SceneManager => sceneManager;
        [SerializeField] private ScoreManager scoreManager;
        public ScoreManager ScoreManager => scoreManager;
        [SerializeField] private TurnManager turnManager;
        public TurnManager TurnManager => turnManager;
        [SerializeField] private CardEffectManager cardEffectManager;
        public CardEffectManager CardEffectManager => cardEffectManager;
        [SerializeField] private InputManager inputManager;
        public InputManager InputManager => inputManager;
        [SerializeField] private ResourceManager resourceManager;
        public ResourceManager ResourceManager => resourceManager;
        [SerializeField] private ServeTableManager serveTableManager;
        public ServeTableManager ServeTableManager => serveTableManager;
        [SerializeField] private TrashBinManager trashBinManager;
        public TrashBinManager TrashBinManager => trashBinManager;
        [SerializeField] private UiManager uiManager;
        public UiManager UiManager => uiManager;
    #endregion

    void Awake()
    {
        Singleton();
        InitManagers();
    }

    void InitManagers()
    {
        PointerManager.Init(this);
        DeckManager.Init(this);
        trashBinManager.Init(this);
        HandsManager.Init(this);
        resourceManager.Init(this);
        TurnManager.Init(this);
        SceneManager.Init(this);
        CardEffectManager.Init(this);
        UiManager.Init(this);
        ScoreManager.Init();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SwitchGameState(GameState targetGameState)
    {
        if(currentGameState == targetGameState)
        {
            return false;
        }

        currentGameState = targetGameState;
        OnGameStateChange.Invoke(currentGameState);

        return true;
    }

    public void SwitchToSelectionState(SelectionCondition selectionCon , GameState targetSelectingState)
    {
        if(targetSelectingState == GameState.normalMode) return;
 
        currentSelectionCondition = selectionCon;
        SwitchGameState(targetSelectingState);
    }
}
