#import System.Colletions
import UnityEngine

class Teste(MonoBehaviour):

    public def Update():
        transform.Translate(Vector3(2,2,0)*Time.deltaTime)