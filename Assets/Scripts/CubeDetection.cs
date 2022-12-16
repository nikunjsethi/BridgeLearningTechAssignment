using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeDetection : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Sphere(Clone)")
        {
            _player.score = _player.score - (2 * _player.sphereScore);
            _player.scoreText.text= "Score : " + _player.score.ToString();
        }

        if (collision.gameObject.name=="Capsule(Clone)")
        {
            _player.score = _player.score - (2 * _player.capsuleScore);
            _player.scoreText.text = "Score : " + _player.score.ToString();
        }
    }
}
