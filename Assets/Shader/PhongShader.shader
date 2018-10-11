// Original Cg/HLSL code stub copyright (c) 2010-2012 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
// Adapted further by Chris Ewin, 23 Sep 2013
// Adapted further (again) by Alex Zable (port to Unity), 19 Aug 2016

//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/PhongShader"
{
	Properties
	{	
		_MaterialColor("Material Color", Color) = (0,134,255,255)
		_PointLightColor("Point Light Color", Color) = (0, 0, 0)
		_PointLightPosition("Point Light Position", Vector) = (0.0, 0.0, 0.0)

		_specN("Specular highlight", Range(0,50)) = 5
		_Ks("Specular constant",Range(0,1)) = 0.08
		_Ka("RGB intensity", Range(0,1)) = 1
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float3 _PointLightColor;
			uniform float3 _PointLightPosition;
			float4 _MaterialColor;
			float _specN;
			float _Ks;
			float _Ka;

			struct vertOut {
				float3 worldPos : TEXCOORD0;
				half3 worldNormal : TEXCOORD1;
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			// Implementation of the vertex shader
			vertOut vert(float4 vertex : POSITION, float3 normal : NORMAL, fixed4 color : COLOR, float3 worldPos : TEXCOORD0)
			{
				vertOut o;
				o.pos = UnityObjectToClipPos(vertex);
				o.worldPos = mul(unity_ObjectToWorld, vertex).xyz;
				o.worldNormal = UnityObjectToWorldNormal(normal);
				o.color = color;

				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut i) : SV_Target
			{	

			i.color = _MaterialColor;
			// add in phong shading, adapted from lab5,  so it applies phong illumination at fragment shader rather than vertex shader
			float4 phongColor = float4(0.0f, 0.0f, 0.0f, 0.0f);

			// Convert Vertex position and corresponding normal into world coords.
			// Note that we have to multiply the normal by the transposed inverse of the world 
			// transformation matrix (for cases where we have non-uniform scaling; we also don't
			// care about the "fourth" dimension, because translations don't affect the normal) 
			float3 worldNormal = normalize(i.worldNormal);

			// Calculate ambient RGB intensities
			// here rather than using v.color.rgb in lab5, we use the color that was determined previously by the height of the vertices
			float3 amb = i.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * _Ka;

			// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
			// (when calculating the reflected ray in our specular component)
			float fAtt = 1;
			float Kd = 1;
			float3 L = normalize(_PointLightPosition - i.worldPos);

			float LdotN = dot(L, worldNormal.xyz);
			float3 dif = fAtt * _PointLightColor.rgb * Kd * i.color.rgb * saturate(LdotN);

			// Calculate specular reflections
			float3 V = normalize(_WorldSpaceCameraPos - i.worldPos.xyz);

			//float3 R = float3(0.0, 0.0, 0.0);
			float3 R = float3(2 * worldNormal * LdotN - L);
			float3 spe = fAtt * _PointLightColor.rgb * _Ks * pow(saturate(dot(V, R)), _specN);

			// Combine Phong illumination model components
			phongColor.rgb = amb.rgb + dif.rgb + spe.rgb;
			i.color.rgb = phongColor.rgb;

			return i.color;
			}
			ENDCG
		}
	}
}
