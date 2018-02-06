// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "First-PassTerrain"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0
		_Control("Control", 2D) = "white" {}
		_BottomColor("BottomColor", Color) = (0.1172414,0,1,0)
		_GradientPower("GradientPower", Range( 0 , 50)) = 5
		_TopColor("TopColor", Color) = (0.1999999,1,0,0)
		_Offset("Offset", Range( -10 , 10)) = 0
		_CoverFalloff("CoverFalloff", Range( 0 , 20)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest-100" "IgnoreProjector" = "True" "SplatCount"="4" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_texcoord;
		};

		uniform float4 _TopColor;
		uniform float4 _BottomColor;
		uniform float _GradientPower;
		uniform float _Offset;
		uniform float _CoverFalloff;
		uniform sampler2D _Control;
		uniform float4 _Control_ST;
		uniform float _Cutoff = 0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float clampResult14 = clamp( ( ( ase_worldPos.y / _GradientPower ) + _Offset ) , 0.0 , 1.0 );
			float4 lerpResult17 = lerp( _TopColor , _BottomColor , clampResult14);
			float temp_output_26_0 = pow( i.worldNormal.y , _CoverFalloff );
			float4 lerpResult20 = lerp( lerpResult17 , float4(1,1,1,1) , temp_output_26_0);
			o.Albedo = lerpResult20.rgb;
			o.Smoothness = 0.0;
			o.Alpha = 1;
			float2 uv_Control = i.uv_texcoord * _Control_ST.xy + _Control_ST.zw;
			float4 tex2DNode3 = tex2D( _Control, uv_Control );
			clip( tex2DNode3.r - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
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
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}

	Dependency "BaseMapShader"="ASESampleShaders/SimpleTerrainBase"
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14201
0;92;1407;655;1037.669;-28.92984;1.81631;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;13;-646.0877,-550.285;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;22;-448.5665,-355.4597;Float;False;Property;_GradientPower;GradientPower;7;0;Create;5;32.8;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;21;-138.6605,-465.5577;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-379.2435,-200.5065;Float;False;Property;_Offset;Offset;9;0;Create;0;0.5;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;9.577621,-40.22743;Float;False;Constant;_Float1;Float 1;6;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;8.13855,-273.9052;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-6.072021,105.8362;Float;False;Constant;_Float2;Float 2;6;0;Create;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;9;-373.4153,676.3492;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;27;-435.0226,952.5336;Float;False;Property;_CoverFalloff;CoverFalloff;10;0;Create;0;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;14;211.2852,-71.52686;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;185.2017,-520.1509;Float;False;Property;_TopColor;TopColor;8;0;Create;0.1999999,1,0,0;0.1999997,1,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;174.7683,-308.011;Float;False;Property;_BottomColor;BottomColor;6;0;Create;0.1172414,0,1,0;0.1172411,0,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;26;-112.4688,751.9531;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-233.6788,444.4637;Float;False;Constant;_CoverColor;CoverColor;6;0;Create;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;17;418.2079,-116.7372;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0.0;False;2;FLOAT;0.0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;20;451.2457,432.7404;Float;False;3;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-190.2361,-63.29733;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;31;73.91248,1082.389;Float;False;Constant;_Int1;Int 1;11;0;Create;1;0;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;30;33.95369,926.1867;Float;False;Constant;_Int0;Int 0;11;0;Create;0;0;0;1;INT;0
Node;AmplifyShaderEditor.ClampOpNode;29;190.1564,759.0862;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-870.1143,206.8227;Float;True;Property;_Splat1;Splat1;1;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-859.8239,611.1824;Float;True;Property;_Splat3;Splat3;5;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;82.61081,377.0971;Float;False;Constant;_Float0;Float 0;6;0;Create;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-876.5969,22.54629;Float;True;Property;_Splat0;Splat0;2;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-850.1,416.714;Float;True;Property;_Splat2;Splat2;4;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SummedBlendNode;6;-406.0635,325.9621;Float;False;5;0;COLOR;0.0;False;1;COLOR;0.0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;3;-863.0644,-176.415;Float;True;Property;_Control;Control;3;0;Create;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;800.8403,84.30807;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;First-PassTerrain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Back;0;0;False;0;0;Masked;0;True;True;-100;False;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;1;SplatCount=4;0;False;1;BaseMapShader=ASESampleShaders/SimpleTerrainBase;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;13;2
WireConnection;21;1;22;0
WireConnection;24;0;21;0
WireConnection;24;1;23;0
WireConnection;14;0;24;0
WireConnection;14;1;15;0
WireConnection;14;2;16;0
WireConnection;26;0;9;2
WireConnection;26;1;27;0
WireConnection;17;0;18;0
WireConnection;17;1;19;0
WireConnection;17;2;14;0
WireConnection;20;0;17;0
WireConnection;20;1;10;0
WireConnection;20;2;26;0
WireConnection;7;0;3;1
WireConnection;7;1;2;4
WireConnection;29;0;26;0
WireConnection;29;1;30;0
WireConnection;29;2;31;0
WireConnection;6;0;3;0
WireConnection;6;1;1;0
WireConnection;6;2;2;0
WireConnection;6;3;4;0
WireConnection;6;4;5;0
WireConnection;0;0;20;0
WireConnection;0;4;12;0
WireConnection;0;9;3;1
WireConnection;0;10;3;1
ASEEND*/
//CHKSM=518B6F7B021B4D5118B95858F130AAC9FE3ABB8E