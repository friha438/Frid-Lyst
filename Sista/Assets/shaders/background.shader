// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/background"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 0.5)
        _Color1 ("Color2", Color) =(1, 1, 1, 0.5)
_Color2 ("Colorbyta1", Color) = (1, 1, 1, 0.5)
_Color3 ("Colorbyta2", Color) =(1, 1, 1, 0.5)
        _Height ("Height", Float) = 10.0
_pos ("position", Float) = 10.0
        _transparency ("transparency", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
        cull off
zwrite off
Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Color, _Color1;
            float _Height, _transparency, _pos;

            v2f vert (appdata v)
            {
                v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.uv, _MainTex);
o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
float pos=i.worldPos.y+_pos;
                col.xyz=lerp(_Color,_Color1, pos/_Height);
                col.a=_transparency;
                return col;
            }
            ENDCG
        }
    }
}
