using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpdater : MonoBehaviour
{
    private TextMeshProUGUI tMesh;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        tMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tMesh.text = "Health  = " + player.Health;
    }
}
