Shader "Custom/Blood"
{
Properties
{
_Color ("Color", Color) = (1,1,1,1)
_MainTex ("Albedo (RGB)", 2D) = "white" {}
_Glossiness ("Smoothness", Range(0,1)) = 0.5
_Metallic ("Metallic", Range(0,1)) = 0.0
}
SubShader
{
Tags { "RenderType"="Opaque" }
LOD 200
    CGPROGRAM
    #pragma surface surf Standard fullforwardshadows
    #pragma target 3.0

    sampler2D _MainTex;

    struct Input
    {
        float2 uv_MainTex;
    };

    half _Glossiness;
    half _Metallic;
    fixed4 _Color;
    float2 _ClickPosition;
    float _Radius;

    UNITY_INSTANCING_BUFFER_START(Props)
    UNITY_INSTANCING_BUFFER_END(Props)

    void surf (Input IN, inout SurfaceOutputStandard o)
    {
        float2 center = _ClickPosition; 
        float2 distanceToCenter = abs(IN.uv_MainTex - center);
        float2 halfSize = float2(_Radius, _Radius) * 0.5;

        if (all(distanceToCenter <= halfSize))
        {
            o.Albedo = fixed3(1, 0, 0); 
        }
        else
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            if (!(c.r == 1.0 && c.g == 0.0 && c.b == 0.0))
            {
                o.Albedo = c.rgb;
            }
        }

        o.Metallic = _Metallic;
        o.Smoothness = _Glossiness;
        o.Alpha = 1; 
    }
    ENDCG
}
FallBack "Diffuse"
}