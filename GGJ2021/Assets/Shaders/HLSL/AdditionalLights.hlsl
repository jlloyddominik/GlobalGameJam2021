#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void RevealSingle_float(float3 Position, int index, out float1 Value, out float3 Colour, out float1 InLight)
{
    float1 value = 0;
    float3 colour = 0;
    float inLight = 0;
#ifndef SHADERGRAPH_PREVIEW
    Light light = GetAdditionalLight(index, Position);
    value = (light.distanceAttenuation * light.shadowAttenuation);
    inLight = light.distanceAttenuation * light.shadowAttenuation;
    colour = light.color * (light.distanceAttenuation * light.shadowAttenuation);
#endif
    Value = value;
    Colour = colour;
    InLight = inLight > 0.01 ? 1 : 0;

}

void RevealLight_float(float3 Position, out float1 Value, out float3 Colour, out float1 InLight)
{
    float1 value = 0;
    float3 colour = 0;
    float inLight = 0;
#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, Position);
        value += (light.distanceAttenuation * light.shadowAttenuation);
        inLight += light.distanceAttenuation * light.shadowAttenuation;
        colour += light.color * (light.distanceAttenuation * light.shadowAttenuation);
    }
#endif
    Value = value;
    Colour = colour;
    InLight = inLight > 0.01 ? 1 : 0;

}

// void DirectionalLight_float(float3 WorldPos, out float3 Colour, out float Shadow)
// {
// #if SHADOWS_SCREEN
//     float4 clipPos = TransformWorldToHClip(WorldPos);
//     float4 shadowCoord = ComputeScreenPos(clipPos);
// #else
//     float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
// #endif
//     Light mainLight = GetMainLight(shadowCoord);
//     Colour = mainLight.color * mainLight.shadowAttenuation;
//     Shadow = mainLight.shadowAttenuation;
// }

void MainLight_float(float3 WorldPos, out float3 Colour, out float Shadow)
{
#if SHADERGRAPH_PREVIEW
    Colour = 1;
    Shadow = 1;
#else
#if SHADOWS_SCREEN
    float4 clipPos = TransformWorldToHClip(WorldPos);
    float4 shadowCoord = ComputeScreenPos(clipPos);
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
    Light mainLight = GetMainLight(shadowCoord);
    Colour = mainLight.color;
    Shadow = mainLight.shadowAttenuation;
#endif
}

#endif