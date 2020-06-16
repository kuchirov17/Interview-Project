using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class ProceduralGeneration : MonoBehaviour
{
    private SpriteShapeController controller;
    [SerializeField] private float maxHeigthHills;
    [SerializeField] private float minHeigthHills;
    [SerializeField] private float maxSmoothHill;
    [SerializeField] private float minSmoothHill;
    [SerializeField] private float minGenerationDist;
    [SerializeField] private float minDeleteDist;
    [SerializeField] private float timer;


    public GameObject Player;
    public GameObject Canister;

    public GameObject[] Enviroments;
    public EdgeCollider2D GroundCollider;
    void Start()
    {
        Random.Range(5f, 15f);

        controller = GetComponent<SpriteShapeController>();

        controller.spline.SetTangentMode(controller.spline.GetPointCount()-1, ShapeTangentMode.Continuous);

        controller.spline.SetRightTangent(
             controller.spline.GetPointCount() - 1,
             controller.spline.GetRightTangent(controller.spline.GetPointCount() - 1) + (Random.Range(minSmoothHill, maxSmoothHill) * Vector3.right)
             );

        controller.spline.SetLeftTangent(
            controller.spline.GetPointCount() - 1,
            controller.spline.GetLeftTangent(controller.spline.GetPointCount() - 1) + (Random.Range(minSmoothHill, maxSmoothHill) * Vector3.left)
            );

        GenerateRoad();
        StartCoroutine(CanisterGenerator());
    }


    private void Update()
    {
        if(Vector3.Distance(Player.transform.position, controller.spline.GetPosition(0))>minDeleteDist){
            controller.spline.RemovePointAt(0);
        }

        if (Vector3.Distance(Player.transform.position, controller.spline.GetPosition(controller.spline.GetPointCount() - 1)) < minGenerationDist)
        {
            GenerateRoad();
        }

    }

    public void GenerateRoad()
    {
            controller.spline.InsertPointAt(
                controller.spline.GetPointCount(), 
                controller.spline.GetPosition(controller.spline.GetPointCount() - 1) +
                new Vector3(10f, Random.Range(minHeigthHills, maxHeigthHills), 0));

            controller.spline.SetTangentMode(controller.spline.GetPointCount() - 1, ShapeTangentMode.Continuous);

            controller.spline.SetRightTangent(
                controller.spline.GetPointCount() - 1,
                controller.spline.GetRightTangent(controller.spline.GetPointCount() - 1) + (Random.Range(minSmoothHill, maxSmoothHill) * Vector3.right)
                );

            controller.spline.SetLeftTangent(
                controller.spline.GetPointCount() - 1,
                controller.spline.GetLeftTangent(controller.spline.GetPointCount() - 1) + (Random.Range(minSmoothHill, maxSmoothHill) * Vector3.left)
                );

            Instantiate(
                Enviroments[Random.Range(0, Enviroments.Length - 1)],
                GroundCollider.ClosestPoint(Player.transform.position + new Vector3(Random.Range(40f, 100f), 0)),
                Quaternion.identity
            );
    }

    IEnumerator CanisterGenerator()
    {
        while (true)
        {
            Vector3 pos = GroundCollider.ClosestPoint(controller.spline.GetPosition(controller.spline.GetPointCount() - 1));
            Instantiate(
                Canister,
                new Vector3(pos.x, pos.y, -1),
                Quaternion.identity
            );
            yield return new WaitForSeconds(10f);
        }
    }
    

}
