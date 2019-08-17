// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Portal"
{
	Properties
	{
		_NoiseMap("Noise Map", 2D) = "white" {}
		_NoiseMapStrength("NoiseMapStrength", Range( 0 , 1)) = 0
		_RingPannerSpeed("RingPannerSpeed", Vector) = (0,0,0,0)
		_NoiseMapSize("NoiseMapSize", Vector) = (512,512,0,0)
		_NoiseMapPannerSpeed("NoiseMapPannerSpeed", Vector) = (0,0,0,0)
		_BaseTexture("Base Texture", 2D) = "white" {}
		_TopColor("TopColor", Color) = (0.2264151,0.1933072,0.1933072,0)
		_BottomColor("BottomColor", Color) = (0,0,0,0)
		_OpacityMask("Opacity Mask", 2D) = "white" {}
		_PortalExtursion("Portal Extursion", Vector) = (0,0,0,0)
		_ExtrusionMask("Extrusion Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _ExtrusionMask;
		uniform float4 _ExtrusionMask_ST;
		uniform float2 _PortalExtursion;
		uniform float4 _BottomColor;
		uniform float4 _TopColor;
		uniform sampler2D _BaseTexture;
		uniform sampler2D _NoiseMap;
		uniform float2 _NoiseMapSize;
		uniform float2 _NoiseMapPannerSpeed;
		uniform float _NoiseMapStrength;
		uniform float2 _RingPannerSpeed;
		uniform sampler2D _OpacityMask;
		uniform float4 _OpacityMask_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_ExtrusionMask = v.texcoord * _ExtrusionMask_ST.xy + _ExtrusionMask_ST.zw;
			v.vertex.xyz += ( tex2Dlod( _ExtrusionMask, float4( uv_ExtrusionMask, 0, 0.0) ) * float4( _PortalExtursion, 0.0 , 0.0 ) ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 lerpResult327 = lerp( _BottomColor , _TopColor , _SinTime.w);
			float2 temp_output_1_0_g33 = _NoiseMapSize;
			float2 appendResult10_g33 = (float2(( (temp_output_1_0_g33).x * i.uv_texcoord.x ) , ( i.uv_texcoord.y * (temp_output_1_0_g33).y )));
			float2 temp_output_11_0_g33 = _NoiseMapPannerSpeed;
			float2 panner18_g33 = ( ( (temp_output_11_0_g33).x * _Time.y ) * float2( 1,0 ) + i.uv_texcoord);
			float2 panner19_g33 = ( ( _Time.y * (temp_output_11_0_g33).y ) * float2( 0,1 ) + i.uv_texcoord);
			float2 appendResult24_g33 = (float2((panner18_g33).x , (panner19_g33).y));
			float2 temp_output_47_0_g33 = _RingPannerSpeed;
			float2 uv_TexCoord78_g33 = i.uv_texcoord * float2( 2,2 );
			float2 temp_output_31_0_g33 = ( uv_TexCoord78_g33 - float2( 1,1 ) );
			float2 appendResult39_g33 = (float2(frac( ( atan2( (temp_output_31_0_g33).x , (temp_output_31_0_g33).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g33 )));
			float2 panner54_g33 = ( ( (temp_output_47_0_g33).x * _Time.y ) * float2( 1,0 ) + appendResult39_g33);
			float2 panner55_g33 = ( ( _Time.y * (temp_output_47_0_g33).y ) * float2( 0,1 ) + appendResult39_g33);
			float2 appendResult58_g33 = (float2((panner54_g33).x , (panner55_g33).y));
			o.Emission = ( lerpResult327 * tex2D( _BaseTexture, ( ( (tex2D( _NoiseMap, ( appendResult10_g33 + appendResult24_g33 ) )).rg * _NoiseMapStrength ) + ( float2( 1,1 ) * appendResult58_g33 ) ) ) ).rgb;
			float2 uv_OpacityMask = i.uv_texcoord * _OpacityMask_ST.xy + _OpacityMask_ST.zw;
			o.Alpha = tex2D( _OpacityMask, uv_OpacityMask ).r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;14;1906;1005;1502.005;794.187;1.950418;True;True
Node;AmplifyShaderEditor.Vector2Node;239;-357.6543,-64.65796;Float;False;Property;_NoiseMapSize;NoiseMapSize;3;0;Create;True;0;0;False;0;512,512;10,10;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;241;-379.0201,59.6485;Float;False;Property;_NoiseMapPannerSpeed;NoiseMapPannerSpeed;4;0;Create;True;0;0;False;0;0,0;0.2,-0.08;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;240;-375.4798,451.084;Float;False;Property;_RingPannerSpeed;RingPannerSpeed;2;0;Create;True;0;0;False;0;0,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;279;-346.5933,297.4621;Float;False;Constant;_Vector0;Vector 0;7;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexturePropertyNode;233;-368.8911,-258.1053;Float;True;Property;_NoiseMap;Noise Map;0;0;Create;True;0;0;False;0;None;5798ded558355430c8a9b13ee12a847c;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;234;-388.6922,186.1725;Float;False;Property;_NoiseMapStrength;NoiseMapStrength;1;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;326;209.0217,-358.5471;Float;False;Property;_TopColor;TopColor;6;0;Create;True;0;0;False;0;0.2264151,0.1933072,0.1933072,0;0.1207744,0.9803922,0.01568625,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;316;82.24505,187.9651;Float;False;RadialUVDistortion;-1;;33;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;;False;1;FLOAT2;0,0;False;11;FLOAT2;0,0;False;65;FLOAT;0;False;68;FLOAT2;0,0;False;47;FLOAT2;0,0;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;317;242.9019,-528.8306;Float;False;Property;_BottomColor;BottomColor;7;0;Create;True;0;0;False;0;0,0,0,0;0,1,0.9231937,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;331;492.5272,-260.5215;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;352;1040.837,787.8378;Float;False;Property;_PortalExtursion;Portal Extursion;9;0;Create;True;0;0;False;0;0,0;0,-1.54;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;276;591.1954,69.80499;Float;True;Property;_BaseTexture;Base Texture;5;0;Create;True;0;0;False;0;None;9fbef4b79ca3b784ba023cb1331520d5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;327;744.0217,-379.5471;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;353;952.9255,574.1463;Float;True;Property;_ExtrusionMask;Extrusion Mask;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;318;1068.514,51.92733;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;351;1418.403,579.4254;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT2;0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;319;1322.451,205.4861;Float;True;Property;_OpacityMask;Opacity Mask;8;0;Create;True;0;0;False;0;None;5228a04ef529d2641937cab585cc1a02;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;230;1666.835,5.143005;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Custom/Portal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;1;1;10;25;True;0.16;True;2;5;False;-1;10;False;-1;0;1;False;-1;1;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;316;60;233;0
WireConnection;316;1;239;0
WireConnection;316;11;241;0
WireConnection;316;65;234;0
WireConnection;316;68;279;0
WireConnection;316;47;240;0
WireConnection;276;1;316;0
WireConnection;327;0;317;0
WireConnection;327;1;326;0
WireConnection;327;2;331;4
WireConnection;318;0;327;0
WireConnection;318;1;276;0
WireConnection;351;0;353;0
WireConnection;351;1;352;0
WireConnection;230;2;318;0
WireConnection;230;9;319;0
WireConnection;230;11;351;0
ASEEND*/
//CHKSM=A1C8573D1A0413ED708516FDBAC13C726FD45B17