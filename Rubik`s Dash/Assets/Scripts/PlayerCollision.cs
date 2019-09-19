using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerCollision : MonoBehaviour{
    public ThirdPersonUserControl movement;
    public ThirdPersonCharacter character;
    public Animator animator;
    public Material green;
    public Rigidbody player;
    public GameObject root;

    private void Start() {
        ToggleRagdoll(false);
    }
    void ToggleRagdoll(bool onOff) {
        Collider[] colliders = root.GetComponentsInChildren<Collider>();
        if (colliders != null) {
            foreach (Collider c in colliders)
                c.enabled = onOff;
        }
        Rigidbody[] rigidbodies = root.GetComponentsInChildren<Rigidbody>();
        if (rigidbodies != null) {
            foreach (Rigidbody rb in rigidbodies)
                rb.detectCollisions = onOff;
        }
        CharacterJoint[] cjs = root.GetComponentsInChildren<CharacterJoint>();
        if (cjs != null) {
            foreach (CharacterJoint cj in cjs)
                cj.enableCollision = onOff;
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Wall") {
            animator.enabled = false;
            ToggleRagdoll(true);
            player.AddForce(Vector3.up*10, ForceMode.Impulse);
            collision.collider.GetComponent<MeshRenderer>().material = green;
            movement.enabled = false;
            character.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
