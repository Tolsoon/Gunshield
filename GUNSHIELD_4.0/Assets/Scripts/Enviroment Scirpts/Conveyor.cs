using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float scrollSpeed = -0.5F;
    public Renderer rend;

    public Texture texture;

    public string direction;

    public float speed;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTexture = texture;
        rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0));

        
    }
   
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            
            switch (direction)
            {
                case "up":
                    {
                        
                        collision.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
                        break;
                    }
                case "down":
                    {
                        collision.gameObject.transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);
                        break;
                    }
                case "left":
                    {
                        collision.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                        break;
                    }
                case "right":
                    {
                        collision.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                        break;
                    }
            }
       
            
        }
    }
}
