// adapted from https://github.com/vux427/ForceFieldFX
Shader "Unlit/Shield"
{
	Properties
	{
		_MainColor("MainColor", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Fresnel("Fresnel Intensity", Range(0,200)) = 20
		_FresnelWidth("Fresnel Width", Range(0,2)) = 0.8
		_Distort("Distort", Range(0, 100)) = 10
		_IntersectionThreshold("Highlight of intersection threshold", range(0,1)) = 0.2 //Max difference for intersections

	}
	SubShader
	{ 
		Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		// grabs the current screen contents into a texture.
		GrabPass{ }
		Pass
		{

			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal: NORMAL;
				float3 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				half3 rimColor :TEXCOORD1;
				float4 screenPos: TEXCOORD2;
			};

			sampler2D _MainTex, _CameraDepthTexture, _GrabTexture;
			float4 _MainTex_ST,_MainColor,_GrabTexture_ST, _GrabTexture_TexelSize;
			float _Fresnel, _FresnelWidth, _Distort, _IntersectionThreshold, _ScrollSpeedU, _ScrollSpeedV;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				// uv displacement overtime
				o.uv.x += _Time;
				o.uv.y += _Time;

				//fresnel 
				fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				fixed dotProduct = 1 - saturate(dot(v.normal, viewDir));
				o.rimColor = smoothstep(1 - _FresnelWidth, 1.0, dotProduct) * .5f;
				o.screenPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.screenPos.z);//eye space depth of the vertex 

				return o;
			}
			
			float4 frag (v2f i,fixed face : VFACE) : SV_Target
			{
				//intersection
				float intersect = saturate((abs(LinearEyeDepth(tex2Dproj(_CameraDepthTexture,i.screenPos).r) - i.screenPos.z)) / _IntersectionThreshold);

				float3 main = tex2D(_MainTex, i.uv);

				//distortion
				i.screenPos.xy += (main.rg * 2 - 1) * _Distort * _GrabTexture_TexelSize.xy;
				float3 distortColor = tex2Dproj(_GrabTexture, i.screenPos);
				distortColor *= _MainColor * _MainColor.a + 1;
				
				//intersect hightlight
				i.rimColor *= intersect * clamp(0,1,face);
				main *= _MainColor * pow(_Fresnel,i.rimColor) ;
				
				//lerp distort color & fresnel color
				main = lerp(distortColor, main, i.rimColor.r);
				main += (1 - intersect) * (face > 0 ? .03:.3) * _MainColor * _Fresnel;
		
				return float4(main,0.9);
			}
			ENDCG
		}
	}
}
