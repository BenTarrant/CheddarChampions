using UnityEngine;
using System.Collections;

public class EatCheese : MonoBehaviour
{
    bool _isPlayerWithinZone = false;
    public ButtonPress buttonPressed;
    private GameObject Cheese;
    private Vector3 size;
    private float cheeseScale = 1.0f;

    private void Start()
    {
        Cheese = this.gameObject;
        size = Cheese.transform.localScale;
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // if the player triggers
            _isPlayerWithinZone = true; // set boolean to true
        print("playerinZone");
    }

    void OnTriggerExit(Collider other) // when trigger leaves collision
    {
        if (other.tag == "Player") // ensure it's the player leaving
            _isPlayerWithinZone = false; // set boolean to false
        print("NOTinZone");
    }

    IEnumerator watchForKeyPress() // button press check
    {
        while (_isPlayerWithinZone) // while the boolean is true
        {
            print("Eating"); // eat METHOD to go here
            cheeseScale += 0.001f;
            Cheese.transform.localScale += new Vector3(size.x * cheeseScale, size.y * cheeseScale, size.z * cheeseScale);
        }

        yield return null; // else return nothing
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = Cheese.transform.localScale;
        Vector3 destinationScale = new Vector3(0.5f, 0.5f, 0.5f);

        float currentTime = 0.0f;

        do
        {
            Cheese.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        Destroy(gameObject);
    }
}
