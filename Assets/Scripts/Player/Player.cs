using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] _playerRotations;
    private Rigidbody2D rgBody;
    private SpriteRenderer _spriteRenderer;
    private Direction _currentDirection = Direction.Towards;
    private int _indexDirection = 0;
    private bool _isFlip = false;
    private int _countDirections = 8;
    private float _degreeStep, _halfStep;
    [SerializeField] private float _velocity = 1;

    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _degreeStep = 360f / _countDirections;
        _halfStep = _degreeStep / 2;
    }
    public void Move(ref Vector2 moveVector)
    {
        moveVector = Vector2.ClampMagnitude(moveVector, 1);
        if (moveVector.magnitude > .1f)
        {
            SetDirectionFromVector(moveVector);
            ChangeDirection();
        }
        moveVector *= _velocity;
        rgBody.MovePosition(moveVector + rgBody.position);
    }
    private void ChangeDirection()
    {
        _spriteRenderer.flipX = _isFlip;
        _spriteRenderer.sprite = _playerRotations[_indexDirection];
    }

    public void SetDirectionFromVector(in Vector2 vec, int countDirections = 8)
    {

        var angle = Vector2.SignedAngle(Vector2.up, vec) + _halfStep;
        _isFlip = angle <= 0 ? false : true;
        var indexDir = Mathf.Abs(Mathf.FloorToInt(angle / _degreeStep));
        _indexDirection = indexDir;
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
        => Physics2D.OverlapPoint(vector, LayerMask.GetMask(Tags.Ground)) != null;
}