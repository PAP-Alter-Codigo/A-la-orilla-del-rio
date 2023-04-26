using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

// public class SlidingPuzzleTile : MonoBehaviour{
//     [SerializeField]
//     SlidingPuzzleBoard board;
//     [SerializeField]
//     float rayDistance;
//     [SerializeField]
//     Flowchart flowchart;
//     Vector2 targetPos;
//     [SerializeField]
//     LayerMask layerMask;

//     private void OnMouseOver() {
//         if(Input.GetMouseButton(0) && !board.isTileMoving) {
//             Vector2[] directions = new Vector2[4] {Vector2.left, Vector2.up, Vector2.right, Vector2.down};
//             Vector2 openDir = Vector2.negativeInfinity;
//             foreach(Vector2 dir in directions) {
//                 Vector2 startPos = new Vector2(transform.position.x + dir.x * transform.localScale.x / 1.5f, transform.position.y + dir.y * transform.localScale.y / 1.5f);
//                 if(!Physics2D.Raycast(startPos, dir, rayDistance, layerMask)) {
//                     Debug.DrawRay(startPos, dir, Color.cyan, rayDistance);
//                     openDir = dir;
//                     break;
//                 }else {
//                     Debug.DrawRay(startPos, dir, Color.green, rayDistance);
//                 }
//             }
//             if(openDir.Equals(Vector2.negativeInfinity)) {
//                 if(!flowchart.FindBlock("TileShakeBlock").IsExecuting() || !flowchart.GetVariable<GameObjectVariable>("currTile").Value.Equals(gameObject)) {
//                     flowchart.GetVariable<GameObjectVariable>("currTile").Value = gameObject;
//                     flowchart.ExecuteBlock("TileShakeBlock");
//                 }
//                 return;
//             }
//             board.isTileMoving = true;
//             flowchart.GetVariable<GameObjectVariable>("currTile").Value = gameObject;
//             flowchart.GetVariable<Vector3Variable>("tileDest").Value = new Vector3(transform.localPosition.x + openDir.x * board.spacing, transform.localPosition.y + openDir.y * board.spacing);
//             flowchart.ExecuteBlock("TileMoveBlock");
//         }
//     }

//     public void Win() => flowchart.ExecuteBlock("PuzzleWonBlock");

//     public Vector2 GetTargetPos() => targetPos;
//     public void SetTargetPos(Vector2 targetPos) => this.targetPos = targetPos;

//     public bool IsOnTargetPos() {
//         return transform.localPosition.Equals(targetPos);
//     }
// }


/// <summary>
/// Este script de Unity es para cada pieza (tile) en el juego de rompecabezas deslizante.
/// Define el comportamiento de las piezas y la lógica de movimiento en el tablero.
/// </summary>
public class SlidingPuzzleTile : MonoBehaviour
{
    [SerializeField]
    private SlidingPuzzleBoard board;

    [SerializeField]
    private float rayDistance;

    [SerializeField]
    private Flowchart flowchart;

    private Vector2 targetPos;

    [SerializeField]
    private LayerMask layerMask;

    /// <summary>
    /// Evento que se llama cuando el mouse se encuentra sobre el objeto.
    /// </summary>
    private void OnMouseOver()
    {
        // ...
    }

    /// <summary>
    /// Ejecuta el bloque "PuzzleWonBlock" en el flowchart de Fungus cuando se gana el rompecabezas.
    /// </summary>
    public void Win() => flowchart.ExecuteBlock("PuzzleWonBlock");

    /// <summary>
    /// Obtiene la posición objetivo de la pieza.
    /// </summary>
    /// <returns>La posición objetivo como Vector2.</returns>
    public Vector2 GetTargetPos() => targetPos;

    /// <summary>
    /// Establece la posición objetivo de la pieza.
    /// </summary>
    /// <param name="targetPos">La posición objetivo como Vector2.</param>
    public void SetTargetPos(Vector2 targetPos) => this.targetPos = targetPos;

    /// <summary>
    /// Verifica si la pieza está en su posición objetivo.
    /// </summary>
    /// <returns>True si la pieza está en su posición objetivo, de lo contrario, False.</returns>
    public bool IsOnTargetPos()
    {
        return transform.localPosition.Equals(targetPos);
    }
}
