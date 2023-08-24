using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    private TextMeshPro textMesh;
    private SpriteRenderer background;
    private int value;

    private float alpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        getComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getComponents()
    {
        textMesh = GetComponent<TextMeshPro>();
        background = GetComponentInChildren<SpriteRenderer>();
    }

    public void updateValue(int newVal)
    {
        value = newVal;
        updateVisuals();
    }

    public void setAlpha(float a)
    {
        if (alpha != a)
        {
            alpha = a;
            textMesh.color = new Color(1f, 1f, 1f, a);
            Color old = background.color;
            background.color = new Color(old.r, old.g, old.b, a);
        }  

    }

    public void updateVisuals()
    {
        if (textMesh == null || background == null)
        {
            getComponents();
        }

        if (value == 0)
        {
            textMesh.SetText("");
            background.color = new Color(0.7f, 0.7f, 0.7f, alpha);
        } else
        {
            textMesh.SetText(value.ToString());
            Color newCol = new Color();
            switch (value)
            {
                case 2:
                    newCol = new Color(0.8f, 0.8f, 0.4f, alpha);
                    break;
                case 4:
                    newCol = new Color(0.8f, 0.8f, 0.2f, alpha);
                    break;
                case 8:
                    newCol = new Color(0.8f, 0.6f, 0.2f, alpha);
                    break;
                case 16:
                    newCol = new Color(0.8f, 0.4f, 0.2f, alpha);
                    break;
                case 32:
                    newCol = new Color(0.8f, 0.2f, 0.5f, alpha);
                    break;
                case 64:
                    newCol = new Color(0.8f, 0.2f, 0.7f, alpha);
                    break;
                case 128:
                    newCol = new Color(0.6f, 0.2f, 0.8f, alpha);
                    break;
                case 256:
                    newCol = new Color(0.35f, 0.2f, 0.8f, alpha);
                    break;
                case 512:
                    newCol = new Color(0.2f, 0.2f, 0.8f, alpha);
                    break;
                case 1024:
                    newCol = new Color(0.2f, 0.5f, 0.8f, alpha);
                    break;
                case 2048:
                    newCol = new Color(0.2f, 0.7f, 0.8f, alpha);
                    break;
                case 4096:
                    newCol = new Color(0.2f, 0.8f, 0.6f, alpha);
                    break;
                case 8192:
                    newCol = new Color(0.2f, 0.8f, 0.35f, alpha);
                    break;
                default:
                    newCol = new Color(1f, 0.0f, 0.0f, alpha);
                    break;
            }
            background.color = newCol;

        }
    }

    public Color getColor()
    {
        return background.color;
    }
}
