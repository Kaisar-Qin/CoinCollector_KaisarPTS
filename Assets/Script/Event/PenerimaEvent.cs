using UnityEngine;

public class PenerimaEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void OnEnable()
    {
        PengirimEvent.OnTekanSpasi += Reaksi;
    }

    void OnDisable()
    {
        PengirimEvent.OnTekanSpasi -= Reaksi;
    }

    void Reaksi()
    {
        Debug.Log("Penerima: aku dengar event spasi!");
    }
}
