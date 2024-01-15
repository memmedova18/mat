using UnityEngine;


class DragAndDrop : MonoBehaviour {
    private bool dragging = false;
    private float distance;
    private Piece this_piece;

    [SerializeField]
    private Board board;

    void Start() {
        this_piece = GetComponent<Piece>(); 
    }

    void Update() {
        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);

            
            transform.position = new Vector3(rayPoint.x - 0.5f, 2.7f, rayPoint.z);
            transform.rotation = new Quaternion(0, 0, 0, 0);

            
            if (board.use_hover) {
                Square closest_square = board.getClosestSquare(transform.position);
                board.hoverClosestSquare(closest_square);
            }
        }
    }

    void OnMouseDown() {
        
        if (board.cur_turn == this_piece.team) {
            GetComponent<Rigidbody>().isKinematic = true;
            
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            if (board.use_hover) {
                board.hoverValidSquares(this_piece);
            }
            dragging = true; 
        }
    }
 
    void OnMouseUp() {
        if (dragging) {
            GetComponent<Rigidbody>().isKinematic = false;
            
            Square closest_square = board.getClosestSquare(transform.position);
            this_piece.movePiece(closest_square);

            if (board.use_hover) board.resetHoveredSquares();
            dragging = false; 
        }
    }
}