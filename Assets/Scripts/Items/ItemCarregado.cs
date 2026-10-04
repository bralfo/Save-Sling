using UnityEngine;

public class ItemCarregado : MonoBehaviour
{
    private Transform ponto;

    public void Seguir(Transform ponto)
    {
        this.ponto = ponto;
    }

    public void PararDeSeguir()
    {
        ponto = null;
    }

    void LateUpdate()
    {
        if (ponto != null)
        {
            transform.position = ponto.position;
            transform.rotation = ponto.rotation;
        }
    }
}