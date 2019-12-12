Shader "Toon/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_OutlineSearchRadius("OutlineSearchRadius", int) = 2
		//_OutlineThickness("Outline Thickness", Range(0.0,15.0) = 2
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
            sampler2D _CameraDepthTexture;
			half _OutlineSearchRadius;
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float4 _MainTex_ST;
			float4 _CameraDepthTexture_TexelSize; //Usually for this 1/1920 = x  1080 = y

            fixed4 frag (v2f i) : SV_Target //Start of Fragment Shader
            {

			fixed4 currentPixel = tex2D(_MainTex, i.uv); //Current pixel color
			fixed4 currentDepth = tex2D(_CameraDepthTexture, i.uv) *_ProjectionParams.z; //Current pixel depth
			currentDepth.rgb = currentDepth.r;
			
			//urrentDepth = Linear01Depth(currentDepth);
           
			float4 temp = 0;
			for (float x = -_OutlineSearchRadius/2.0; x <= _OutlineSearchRadius/2.0; x++)
			{

				for (float y = -_OutlineSearchRadius/2.0; y <= _OutlineSearchRadius/2.0; y++)
				{

						fixed4 neighbourPixel = tex2D(_CameraDepthTexture, float2(i.uv.x+ _CameraDepthTexture_TexelSize.x*x, i.uv.y+ _CameraDepthTexture_TexelSize.y*y) );//get current depth pixel offsetet by x and y
						neighbourPixel.r *= _ProjectionParams.z;//Scale the depth with clipping distance
						temp.rgb += distance(neighbourPixel.r,currentDepth.r);//Difference between current depth and neighbourpixel
				
					
				}
			}
			
			temp = temp / pow(_OutlineSearchRadius,2);
			//for (float ot = _OutlineThickness)

			//currentPixel -= temp;
			return currentPixel - temp.r;
            }
            ENDCG
        }
    }
}
