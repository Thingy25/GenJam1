Shader "Custom/GrayscaleInvert"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Contrast ("Contrast", Range(0.0, 3.0)) = 1.0
        _Brightness ("Brightness", Range(-1.0, 1.0)) = 0.0
        _Invert ("Invert", Range(0.0, 1.0)) = 0.0
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
            float _Contrast;
            float _Brightness;
            float _Invert;

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

                // Aplicar contraste alrededor de gris medio (0.5)
                gray = (gray - 0.5) * _Contrast + 0.5;

                // Aplicar brillo
                gray += _Brightness;

                // Saturar para evitar desbordes
                gray = saturate(gray);

                // Inversión controlada (valor continuo, no condicional)
                gray = lerp(gray, 1.0 - gray, _Invert);

                return fixed4(gray, gray, gray, col.a);
            }
            ENDCG
        }
    }
}
