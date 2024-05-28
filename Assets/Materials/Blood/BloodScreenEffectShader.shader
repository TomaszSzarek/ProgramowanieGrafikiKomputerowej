Shader "Custom/BloodScreenEffectShader"
{
    Properties
    {
        _RedIntensity("Red Intensity", Range(0, 1)) = 0.0
        _MainTex("Main Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _RedIntensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Dodawanie intensywnoœci czerwonej sk³adowej
                fixed4 redColor = fixed4(1.0, 0.0, 0.0, _RedIntensity);

                return redColor;
            }
            ENDCG
        }
    }
}
