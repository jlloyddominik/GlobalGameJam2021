#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void RevealLight_float(float3 Position, out float1 Value, out float3 Colour, out float1 InLight)
{
    float1 value = 0;
    float3 colour = 0;
    float inLight;
#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i)
    {
        Light light = GetAdditionalLight(i, Position);
        value += (light.distanceAttenuation * light.shadowAttenuation);
        inLight += light.distanceAttenuation;
        colour += light.color * (light.distanceAttenuation * light.shadowAttenuation);
    }
#endif
    Value = value;
    Colour = colour;
    InLight = inLight > 0 ? 1 : 0;
}
#endif