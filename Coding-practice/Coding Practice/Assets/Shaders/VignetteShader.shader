Shader "Hidden/VignetteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BorderColor ("Border Color", Color) = (1, 0, 0, 1)
        _VignetteStrength ("Vignette Strength", Range(0, 1)) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _BorderColor;
            float _VignetteStrength;

            fixed4 frag (v2f i) : SV_Target
            {
                float vignetteCoefficient = _VignetteStrength + cos(8*_Time.y)/16;
                fixed4 col = tex2D(_MainTex, i.uv);
                float t = length(i.uv - float2(0.5, 0.5)) * 1.41421356237;
                return lerp(col, _BorderColor, t + (vignetteCoefficient - 0.5) * 2);
            }
            ENDCG
        }
    }
}