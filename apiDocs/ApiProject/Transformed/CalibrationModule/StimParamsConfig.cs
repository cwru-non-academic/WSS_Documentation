namespace WSSInterfacing {
using Newtonsoft.Json.Linq;

/// <summary>
/// JSON-backed stimulation-parameters configuration. Inherits common JSON
/// handling from <see cref="DictConfigBase"/> and seeds default per-channel
/// values under the <c>stim.ch</c> hierarchy.
/// </summary>
public sealed class StimParamsConfig : DictConfigBase
{
    public StimParamsConfig(string path)
        : base(path, defaults: new JObject
        {
            ["stim"] = new JObject
            {
                ["ch"] = new JObject
                {
                    ["1"] = new JObject { ["maxPW"]=10, ["minPW"]=0, ["amp"]=3.0, ["IPI"]=10 }
                }
            }
        })
    { }
}

}
