using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance; // 单例实例
    public List<Block> blocks = new List<Block>(); // 存储所有Block对象
    public Block currentActivateBlock; // 当前激活的Block
    public bool isPlaying = false; // 添加播放状态控制

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

    public void PlayMode()
    {
        isPlaying = !isPlaying;
        
        if (isPlaying)
        {            // 开始播放
            foreach (Block block in blocks)
            {
                block.StartPlayMode();
            }
        }
        else
        {            // 停止播放
            foreach (Block block in blocks)
            {
                block.PausePlayMode();
            }
        }
    }

    public void StopMode()
    {
        isPlaying = false;
        // 停止所有Block的播放
        foreach (Block block in blocks)
        {
            block.StopPlayMode();
        }
    }
}