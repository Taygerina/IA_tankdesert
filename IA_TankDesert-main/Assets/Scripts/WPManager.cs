using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Criar struct para conter todas as variaveis dos Links (conexões)
public struct Link
{
    public enum direction { UNI, BI }   // Enum para definir direção do link
    public GameObject node1;            // 1° node do link
    public GameObject node2;            // 2° node do link
    public direction dir;               // variavel para pegar a direção link
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    private void Start()
    {
        // Se o número de waypoints for maior que 0
        if (waypoints.Length > 0)
        {
            // Adicionar cada um dos nodes ao Graph
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            // Adicionar cada um dos links ao Graph
            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                // Se o tanque puder se mover nas duas direções neste link adicionar mais um link na direção contrária
                if (l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    private void Update()
    {
        // Cria visualização do Graph
        graph.debugDraw();
    }
}
