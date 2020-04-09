Shader "Unlit/perlin"
{
Properties
{
_MainTex ("Texture", 2D) = "white" {}
_a ("Speed", Range(0.0, 10.0)) = 0.1
_b ("Warp", Range(0, 20)) = 0.1
_c ("Size", Range(0, 1)) = 0.1
_CloudMul ("Color multiplied", Color) = (1, 1, 1, 0.5)
_Color ("Color Add", Color) = (1, 1, 1, 0.5)

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
float _b,_c;
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

// 2D Noise based on Morgan McGuire @morgan3d
// https://www.shadertoy.com/view/4dS3Wd
float noise (in float2 st, float n) {
float2 i = floor(st);
float2 f = frac(st);

// Four corners in 2D of a tile
float a = random(i, n);
float b = random(i + float2(1.0, 0.0), n);
float c = random(i + float2(0.0, 1.0), n);
float d = random(i + float2(1.0, 1.0), n);

// Smooth Interpolation

// Cubic Hermine Curve.  Same as SmoothStep()
float2 u = f*f*(3.0-2.0*f);
// u = smoothstep(0.,1.,f);

// Mix 4 coorners percentages
return lerp(a, b, u.x) +
(c - a)* u.y * (1.0 - u.x) +
(d - b) * u.x * u.y;
}


#define NUM_OCTAVES 5
float fbm ( in float2 _st, float n) {
float v = 0.0;
float a = 0.5;
float2 shift = float2(100.0,100.0);
// Rotate to reduce axial bias
float2 rota = float2(cos(0.5), sin(0.5));
float2 rotb= float2(-sin(0.5), cos(0.50));

for (int i = 0; i < NUM_OCTAVES; ++i) {
v += a * noise(_st,n);
_st.x = (rota * _st.x * 2.0 + shift);
_st.y = (rotb * _st.y * 3.0 + shift);
a *= 0.5;
}
return v;
}




sampler2D _MainTex;
float4 _MainTex_ST;

v2f vert (appdata v)
{
v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);


o.uv = TRANSFORM_TEX(v.uv, _MainTex)* fixed2(2,2)+ _Time.y*_a  * fixed2(1, -3);
//o.uv += TRANSFORM_TEX(v.uv, _MainTex)* fixed2(2, 3)+ _Time.y*_a  * fixed2(3, 1.0);



//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
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
float2 st= i.uv;

//En tom variabel för färg
float color= float(0);



float2 q = float2(0,0);
q.x = fbm( st + 0.00,1)*_b;
q.y = fbm( st + 1.0,1)*_b;

float2 r = float2(0,0);
r.x = fbm( st + 1.0*q + float2(1.7,9.2)+ 0.15,1)*_b;
r.y = fbm( st + 1.0*q + float2(8.3,2.8)+ 0.126,1)*_b;

float f = fbm(st+r,1);

//Kalla på funktionen fbm som utför fractal brownian motion
//med textur positionen för punkten och ger tillbaka en färg för den punkten
color=fbm(st+r, 1)*0.5;

//Samma men en till
float color2=fbm(st+r, 2)*0.5;

//Ge våran textur färgen
col.xyz= lerp(color2,color, clamp((f*f)*4.0,0.0,1.0));

//Gör satt värdet av col.xyz är mellan o-1
col.xyz=saturate(col.xyz);

//Addera färg
col.xyz+=_Color;
col.xyz*=_CloudMul;

//Skapa ett värde för transparans kannalen
col.a = col.xyz-_c;
//se till att värdet för transparans är mellan 0-1
col.a= saturate(col.a);

return col;
}
ENDCG
}
}
}
