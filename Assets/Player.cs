using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float moveSpeed = 5;
    public TextMeshProUGUI hpText;
    public float hp = 10;
    bool inv = false;
    // Start is called before the first frame update
    void Start()
    {
        hpText.text = "10 HP";
    }

    // Update is called once per frame
    void Update() {

        Move();
        hpText.text = hp.ToString() + " HP";
        if(hp <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!inv)
        {
            hp -= 1;
            StartCoroutine(InvincibilityFrames());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "boss")
        {
            if (!inv)
            {
                hp -= 1;
                StartCoroutine(InvincibilityFrames());
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {
        inv = true;
        yield return new WaitForSeconds(1f);
        inv = false;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);
        transform.position = new Vector2(transform.position.x, transform.position.y + deltaY);
        this.transform.Rotate(0f, 0.0f, Input.GetAxis("Horizontal") * -360 * Time.deltaTime, Space.Self);
    }
}
