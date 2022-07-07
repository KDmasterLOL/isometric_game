using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rgBody;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private PlayerRotation[] _playerRotations;
    private Direction _currentDirection = Direction.Towards;
    public Direction PlayerDirection
    {
        get => _currentDirection;
        set
        {
            if (value == _currentDirection) return;
            print(value);
            ChangeDirection(value);
        }
    }
    public float velocity = 1;
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Move(ref Vector2 moveVector)
    {
        moveVector *= velocity;
        moveVector += rgBody.position;

        if (OnTheGround(moveVector))
            rgBody.MovePosition(moveVector);
    }
    private void ChangeDirection(in Direction dir)
    {
        foreach (var rotation in _playerRotations)
        {
            if (dir == rotation.Direction)
            {
                _spriteRenderer.sprite = rotation.PlayerSprite;
                _spriteRenderer.flipX = rotation.IsFlip;
                _currentDirection = dir;
            }
        }
    }

    public Direction GetDirectionFromVector(in Vector3 vec)
    {
        Direction direction = Direction.None;
        switch (vec.x)
        {
            case 0: break;
            case < 0:
                direction |= Direction.Left;
                break;
            case > 0:
                direction |= Direction.Right;
                break;
        }
        switch (vec.y)
        {
            case 0: break;
            case < 0:
                direction |= Direction.Backwards;
                break;
            case > 0:
                direction |= Direction.Towards;
                break;
        }
        return direction;
    }
    [System.Flags]
    public enum Direction
    {
        None = 0,
        Towards = 1 << 0,
        Backwards = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
    }
    public bool OnTheGround(in Vector2 vector)
    => Physics2D.OverlapCircle(vector, 0.1f, LayerMask.GetMask(Tags.Ground)) != null;
}

[System.Serializable]
public struct PlayerRotation
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private Player.Direction _direction;
    [SerializeField]
    private bool _isFlip;
    public Sprite PlayerSprite => _sprite;
    public Player.Direction Direction => _direction;
    public bool IsFlip => _isFlip;
}