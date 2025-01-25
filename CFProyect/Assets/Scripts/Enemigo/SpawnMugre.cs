using Unity.VisualScripting;
using UnityEngine;

public class SpawnMugre : MonoBehaviour
{
    public void Crear(GameObject mugre, Transform posicion)
    {
        Vector3 vectorPosicion = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(mugre, vectorPosicion, Quaternion.identity);
    }
}
