Shader "Custom/Grayscale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GrayContrast ("Contrast", Range(0.0, 3.0)) = 1.0
        _GrayBrightness ("Brightness", Range(-1.0, 1.0)) = 0.0
        _GrayGamma ("Gamma", Range(0.2, 2.5)) = 1.0
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
            float4 _MainTex_ST;
            float _GrayContrast;
            float _GrayBrightness;
            float _GrayGamma;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));

                gray = (gray - 0.5) * _GrayContrast + 0.5;
                gray += _GrayBrightness;
                gray = saturate(gray);
                gray = pow(gray, _GrayGamma);

                return fixed4(gray, gray, gray, col.a);
            }
            ENDCG
        }
    }
}

