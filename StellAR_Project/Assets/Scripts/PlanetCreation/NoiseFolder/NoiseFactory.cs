using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFactory {
    public static NoiseInterface createNoiseFilter(NoiseSettings settings){
        switch (settings.filterType){
            case NoiseSettings.FilterType.Simple:
                return new NoiseFilter(settings);
            case NoiseSettings.FilterType.Rigid:
                return new RidgeNoise(settings);
            case NoiseSettings.FilterType.LandMass:
                return new LandmassNoise(settings);
            case NoiseSettings.FilterType.OceanBed:
                return new OceanBedNoise(settings);
        }
        return null;
    }

}
