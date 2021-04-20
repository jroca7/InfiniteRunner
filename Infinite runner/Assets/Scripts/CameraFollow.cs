using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform jugador;
    Vector3 offstet;
    private void Start()
    {
        offstet = transform.position - jugador.position;
    }

    private void Update()
    {
        Vector3 targetPos = jugador.position + offstet;
        targetPos.x = 0;
        transform.position = targetPos;
    }
}
