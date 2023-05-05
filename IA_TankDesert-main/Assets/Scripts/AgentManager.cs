using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    // Array para pegar os gameobjects que tem Navmesh Agent
    GameObject[] agents;

    private void Start()
    {
        // Pegar todos os Navmesh agents
        agents = GameObject.FindGameObjectsWithTag("ai");
    }

    // Update is called once per frame
    void Update()
    {
        // Ao clicar com o botão esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Criar raycast da camera para o ponto onde foi clicado com o mouse, e verificar se o raycast detectou um hit
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
            {
                // Para cada agente no array, colocar o ponto clicado como o destino
                foreach (GameObject a in agents)
                    a.GetComponent<AIControl>().agent.SetDestination(hit.point);
            }
        }
    }
}
