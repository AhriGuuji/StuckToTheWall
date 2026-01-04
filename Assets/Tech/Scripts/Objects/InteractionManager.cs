using System.Collections.Generic;
using Tech.Scripts.Objects;
using Tech.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager _instance;

    public static InteractionManager instance
    {
        get
        {
            if (_instance == null)
                FindFirstObjectByType<InteractionManager>().Init();

            return _instance;
        }
    }
    [SerializeField] private PlayerInventory    _playerInventory;
    [SerializeField] private PlayerInteraction  _playerInteraction;
    [SerializeField] private Player             _playerMovement;
    [SerializeField] private PlayerMinigame     _playerMinigame;
    [SerializeField] private CinemachineCamera  _playerHead;
    [SerializeField] private string             _interactPrefix;
    [SerializeField] private string             _pickPrefix;
    [SerializeField] private string             _awakeAnimationName;
    [SerializeField] private string             _interactAnimationName;
    
    [Header("InputManagement")]
    [SerializeField] private string moveInputName = "Move";
    [SerializeField] private string lookInputName = "Look";
    [SerializeField] private string interactInputName = "Interact";
    [SerializeField] private string dropInputName = "Drop";
    [SerializeField] private string escapeInputName = "Escape";
    public InputAction         InputLook => InputSystem.actions.FindAction(lookInputName);
    public InputAction         InputMovement => InputSystem.actions.FindAction(moveInputName);
    public InputAction         InputInteract => InputSystem.actions.FindAction(interactInputName);
    public InputAction         InputDrop => InputSystem.actions.FindAction(dropInputName);
    public InputAction         InputEscape => InputSystem.actions.FindAction(escapeInputName);

    private List<Interactive>  _interactives;

    public PlayerInventory      playerInventory         => _playerInventory;
    public PlayerInteraction    playerInteraction       => _playerInteraction;
    public Player               player                  => _playerMovement;
    public PlayerMinigame       playerMinigame          => _playerMinigame;
    public CinemachineCamera               playerHead              => _playerHead;
    public string               awakeAnimationName      => _awakeAnimationName;
    public string               interactAnimationName   => _interactAnimationName;

    //Changed By Me
    private List<Interactive> _completedPuzzles;
    private Vector3 _lastCompletedPuzzle;
    //ConnectionPuzzle
    public InteractiveConnector lastConnector;

    void Awake()
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }
    
    private void Init()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _interactives = new List<Interactive>();
    }

    public void RegisterInteractive(Interactive interactive)
    {
        _interactives?.Add(interactive);
    }

    void Start()
    {
        ProcessDependencies();
        _interactives = null;
    }

    private void ProcessDependencies()
    {
        foreach (Interactive interactive in _interactives)
        {
            foreach (InteractiveData requirementData in interactive?.interactiveData?.requirements)
            {
                Interactive requirement = FindInteractive(requirementData);
                interactive?.AddRequirement(requirement);
                requirement?.AddDependent(interactive);
            }
        }
    }
    
    public Interactive FindInteractive(InteractiveData interactiveData)
    {
        foreach (Interactive interactive in _interactives)
            if (interactive.interactiveData == interactiveData)
                return interactive;

        return null;
    }

    public string GetPickMessage(string objectName)
    {
        return _interactPrefix + " " + _pickPrefix + " " + objectName;
    }

    public string GetInteractionMessage(string message)
    {
        return _interactPrefix + " " + message;
    }
}
