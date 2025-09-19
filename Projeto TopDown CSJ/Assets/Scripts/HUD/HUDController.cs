using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private int currentHp;
    private int currentCarrots;
    private int currentWood;
    private float currentWater;
    private int currentFishes;
    private int hpMax;
    private int waterMax;
    private int woodMax;
    private int carrotsMax;
    private int fishesMax;
    private int handlingObj;

    [Header("Items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;
    [SerializeField] private Image hpUIBar;

    [Header("Tools")]
    // [SerializeField] private Image swordUI;
    // [SerializeField] private Image axeUI;
    // [SerializeField] private Image shovelUI;
    // [SerializeField] private Image bucketUI;
    public List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private Player player;
    private PlayerInventory playerInventory;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    void Start()
    {
        waterMax = playerInventory.WaterMax;
        woodMax = playerInventory.WoodMax;
        carrotsMax = playerInventory.CarrotsMax;
        fishesMax = playerInventory.FishesMax;
        hpMax = playerInventory.HpMax;

        waterUIBar.fillAmount = 0;
        woodUIBar.fillAmount = 0;
        carrotUIBar.fillAmount = 0;
    }

    void Update()
    {
        GetCurrentValues();
        UpdateUI();
    }

    void GetCurrentValues()
    {
        currentCarrots = playerInventory.CurrentCarrots;
        currentWood = playerInventory.CurrentWood;
        currentWater = playerInventory.CurrentWater;
        currentFishes = playerInventory.CurrentFishes;
        currentHp = playerInventory.CurrentHp;
        handlingObj = player.HandlingObj;
    }

    void UpdateUI()
    {
        //HP
        hpUIBar.fillAmount = (float) currentHp / hpMax;

        //Items
        waterUIBar.fillAmount = currentWater / waterMax;
        woodUIBar.fillAmount = (float) currentWood / woodMax;
        carrotUIBar.fillAmount = (float) currentCarrots / carrotsMax;
        fishUIBar.fillAmount = (float) currentFishes / fishesMax;

        //Tools

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == handlingObj)
            {
                toolsUI[i].color = selectColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
