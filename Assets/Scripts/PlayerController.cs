using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Show in Inspector

    public float speed;
    public int health = 5;
    public Transform[] _teleporters;
    [SerializeField] Text scoreText;

    #endregion


    private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        GetInputs();

        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            score = 0;
            health = 5;
        }
    }

    private void FixedUpdate()
    {
        MoveWithVelocity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.tag == "Trap")
        {
            health--;
            Debug.Log("Health: " + health);
        }

        if (other.tag == "Goal")
        {
            Debug.Log("You win!");
        }

        if (other.tag == "Teleporter" && CanTeleport)
        {
            _lastTeleport = Time.time;

            float distanceA = Vector3.Distance(_transform.position, _teleporters[0].position);
            float distanceB = Vector3.Distance(_transform.position, _teleporters[1].position);

            // If taking teleporter A
            if (distanceA < distanceB && _teleporters[0].GetComponent<Teleporters>()._teleported == false)
            {
                // Teleports player to TeleporterB
                _transform.position = new Vector3(_teleporters[1].position.x, _transform.position.y, _teleporters[1].position.z);
                _teleporters[0].GetComponent<Teleporters>()._teleported = true;
            }

            // If taking teleporter B
            if (distanceA > distanceB && _teleporters[1].GetComponent<Teleporters>()._teleported == false)
            {
                _transform.position = new Vector3(_teleporters[0].position.x, _transform.position.y, _teleporters[0].position.z);
                _teleporters[1].GetComponent<Teleporters>()._teleported = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reseting _teleported values after teleporting while player can't teleport anyways.
        // To teleport again, the player has to TriggerEnter again
        if (other.CompareTag("Teleporter") && !CanTeleport)
        {
            if (_teleporters[0].GetComponent<Teleporters>()._teleported == true)
                _teleporters[0].GetComponent<Teleporters>()._teleported = false;
            if (_teleporters[1].GetComponent<Teleporters>()._teleported == true)
                _teleporters[1].GetComponent<Teleporters>()._teleported = false;
        }
    }

    private bool CanTeleport
    {
        get { return Time.time - _lastTeleport >= _teleportCooldown; }
    }

    #region Private methods

    /// <summary>
    /// Collects inputs from user
    /// </summary>
    private void GetInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movementDirection = new Vector3(horizontal, 0, vertical);
        _movementDirection.Normalize();
    }

    /// <summary>
    /// Rigidbody.velocity = newVelocity
    /// Uses above Vector3 to give velocity to the rigidbody.
    /// The engine applies the movement.
    /// </summary>
    private void MoveWithVelocity()
    {
        Vector3 velocity = _movementDirection * speed;
        _rigidbody.velocity = velocity;
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    #endregion

    #region Private 

    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _movementDirection;
    private int score = 0;
    private float _lastTeleport;
    private float _teleportCooldown = 0.2f;

    #endregion
}
