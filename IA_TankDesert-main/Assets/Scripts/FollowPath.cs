using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Variavel que define a transform do objetivo de movimentação
    Transform goal;

    // Variaveis de movimentação do tanque 
    public float speed = 5.0f;
    public float accuracy = 0.05f;
    public float rotSpeed = 0.2f;

    // Variaveis relacionadas aos waypoints e ao graph que irá definir o movimento do tanque
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        // Peagr o waypoint manager e o Graph e colocar o node atual como o primeiro waypoint do array
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    // Função que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Heli)
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    // Função que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Ruins)
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    // Função que utiliza o algoritmo A* para direcionar o tanque ao waypoint especificado (Factory)
    public void GoToFactory()
    {
        g.AStar(currentNode, wps[8]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        // Se a distância do caminho for 0 OU o waypoint atual for igual ao último ponto do caminho retornar e não executar nenhum código
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) return;

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }

        // Se o waypoint atual for menor que a distância do caminho executar o código de movimentação
        if (currentWP < g.getPathLength())
        {
            // Definir o objetivo como a transform do próximo ponto
            goal = g.getPathPoint(currentWP).transform;

            // Calcular vetor que indica a posição do objetivo
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

            // Calcular a direção que o tanque deve seguir para chegar ao objetivo
            Vector3 direction = lookAtGoal - this.transform.position;

            //  Rotacionar e mover o tanque na direção do objetivo
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }
}
