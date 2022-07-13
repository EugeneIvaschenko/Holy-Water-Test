using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float randomRangeAngleRotation = 50;
    [SerializeField] private float angleRotationFromWall = 360;
    [SerializeField] private float changeAngleRotationDelay = 2;

    private Rigidbody rb;
    private Animator animator;
    private float angleRotation;

    public event Action CoinCollected;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeAngleRotation());
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnTriggerEnter(Collider other) {
        Coin coin = other.GetComponentInParent<Coin>();
        if (coin) {
            Destroy(coin.gameObject);
            CoinCollected?.Invoke();
        }
    }

    private void Move() {
        animator.SetFloat("Speed", moveSpeed);
        transform.Rotate(new Vector3(0, Time.fixedDeltaTime * angleRotation, 0));
        if (CheckWall())
            transform.Rotate(new Vector3(0, Time.fixedDeltaTime * angleRotationFromWall * Mathf.Sign(angleRotation), 0));
        rb.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    private bool CheckWall() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, moveSpeed)) {
            return hit.collider.CompareTag("Wall");
        }
        return false;
    }

    private IEnumerator ChangeAngleRotation() {
        while (true) {
            angleRotation = Random.Range(-randomRangeAngleRotation, randomRangeAngleRotation);
            yield return new WaitForSeconds(changeAngleRotationDelay);
        }
    }
}