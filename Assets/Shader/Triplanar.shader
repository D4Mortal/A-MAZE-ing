Shader "textureMapping/Triplanar"
{	
	// https://medium.com/@bgolus/normal-mapping-for-a-triplanar-shader-10bf39dca05a
	Properties
	{
		_DiffuseMap("Diffuse Map ", 2D) = "white" {}
		_TextureScale("Texture Scale",float) = 1
		_TriplanarBlendSharpness("Blend Sharpness",float) = 1

		_Metallic("Metallic", Range(0,1)) = 0.0
		_Smoothness("BlendSmoothness", Range(0,1)) = 0.0
		_Occlusion("Occlusion", Range(0,4)) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
			LOD 200

			CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard fullforwardshadows

			sampler2D _DiffuseMap;
			float _TextureScale;
			float _TriplanarBlendSharpness;
			float _Metallic, _Smoothness, _Occlusion;
			struct Input
			{
				float3 worldPos;
				float3 worldNormal;
			};

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				// Find our UVs for each axis based on world position of the fragment.
				half2 yUV = IN.worldPos.xz / _TextureScale;
				half2 xUV = IN.worldPos.zy / _TextureScale;
				half2 zUV = IN.worldPos.xy / _TextureScale;

				// Now do texture samples from our diffuse map with each of the 3 UV set's we've just made.
				half3 yDiff = tex2D(_DiffuseMap, yUV);
				half3 xDiff = tex2D(_DiffuseMap, xUV);
				half3 zDiff = tex2D(_DiffuseMap, zUV);
				// Get the absolute value of the world normal.
				// Put the blend weights to the power of BlendSharpness, the higher the value, 
				// the sharper the transition between the planar maps will be.
				half3 blendWeights = pow(abs(IN.worldNormal), _TriplanarBlendSharpness);
				// Divide our blend mask by the sum of it's components, this will make x+y+z=1
				blendWeights = blendWeights / (blendWeights.x + blendWeights.y + blendWeights.z);
				// Finally, blend together all three samples based on the blend mask.
				o.Albedo = xDiff * blendWeights.x + yDiff * blendWeights.y + zDiff * blendWeights.z;

				o.Metallic = _Metallic;
				o.Smoothness = _Smoothness;
				o.Occlusion = _Occlusion;
			}
			ENDCG
		}
}