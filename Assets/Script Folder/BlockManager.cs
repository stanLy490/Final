using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance; // 单例实例
    private List<Block> blocks = new List<Block>(); // 存储所有Block对象

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
    }

    public void RegisterBlock(Block block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);
            block.gameObject.SetActive(false); // 默认不激活Block
        }
    }

    public void ActivateBlock(Block block)
    {
        if (instance.blocks.Contains(block))
        {
            // 停用所有Block
            foreach (Block b in instance.blocks)
            {
                b.gameObject.SetActive(false);
                b.play = false;
            }
            // 激活指定的Block
            block.gameObject.SetActive(true);
            block.play = true;
        }
    }
}