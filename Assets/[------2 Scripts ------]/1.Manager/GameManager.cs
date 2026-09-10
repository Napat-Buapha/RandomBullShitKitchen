using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region SubManager Refference
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
        HandsManager.Init(this);
        resourceManager.Init(this);
        TurnManager.Init(this);
        SceneManager.Init(this);
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
}
