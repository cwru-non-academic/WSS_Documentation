namespace WSSInterfacing {
using System;

/// <summary>
/// Utility for constructing charge-balanced stimulation waveforms.
/// Given a cathodic shape, it computes a matching anodic recharge segment and
/// exposes helpers to retrieve shapes and total area.
/// </summary>
public class WaveformBuilder
{
    private const int shapeSize = 32;
    private const float maxAmp = 2000.0f;
    private int[] catShape = new int[shapeSize];
    private int[] anShape = new int[shapeSize];
    private Waveform wave;
    private float area = 0;
    /// <summary>
    /// Initializes a builder from a cathodic waveform and computes a recharge segment
    /// with equal area.
    /// </summary>
    /// <param name="catWaveform">Cathodic samples (length 32).</param>
    public WaveformBuilder(int[] catWaveform)
    {
        catShape = catWaveform;
        anShape = anodicWaveMaker(areaCalculation(catWaveform));
        wave = new Waveform(catShape, anShape, area);
    }
    /// <summary>
    /// Initializes a builder from an existing <see cref="Waveform"/>.
    /// </summary>
    /// <param name="wave">Existing waveform instance.</param>
    public WaveformBuilder(Waveform wave)
    {
        this.wave = wave;
        wave.catShape = catShape;
        wave.anShape = anShape;
        wave.area = area;
    }

    /// <summary>
    /// Computes the total area for the cathodic waveform using a rectangle+triangle approximation.
    /// </summary>
    private float areaCalculation(int[] catWaveform)
    {
        int prevY = 0;
        for (int i = 0; i < catWaveform.Length; i++)
        {
            //calculate triangle difference area 
            area += (1.0f / (shapeSize * 2)) * ((catWaveform[i] - prevY) / maxAmp); //base/2 * height
            //calculate square area based on previus y
            area += (1.0f / shapeSize) * (prevY / maxAmp); //base * height
            prevY = catWaveform[i];
        }
        return area;
    }

    /// <summary>
    /// Produces a constant recharge segment sized to match the given area.
    /// </summary>
    private int[] anodicWaveMaker(float area)
    {
        float rechargeHeight = area *maxAmp;
        int[] anodicWaveform = new int[shapeSize];
        Array.Fill(anodicWaveform, (int)rechargeHeight);
        return anodicWaveform;
    }

    /// <summary>
    /// Returns the computed anodic (recharge) shape array.
    /// </summary>
    public int[] getAnodicShapeArray()
    {
        return anShape;
    }

    /// <summary>
    /// Returns the cathodic (primary) shape array.
    /// </summary>
    public int[] getCatShapeArray()
    {
        return catShape;
    }
    /// <summary>
    /// Returns the total area computed for the cathodic waveform.
    /// </summary>
    public float getArea()
    {
        return area;
    }
    /// <summary>
    /// Builds the immutable <see cref="Waveform"/> instance with current shapes and area.
    /// </summary>
    public Waveform getWave()
    {
        return wave;
    }
}


}
