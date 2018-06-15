using UnityEngine;
using System.Reflection;

namespace UnityEditor.ShaderGraph
{
    [Title("Math", "Wave", "Noise Wave")]
    class NoiseWaveNode : CodeFunctionNode
    {
        public NoiseWaveNode()
        {
            name = "Noise Wave";
        }

        public override string documentationURL
        {
            get { return "https://github.com/Unity-Technologies/ShaderGraph/wiki/Noise-Wave-Node"; }
        }

        protected override MethodInfo GetFunctionToConvert()
        {
            return GetType().GetMethod("NoiseWave", BindingFlags.Static | BindingFlags.NonPublic);
        }

        static string NoiseWave(
            [Slot(0, Binding.None)] DynamicDimensionVector In,
            [Slot(1, Binding.MeshUV0)] Vector2 Seed,
            [Slot(3, Binding.None, -0.5f, 0.5f, 1, 1)] Vector2 MinMax,
            [Slot(4, Binding.None)] out DynamicDimensionVector Out)
        {
            return
                @"
{
    {precision} randomno =  frac(sin(dot(Seed.x, float2(12.9898, 78.233)))*43758.5453);
    float noise = lerp(MinMax.x, MinMax.y, randomno);
    Out = sin(In) + noise;
}
";
        }
    }
}
