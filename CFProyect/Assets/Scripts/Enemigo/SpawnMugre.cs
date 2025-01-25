using UnityEngine;

public class SpawnMugre : MonoBehaviour
{
    void CrearMugre(GameObject mugre, Vector3 posicion)
    {
        Instantiate(mugre, posicion, Quaternion.identity);
    }
}
