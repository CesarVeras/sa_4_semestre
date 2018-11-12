using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public static bool isMoving;

    private float HEIGHT;
    private float WIDTH;

    public float transitionTime = 1f;

    public GameObject objectToFollow;
    public GameObject barriers;
    public SceneControl sceneControl;

    public Vector2[] sides = new Vector2[] { Vector2.left, Vector2.right, Vector2.down, Vector2.up };

    void Start()
    {
        sceneControl = FindObjectOfType<SceneControl>();
        barriers = GameObject.Find("Barriers");
        HEIGHT = Camera.main.orthographicSize;
        WIDTH = HEIGHT * Camera.main.aspect;

        for (int i = 0; i < 4; i++)
        {
            Transform side = barriers.transform.GetChild(i);
            side.localPosition = sides[i] * (i < 2 ? WIDTH : HEIGHT);

            BoxCollider2D c = side.GetComponent<BoxCollider2D>();
            if (i < 2)
            {
                Vector2 v = c.size;
                v.y = 2 * HEIGHT;
                c.size = v;
            }
            else
            {
                Vector2 v = c.size;
                v.x = 2 * WIDTH;
                c.size = v;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        if (objectToFollow.transform.position.x > transform.position.x + WIDTH)
            newPosition.x += WIDTH * 2;
        else if (objectToFollow.transform.position.x < transform.position.x - WIDTH)
            newPosition.x -= WIDTH * 2;
        else if (objectToFollow.transform.position.y > transform.position.y + HEIGHT)
            newPosition.y += HEIGHT * 2;
        else if (objectToFollow.transform.position.y < transform.position.y - HEIGHT)
            newPosition.y -= HEIGHT * 2;
        //transform.position = newPosition;
        if (IsDifferent(newPosition, transform.position))
        {
            //print("RUN");
            StartCoroutine(GoTo(newPosition));
        }
        if (sceneControl.enemyCount <= 0)
        {
            barriers.SetActive(false);
        }
    }

    bool IsDifferent(Vector3 a, Vector3 b, float c = 0.0001f)
    {
        return Mathf.Abs(a.x - b.x) > c || Mathf.Abs(a.y - b.y) > c || Mathf.Abs(a.z - b.z) > c;
    }

    IEnumerator GoTo(Vector3 position)
    {
        isMoving = true;
        barriers.SetActive(false);
        float startTime = Time.time;

        Vector3 start = transform.position;

        do
        {
            transform.position = Vector3.Lerp(start, position, (Time.time - startTime) / transitionTime);
            yield return null;
        } while (Time.time - startTime <= transitionTime);

        transform.position = position;
        isMoving = false;
        barriers.SetActive(true);
    }

}
