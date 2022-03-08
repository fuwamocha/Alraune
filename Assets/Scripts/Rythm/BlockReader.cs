using UnityEngine;

/// <summary>
/// ブロックの種類を読み取るクラス
/// </summary>
public class BlockReader : MonoBehaviour
{
    [SerializeField] private PlayerManager player = default;

    public enum Block
    {
        NORMAL,
        TWICE,
        SKIP,
        CROSSUP,
        CROSSDOWN,
        CROSSLEFT,
        CROSSRIGHT,
        NONE
    }
    public Block block { get; private set; }

    private string hitBlock;
    private RaycastHit2D _hit;


    /// <summary>
    /// ブロックの種類を読み取る
    /// </summary>
    public void ReadBlock()
    {
        if (_hit.collider == null)
        {
            return;
        }

        hitBlock = _hit.collider.name;
        if (hitBlock == "Normal")
        {
            block = Block.NORMAL;
        }
        else if (hitBlock == "Twice")
        {
            block = Block.TWICE;
        }
        else if (hitBlock == "Skip")
        {
            block = Block.SKIP;
        }
        else if (hitBlock == "CrossUp")
        {
            block = Block.CROSSUP;
        }
        else if (hitBlock == "CrossDown")
        {
            block = Block.CROSSDOWN;
        }
        else if (hitBlock == "CrossLeft")
        {
            block = Block.CROSSLEFT;
        }
        else if (hitBlock == "CrossRight")
        {
            block = Block.CROSSRIGHT;
        }
        else if (hitBlock == "None")
        {
            block = Block.NONE;
        }
    }

    private void Update()
    {
        _hit = player.hit;
    }
}