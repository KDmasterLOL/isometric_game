using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            var vector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _player.PlayerDirection = _player.GetDirectionFromVector(vector);
            _player.Move(ref vector);
        }
    }
}

// If player not on ground don't move
