Shader "Toon/ToonShader"
{
    Properties
    {
        _Color ("Color", Color) = (0.5,0.5,0.5,0.5)
        _Glossiness ("Smoothness", Range(0.1,1.0)) = 0.9
		_RampTex ("Shades", 2D) = "grey"{}
		_OutlineColor ("Outline Color", Color) = (0,0,0,0)
		_SpecStrength ("Spec Strength", Range(0,1)) = 1
    }
    SubShader
    {

        CGPROGRAM
        // Toon Shading lighting
        #pragma surface surf ToonShading addshadow
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		float4 _Color;
		sampler2D _RampTex;
		half _Glossiness;
		half _SpecStrength;

		float4 LightingToonShading(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize(lightDir + viewDir);
			half diff = max(0,dot (s.Normal,lightDir));
			float cel = diff *0.5 +0.5;//

			float nh = max(0,dot(s.Normal, h));
			float spec = pow(nh,48.0);
			float3 ramp = tex2D(_RampTex, cel).rgb;
			float4 specCel= spec > _Glossiness ? _LightColor0*_SpecStrength  : (0,0,0,0);

			float4 c;
					//Cel Shading Colors	Brightness based on Light 		Specular Highlight
			c.rgb = (s.Albedo * ramp * _LightColor0)+ specCel* atten * _LightColor0;// * 	(_LightColor0.rgb * diff)   	 ;
			//c.rgb = (s.Albedo * ramp);
			c.a = s.Alpha;
			return c;
		}
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;
        };

		half4 _OutlineColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Metallic and smoothness come from slider variables


			//o.Albedo = dot(normalize(IN.viewDir),o.Normal) < 0.2 ? _OutlineColor.rgb :_Color.rgb;
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
