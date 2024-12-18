Shader "Painted Render Texture Shader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0.95)
        _MainTex ("Canvas", 2D) = "black" {}
        _Smoothness ("Feather", Range(0,0.1)) = 0.005
    }

    SubShader
     {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "LightMode"="ForwardBase" }
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting off
        LOD 200
     
        CGPROGRAM
        #pragma surface surf NoLighting noambient alpha:blend

        fixed4 _Color;
        sampler2D _MainTex;
        float _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, float aten)
        {
            fixed4 color;
            color.rgb = s.Albedo;
            color.a = s.Alpha;
            return color;
        }


        void surf (Input IN, inout SurfaceOutput o)
        {

            half4 blurH   = tex2D (_MainTex, IN.uv_MainTex + float2(-_Smoothness,0))*0.25;
            blurH  += tex2D (_MainTex, IN.uv_MainTex                         )*0.5  ;
            blurH  += tex2D (_MainTex, IN.uv_MainTex + float2( _Smoothness,0))*0.25;

            half4 blurV   = tex2D (_MainTex, IN.uv_MainTex + float2(0,-_Smoothness))*0.25;
            blurV  += tex2D (_MainTex, IN.uv_MainTex                        ) *0.5  ;
            blurV  += tex2D (_MainTex, IN.uv_MainTex + float2(0, _Smoothness))*0.25;

            half4 blurred    = (blurH+ blurV)*0.5;

            o.Albedo = _Color.rgba * blurred.a;
            o.Alpha = _Color.a - blurred.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}