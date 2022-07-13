using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField] private BoxCollider floorDetector;
    [SerializeField] private float rotationSpeed = 50f;

    private Rigidbody rb;
    private float lifetime;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void SetLifetime(float time) {
        lifetime = time;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Floor")) {
            StartCoroutine(StartDestroyDelay());
            StartCoroutine(StartRotating());
            Debug.Log("FLOOR!");
            floorDetector.enabled = false;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    private IEnumerator StartDestroyDelay() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        Debug.Log("CoinDestroyed");
    }

    private IEnumerator StartRotating() {
        while (true) {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }
}