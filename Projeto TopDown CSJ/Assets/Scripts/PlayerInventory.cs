using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Currents")]
    [SerializeField] private int currentHp;
    [SerializeField] private float currentWater;
    [SerializeField] private int currentWood;
    [SerializeField] private int currentCarrots;
    [SerializeField] private int currentFishes;

    [Header("Limits")]
    [SerializeField] private int hpMax = 5;
    [SerializeField] private int waterMax = 50;
    [SerializeField] private int carrotsMax = 10;
    [SerializeField] private int woodMax = 5;
    [SerializeField] private int fishesMax = 5;
    

    public float CurrentWater { get => currentWater; set => currentWater = value; }
    public int WaterMax { get => waterMax; set => waterMax = value; }
    public int CurrentWood { get => currentWood; set => currentWood = value; }
    public int WoodMax { get => woodMax; set => woodMax = value; }
    public int CurrentCarrots { get => currentCarrots; set => currentCarrots = value; }
    public int CarrotsMax { get => carrotsMax; set => carrotsMax = value; }
    public int CurrentFishes { get => currentFishes; set => currentFishes = value; }
    public int FishesMax { get => fishesMax; set => fishesMax = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }

    void Start()
    {
        currentHp = hpMax;
    }

    void Update()
    {

    }

    public void WaterLimit(float water)
    {
        if (currentWater < waterMax)
        { 
            currentWater += water;
        }
    }
}
