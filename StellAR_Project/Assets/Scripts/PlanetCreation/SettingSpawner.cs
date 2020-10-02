using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSpawner {
    // Start is called before the first frame update
    public static ShapeSettings loadDefaultShape(){
        var shapeSettings = Resources.Load<ShapeSettings>("Settings/DefaultShape");
        return shapeSettings;
    }

    public static ColorSettings loadDefaultColor(){
        var colorSettings = Resources.Load<ColorSettings>("Settings/DefaultColor");
        return colorSettings;
    }
}
