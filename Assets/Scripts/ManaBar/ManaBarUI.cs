using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour {

    // Setted in the editor
    [SerializeField] private ManaBar mana;
    [SerializeField] private Transform ManaUI;
    [SerializeField] private Text CurrentAvailableMana;
    [SerializeField] private string ManaBlockCallCode = "ManaBlock";

    private Image[] blocks;
    private float initialAlpha = .1f;

    private void Awake()
    {
        ManaBar.OnManaLoaded += BlockLoaded;
    }

    private void OnDestroy()
    {
        ManaBar.OnManaLoaded -= BlockLoaded;
    }

    private void Start()
    {
        Init();
        PopulateUI();
    }

    private void Init()
    {
        int blockNumber = mana.ManaBlock;
        blocks = new Image[blockNumber];
    }

    private void PopulateUI()
    {
        for (int b = 0; b < blocks.Length; b++)
        {
            Transform block = ObjectFactory.Instance.CreateObjectCode(ManaBlockCallCode).transform;
            block.SetParent(ManaUI, false);

            Image im = block.GetComponent<Image>();
            blocks[b] = im != null ? im : block.gameObject.AddComponent<Image>();
        }

        ResetAllBlocks();
    }

    private void LateUpdate()
    {
        SetBlockColor();
    }

    private void SetBlockColor()
    {
        int block = mana.LoadingBlock();
        float balockAlpha = mana.LoadingBlockPercentage() + initialAlpha;
        SetAlphaOfBlock(block, balockAlpha);
    }

    private void ResetAllBlocks()
    {
        for (int i = 0; i < blocks.Length; i ++)
            SetAlphaOfBlock(i, initialAlpha);
    }

    private void BlockLoaded(int block)
    {
        ResetAllBlocks();
        for (int i = 0; i < block; i ++)
            SetAlphaOfBlock(i, 1f);
    }

    private void SetAlphaOfBlock(int blockIndex, float alpha)
    {
        CurrentAvailableMana.text = blockIndex.ToString();
        if (blockIndex >= blocks.Length)
            return;

        Color blockColor = blocks[blockIndex].color;
        blockColor.a = alpha;
        blocks[blockIndex].color = blockColor;
    }
}
