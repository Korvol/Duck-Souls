using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform Player;
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;
    [SerializeField] public int Sanity;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] private TMP_Text sanityText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        sanityText.text = "Sanity: " + Sanity;  
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MoveCamera();
        PlayerCamera.position = transform.position + new Vector3(0f, 0.5f, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            // launch a projectile
            Instantiate(projectilePrefab, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
            if (Sanity <= 0)
        {
            // kill player
            SceneManager.LoadScene("DeathScene");
        }
        if (transform.position.y != 4.7)
        {
            transform.position = new Vector3(transform.position.x, 4.7f, transform.position.z);
        }
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);
        Controller.Move(MoveVector * Speed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);

    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * RotationSpeed;

        transform.Rotate(0f, PlayerMouseInput.x * RotationSpeed, 0f);
        PlayerCamera.transform.rotation = transform.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Damage":
                collision.gameObject.transform.position = new Vector3(0, -10f, 0);
                Sanity -= 1;
                break;
        }
    }
}