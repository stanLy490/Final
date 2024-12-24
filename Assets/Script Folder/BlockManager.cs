using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance; // 单例实例
    public List<Block> blocks = new List<Block>(); // 存储所有Block对象
    public Block currentActivateBlock; // 当前激活的Block

    private void Awake()
    {
        // 确保只有一个BlockManager实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 防止切换场景时销毁
        }
        else
        {
            Destroy(gameObject);
        }
        currentActivateBlock = null;
    }

    public void RegisterBlock(Block block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);
        }
    }

    public void ActivateBlock(Block block)
    {
        if (instance.blocks.Contains(block))
        {
            currentActivateBlock = block;
        }
    }
}