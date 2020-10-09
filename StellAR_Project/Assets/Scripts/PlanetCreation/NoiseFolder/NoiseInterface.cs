using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  NoiseInterface {
    // Start is called before the first frame update
    float Evaluate(Vector3 point);
}
