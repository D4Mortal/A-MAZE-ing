Shader "water"
{
    Properties
    {
       _Colour ("Colour", Color) = (0, 0, 1, 1)
       _Transparency("Transparency", Range(0.0,1.0)) = 0.5
       _Shininess ("Shininess", Float) = 25
       _SpecularColour ("Specular Colour", Color) = (1, 1, 1, 1) 
       _AmbientRefl ("Ambient Reflectivity", Float) = 1.0
       _SpecRefl ("Specular Reflectivity", Float) = 1.0
       _DiffuseRefl ( "Diffuse Reflectivity", Float) =  1.0
       _WaveAmp ("Wave Amplitude", Float) = 0.1
       _WaveFrequency ("Wave Frequency", Float) = 1.0
       //Received guidance from
       //http://janhalozan.com/2017/08/12/phong-shader/, Tutorial 5

    }
    
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct vertexInput {
                float4 vertex : POSITION;
                float4 normal : NORMAL;

            };

            struct vertexOutput{
                float4 position : SV_POSITION;
                float4 col : TEXCOORD0;
                float3 normal : NORMAL;
                float4 worldPos : TEXCOORD1;
            };

            float _Transparency;
            float _Shininess;
            float _AmbientRefl;
            float _DiffuseRefl;
            float _SpecRefl;
            float4 _SpecularColour;
            float _WaveAmp;
            float _WaveFrequency;
            float4 _Colour;

            vertexOutput vert(vertexInput input) {
                input.vertex += _WaveAmp * float4(0.0, sin(_WaveFrequency * (_Time.y + input.vertex.x)), 0.0, 0.0);


                vertexOutput output;
                output.worldPos = mul(unity_ObjectToWorld, input.vertex);

                output.normal = normalize(mul(input.normal, unity_WorldToObject).xyz);
                output.position = UnityObjectToClipPos(input.vertex);
                output.col = _Colour;
                return output;
            }

            fixed4 frag(vertexOutput output) : SV_TARGET {
                float3 normalDirection = normalize(output.normal);
                float3 viewDirection = normalize(_WorldSpaceCameraPos - output.worldPos.xyz);
                float3 lightDir = normalize(_WorldSpaceLightPos0 - output.worldPos.xyz);

                float oneOverDistance = 1.0 / length(lightDir);
                float attenuation = lerp(1.0, oneOverDistance, _WorldSpaceLightPos0.w); 

                float3 ambient = output.col * _AmbientRefl;

                float LdotN = dot(lightDir, normalDirection);
                float3 diffuse =  attenuation * _DiffuseRefl * output.col * saturate(LdotN);

                float Ks = 1;
                float3 H = normalize(viewDirection + lightDir);
                float3 specular = attenuation * _SpecRefl * pow(saturate(dot(normalDirection, H)), _Shininess);

                fixed4 colour;
                colour.xyz = ambient + diffuse + specular;
                colour.a = _Transparency;
                return colour;
            }

            ENDCG
        }
    }
}
        