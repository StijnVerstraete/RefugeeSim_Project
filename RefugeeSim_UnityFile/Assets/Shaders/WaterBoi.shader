Shader "Unlit/WaterBoi"
{
	Properties
	{
		_Color("Extra Color", Color) = (1, 1, 1, .5)

		 _MainTex("Color (RGB) Alpha (A)", 2D) = "white"
		_NoiseTex("Noise On Waves", 2D) = "white" {}
		_DistortTex("Distortion Texture", 2D) = "bump" {}

		_Speed("Speed", Range(0,1)) = 0.5
		_Amount("Amount of Waves", Range(0,1)) = 0.5
		_Height("Height", Range(0,1)) = 0.5

		_Foam("Foamline Thickness", Range(0,5)) = 0.5

		_DistortAmount("Distortion", range(0,2)) = .5

		_Tiling("Texture Tiling",float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent"  "Queue" = "Transparent" }
		LOD 100
		//Blend OneMinusDstColor One

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
				float4 scrPos : TEXCOORD1;//
				float4 worldPos : TEXCOORD2;//
			};
			float _DistortAmount, _Tiling;
			float4 _Color;

			sampler2D _CameraDepthTexture; //Depth Texture
			sampler2D _MainTex, _NoiseTex, _DistortTex;//
			
			float4 _MainTex_ST;
			float _Speed, _Amount, _Height, _Foam;// 
			
			v2f vert (appdata v)
			{
				v2f o;
				float4 tex = tex2Dlod(_NoiseTex, float4(v.uv.xy, 0, 0));//extra noise tex

				v.vertex.y += sin(_Time.z * _Speed + (v.vertex.x * v.vertex.z * _Amount * tex)) * _Height;//movement
				UNITY_TRANSFER_FOG(o, o.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.scrPos = ComputeScreenPos(o.vertex); // grab position on screen

				o.worldPos = mul(unity_ObjectToWorld,v.vertex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed distortion = ( tex2D(_DistortTex, (i.worldPos.xz * _Tiling)  + (_Time.x * 0.2)).r );  

				half4 col = tex2D(_MainTex, (i.worldPos.xz * _Tiling) - (distortion * _DistortAmount)) * _Color; 

				half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos ))); 

				half4 foamLine =1 - saturate(_Foam* (depth - i.scrPos.w ) ) ;
			
				col += (foamLine * _Color); // add foam & extra Color
				col = col * col.a ;

				UNITY_APPLY_FOG(i.fogCoord, col);
				UNITY_OPAQUE_ALPHA(col.a);
				
				return   col;
			}
			ENDCG
		}
	}
}
