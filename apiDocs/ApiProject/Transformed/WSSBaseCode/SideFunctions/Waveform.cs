namespace WSSInterfacing {

/// <summary>
/// Immutable waveform container holding cathodic and anodic shape arrays and
/// the computed total area used for balanced charge calculations.
/// </summary>
[System.Serializable]
public class Waveform
{
    /// <summary>
    /// Cathodic (primary) phase samples.
    /// </summary>
    public int[] catShape;
    /// <summary>
    /// Anodic (recharge) phase samples.
    /// </summary>
    public int[] anShape;
    /// <summary>
    /// Total area under the waveform used for computing recharge amplitude.
    /// </summary>
    public float area;
    /// <summary>
    /// Creates a new <see cref="Waveform"/> from catodic and anodic arrays and area value.
    /// </summary>
    /// <param name="catWaveform">Cathodic shape samples.</param>
    /// <param name="anodicWaveform">Anodic shape samples.</param>
    /// <param name="area">Computed total area.</param>
    public Waveform(int[] catWaveform, int[] anodicWaveform, float area) 
    {
        catShape = catWaveform;
        anShape = anodicWaveform;
        this.area = area;
    }
}

}
