Shader "Unlit/stars"
{
Properties
{
_MainTex ("Texture", 2D) = "white" {}
_a ("Speed", Range(0.0, 10.0)) = 0.1

_c ("Size", Range(0, 0.03)) = 0.1
_CloudMul ("Color multiplied", Color) = (1, 1, 1, 0.5)
_Color ("Color Add", Color) = (1, 1, 1, 0.5)
_t ("Trans", Range(0, 1)) = 0.1

}
SubShader
{
Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
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



float _a;
float _b,_c,_t;
float4  _CloudMul, _Color;

struct appdata
{
float4 vertex : POSITION;
float2 uv : TEXCOORD0;
};

struct v2f
{
float2 uv : TEXCOORD0;
float4 vertex : POSITION;
};

float2 u_resolution;
float2 u_mouse;
float u_time;
float _Cutoff;

// 2D Random
float random (in float2 st, float n) {
return frac(sin(dot(st.xy,
float2(12.9898,78.233)))
* 43758.5453123*n);
}

sampler2D _MainTex;
float4 _MainTex_ST;

v2f vert (appdata v)
{
v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);




o.uv = TRANSFORM_TEX(v.uv, _MainTex);
return o;
}

float edge(float v, float center, float edge0, float edge1) {
return 1.0 - smoothstep(edge0, edge1, abs(v - center));
}


fixed4 frag (v2f i) : SV_Target
{
// sample the texture
fixed4 col = fixed4(1,1,1,1);

//Textur position
float2 st= i.uv*_a;

//En tom variabel för färg
float color= float(0);
float colorstar= float(0);

// Tile the space
float2 i_st = floor(st);
float2 f_st = frac(st);

float m_dist = 1.;  // minimun distance
for (int j= -1; j <= 1; j++ ) {
for (int i= -1; i <= 1; i++ ) {
// Neighbor place in the grid
float2 neighbor = float2(float(i),float(j));

// Random position from current + neighbor place in the grid
float2 offset = float2(random(i_st + neighbor,1),random(i_st + neighbor,2));

// Animate the offset
offset = 0.5 + 0.5*sin(6.2831*offset);

// Position of the cell
float2 pos = neighbor + offset - f_st;

// Cell distance
float dist = length(pos);

// Metaball it!
m_dist = min(m_dist, m_dist*dist);
}
}

// Draw cells
colorstar += smoothstep(0, m_dist,sin(random(st.y,1)*_Time.y*2)*_c*sin(_Time.y*0.1));

//

col.xyz=colorstar;
col.a=1;
col.a*=_t;
return col;
}
ENDCG
}
}
}

