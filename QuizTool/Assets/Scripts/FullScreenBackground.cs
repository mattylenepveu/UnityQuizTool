using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenBackground : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private float m_fCameraHeight;
    private Vector2 m_v2CameraSize;
    private Vector2 m_v2SpriteSize;
    private Vector2 m_v2Scale;

    // Start is called before the first frame update
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

        m_fCameraHeight = Camera.main.orthographicSize * 2;
        m_v2CameraSize = new Vector2(Camera.main.aspect * m_fCameraHeight, m_fCameraHeight);
        m_v2SpriteSize = m_spriteRenderer.sprite.bounds.size;

        m_v2Scale = transform.localScale;

        // Landscape (or equal)
        if (m_v2CameraSize.x >= m_v2CameraSize.y)
        { 
            m_v2Scale *= m_v2CameraSize.x / m_v2SpriteSize.x;
        }
        // Portrait
        else
        { 
            m_v2Scale *= m_v2CameraSize.y / m_v2SpriteSize.y;
        }

        transform.position = Vector2.zero;
        transform.localScale = m_v2Scale;
    }
}
