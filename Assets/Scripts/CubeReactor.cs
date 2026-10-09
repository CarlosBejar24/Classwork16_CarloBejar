using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeReactor : MonoBehaviour
{
    [SerializeField] private ComboSystem comboSystem;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float spin = 4f;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        comboSystem.ComboPerformed += OnComboPerformed;
    }

    private void OnDisable()
    {
        comboSystem.ComboPerformed -= OnComboPerformed;
    }

    private void OnComboPerformed(ComboData combo)
    {
        Vector3 impulse = Vector3.up * jumpForce + combo.Direction * moveForce;
        body.AddForce(impulse, ForceMode.VelocityChange);
        body.AddTorque(-combo.Direction.z * spin, 0f, -combo.Direction.x * spin, ForceMode.VelocityChange);
    }
}