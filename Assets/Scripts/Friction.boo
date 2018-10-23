import System.Collections
import UnityEngine

class Friction(MonoBehaviour):
    public friction = 0.4
    private rb as Rigidbody2D

    public def Start():
        rb = GetComponent[of Rigidbody2D]()
        
    public def Update():
        rb.velocity *= 1 - (friction*Time.deltaTime)
        