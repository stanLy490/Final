using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方块管理器：管理所有Block和Drag_2D组件的状态
/// </summary>
public class BlockManager : MonoBehaviour
{
    public static BlockManager instance; // 单例实例
    public List<Block> blocks = new List<Block>(); // 存储所有Block对象
    public Block currentActivateBlock; // 当前激活的Block
    public bool isPlaying = false; // 添加播放状态控制
    public List<Drag_2D> dragObjects = new List<Drag_2D>(); // 存储所有Drag_2D对象

    /// <summary>
    /// 初始化单例实例
    /// </summary>
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

    /// <summary>
    /// 注册Block对象
    /// </summary>
    public void RegisterBlock(Block block)
    {
        if (!blocks.Contains(block))
        {
            blocks.Add(block);
        }
    }

    /// <summary>
    /// 注册Drag_2D对象
    /// </summary>
    public void RegisterDragObject(Drag_2D dragObject)
    {
        if (!dragObjects.Contains(dragObject))
        {
            dragObjects.Add(dragObject);
        }
    }

    /// <summary>
    /// 激活指定的Block
    /// </summary>
    public void ActivateBlock(Block block)
    {
        if (instance.blocks.Contains(block))
        {
            currentActivateBlock = block;
        }
    }

    /// <summary>
    /// Play按钮功能：切换播放/暂停状态
    /// 同时控制所有Drag_2D对象的拖拽功能
    /// </summary>
    public void PlayMode()
    {
        isPlaying = !isPlaying;
        
        if (isPlaying)
        {
            // 开始播放
            foreach (Block block in blocks)
            {
                block.StartPlayMode();
            }
            // 禁用所有拖拽功能
            Drag_2D.isGamePlaying = true;
        }
        else
        {
            // 暂停播放
            foreach (Block block in blocks)
            {
                block.PausePlayMode();
            }
            // 保持拖拽禁用状态
        }
    }

    /// <summary>
    /// Reset按钮功能：停止播放并重置所有状态
    /// 同时重置所有Drag_2D对象的拖拽功能
    /// </summary>
    public void StopMode()
    {
        isPlaying = false;
        // 停止所有Block的播放
        foreach (Block block in blocks)
        {
            block.StopPlayMode();
        }
        // 启用所有拖拽功能
        Drag_2D.isGamePlaying = false;
    }
}