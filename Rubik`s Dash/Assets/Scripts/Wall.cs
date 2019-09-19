using UnityEngine;

public class Wall : MonoBehaviour {
    public Rigidbody[] rb;
    float speed = 1f;
    public float minSpeed = 2f;
    public float maxSpeed = 10f;
    GameObject cameraPosition;
    void Start() {
        speed = Random.Range(minSpeed, maxSpeed);
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update() {
        foreach (Rigidbody el in rb) {
            GameObject obj = el.gameObject;
            if (Vector3.Distance(obj.transform.position, cameraPosition.transform.position) < 15f && cameraPosition.transform.position.z + 7.5f >= obj.transform.position.z) {
                Color c = obj.GetComponent<Renderer>().material.color;
                c.a = 0.05f;
                obj.GetComponent<Renderer>().material.color = c;
            }
        }
    }
    void FixedUpdate() {
        foreach (Rigidbody el in rb) {
            el.MovePosition(el.position + transform.right * Time.deltaTime * speed);
        }
    }
}
