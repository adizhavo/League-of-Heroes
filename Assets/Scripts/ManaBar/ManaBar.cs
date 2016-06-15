using UnityEngine;
using System.Collections;

public class ManaBar : MonoBehaviour 
{
    [SerializeField] private float GenerationSpeed = 0.5f;
    [SerializeField] private int manaBlocks = 10;
    public int ManaBlock { get { return manaBlocks; } }

    private int currentBlock = 0;
    private float currentMana = 0f;

    public delegate void BlockLoaded(int block);
    public static event BlockLoaded OnManaLoaded;

    private static ManaBar instance;
    public static ManaBar Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentBlock = LoadingBlock();
    }

    private void Update()
    {
        ReloadMana();
        CheckLoadedBlock();
    }

    private void ReloadMana()
    {
        currentMana += GenerationSpeed * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0, manaBlocks);
    }

    private void CheckLoadedBlock()
    {
        if (currentBlock != LoadingBlock())
        {
            currentBlock = LoadingBlock();
            if (OnManaLoaded != null)
                OnManaLoaded(currentBlock);
        }
    }

    public int LoadingBlock()
    {
        return (int)currentMana;
    }

    public float LoadingBlockPercentage()
    {
        return currentMana - LoadingBlock();
    }

    public void ConsumeMana(int manaUnits)
    {
        if (LoadingBlock() - manaUnits < 0)
            return;
        
        currentMana -= manaUnits;
    }

    public void Reset()
    {
        currentMana = 0f;
    }
}